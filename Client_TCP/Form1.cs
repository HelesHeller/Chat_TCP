using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Server_TCP;


namespace Client_TCP
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;
        private UserRepository _userRepository;

        public Form1()
        {
            InitializeComponent();
            enter_button.Click += new EventHandler(enter_button_Click);  // Linking the button click event
            _userRepository = new UserRepository(new ApplicationContext());
        }

        private async void enter_button_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            if (!await TryConnectToServer())
                return;

            if (await TryRegisterUser())
                HandleSuccessfulRegistration();
            else
                HandleFailedRegistration();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(username_textBox.Text) || string.IsNullOrEmpty(password_textBox.Text) || string.IsNullOrEmpty(password2_textBox.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (password_textBox.Text != password2_textBox.Text)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private async Task<bool> TryConnectToServer()
        {
            if (!await ConnectToServer())
            {
                MessageBox.Show("Cannot connect to the server. Please try again.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private async Task<bool> TryRegisterUser()
        {
            return await RegisterUser();
        }

        private void HandleSuccessfulRegistration()
        {
            MessageBox.Show("Registration successful. You can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Disconnect();
            this.Close(); // Optionally close this form and return to the login form
        }

        private void HandleFailedRegistration()
        {
            MessageBox.Show("Registration failed. Please try different credentials.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                bool isRegistered = await _userRepository.RegisterUserAsync(username, password);
                if (isRegistered)
                {
                    MessageBox.Show("Регистрация прошла успешно!");
                }
                else
                {
                    MessageBox.Show("Не удалось зарегистрироваться.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
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
