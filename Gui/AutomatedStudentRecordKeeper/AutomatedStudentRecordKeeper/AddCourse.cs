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
    public partial class AddCourse : Form
    {
        public AddCourse()
        {
            InitializeComponent();
            int tempyear = DateTime.Now.Year;
            for (int i = 6; i >= -1; i--)
            {
                this.yeardropbox.Items.Add((tempyear - i).ToString() + "/" + (tempyear - i + 1).ToString());
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            AddCourseTable.Hide();
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                if (Yearleveldropbox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a Year level");

                }
                else if (sectiondropbox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a section");
                }
                else if (yeardropbox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a Year");
                }
                else {
                    int count = 0;
                    for (int j = 0; j < this.AddCourseTable.RowCount; j++)
                    {
                        if (string.IsNullOrWhiteSpace(AddCourseTable.GetControlFromPosition(0, j).Text) ||
                            string.IsNullOrWhiteSpace(AddCourseTable.GetControlFromPosition(1, j).Text) ||
                            string.IsNullOrWhiteSpace(AddCourseTable.GetControlFromPosition(2, j).Text) ||
                            string.IsNullOrWhiteSpace(AddCourseTable.GetControlFromPosition(3, j).Text))
                        {

                        }
                        else if (AddCourseTable.GetControlFromPosition(0,j).Text.Length!=4 || AddCourseTable.GetControlFromPosition(1, j).Text.Length != 4)
                        {

                        }
                        else
                        {
                            NpgsqlDataReader reader;
                            NpgsqlCommand cmd;
                            string checkifexists = "False";
                            cmd = new NpgsqlCommand("select exists(select true from courses where coursesubject = :sub and coursenumber = :num  and lastusedyear is null)", conn);
                            cmd.Parameters.Add(new NpgsqlParameter("sub", AddCourseTable.GetControlFromPosition(0, j).Text));
                            cmd.Parameters.Add(new NpgsqlParameter("num", AddCourseTable.GetControlFromPosition(1, j).Text));
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                checkifexists = reader[0].ToString();
                            }
                            cmd.Cancel();
                            reader.Close();
                            if (checkifexists == "False")
                            {
                                cmd = new NpgsqlCommand("insert into courses values(:sub, :num, :sec, :name, :cred, :yrlvl, :yrsec, :entyear)", conn);
                                cmd.Parameters.Add(new NpgsqlParameter("sub", AddCourseTable.GetControlFromPosition(0, j).Text));
                                cmd.Parameters.Add(new NpgsqlParameter("num", AddCourseTable.GetControlFromPosition(1, j).Text));
                                cmd.Parameters.Add(new NpgsqlParameter("sec", sectiondropbox.Text));
                                cmd.Parameters.Add(new NpgsqlParameter("name", AddCourseTable.GetControlFromPosition(2, j).Text));
                                cmd.Parameters.Add(new NpgsqlParameter("cred", double.Parse(AddCourseTable.GetControlFromPosition(3, j).Text)));
                                cmd.Parameters.Add(new NpgsqlParameter("yrlvl", int.Parse(Yearleveldropbox.Text)));
                                cmd.Parameters.Add(new NpgsqlParameter("yrsec", yeardropbox.Text));
                                cmd.Parameters.Add(new NpgsqlParameter("entyear", int.Parse(yeardropbox.Text.Substring(0, 4))));
                                cmd.ExecuteNonQuery();
                               
                            }
                            AddCourseTable.GetControlFromPosition(0, j).Text = "";
                            AddCourseTable.GetControlFromPosition(1, j).Text = "";
                            AddCourseTable.GetControlFromPosition(2, j).Text = "";
                            AddCourseTable.GetControlFromPosition(3, j).Text = "";
                            count++;
                        }
                    }
                    conn.Close();
                    MessageBox.Show(count.ToString()+" rows added to table, check formating if form not cleared");
                }
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }
            AddCourseTable.Show();
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

        private void richTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }
    }
}
