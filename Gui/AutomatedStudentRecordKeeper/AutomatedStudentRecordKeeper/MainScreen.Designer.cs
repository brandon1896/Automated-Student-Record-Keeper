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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
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
            this.viewstudentsbutton.Location = new System.Drawing.Point(134, 12);
            this.viewstudentsbutton.Name = "viewstudentsbutton";
            this.viewstudentsbutton.Size = new System.Drawing.Size(116, 52);
            this.viewstudentsbutton.TabIndex = 2;
            this.viewstudentsbutton.Text = "View Students";
            this.viewstudentsbutton.UseVisualStyleBackColor = true;
            this.viewstudentsbutton.Click += new System.EventHandler(this.viewstudentsbutton_Click);
            // 
            // AddCoursesButton
            // 
            this.AddCoursesButton.Location = new System.Drawing.Point(12, 82);
            this.AddCoursesButton.Name = "AddCoursesButton";
            this.AddCoursesButton.Size = new System.Drawing.Size(116, 52);
            this.AddCoursesButton.TabIndex = 3;
            this.AddCoursesButton.Text = "Add Courses";
            this.AddCoursesButton.UseVisualStyleBackColor = true;
            this.AddCoursesButton.Click += new System.EventHandler(this.AddCoursesButton_Click);
            // 
            // ViewCoursesButton
            // 
            this.ViewCoursesButton.Location = new System.Drawing.Point(77, 140);
            this.ViewCoursesButton.Name = "ViewCoursesButton";
            this.ViewCoursesButton.Size = new System.Drawing.Size(116, 52);
            this.ViewCoursesButton.TabIndex = 4;
            this.ViewCoursesButton.Text = "View Courses";
            this.ViewCoursesButton.UseVisualStyleBackColor = true;
            this.ViewCoursesButton.Click += new System.EventHandler(this.ViewCoursesButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(77, 195);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 52);
            this.button1.TabIndex = 6;
            this.button1.Text = "Export Database";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(134, 82);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 52);
            this.button2.TabIndex = 7;
            this.button2.Text = "Add Complementary Courses";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 269);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

