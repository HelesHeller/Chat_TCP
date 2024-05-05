using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client_TCP
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;

        public Form1()
        {
            InitializeComponent();
            enter_button.Click += new EventHandler(enter_button_Click);  // Linking the button click event
        }

        private async void enter_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(username_textBox.Text) || string.IsNullOrEmpty(password_textBox.Text) || string.IsNullOrEmpty(password2_textBox.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password_textBox.Text != password2_textBox.Text)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!await ConnectToServer())
            {
                MessageBox.Show("Cannot connect to the server. Please try again.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (await RegisterUser())
            {
                MessageBox.Show("Registration successful. You can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Disconnect();
                this.Close();  // Optionally close this form and return to the login form
            }
            else
            {
                MessageBox.Show("Registration failed. Please try different credentials.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<bool> ConnectToServer()
        {
            try
            {
                client = new TcpClient();
                await client.ConnectAsync("127.0.0.1", 7777);
                NetworkStream stream = client.GetStream();
                writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
                reader = new StreamReader(stream, Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to server: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private async Task<bool> RegisterUser()
        {
            try
            {
                await writer.WriteLineAsync(username_textBox.Text);
                await writer.WriteLineAsync(password_textBox.Text);
                await writer.WriteLineAsync("register"); // Command to indicate registration

                var response = await reader.ReadLineAsync();
                return response.Equals("Success", StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during registration: {ex.Message}", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void Disconnect()
        {
            if (client != null && client.Connected)
            {
                client.Close();
            }
        }
    }
}
