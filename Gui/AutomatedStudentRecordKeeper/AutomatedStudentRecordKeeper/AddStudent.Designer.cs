namespace AutomatedStudentRecordKeeper
{
    partial class TransferStudent
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
            this.AddStudentButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Namebox = new System.Windows.Forms.TextBox();
            this.StudentNumberBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CollegeBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PrevProgramBox = new System.Windows.Forms.TextBox();
            this.Yeardropbox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.MakeupCourseTable = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.AddRowMakeup = new System.Windows.Forms.Button();
            this.RemoveRowMakeup = new System.Windows.Forms.Button();
            this.RemoveRowCredit = new System.Windows.Forms.Button();
            this.AddRowCredit = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // AddStudentButton
            // 
            this.AddStudentButton.BackColor = System.Drawing.SystemColors.Window;
            this.AddStudentButton.Location = new System.Drawing.Point(183, 271);
            this.AddStudentButton.Name = "AddStudentButton";
            this.AddStudentButton.Size = new System.Drawing.Size(75, 23);
            this.AddStudentButton.TabIndex = 0;
            this.AddStudentButton.Text = "Confirm";
            this.AddStudentButton.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // Namebox
            // 
            this.Namebox.Location = new System.Drawing.Point(62, 37);
            this.Namebox.Name = "Namebox";
            this.Namebox.Size = new System.Drawing.Size(133, 20);
            this.Namebox.TabIndex = 2;
            // 
            // StudentNumberBox
            // 
            this.StudentNumberBox.Location = new System.Drawing.Point(291, 37);
            this.StudentNumberBox.Name = "StudentNumberBox";
            this.StudentNumberBox.Size = new System.Drawing.Size(130, 20);
            this.StudentNumberBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Student Number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "College";
            // 
            // CollegeBox
            // 
            this.CollegeBox.Location = new System.Drawing.Point(62, 71);
            this.CollegeBox.Name = "CollegeBox";
            this.CollegeBox.Size = new System.Drawing.Size(133, 20);
            this.CollegeBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(201, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Previous Program";
            // 
            // PrevProgramBox
            // 
            this.PrevProgramBox.Location = new System.Drawing.Point(291, 71);
            this.PrevProgramBox.Name = "PrevProgramBox";
            this.PrevProgramBox.Size = new System.Drawing.Size(130, 20);
            this.PrevProgramBox.TabIndex = 8;
            // 
            // Yeardropbox
            // 
            this.Yeardropbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Yeardropbox.FormattingEnabled = true;
            this.Yeardropbox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.Yeardropbox.Location = new System.Drawing.Point(183, 108);
            this.Yeardropbox.Name = "Yeardropbox";
            this.Yeardropbox.Size = new System.Drawing.Size(54, 21);
            this.Yeardropbox.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(119, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Year Level";
            // 
            // MakeupCourseTable
            // 
            this.MakeupCourseTable.BackColor = System.Drawing.SystemColors.Window;
            this.MakeupCourseTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.MakeupCourseTable.ColumnCount = 3;
            this.MakeupCourseTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.42105F));
            this.MakeupCourseTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.57895F));
            this.MakeupCourseTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.MakeupCourseTable.Location = new System.Drawing.Point(12, 169);
            this.MakeupCourseTable.Name = "MakeupCourseTable";
            this.MakeupCourseTable.RowCount = 2;
            this.MakeupCourseTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.71429F));
            this.MakeupCourseTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.28571F));
            this.MakeupCourseTable.Size = new System.Drawing.Size(200, 36);
            this.MakeupCourseTable.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(59, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Make Up Courses";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 153);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Subject";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(119, 153);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Course Name";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(288, 138);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Course Credits";
            // 
            // AddRowMakeup
            // 
            this.AddRowMakeup.BackColor = System.Drawing.SystemColors.Window;
            this.AddRowMakeup.Location = new System.Drawing.Point(24, 211);
            this.AddRowMakeup.Name = "AddRowMakeup";
            this.AddRowMakeup.Size = new System.Drawing.Size(65, 23);
            this.AddRowMakeup.TabIndex = 19;
            this.AddRowMakeup.Text = "Add Row";
            this.AddRowMakeup.UseVisualStyleBackColor = false;
            // 
            // RemoveRowMakeup
            // 
            this.RemoveRowMakeup.BackColor = System.Drawing.SystemColors.Window;
            this.RemoveRowMakeup.Location = new System.Drawing.Point(107, 211);
            this.RemoveRowMakeup.Name = "RemoveRowMakeup";
            this.RemoveRowMakeup.Size = new System.Drawing.Size(83, 23);
            this.RemoveRowMakeup.TabIndex = 20;
            this.RemoveRowMakeup.Text = "Remove Row";
            this.RemoveRowMakeup.UseVisualStyleBackColor = false;
            // 
            // RemoveRowCredit
            // 
            this.RemoveRowCredit.BackColor = System.Drawing.SystemColors.Window;
            this.RemoveRowCredit.Location = new System.Drawing.Point(338, 211);
            this.RemoveRowCredit.Name = "RemoveRowCredit";
            this.RemoveRowCredit.Size = new System.Drawing.Size(83, 23);
            this.RemoveRowCredit.TabIndex = 22;
            this.RemoveRowCredit.Text = "Remove Row";
            this.RemoveRowCredit.UseVisualStyleBackColor = false;
            // 
            // AddRowCredit
            // 
            this.AddRowCredit.BackColor = System.Drawing.SystemColors.Window;
            this.AddRowCredit.Location = new System.Drawing.Point(255, 211);
            this.AddRowCredit.Name = "AddRowCredit";
            this.AddRowCredit.Size = new System.Drawing.Size(65, 23);
            this.AddRowCredit.TabIndex = 21;
            this.AddRowCredit.Text = "Add Row";
            this.AddRowCredit.UseVisualStyleBackColor = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(63, 153);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Number";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(289, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Number";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(345, 153);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Course Name";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(240, 153);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(43, 13);
            this.label13.TabIndex = 25;
            this.label13.Text = "Subject";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.42105F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.57895F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(238, 169);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.71429F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.28571F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 36);
            this.tableLayoutPanel1.TabIndex = 24;
            // 
            // TransferStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 306);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.RemoveRowCredit);
            this.Controls.Add(this.AddRowCredit);
            this.Controls.Add(this.RemoveRowMakeup);
            this.Controls.Add(this.AddRowMakeup);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.MakeupCourseTable);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Yeardropbox);
            this.Controls.Add(this.PrevProgramBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CollegeBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.StudentNumberBox);
            this.Controls.Add(this.Namebox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AddStudentButton);
            this.Name = "TransferStudent";
            this.Text = "TransferStudent";
            this.Load += new System.EventHandler(this.TransferStudent_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddStudentButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Namebox;
        private System.Windows.Forms.TextBox StudentNumberBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox CollegeBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PrevProgramBox;
        private System.Windows.Forms.ComboBox Yeardropbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel MakeupCourseTable;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button AddRowMakeup;
        private System.Windows.Forms.Button RemoveRowMakeup;
        private System.Windows.Forms.Button RemoveRowCredit;
        private System.Windows.Forms.Button AddRowCredit;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}