using System.Net.Sockets;
using System.Text;

namespace Client_TCP
{
    public partial class ClientForm : Form
    {
        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;

        private string[] serverIPs = { "127.0.0.1" };

        public ClientForm()
        {
            InitializeComponent();
            users_ListBox.SelectedIndexChanged += users_ListBox_SelectedIndexChanged;
        }

        private void connect_Button_Click(object sender, EventArgs e)
        {
            RunClient();
        }

        private void disconnect_Button_Click(object sender, EventArgs e)
        {
            StopClient();
        }

        private void send_Button_Click(object sender, EventArgs e)
        {
            Send();
        }

        private void clear_Chat_Button_Click(object sender, EventArgs e)
        {
            chatTextBox.Clear();
        }

        private void registration_label_Click(object sender, EventArgs e)
        {
            Form1 registrationForm = new Form1();
            registrationForm.ShowDialog();
        }

        private async void RunClient()
        {
            connect_Button.Enabled = false;
            login_groupBox.Enabled = false;
            try
            {
                foreach (string ip in serverIPs)
                {
                    client = new TcpClient();
                    await client.ConnectAsync(ip, 7777);
                    break;
                }

                if (client == null || !client.Connected)
                {
                    throw new Exception("Неможливо підключитись до жодного сервера...");
                }

                NetworkStream stream = client.GetStream();
                writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
                reader = new StreamReader(stream, Encoding.UTF8);

                await writer.WriteLineAsync(usernameTextBox.Text);
                await writer.WriteLineAsync(passwordTextBox.Text);

                string response = await reader.ReadLineAsync();
                bool authorized = bool.Parse(response);

                if (authorized)
                {
                    _ = Task.Run(async () =>
                    {
                        string message;
                        while ((message = await reader.ReadLineAsync()) != null)
                        {
                            Invoke(new Action(() =>
                            {
                                if (message.Contains(","))
                                {
                                    string[] users = message.Split(',');

                                    users_ListBox.Items.Clear();

                                    foreach (var user in users)
                                    {
                                        users_ListBox.Items.Add(user);
                                    }
                                }
                                else
                                {
                                    chatTextBox.AppendText($"[{DateTime.Now.ToLongTimeString()}] {message}" + Environment.NewLine);
                                }
                            }));
                        }
                    });
                }
                else
                {
                    MessageBox.Show("Даний користувач не зареєстрований. Пройдіть реєстрацію", "Помилка авторизації", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    StopClient();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to server: " + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StopClient()
        {
            connect_Button.Enabled = true;
            login_groupBox.Enabled = true;
            if (client != null && client.Connected)
            {
                try
                {
                    writer.Close();
                    reader.Close();
                    client.Close();
                    users_ListBox.Items.Clear();
                    usernameTextBox.Clear();
                    passwordTextBox.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error disconnecting from server: " + ex.Message, "Disconnection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private async void Send()
        {
            if (client != null && client.Connected)
            {
                try
                {
                    await writer.WriteLineAsync(send_TextBox.Text);
                    send_TextBox.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error sending message: " + ex.Message, "Send Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Not connected to server.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void users_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (users_ListBox.SelectedItem != null)
            {
                string selectedUser = users_ListBox.SelectedItem.ToString();
                send_TextBox.Text = $"для <<{selectedUser}>>: ";
            }
        }
    }
}