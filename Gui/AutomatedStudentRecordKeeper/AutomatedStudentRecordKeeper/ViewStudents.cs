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
        public ViewStudents()
        {
            InitializeComponent();
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
                    RowStyle temp = StudentTable.RowStyles[StudentTable.RowCount - 1];
                    StudentTable.RowCount++;
                    StudentTable.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));
                    StudentTable.Controls.Add(new Label() { Text = reader[0].ToString() }, 0, StudentTable.RowCount - 1);
                    StudentTable.Controls.Add(new Label() { Text = reader[1].ToString() }, 1, StudentTable.RowCount - 1);
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
