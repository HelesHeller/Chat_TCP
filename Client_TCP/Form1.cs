using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client_TCP
{
    public partial class Form1 : Form
    {
        private string[] serverIPs = { "127.0.0.1" };

        public Form1()
        {
            InitializeComponent();
        }

        private async void enter_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(username_textBox.Text) || string.IsNullOrEmpty(password_textBox.Text))
            {
                MessageBox.Show("Логін або пороль не заповнені. Спробуйте ще раз.");
                return;
            }

            if (password_textBox.Text != password2_textBox.Text)
            {
                MessageBox.Show("Паролі не співпадають. Спробуйте ще раз.");
                return;
            }

            bool isConnected = await ConnectToServer();
            if (!isConnected)
            {
                MessageBox.Show("Не вдалося підключитися до сервера.");
                return;
            }

            await RegisterUser();
        }

        private async Task<bool> ConnectToServer()
        {
            foreach (string ip in serverIPs)
            {
                try
                {
                    TcpClient client = new TcpClient();
                    await client.ConnectAsync(ip, 7777);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при підключенні до серверу {ip}: {ex.Message}");
                }
            }
            return false;
        }

        private async Task RegisterUser()
        {
            using (TcpClient client = new TcpClient(serverIPs[0], 7777))
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true })
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    // Send registration details to the server
                    await writer.WriteLineAsync(username_textBox.Text);
                    await writer.WriteLineAsync(password_textBox.Text);
                    await writer.WriteLineAsync("register");

                    // Read the response from the server
                    var response = await reader.ReadLineAsync();

                    // Attempt to parse the response as a Boolean
                    if (bool.TryParse(response, out bool isRegistered))
                    {
                        if (isRegistered)
                        {
                            MessageBox.Show("Користувач успішно зареєстрований.");
                            ShowClientForm(); // Proceed to show the main client form
                        }
                        else
                        {
                            MessageBox.Show("Цей користувач вже зареєстрований.");
                        }
                    }
                    else
                    {
                        // The response was not a valid boolean string, handle accordingly
                        MessageBox.Show($"Unexpected response from server: {response}");
                    }
                }
            }
        }

        private void ShowClientForm()
        {
            ClientForm clientForm = new ClientForm(); 
            this.Hide(); 
            clientForm.ShowDialog(); 
            this.Close(); 
        }




    }
}
