﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomatedStudentRecordKeeper
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.ShowDialog();
        }

        private void addstudentbutton_Click(object sender, EventArgs e)
        {
            AddStudent add_student = new AddStudent();
            add_student.ShowDialog();
        }

        private void viewstudentsbutton_Click(object sender, EventArgs e)
        {
            ViewStudents view_student = new ViewStudents();
            view_student.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TransferStudent add_transfer = new TransferStudent();
            add_transfer.ShowDialog();
        }

        private void AddCoursesButton_Click(object sender, EventArgs e)
        {
            AddCourse add_course = new AddCourse();
            add_course.ShowDialog();
        }

        private void ViewCoursesButton_Click(object sender, EventArgs e)
        {
            ViewCourse view_course = new ViewCourse();
            view_course.ShowDialog();
        }
    }
}
