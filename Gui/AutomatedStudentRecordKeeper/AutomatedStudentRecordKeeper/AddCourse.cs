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
        }


        private void button1_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {


                for (int j = 0; j < this.AddCourseTable.RowCount; j++)
                {
                    if (string.IsNullOrWhiteSpace(AddCourseTable.GetControlFromPosition(0, j).Text) ||
                        string.IsNullOrWhiteSpace(AddCourseTable.GetControlFromPosition(1, j).Text) ||
                        string.IsNullOrWhiteSpace(AddCourseTable.GetControlFromPosition(2, j).Text) ||
                        string.IsNullOrWhiteSpace(AddCourseTable.GetControlFromPosition(3, j).Text) ||
                        Yearleveldropbox.SelectedIndex == -1 ||
                        sectiondropbox.SelectedIndex == -1)
                    {

                    }
                    else
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand("insert into courses values(:sub, :num, :sec, :name, :cred, :yrlvl)", conn);
                        cmd.Parameters.Add(new NpgsqlParameter("sub", AddCourseTable.GetControlFromPosition(0, j).Text));
                        cmd.Parameters.Add(new NpgsqlParameter("num", int.Parse(AddCourseTable.GetControlFromPosition(1, j).Text)));
                        cmd.Parameters.Add(new NpgsqlParameter("sec", sectiondropbox.Text));
                        cmd.Parameters.Add(new NpgsqlParameter("name", AddCourseTable.GetControlFromPosition(2, j).Text));
                        cmd.Parameters.Add(new NpgsqlParameter("cred", double.Parse(AddCourseTable.GetControlFromPosition(3, j).Text)));
                        cmd.Parameters.Add(new NpgsqlParameter("yrlvl", int.Parse(Yearleveldropbox.Text)));
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
