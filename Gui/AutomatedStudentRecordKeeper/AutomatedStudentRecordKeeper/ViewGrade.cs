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
    public partial class ViewGrade : Form
    {
        public ViewGrade()
        {
            InitializeComponent();
            Fulltable1.Hide();
            Fulltable2.Hide();
            Fulltable3.Hide();
            Fulltable4.Hide();
            Fulltable5.Hide();
            Fulltable6.Hide();

        }
        private void Calculate_Click(object sender, EventArgs e)
        {
            List<string> years = new List<string>();
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {

                Fulltable1.Hide();
                Fulltable2.Hide();
                Fulltable3.Hide();
                Fulltable4.Hide();
                Fulltable5.Hide();
                Fulltable6.Hide();
                NpgsqlCommand cmd;
                NpgsqlDataReader reader;
                cmd = new NpgsqlCommand("select distinct yearsection from \""+ StudentBox.Text +"\" order by yearsection asc", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    years.Add(reader[0].ToString());
                }
                cmd.Cancel();
                reader.Close();

                switch (years.Count)
                {
                    case 1:
                        date1.Text = years[0];
                        Fulltable1.Show();
                        break;
                    case 2:
                        date1.Text = years[0];
                        date2.Text = years[1];
                        Fulltable1.Show();
                        Fulltable2.Show();
                        break;
                    case 3:
                        date1.Text = years[0];
                        date2.Text = years[1];
                        date3.Text = years[2];
                        Fulltable1.Show();
                        Fulltable2.Show();
                        Fulltable3.Show();
                        break;
                    case 4:
                        date1.Text = years[0];
                        date2.Text = years[1];
                        date3.Text = years[2];
                        date4.Text = years[3];
                        Fulltable1.Show();
                        Fulltable2.Show();
                        Fulltable3.Show();
                        Fulltable4.Show();
                        break;
                    case 5:
                        date1.Text = years[0];
                        date2.Text = years[1];
                        date3.Text = years[2];
                        date4.Text = years[3];
                        date5.Text = years[4];
                        Fulltable1.Show();
                        Fulltable2.Show();
                        Fulltable3.Show();
                        Fulltable4.Show();
                        Fulltable5.Show();
                        break;
                    case 6:
                        date1.Text = years[0];
                        date2.Text = years[1];
                        date3.Text = years[2];
                        date4.Text = years[3];
                        date5.Text = years[4];
                        date6.Text = years[5];
                        Fulltable1.Show();
                        Fulltable2.Show();
                        Fulltable3.Show();
                        Fulltable4.Show();
                        Fulltable5.Show();
                        Fulltable6.Show();
                        break;
                    default:
                        MessageBox.Show("Student Has No Grades Yet");
                        break;
                }


                conn.Close();
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }

        }
        public void Loadtable1(String date)
        {

        }
        public void Loadtable2(String date)
        {

        }
        public void Loadtable3(String date)
        {

        }
        public void Loadtable4(String date)
        {

        }
        public void Loadtable5(String date)
        {

        }
        public void Loadtable6(String date)
        {

        }
    }
  
}

