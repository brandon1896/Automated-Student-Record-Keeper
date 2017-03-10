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
    public partial class ViewStudents : Form
    {
        int tablepage = 1;
        List<studentinfo> students = new List<studentinfo>();
        public ViewStudents()
        {
            InitializeComponent();
            StudentTable.Hide();
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                NpgsqlCommand cmd;
                NpgsqlDataReader reader;
                //Complementary courses
                cmd = new NpgsqlCommand("SELECT name, studentid FROM student", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new studentinfo {Name = reader[0].ToString(), Number = reader[1].ToString() });
                }
                for (int i = 1; i <= 14; i++)
                {
                    if (students.Count >= i)
                    {
                        StudentTable.GetControlFromPosition(0, i).Text = students[i - 1].Name;
                        StudentTable.GetControlFromPosition(1, i).Text = students[i - 1].Number;

                    }
                    else
                    {
                        StudentTable.GetControlFromPosition(0, i).Text = " ";
                        StudentTable.GetControlFromPosition(1, i).Text = " ";
                    }
                }
                conn.Close();
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }
            StudentTable.Show();

        }

        private void removebutton_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                NpgsqlCommand cmd;
                cmd = new NpgsqlCommand("delete from student where studentid = '"+removetext.Text+"'", conn);
                cmd.ExecuteNonQuery();
                cmd = new NpgsqlCommand("delete from makeupcourses where studentid = '"+removetext.Text+"'", conn);
                cmd.ExecuteNonQuery();
                cmd = new NpgsqlCommand("drop table if exists \"" + removetext.Text + "\"", conn);
                cmd.ExecuteNonQuery();

                conn.Close();
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }

        }

        private void tableprev_Click(object sender, EventArgs e)
        {
            if (tablepage > 1)
            {
                tablepage--;
                StudentTable.Hide();
                int j = 1;
                for (int i = ((tablepage - 1) * 14) + 1; i <= (14 * tablepage); i++)
                {
                    if (students.Count >= i)
                    {
                        StudentTable.GetControlFromPosition(0, i).Text = students[i - 1].Name;
                        StudentTable.GetControlFromPosition(1, i).Text = students[i - 1].Number;

                    }
                    else
                    {
                        StudentTable.GetControlFromPosition(0, i).Text = " ";
                        StudentTable.GetControlFromPosition(1, i).Text = " ";
                    }

                    j++;
                }
                StudentTable.Show();
            }
            else { }
        }

        private void tablenext_Click(object sender, EventArgs e)
        {
            if (students.Count > (tablepage * 14))
            {
                StudentTable.Hide();
                int j = 1;
                tablepage++;
                for (int i = ((tablepage - 1) * 14) + 1; i <= (14 * tablepage); i++)
                {
                    if (students.Count >= i)
                    {
                        StudentTable.GetControlFromPosition(0, i).Text = students[i - 1].Name;
                        StudentTable.GetControlFromPosition(1, i).Text = students[i - 1].Number;

                    }
                    else
                    {
                        StudentTable.GetControlFromPosition(0, i).Text = " ";
                        StudentTable.GetControlFromPosition(1, i).Text = " ";
                    }
                    j++;
                }
                StudentTable.Show();
            }
            else { }
        }
    }
    }
