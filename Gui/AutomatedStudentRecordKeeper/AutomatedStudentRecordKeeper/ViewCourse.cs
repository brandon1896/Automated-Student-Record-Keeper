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
    public partial class ViewCourse : Form
    {
        public ViewCourse()
        {
            InitializeComponent();
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                NpgsqlCommand cmd;
                NpgsqlDataReader reader;
                int j;
                //Complementary courses
                cmd = new NpgsqlCommand("SELECT * FROM complementarycourses", conn);
                reader = cmd.ExecuteReader();
                j = 0;
                while (reader.Read())
                {
                    RowStyle temp = comptable.RowStyles[comptable.RowCount - 1];
                    comptable.RowCount++;
                    comptable.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));
                    comptable.Controls.Add(new Label() { Text = reader[0].ToString() }, 0, comptable.RowCount - 1);
                    comptable.Controls.Add(new Label() { Text = reader[1].ToString() }, 1, comptable.RowCount - 1);
                    comptable.Controls.Add(new Label() { Text = reader[2].ToString() }, 2, comptable.RowCount - 1);
                    comptable.Controls.Add(new Label() { Text = reader[3].ToString() }, 3, comptable.RowCount - 1);
                }
                cmd.Cancel();
                reader.Close();
                //Fall year1
                cmd = new NpgsqlCommand("select coursesubject,coursenumber,coursename,credits FROM courses WHERE coursesection = 'Fall' AND yearlevel = 1", conn);
                reader = cmd.ExecuteReader();
                j = 0;
                while (reader.Read())
                {
                    RowStyle temp = year1falltable.RowStyles[year1falltable.RowCount - 1];
                    year1falltable.RowCount++;
                    year1falltable.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));
                    year1falltable.Controls.Add(new Label() { Text = reader[0].ToString() }, 0, year1falltable.RowCount - 1);
                    year1falltable.Controls.Add(new Label() { Text = reader[1].ToString() }, 1, year1falltable.RowCount - 1);
                    year1falltable.Controls.Add(new Label() { Text = reader[2].ToString() }, 2, year1falltable.RowCount - 1);
                    year1falltable.Controls.Add(new Label() { Text = reader[3].ToString() }, 3, year1falltable.RowCount - 1);
                }
                cmd.Cancel();
                reader.Close();

                conn.Close();
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }
        }


    }
 }
