
namespace Client_TCP
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label2 = new Label();
            password_textBox = new TextBox();
            label1 = new Label();
            username_textBox = new TextBox();
            enter_button = new Button();
            label3 = new Label();
            password2_textBox = new TextBox();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 58);
            label2.Name = "label2";
            label2.Size = new Size(117, 15);
            label2.TabIndex = 10;
            label2.Text = "Enter your password:";
            // 
            // password_textBox
            // 
            password_textBox.BackColor = Color.LavenderBlush;
            password_textBox.BorderStyle = BorderStyle.FixedSingle;
            password_textBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            password_textBox.Location = new Point(146, 54);
            password_textBox.Multiline = true;
            password_textBox.Name = "password_textBox";
            password_textBox.PasswordChar = '*';
            password_textBox.Size = new Size(316, 23);
            password_textBox.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 29);
            label1.Name = "label1";
            label1.Size = new Size(119, 15);
            label1.TabIndex = 8;
            label1.Text = "Enter your username:";
            label1.Click += label1_Click;
            // 
            // username_textBox
            // 
            username_textBox.BackColor = Color.LavenderBlush;
            username_textBox.BorderStyle = BorderStyle.FixedSingle;
            username_textBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            username_textBox.Location = new Point(146, 25);
            username_textBox.Multiline = true;
            username_textBox.Name = "username_textBox";
            username_textBox.Size = new Size(316, 23);
            username_textBox.TabIndex = 7;
            // 
            // enter_button
            // 
            enter_button.BackColor = Color.LightPink;
            enter_button.Location = new Point(162, 112);
            enter_button.Name = "enter_button";
            enter_button.Size = new Size(154, 42);
            enter_button.TabIndex = 6;
            enter_button.Text = "Registrate";
            enter_button.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 87);
            label3.Name = "label3";
            label3.Size = new Size(134, 15);
            label3.TabIndex = 12;
            label3.Text = "Confirm your password:";
            // 
            // password2_textBox
            // 
            password2_textBox.BackColor = Color.LavenderBlush;
            password2_textBox.BorderStyle = BorderStyle.FixedSingle;
            password2_textBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            password2_textBox.Location = new Point(146, 83);
            password2_textBox.Multiline = true;
            password2_textBox.Name = "password2_textBox";
            password2_textBox.PasswordChar = '*';
            password2_textBox.Size = new Size(316, 23);
            password2_textBox.TabIndex = 11;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MistyRose;
            ClientSize = new Size(474, 164);
            Controls.Add(label3);
            Controls.Add(password2_textBox);
            Controls.Add(label2);
            Controls.Add(password_textBox);
            Controls.Add(label1);
            Controls.Add(username_textBox);
            Controls.Add(enter_button);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Registration";
            ResumeLayout(false);
            PerformLayout();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Label label2;
        private TextBox password_textBox;
        private Label label1;
        private TextBox username_textBox;
        private Button enter_button;
        private Label label3;
        private TextBox password2_textBox;
    }
}