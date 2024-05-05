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
            send_TextBox = new TextBox();
            users_ListBox = new ListBox();
            connect_Button = new Button();
            disconnect_Button = new Button();
            send_Button = new Button();
            clear_Chat_Button = new Button();
            chatTextBox = new TextBox();
            SuspendLayout();
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
            users_ListBox.Size = new Size(121, 429);
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
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox send_TextBox;
        private ListBox users_ListBox;
        private Button connect_Button;
        private Button disconnect_Button;
        private Button send_Button;
        private Button clear_Chat_Button;
        private TextBox chatTextBox;
    }
}
