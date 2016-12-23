namespace AutomatedStudentRecordKeeper
{
    partial class LoginForm
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
            this.enterbutton = new System.Windows.Forms.Button();
            this.userbox = new System.Windows.Forms.TextBox();
            this.passbox = new System.Windows.Forms.TextBox();
            this.userlabel = new System.Windows.Forms.Label();
            this.passlabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // enterbutton
            // 
            this.enterbutton.Location = new System.Drawing.Point(114, 96);
            this.enterbutton.Name = "enterbutton";
            this.enterbutton.Size = new System.Drawing.Size(75, 23);
            this.enterbutton.TabIndex = 0;
            this.enterbutton.Text = "Login";
            this.enterbutton.UseVisualStyleBackColor = true;
            this.enterbutton.Click += new System.EventHandler(this.enterbutton_Click);
            // 
            // userbox
            // 
            this.userbox.Location = new System.Drawing.Point(91, 17);
            this.userbox.MaxLength = 40;
            this.userbox.Name = "userbox";
            this.userbox.Size = new System.Drawing.Size(181, 20);
            this.userbox.TabIndex = 2;
            // 
            // passbox
            // 
            this.passbox.Location = new System.Drawing.Point(89, 51);
            this.passbox.MaxLength = 40;
            this.passbox.Name = "passbox";
            this.passbox.PasswordChar = '*';
            this.passbox.Size = new System.Drawing.Size(183, 20);
            this.passbox.TabIndex = 3;
            // 
            // userlabel
            // 
            this.userlabel.AutoSize = true;
            this.userlabel.Location = new System.Drawing.Point(27, 20);
            this.userlabel.Name = "userlabel";
            this.userlabel.Size = new System.Drawing.Size(58, 13);
            this.userlabel.TabIndex = 4;
            this.userlabel.Text = "Username:";
            // 
            // passlabel
            // 
            this.passlabel.AutoSize = true;
            this.passlabel.Location = new System.Drawing.Point(27, 54);
            this.passlabel.Name = "passlabel";
            this.passlabel.Size = new System.Drawing.Size(56, 13);
            this.passlabel.TabIndex = 5;
            this.passlabel.Text = "Password:";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 131);
            this.Controls.Add(this.passlabel);
            this.Controls.Add(this.userlabel);
            this.Controls.Add(this.passbox);
            this.Controls.Add(this.userbox);
            this.Controls.Add(this.enterbutton);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button enterbutton;
        private System.Windows.Forms.TextBox userbox;
        private System.Windows.Forms.TextBox passbox;
        private System.Windows.Forms.Label userlabel;
        private System.Windows.Forms.Label passlabel;
    }
}