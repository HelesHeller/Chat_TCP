using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client_TCP
{
    public partial class LoginForm : Form
    {
        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;

        private string[] serverIPs = { "127.0.0.1" };  // Server IPs

        public LoginForm()
        {
            InitializeComponent();
            // Initialize your UI elements here like buttons, textboxes, etc.
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            bool isConnected = await ConnectToServer();
            if (!isConnected)
            {
                MessageBox.Show("Cannot connect to server. Please try again.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (await AuthenticateUser())
            {
                ClientForm clientForm = new ClientForm(client, reader, writer);
                this.Hide();
                clientForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (client.Connected)
                {
                    client.Close();
                }
            }
        }

        private async Task<bool> ConnectToServer()
        {
            try
            {
                client = new TcpClient();
                await client.ConnectAsync(serverIPs[0], 7777);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to server: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private async Task<bool> AuthenticateUser()
        {
            NetworkStream stream = client.GetStream();
            writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
            reader = new StreamReader(stream, Encoding.UTF8);

            // Send credentials to server
            await writer.WriteLineAsync(txtUsername.Text);  // Assume txtUsername is the TextBox for username
            await writer.WriteLineAsync(txtPassword.Text);  // Assume txtPassword is the TextBox for password

            // Read response from server
            var response = await reader.ReadLineAsync();

            // Check if the response is in expected format
            if (response.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                response.Equals("false", StringComparison.OrdinalIgnoreCase))
            {
                return bool.Parse(response);
            }
            else
            {
                // Log unexpected response or show it in a message box
                MessageBox.Show("Unexpected response from server: " + response, "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        private void lblUsername_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
            this.Close();
        }
    }
}
