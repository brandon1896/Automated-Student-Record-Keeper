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
    public partial class AddComplementaryCourses : Form
    {
        public AddComplementaryCourses()
        {
            InitializeComponent();
        }

        private void submittable_Click_1(object sender, EventArgs e)
        {
            CourseTable.Hide();
            int count = 0;
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {


                for (int j = 0; j < this.CourseTable.RowCount; j++)
                {
                    if (string.IsNullOrWhiteSpace(CourseTable.GetControlFromPosition(0, j).Text) ||
                        string.IsNullOrWhiteSpace(CourseTable.GetControlFromPosition(1, j).Text) ||
                        string.IsNullOrWhiteSpace(CourseTable.GetControlFromPosition(2, j).Text) ||
                        string.IsNullOrWhiteSpace(CourseTable.GetControlFromPosition(3, j).Text))
                    {

                    }
                    else if (CourseTable.GetControlFromPosition(0, j).Text.Length!=4 || CourseTable.GetControlFromPosition(1, j).Text.Length!=4)
                    {

                    }
                    else
                    {
                        NpgsqlDataReader reader;
                        NpgsqlCommand cmd;
                        string checkifexists="False";
                        cmd = new NpgsqlCommand("select exists(select true from complementarycourses where coursesubject =  :sub and coursenumber = :num  and lastusedyear is null)", conn);
                        cmd.Parameters.Add(new NpgsqlParameter("sub", CourseTable.GetControlFromPosition(0, j).Text));
                        cmd.Parameters.Add(new NpgsqlParameter("num", CourseTable.GetControlFromPosition(1, j).Text));
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            checkifexists = reader[0].ToString();
                        }
                        cmd.Cancel();
                        reader.Close();
                        if (checkifexists == "False")
                        {

                            cmd = new NpgsqlCommand("insert into complementarycourses values(:sub, :num, :name, :cred, :entyear)", conn);
                            cmd.Parameters.Add(new NpgsqlParameter("sub", CourseTable.GetControlFromPosition(0, j).Text));
                            cmd.Parameters.Add(new NpgsqlParameter("num", CourseTable.GetControlFromPosition(1, j).Text));
                            cmd.Parameters.Add(new NpgsqlParameter("name", CourseTable.GetControlFromPosition(2, j).Text));
                            cmd.Parameters.Add(new NpgsqlParameter("cred", double.Parse(CourseTable.GetControlFromPosition(3, j).Text)));
                            cmd.Parameters.Add(new NpgsqlParameter("entyear", DateTime.Now.Year));
                            cmd.ExecuteNonQuery();
                        }
                        CourseTable.GetControlFromPosition(0, j).Text ="";
                        CourseTable.GetControlFromPosition(1, j).Text ="";
                        CourseTable.GetControlFromPosition(2, j).Text ="";
                        CourseTable.GetControlFromPosition(3, j).Text = "";
                        count++;
                    }
                }
                conn.Close();
                MessageBox.Show(count.ToString()+" rows added to table, check formating if any field not cleared");
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }
            CourseTable.Show();
        }

        private void richTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if(!Char.IsDigit(ch) && ch!= 8)
            {
                e.Handled = true;
            }
        }
        

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (Char.IsDigit(ch))
            {
                e.Handled = true;
            }
            else
            {
                e.KeyChar = Char.ToUpper(ch);
            }
        }

        

    }
    }
