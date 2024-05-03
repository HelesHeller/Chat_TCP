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
            usernameTextBox = new TextBox();
            send_TextBox = new TextBox();
            users_ListBox = new ListBox();
            connect_Button = new Button();
            disconnect_Button = new Button();
            send_Button = new Button();
            clear_Chat_Button = new Button();
            label2 = new Label();
            passwordTextBox = new TextBox();
            label4 = new Label();
            login_groupBox = new GroupBox();
            registration_label = new Label();
            chatTextBox = new TextBox();
            login_groupBox.SuspendLayout();
            SuspendLayout();
            // 
            // usernameTextBox
            // 
            usernameTextBox.BackColor = Color.LavenderBlush;
            usernameTextBox.BorderStyle = BorderStyle.FixedSingle;
            usernameTextBox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            usernameTextBox.Location = new Point(6, 47);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(109, 23);
            usernameTextBox.TabIndex = 0;
            // 
            // send_TextBox
            // 
            send_TextBox.BackColor = Color.LavenderBlush;
            send_TextBox.BorderStyle = BorderStyle.FixedSingle;
            send_TextBox.Location = new Point(139, 479);
            send_TextBox.Name = "send_TextBox";
            send_TextBox.Size = new Size(660, 23);
            send_TextBox.TabIndex = 2;
            // 
            // users_ListBox
            // 
            users_ListBox.BackColor = Color.LavenderBlush;
            users_ListBox.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 204);
            users_ListBox.FormattingEnabled = true;
            users_ListBox.ItemHeight = 17;
            users_ListBox.Location = new Point(12, 12);
            users_ListBox.Name = "users_ListBox";
            users_ListBox.Size = new Size(121, 259);
            users_ListBox.TabIndex = 3;
            // 
            // connect_Button
            // 
            connect_Button.BackColor = Color.PaleVioletRed;
            connect_Button.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            connect_Button.Location = new Point(12, 442);
            connect_Button.Name = "connect_Button";
            connect_Button.Size = new Size(121, 27);
            connect_Button.TabIndex = 4;
            connect_Button.Text = "Connect";
            connect_Button.UseVisualStyleBackColor = false;
            connect_Button.Click += connect_Button_Click;
            // 
            // disconnect_Button
            // 
            disconnect_Button.BackColor = Color.Pink;
            disconnect_Button.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            disconnect_Button.Location = new Point(12, 475);
            disconnect_Button.Name = "disconnect_Button";
            disconnect_Button.Size = new Size(121, 27);
            disconnect_Button.TabIndex = 5;
            disconnect_Button.Text = "Disconnect";
            disconnect_Button.UseVisualStyleBackColor = false;
            disconnect_Button.Click += disconnect_Button_Click;
            // 
            // send_Button
            // 
            send_Button.BackColor = Color.YellowGreen;
            send_Button.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            send_Button.Location = new Point(805, 479);
            send_Button.Name = "send_Button";
            send_Button.Size = new Size(57, 25);
            send_Button.TabIndex = 6;
            send_Button.Text = "Send";
            send_Button.UseVisualStyleBackColor = false;
            send_Button.Click += send_Button_Click;
            // 
            // clear_Chat_Button
            // 
            clear_Chat_Button.BackColor = Color.IndianRed;
            clear_Chat_Button.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            clear_Chat_Button.Location = new Point(868, 479);
            clear_Chat_Button.Name = "clear_Chat_Button";
            clear_Chat_Button.Size = new Size(51, 26);
            clear_Chat_Button.TabIndex = 7;
            clear_Chat_Button.Text = "Clear";
            clear_Chat_Button.UseVisualStyleBackColor = false;
            clear_Chat_Button.Click += clear_Chat_Button_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(6, 27);
            label2.Name = "label2";
            label2.Size = new Size(80, 17);
            label2.TabIndex = 12;
            label2.Text = "Введіть ім'я:";
            // 
            // passwordTextBox
            // 
            passwordTextBox.BackColor = Color.LavenderBlush;
            passwordTextBox.BorderStyle = BorderStyle.FixedSingle;
            passwordTextBox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            passwordTextBox.Location = new Point(6, 93);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Size = new Size(109, 23);
            passwordTextBox.TabIndex = 14;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label4.Location = new Point(6, 73);
            label4.Name = "label4";
            label4.Size = new Size(102, 17);
            label4.TabIndex = 15;
            label4.Text = "Введіть пароль:";
            // 
            // login_groupBox
            // 
            login_groupBox.Controls.Add(registration_label);
            login_groupBox.Controls.Add(label2);
            login_groupBox.Controls.Add(label4);
            login_groupBox.Controls.Add(usernameTextBox);
            login_groupBox.Controls.Add(passwordTextBox);
            login_groupBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            login_groupBox.Location = new Point(12, 277);
            login_groupBox.Name = "login_groupBox";
            login_groupBox.Size = new Size(121, 159);
            login_groupBox.TabIndex = 16;
            login_groupBox.TabStop = false;
            login_groupBox.Text = "Вхід";
            // 
            // registration_label
            // 
            registration_label.AutoSize = true;
            registration_label.Font = new Font("Segoe UI", 9F, FontStyle.Italic | FontStyle.Underline, GraphicsUnit.Point, 204);
            registration_label.Location = new Point(27, 129);
            registration_label.Name = "registration_label";
            registration_label.Size = new Size(70, 15);
            registration_label.TabIndex = 17;
            registration_label.Text = "Реєстрація";
            registration_label.Click += registration_label_Click;
            // 
            // chatTextBox
            // 
            chatTextBox.BackColor = Color.LavenderBlush;
            chatTextBox.BorderStyle = BorderStyle.FixedSingle;
            chatTextBox.Location = new Point(139, 12);
            chatTextBox.Multiline = true;
            chatTextBox.Name = "chatTextBox";
            chatTextBox.ReadOnly = true;
            chatTextBox.ScrollBars = ScrollBars.Vertical;
            chatTextBox.Size = new Size(780, 461);
            chatTextBox.TabIndex = 17;
            // 
            // ClientForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MistyRose;
            ClientSize = new Size(931, 513);
            Controls.Add(chatTextBox);
            Controls.Add(login_groupBox);
            Controls.Add(clear_Chat_Button);
            Controls.Add(send_Button);
            Controls.Add(disconnect_Button);
            Controls.Add(connect_Button);
            Controls.Add(users_ListBox);
            Controls.Add(send_TextBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ClientForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = " Bla-Bla Chat";
            login_groupBox.ResumeLayout(false);
            login_groupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox usernameTextBox;
        private TextBox send_TextBox;
        private ListBox users_ListBox;
        private Button connect_Button;
        private Button disconnect_Button;
        private Button send_Button;
        private Button clear_Chat_Button;
        private Label label2;
        private TextBox passwordTextBox;
        private Label label4;
        private GroupBox login_groupBox;
        private Label registration_label;
        private TextBox chatTextBox;
    }
}
