using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client_TCP
{
    public partial class ClientForm : Form
    {
        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;

        // Constructor now requires the already connected TcpClient, StreamReader, and StreamWriter
        public ClientForm(TcpClient client, StreamReader reader, StreamWriter writer)
        {
            InitializeComponent();
            this.client = client;
            this.reader = reader;
            this.writer = writer;

            users_ListBox.SelectedIndexChanged += users_ListBox_SelectedIndexChanged;

            // Start listening to incoming messages as soon as the form is loaded
            Task.Run(async () => await ReceiveMessages());
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

        private async Task ReceiveMessages()
        {
            try
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error receiving messages: " + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Optionally reconnect or close the client
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
