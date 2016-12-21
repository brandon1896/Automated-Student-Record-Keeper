namespace AutomatedStudentRecordKeeper
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
            this.loginbutton = new System.Windows.Forms.Button();
            this.addstudentbutton = new System.Windows.Forms.Button();
            this.viewstudentsbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loginbutton
            // 
            this.loginbutton.Location = new System.Drawing.Point(12, 12);
            this.loginbutton.Name = "loginbutton";
            this.loginbutton.Size = new System.Drawing.Size(116, 52);
            this.loginbutton.TabIndex = 0;
            this.loginbutton.Text = "Login";
            this.loginbutton.UseVisualStyleBackColor = true;
            this.loginbutton.Click += new System.EventHandler(this.login_Click);
            // 
            // addstudentbutton
            // 
            this.addstudentbutton.Location = new System.Drawing.Point(156, 12);
            this.addstudentbutton.Name = "addstudentbutton";
            this.addstudentbutton.Size = new System.Drawing.Size(116, 52);
            this.addstudentbutton.TabIndex = 1;
            this.addstudentbutton.Text = "Add Student";
            this.addstudentbutton.UseVisualStyleBackColor = true;
            this.addstudentbutton.Click += new System.EventHandler(this.addstudentbutton_Click);
            // 
            // viewstudentsbutton
            // 
            this.viewstudentsbutton.Location = new System.Drawing.Point(85, 79);
            this.viewstudentsbutton.Name = "viewstudentsbutton";
            this.viewstudentsbutton.Size = new System.Drawing.Size(116, 52);
            this.viewstudentsbutton.TabIndex = 2;
            this.viewstudentsbutton.Text = "View Students";
            this.viewstudentsbutton.UseVisualStyleBackColor = true;
            this.viewstudentsbutton.Click += new System.EventHandler(this.viewstudentsbutton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 155);
            this.Controls.Add(this.viewstudentsbutton);
            this.Controls.Add(this.addstudentbutton);
            this.Controls.Add(this.loginbutton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button loginbutton;
        private System.Windows.Forms.Button addstudentbutton;
        private System.Windows.Forms.Button viewstudentsbutton;
    }
}

