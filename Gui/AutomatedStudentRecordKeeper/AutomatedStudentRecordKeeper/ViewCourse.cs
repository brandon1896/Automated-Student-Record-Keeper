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
                //Complementary courses
                cmd = new NpgsqlCommand("SELECT * FROM complementarycourses", conn);
                reader = cmd.ExecuteReader();
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

                //Winter year1
                cmd = new NpgsqlCommand("select coursesubject,coursenumber,coursename,credits FROM courses WHERE coursesection = 'Winter' AND yearlevel = 1", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RowStyle temp = year1wintertable.RowStyles[year1wintertable.RowCount - 1];
                    year1wintertable.RowCount++;
                    year1wintertable.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));
                    year1wintertable.Controls.Add(new Label() { Text = reader[0].ToString() }, 0, year1wintertable.RowCount - 1);
                    year1wintertable.Controls.Add(new Label() { Text = reader[1].ToString() }, 1, year1wintertable.RowCount - 1);
                    year1wintertable.Controls.Add(new Label() { Text = reader[2].ToString() }, 2, year1wintertable.RowCount - 1);
                    year1wintertable.Controls.Add(new Label() { Text = reader[3].ToString() }, 3, year1wintertable.RowCount - 1);
                }
                cmd.Cancel();
                reader.Close();

                //Fall year2
                cmd = new NpgsqlCommand("select coursesubject,coursenumber,coursename,credits FROM courses WHERE coursesection = 'Fall' AND yearlevel = 2", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RowStyle temp = year2falltable.RowStyles[year2falltable.RowCount - 1];
                    year2falltable.RowCount++;
                    year2falltable.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));
                    year2falltable.Controls.Add(new Label() { Text = reader[0].ToString() }, 0, year2falltable.RowCount - 1);
                    year2falltable.Controls.Add(new Label() { Text = reader[1].ToString() }, 1, year2falltable.RowCount - 1);
                    year2falltable.Controls.Add(new Label() { Text = reader[2].ToString() }, 2, year2falltable.RowCount - 1);
                    year2falltable.Controls.Add(new Label() { Text = reader[3].ToString() }, 3, year2falltable.RowCount - 1);
                }
                cmd.Cancel();
                reader.Close();

                //Winter year2
                cmd = new NpgsqlCommand("select coursesubject,coursenumber,coursename,credits FROM courses WHERE coursesection = 'Winter' AND yearlevel = 2", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RowStyle temp = year2wintertable.RowStyles[year2wintertable.RowCount - 1];
                    year2wintertable.RowCount++;
                    year2wintertable.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));
                    year2wintertable.Controls.Add(new Label() { Text = reader[0].ToString() }, 0, year2wintertable.RowCount - 1);
                    year2wintertable.Controls.Add(new Label() { Text = reader[1].ToString() }, 1, year2wintertable.RowCount - 1);
                    year2wintertable.Controls.Add(new Label() { Text = reader[2].ToString() }, 2, year2wintertable.RowCount - 1);
                    year2wintertable.Controls.Add(new Label() { Text = reader[3].ToString() }, 3, year2wintertable.RowCount - 1);
                }
                cmd.Cancel();
                reader.Close();
                //Fall year3
                cmd = new NpgsqlCommand("select coursesubject,coursenumber,coursename,credits FROM courses WHERE coursesection = 'Fall' AND yearlevel = 3", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RowStyle temp = year3falltable.RowStyles[year3falltable.RowCount - 1];
                    year3falltable.RowCount++;
                    year3falltable.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));
                    year3falltable.Controls.Add(new Label() { Text = reader[0].ToString() }, 0, year3falltable.RowCount - 1);
                    year3falltable.Controls.Add(new Label() { Text = reader[1].ToString() }, 1, year3falltable.RowCount - 1);
                    year3falltable.Controls.Add(new Label() { Text = reader[2].ToString() }, 2, year3falltable.RowCount - 1);
                    year3falltable.Controls.Add(new Label() { Text = reader[3].ToString() }, 3, year3falltable.RowCount - 1);
                }
                cmd.Cancel();
                reader.Close();

                //Winter year3
                cmd = new NpgsqlCommand("select coursesubject,coursenumber,coursename,credits FROM courses WHERE coursesection = 'Winter' AND yearlevel = 3", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RowStyle temp = year3wintertable.RowStyles[year3wintertable.RowCount - 1];
                    year3wintertable.RowCount++;
                    year3wintertable.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));
                    year3wintertable.Controls.Add(new Label() { Text = reader[0].ToString() }, 0, year3wintertable.RowCount - 1);
                    year3wintertable.Controls.Add(new Label() { Text = reader[1].ToString() }, 1, year3wintertable.RowCount - 1);
                    year3wintertable.Controls.Add(new Label() { Text = reader[2].ToString() }, 2, year3wintertable.RowCount - 1);
                    year3wintertable.Controls.Add(new Label() { Text = reader[3].ToString() }, 3, year3wintertable.RowCount - 1);
                }
                cmd.Cancel();
                reader.Close();
                
                 //Fall year4
                cmd = new NpgsqlCommand("select coursesubject,coursenumber,coursename,credits FROM courses WHERE coursesection = 'Fall' AND yearlevel = 4", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RowStyle temp = year4falltable.RowStyles[year4falltable.RowCount - 1];
                    year4falltable.RowCount++;
                    year4falltable.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));
                    year4falltable.Controls.Add(new Label() { Text = reader[0].ToString() }, 0, year4falltable.RowCount - 1);
                    year4falltable.Controls.Add(new Label() { Text = reader[1].ToString() }, 1, year4falltable.RowCount - 1);
                    year4falltable.Controls.Add(new Label() { Text = reader[2].ToString() }, 2, year4falltable.RowCount - 1);
                    year4falltable.Controls.Add(new Label() { Text = reader[3].ToString() }, 3, year4falltable.RowCount - 1);
                }
                cmd.Cancel();
                reader.Close();

                //Winter year4
                cmd = new NpgsqlCommand("select coursesubject,coursenumber,coursename,credits FROM courses WHERE coursesection = 'Winter' AND yearlevel = 4", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RowStyle temp = year4wintertable.RowStyles[year4wintertable.RowCount - 1];
                    year4wintertable.RowCount++;
                    year4wintertable.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));
                    year4wintertable.Controls.Add(new Label() { Text = reader[0].ToString() }, 0, year4wintertable.RowCount - 1);
                    year4wintertable.Controls.Add(new Label() { Text = reader[1].ToString() }, 1, year4wintertable.RowCount - 1);
                    year4wintertable.Controls.Add(new Label() { Text = reader[2].ToString() }, 2, year4wintertable.RowCount - 1);
                    year4wintertable.Controls.Add(new Label() { Text = reader[3].ToString() }, 3, year4wintertable.RowCount - 1);
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

        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }
    }
 }
