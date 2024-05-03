using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Client_TCP
{
    partial class ClientForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.usernameTextBox = new TextBox();
            this.sendTextBox = new TextBox();
            this.usersListBox = new ListBox();
            this.connectButton = new Button();
            this.disconnectButton = new Button();
            this.sendButton = new Button();
            this.clearChatButton = new Button();
            this.labelUsername = new Label();
            this.passwordTextBox = new TextBox();
            this.labelPassword = new Label();
            this.loginGroupBox = new GroupBox();
            this.registrationLabel = new Label();
            this.chatTextBox = new TextBox();

            // Setup for loginGroupBox
            this.loginGroupBox.Controls.Add(this.registrationLabel);
            this.loginGroupBox.Controls.Add(this.labelUsername);
            this.loginGroupBox.Controls.Add(this.labelPassword);
            this.loginGroupBox.Controls.Add(this.usernameTextBox);
            this.loginGroupBox.Controls.Add(this.passwordTextBox);
            this.loginGroupBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            this.loginGroupBox.Location = new Point(12, 277);
            this.loginGroupBox.Size = new Size(121, 159);
            this.loginGroupBox.Text = "Login";

            // Setup for usernameTextBox
            this.usernameTextBox.Location = new Point(6, 47);
            this.usernameTextBox.Size = new Size(109, 23);

            // Setup for passwordTextBox
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Location = new Point(6, 93);
            this.passwordTextBox.Size = new Size(109, 23);

            // Setup for labels
            this.labelUsername.Text = "Enter username:";
            this.labelUsername.Location = new Point(6, 27);
            this.labelPassword.Text = "Enter password:";
            this.labelPassword.Location = new Point(6, 73);

            // Setup for registrationLabel
            this.registrationLabel.Font = new Font("Segoe UI", 9F, FontStyle.Italic | FontStyle.Underline);
            this.registrationLabel.Location = new Point(27, 129);
            this.registrationLabel.Text = "Register";
            this.registrationLabel.Click += RegistrationLabel_Click;

            // Setup for buttons
            this.connectButton.Text = "Connect";
            this.connectButton.Location = new Point(12, 442);
            this.connectButton.Click += ConnectButton_Click;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.Location = new Point(12, 475);
            this.disconnectButton.Click += DisconnectButton_Click;
            this.sendButton.Text = "Send";
            this.sendButton.Location = new Point(463, 471);
            this.sendButton.Click += SendButton_Click;
            this.clearChatButton.Text = "Clear";
            this.clearChatButton.Location = new Point(526, 471);
            this.clearChatButton.Click += ClearChatButton_Click;

            // Setup for chatTextBox
            this.chatTextBox.Multiline = true;
            this.chatTextBox.ScrollBars = ScrollBars.Vertical;
            this.chatTextBox.Location = new Point(139, 12);
            this.chatTextBox.Size = new Size(429, 453);
            this.chatTextBox.ReadOnly = true;

            // Add controls to form
            this.Controls.Add(this.chatTextBox);
            this.Controls.Add(this.loginGroupBox);
            this.Controls.Add(this.clearChatButton);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.usersListBox);
            this.Controls.Add(this.sendTextBox);

            // Form properties
            this.ClientSize = new Size(580, 513);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Bla-Bla Chat";
        }
        private TextBox usernameTextBox;
        private TextBox sendTextBox;
        private ListBox usersListBox;
        private Button connectButton;
        private Button disconnectButton;
        private Button sendButton;
        private Button clearChatButton;
        private Label labelUsername;
        private TextBox passwordTextBox;
        private Label labelPassword;
        private GroupBox loginGroupBox;
        private Label registrationLabel;
        private TextBox chatTextBox;
    }
}

        #endregion

       
    
