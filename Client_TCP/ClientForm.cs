using Server_TCP;
using System.Net.Sockets;

namespace Client_TCP
{
    public partial class ClientForm : Form
    {
        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;
        private Server server;

        public ClientForm(TcpClient client, StreamReader reader, StreamWriter writer)
        {
            InitializeComponent();
            this.client = client;
            this.reader = reader;
            this.writer = writer;

            users_ListBox.SelectedIndexChanged += users_ListBox_SelectedIndexChanged;
            Load += async (sender, e) => await ReceiveMessages();
            button_refresh.Click += async (sender, e) => await RequestUserListUpdate();
        }

        private void send_Button_Click(object sender, EventArgs e)
        {
            Send();
        }

        private void clear_Chat_Button_Click(object sender, EventArgs e)
        {
            chatTextBox.Clear();
        }

        private async void Send()
        {
            if (string.IsNullOrWhiteSpace(send_TextBox.Text)) return;

            if (client != null && client.Connected)
            {
                try
                {
                    await writer.WriteLineAsync(send_TextBox.Text);
                    send_TextBox.Clear();
                }
                catch (Exception ex)
                {
                    UpdateStatus($"Error sending message: {ex.Message}");
                }
            }
            else
            {
                UpdateStatus("Not connected to server.");
            }
        }
        private async Task RequestUserListUpdate()
        {
            if (client != null && client.Connected)
            {
                try
                {
                    // Отправляем специальный запрос для обновления списка пользователей
                    await writer.WriteLineAsync("request_users");
                }
                catch (Exception ex)
                {
                    UpdateStatus($"Error requesting user list: {ex.Message}");
                }
            }
            else
            {
                UpdateStatus("Not connected to server.");
            }
        }

        private async Task ReceiveMessages()
        {
            try
            {
                string message;
                while ((message = await reader.ReadLineAsync()) != null)
                {
                    if (message.StartsWith("USERS:"))
                    {
                        UpdateUserList(message.Substring(6));
                    }
                    else if (message.StartsWith("HISTORY:"))
                    {
                        AddMessageToChat(message.Substring(8)); // Предполагается что HISTORY: уже удалено
                    }
                    else
                    {
                        AddMessageToChat(message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error receiving messages: " + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void UpdateUserList(string userList)
        {
            Invoke(new Action(() =>
            {

                users_ListBox.Items.Clear();
                string[] users = userList.Split(',');
                foreach (var user in users)
                {
                    users_ListBox.Items.Add(user);
                }
            }));
        }

        private void AddMessageToChat(string message)
        {
            Invoke(new Action(() =>
            {
                chatTextBox.AppendText($"[{DateTime.Now.ToLongTimeString()}] {message}" + Environment.NewLine);
            }));
        }

        private void UpdateStatus(string status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => MessageBox.Show(status, "Connection Status", MessageBoxButtons.OK, MessageBoxIcon.Information)));
            }
            else
            {
                MessageBox.Show(status, "Connection Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void chatTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
