using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;

namespace AutomatedStudentRecordKeeper
{
    public partial class AddStudent : Form
    {
        public AddStudent()
        {
            InitializeComponent();
        }

        private void TransferStudent_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void AddRowMakeup_Click(object sender, EventArgs e)
        {
            RowStyle temp = MakeupCourseTable.RowStyles[MakeupCourseTable.RowCount - 1];
            MakeupCourseTable.RowCount++;
            MakeupCourseTable.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));
            MakeupCourseTable.Controls.Add(new RichTextBox() { Width = 46, Height = 21 }, 0, MakeupCourseTable.RowCount - 1);
            MakeupCourseTable.Controls.Add(new RichTextBox() { Width = 53, Height = 21 }, 1, MakeupCourseTable.RowCount - 1);
            MakeupCourseTable.Controls.Add(new RichTextBox() { Width = 233, Height = 21 }, 2, MakeupCourseTable.RowCount - 1);
        }

        private void AddRowCredits_Click(object sender, EventArgs e)
        {
            RowStyle temp = CourseCredTable.RowStyles[CourseCredTable.RowCount - 1];
            CourseCredTable.RowCount++;
            CourseCredTable.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));
            CourseCredTable.Controls.Add(new RichTextBox() { Width = 46, Height = 21 }, 0, CourseCredTable.RowCount - 1);
            CourseCredTable.Controls.Add(new RichTextBox() { Width = 53, Height = 21 }, 1, CourseCredTable.RowCount - 1);
            CourseCredTable.Controls.Add(new RichTextBox() { Width = 233, Height = 21 }, 2, CourseCredTable.RowCount - 1);
        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddStudentButton_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {




                if (string.IsNullOrWhiteSpace(Namebox.Text) ||
                     string.IsNullOrWhiteSpace(StudentNumberBox.Text) ||
                     string.IsNullOrWhiteSpace(PrevSchoolBox.Text) ||
                     string.IsNullOrWhiteSpace(PrevProgramBox.Text) ||
                     Yeardropbox.SelectedIndex == -1)
                {

                }
                else
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("insert into student values(:name, :num, :prevsc, :prevprog, :yrlvl,:yrsec ,:entyear)", conn);
                    cmd.Parameters.Add(new NpgsqlParameter("name", Namebox.Text));
                    cmd.Parameters.Add(new NpgsqlParameter("num", StudentNumberBox.Text));
                    cmd.Parameters.Add(new NpgsqlParameter("prevsc", PrevSchoolBox.Text));
                    cmd.Parameters.Add(new NpgsqlParameter("prevprog", PrevProgramBox.Text));
                    cmd.Parameters.Add(new NpgsqlParameter("yrlvl", int.Parse(Yeardropbox.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("yrsec", DateTime.Now.Year.ToString()+"/"+ (DateTime.Now.Year+1).ToString()));
                    cmd.Parameters.Add(new NpgsqlParameter("entyear", DateTime.Now.Year));
                    cmd.ExecuteNonQuery();

                    cmd.Cancel();
                    cmd = new NpgsqlCommand("create table \"" + StudentNumberBox.Text + "\"(coursesubject text, coursenumber text, coursename text, coursegrade integer, yearsection text, year integer)", conn);
                    cmd.ExecuteNonQuery();

                    cmd.Cancel();
                    for (int j = 0; j < this.MakeupCourseTable.RowCount; j++)
                    {
                        if (string.IsNullOrWhiteSpace(MakeupCourseTable.GetControlFromPosition(0, j).Text) ||
                            string.IsNullOrWhiteSpace(MakeupCourseTable.GetControlFromPosition(1, j).Text))
                        {

                        }
                        else
                        {
                            cmd = new NpgsqlCommand("insert into makeupcourses values(:student, :sub, :num, :name, :cred)", conn);
                            cmd.Parameters.Add(new NpgsqlParameter("student", StudentNumberBox.Text));
                            cmd.Parameters.Add(new NpgsqlParameter("sub", MakeupCourseTable.GetControlFromPosition(0, j).Text));
                            cmd.Parameters.Add(new NpgsqlParameter("num", MakeupCourseTable.GetControlFromPosition(1, j).Text));
                            cmd.Parameters.Add(new NpgsqlParameter("name", MakeupCourseTable.GetControlFromPosition(2, j).Text));
                            cmd.Parameters.Add(new NpgsqlParameter("cred", 0.5 ));
                            cmd.ExecuteNonQuery();
                        }
                    }

                    cmd.Cancel();
                    for (int j = 0; j < this.CourseCredTable.RowCount; j++)
                    {
                        if (string.IsNullOrWhiteSpace(CourseCredTable.GetControlFromPosition(0, j).Text) ||
                            string.IsNullOrWhiteSpace(CourseCredTable.GetControlFromPosition(1, j).Text))
                        {

                        }
                        else
                        {
                            cmd = new NpgsqlCommand("insert into \"" + StudentNumberBox.Text + "\"(coursesubject, coursenumber, coursename, yearsection, year) values( :sub, :num, :name, :yrsec, :yr)", conn);
                            cmd.Parameters.Add(new NpgsqlParameter("sub", CourseCredTable.GetControlFromPosition(0, j).Text));
                            cmd.Parameters.Add(new NpgsqlParameter("num", CourseCredTable.GetControlFromPosition(1, j).Text));
                            cmd.Parameters.Add(new NpgsqlParameter("name", CourseCredTable.GetControlFromPosition(2, j).Text));
                            cmd.Parameters.Add(new NpgsqlParameter("yrsec", DateTime.Now.Year.ToString() + "/" + (DateTime.Now.Year + 1).ToString()));
                            cmd.Parameters.Add(new NpgsqlParameter("yr", DateTime.Now.Year));
                            cmd.ExecuteNonQuery();
                        }
                    }

                }
                conn.Close();
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }
            }
        }
    } 
   

