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
                    else
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand("insert into complementarycourses values(:sub, :num, :name, :cred)", conn);
                        cmd.Parameters.Add(new NpgsqlParameter("sub", CourseTable.GetControlFromPosition(0, j).Text));
                        cmd.Parameters.Add(new NpgsqlParameter("num", int.Parse(CourseTable.GetControlFromPosition(1, j).Text)));
                        cmd.Parameters.Add(new NpgsqlParameter("name", CourseTable.GetControlFromPosition(2, j).Text));
                        cmd.Parameters.Add(new NpgsqlParameter("cred", int.Parse(CourseTable.GetControlFromPosition(3, j).Text)));
                        cmd.ExecuteNonQuery();
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
