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
    public partial class AddGrade : Form
    {
        public AddGrade()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                NpgsqlCommand cmd = new NpgsqlCommand("select exists (select true from pg_tables where tablename = '" + StudentNumber.Text + "')", conn);
                string checknum = "f";
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    checknum = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();
                if (checknum == "False")
                {
                    MessageBox.Show("Student Doesnt exist");
                }
                else
                {
                    for (int j = 0; j < this.CourseTable.RowCount; j++)
                    {
                        if (string.IsNullOrWhiteSpace(CourseTable.GetControlFromPosition(0, j).Text) ||
                            string.IsNullOrWhiteSpace(CourseTable.GetControlFromPosition(1, j).Text) ||
                            string.IsNullOrWhiteSpace(CourseTable.GetControlFromPosition(2, j).Text))
                        {

                        }
                        else
                        {
                            cmd = new NpgsqlCommand("insert into \"" + StudentNumber.Text + "\"(coursesubject,coursenumber,coursegrade) values(:sub, :num, :grade)", conn);
                            cmd.Parameters.Add(new NpgsqlParameter("sub", CourseTable.GetControlFromPosition(0, j).Text));
                            cmd.Parameters.Add(new NpgsqlParameter("num", int.Parse(CourseTable.GetControlFromPosition(1, j).Text)));
                            cmd.Parameters.Add(new NpgsqlParameter("grade", int.Parse(CourseTable.GetControlFromPosition(2, j).Text)));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }
            }
    }
    }

