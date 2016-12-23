namespace AutomatedStudentRecordKeeper
{
    partial class MainScreen
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
            this.addstudentbutton = new System.Windows.Forms.Button();
            this.viewstudentsbutton = new System.Windows.Forms.Button();
            this.AddCoursesButton = new System.Windows.Forms.Button();
            this.ViewCoursesButton = new System.Windows.Forms.Button();
            this.AddTransferButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addstudentbutton
            // 
            this.addstudentbutton.Location = new System.Drawing.Point(12, 12);
            this.addstudentbutton.Name = "addstudentbutton";
            this.addstudentbutton.Size = new System.Drawing.Size(116, 52);
            this.addstudentbutton.TabIndex = 1;
            this.addstudentbutton.Text = "Add New Student";
            this.addstudentbutton.UseVisualStyleBackColor = true;
            this.addstudentbutton.Click += new System.EventHandler(this.addstudentbutton_Click);
            // 
            // viewstudentsbutton
            // 
            this.viewstudentsbutton.Location = new System.Drawing.Point(77, 70);
            this.viewstudentsbutton.Name = "viewstudentsbutton";
            this.viewstudentsbutton.Size = new System.Drawing.Size(116, 52);
            this.viewstudentsbutton.TabIndex = 2;
            this.viewstudentsbutton.Text = "View Students";
            this.viewstudentsbutton.UseVisualStyleBackColor = true;
            this.viewstudentsbutton.Click += new System.EventHandler(this.viewstudentsbutton_Click);
            // 
            // AddCoursesButton
            // 
            this.AddCoursesButton.Location = new System.Drawing.Point(77, 128);
            this.AddCoursesButton.Name = "AddCoursesButton";
            this.AddCoursesButton.Size = new System.Drawing.Size(116, 52);
            this.AddCoursesButton.TabIndex = 3;
            this.AddCoursesButton.Text = "Add Courses";
            this.AddCoursesButton.UseVisualStyleBackColor = true;
            this.AddCoursesButton.Click += new System.EventHandler(this.AddCoursesButton_Click);
            // 
            // ViewCoursesButton
            // 
            this.ViewCoursesButton.Location = new System.Drawing.Point(77, 186);
            this.ViewCoursesButton.Name = "ViewCoursesButton";
            this.ViewCoursesButton.Size = new System.Drawing.Size(116, 52);
            this.ViewCoursesButton.TabIndex = 4;
            this.ViewCoursesButton.Text = "View Courses";
            this.ViewCoursesButton.UseVisualStyleBackColor = true;
            this.ViewCoursesButton.Click += new System.EventHandler(this.ViewCoursesButton_Click);
            // 
            // AddTransferButton
            // 
            this.AddTransferButton.Location = new System.Drawing.Point(144, 12);
            this.AddTransferButton.Name = "AddTransferButton";
            this.AddTransferButton.Size = new System.Drawing.Size(116, 52);
            this.AddTransferButton.TabIndex = 5;
            this.AddTransferButton.Text = "Add Transfer Student";
            this.AddTransferButton.UseVisualStyleBackColor = true;
            this.AddTransferButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 269);
            this.Controls.Add(this.AddTransferButton);
            this.Controls.Add(this.ViewCoursesButton);
            this.Controls.Add(this.AddCoursesButton);
            this.Controls.Add(this.viewstudentsbutton);
            this.Controls.Add(this.addstudentbutton);
            this.Name = "MainScreen";
            this.Text = "Automated Student Record Keeper";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button addstudentbutton;
        private System.Windows.Forms.Button viewstudentsbutton;
        private System.Windows.Forms.Button AddCoursesButton;
        private System.Windows.Forms.Button ViewCoursesButton;
        private System.Windows.Forms.Button AddTransferButton;
    }
}

