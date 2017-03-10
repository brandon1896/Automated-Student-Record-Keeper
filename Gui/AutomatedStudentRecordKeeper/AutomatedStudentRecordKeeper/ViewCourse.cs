using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using Npgsql;
using NpgsqlTypes;
namespace AutomatedStudentRecordKeeper
{
    public partial class ViewCourse : Form
    {
        bool alldone = false, fallfin1 = false, fallfin2 = false, fallfin3 = false, fallfin4 = false, wintfin1 = false, wintfin2 = false, wintfin3 = false, wintfin4 = false, compfin = false;
        int fall1page = 1, wint1page = 1, fall2page = 1, wint2page = 1, fall3page = 1, wint3page = 1, fall4page = 1, wint4page = 1, comppage = 1;
        List<viewtabletinfo> fall1list = new List<viewtabletinfo>();
        List<viewtabletinfo> wint1list = new List<viewtabletinfo>();
        List<viewtabletinfo> fall2list = new List<viewtabletinfo>();
        List<viewtabletinfo> wint2list = new List<viewtabletinfo>();
        List<viewtabletinfo> fall3list = new List<viewtabletinfo>();
        List<viewtabletinfo> wint3list = new List<viewtabletinfo>();
        List<viewtabletinfo> fall4list = new List<viewtabletinfo>();
        List<viewtabletinfo> wint4list = new List<viewtabletinfo>();
        List<viewtabletinfo> complist = new List<viewtabletinfo>();
        public ViewCourse()
        {
            InitializeComponent();
            Thread threadfall1 = new Thread(new ThreadStart(loadtablefall1));
            Thread threadfall2 = new Thread(new ThreadStart(loadtablefall2));
            Thread threadfall3 = new Thread(new ThreadStart(loadtablefall3));
            Thread threadfall4 = new Thread(new ThreadStart(loadtablefall4));
            Thread threadwinter1 = new Thread(new ThreadStart(loadtablewinter1));
            Thread threadwinter2 = new Thread(new ThreadStart(loadtablewinter2));
            Thread threadwinter3 = new Thread(new ThreadStart(loadtablewinter3));
            Thread threadwinter4 = new Thread(new ThreadStart(loadtablewinter4));
            Thread threadcomp = new Thread(new ThreadStart(loadcomptable));
            waitScreen waitscrn = new waitScreen();
            waitscrn.Show();
            Application.DoEvents();
            threadcomp.Start();
            threadfall1.Start();
            threadfall2.Start();
            threadfall3.Start();
            threadfall4.Start();
            threadwinter1.Start();
            threadwinter2.Start();
            threadwinter3.Start();
            threadwinter4.Start();
            while (alldone==false)
            if (compfin && fallfin1 && fallfin2 && fallfin3 && fallfin4 && wintfin1 && wintfin2 && wintfin3 && wintfin4)
            {
                
                alldone = true;
                    waitscrn.Hide();
                
            }
        }
        public void loadcomptable()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                NpgsqlCommand cmd;
                NpgsqlDataReader reader;
                //Fall year1
                cmd = new NpgsqlCommand("select coursesubject,coursenumber,coursename,credits FROM complementarycourses where lastusedyear IS NULL", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    complist.Add(new viewtabletinfo { Subject = reader[0].ToString(), Number = reader[1].ToString(), Name = reader[2].ToString(), Credits = reader[3].ToString() });
                }
                cmd.Cancel();
                reader.Close();
                for (int i = 1; i <= 18; i++)
                {
                    if (complist.Count >= i)
                    {
                        SetControlPropertyThreadSafe(complementtable.GetControlFromPosition(0, i), "Text", complist[i - 1].Subject);
                        SetControlPropertyThreadSafe(complementtable.GetControlFromPosition(1, i), "Text", complist[i - 1].Number);
                        SetControlPropertyThreadSafe(complementtable.GetControlFromPosition(2, i), "Text", complist[i - 1].Name);
                        SetControlPropertyThreadSafe(complementtable.GetControlFromPosition(3, i), "Text", complist[i - 1].Credits);
                    }
                    else
                    {
                        SetControlPropertyThreadSafe(complementtable.GetControlFromPosition(0, i), "Text", "");
                        SetControlPropertyThreadSafe(complementtable.GetControlFromPosition(1, i), "Text", "");
                        SetControlPropertyThreadSafe(complementtable.GetControlFromPosition(2, i), "Text", "");
                        SetControlPropertyThreadSafe(complementtable.GetControlFromPosition(3, i), "Text", "");
                    }
                }
                conn.Close();
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }
            compfin = true;
        }
        public void loadtablefall1()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                NpgsqlCommand cmd;
                NpgsqlDataReader reader;
                //Fall year1
                cmd = new NpgsqlCommand("select coursesubject,coursenumber,coursename,credits FROM courses WHERE coursesection = 'Fall' AND yearlevel = 1 AND lastusedyear IS NULL", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    fall1list.Add(new viewtabletinfo { Subject = reader[0].ToString(), Number = reader[1].ToString(), Name = reader[2].ToString(), Credits = reader[3].ToString() });
                }
                cmd.Cancel();
                reader.Close();
                for (int i = 1; i <= 7; i++)
                {
                    if (fall1list.Count >= i)
                    {
                        SetControlPropertyThreadSafe(year1falltable.GetControlFromPosition(0, i),"Text",  fall1list[i - 1].Subject);
                        SetControlPropertyThreadSafe(year1falltable.GetControlFromPosition(1, i), "Text", fall1list[i - 1].Number);
                        SetControlPropertyThreadSafe(year1falltable.GetControlFromPosition(2, i), "Text", fall1list[i - 1].Name);
                        SetControlPropertyThreadSafe(year1falltable.GetControlFromPosition(3, i), "Text", fall1list[i - 1].Credits);
                    }
                    else
                    {
                        SetControlPropertyThreadSafe(year1falltable.GetControlFromPosition(0, i), "Text", "");
                        SetControlPropertyThreadSafe(year1falltable.GetControlFromPosition(1, i), "Text", "");
                        SetControlPropertyThreadSafe(year1falltable.GetControlFromPosition(2, i), "Text", "");
                        SetControlPropertyThreadSafe(year1falltable.GetControlFromPosition(3, i), "Text", "");
                    }
                }
                    conn.Close();
                }
            else
            {
                    MessageBox.Show("Connection error to database");
                }
            fallfin1 = true;
            }
        public void loadtablewinter1()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                NpgsqlCommand cmd;
                NpgsqlDataReader reader;
                //Winter year1
                cmd = new NpgsqlCommand("select coursesubject,coursenumber,coursename,credits FROM courses WHERE coursesection = 'Winter' AND yearlevel = 1 AND lastusedyear IS NULL", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    wint1list.Add(new viewtabletinfo { Subject = reader[0].ToString(), Number = reader[1].ToString(), Name = reader[2].ToString(), Credits = reader[3].ToString() });
                }
                cmd.Cancel();
                reader.Close();
                for (int i = 1; i <= 7; i++)
                {
                    if (wint1list.Count >= i)
                    {
                        SetControlPropertyThreadSafe(year1wintertable.GetControlFromPosition(0, i), "Text", wint1list[i - 1].Subject);
                        SetControlPropertyThreadSafe(year1wintertable.GetControlFromPosition(1, i), "Text", wint1list[i - 1].Number);
                        SetControlPropertyThreadSafe(year1wintertable.GetControlFromPosition(2, i), "Text", wint1list[i - 1].Name);
                        SetControlPropertyThreadSafe(year1wintertable.GetControlFromPosition(3, i), "Text", wint1list[i - 1].Credits);
                    }
                    else
                    {
                        SetControlPropertyThreadSafe(year1wintertable.GetControlFromPosition(0, i), "Text", "");
                        SetControlPropertyThreadSafe(year1wintertable.GetControlFromPosition(1, i), "Text", "");
                        SetControlPropertyThreadSafe(year1wintertable.GetControlFromPosition(2, i), "Text", ""); 
                        SetControlPropertyThreadSafe(year1wintertable.GetControlFromPosition(3, i), "Text", "");
                    }
                }
                conn.Close();
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }

            wintfin1 = true;

        }

        private void fall1prev_Click(object sender, EventArgs e)
        {
            year1falltable.SuspendLayout();
            if(fall1page > 1) {
                int j = 1;
                fall1page--;
                for (int i = ((fall1page - 1) * 7) + 1; i <= (7*fall1page); i++)
                {
                    if (fall1list.Count >= i)
                    {
                        year1falltable.GetControlFromPosition(0, j).Text = fall1list[i - 1].Subject;
                        year1falltable.GetControlFromPosition(1, j).Text = fall1list[i - 1].Number;
                        year1falltable.GetControlFromPosition(2, j).Text = fall1list[i - 1].Name;
                        year1falltable.GetControlFromPosition(3, j).Text = fall1list[i - 1].Credits;
                    }
                    else
                    {
                        year1falltable.GetControlFromPosition(0, j).Text = "";
                        year1falltable.GetControlFromPosition(1, j).Text = "";
                        year1falltable.GetControlFromPosition(2, j).Text = "";
                        year1falltable.GetControlFromPosition(3, j).Text = "";
                    }
                    j++;
                }
            }
            else { }
            year1falltable.ResumeLayout();
        }

        private void fall1next_Click(object sender, EventArgs e)
        {
            year1falltable.SuspendLayout();
            if (fall1list.Count > (fall1page*7))
            {
                int j = 1;
                fall1page++;
                for (int i = ((fall1page - 1 )* 7) + 1; i <= (7 * fall1page); i++)
                {
                    if (fall1list.Count >= i)
                    {
                        year1falltable.GetControlFromPosition(0, j).Text = fall1list[i - 1].Subject;
                        year1falltable.GetControlFromPosition(1, j).Text = fall1list[i - 1].Number;
                        year1falltable.GetControlFromPosition(2, j).Text = fall1list[i - 1].Name;
                        year1falltable.GetControlFromPosition(3, j).Text = fall1list[i - 1].Credits;
                    }
                    else
                    {
                        year1falltable.GetControlFromPosition(0, j).Text = "";
                        year1falltable.GetControlFromPosition(1, j).Text = "";
                        year1falltable.GetControlFromPosition(2, j).Text = "";
                        year1falltable.GetControlFromPosition(3, j).Text = "";
                    }

                    j++;
                }
            }
            else { }
            year1falltable.ResumeLayout();
        }

        private void wint1prev_Click(object sender, EventArgs e)
        {
            year1wintertable.SuspendLayout();
            if (wint1page > 1)
            {
                int j = 1;
                wint1page--;
                for (int i = ((wint1page - 1 )* 7) + 1; i <= (7 * wint1page); i++)
                {
                    if (wint1list.Count >= i)
                    {
                        year1wintertable.GetControlFromPosition(0, j).Text = wint1list[i - 1].Subject;
                        year1wintertable.GetControlFromPosition(1, j).Text = wint1list[i - 1].Number;
                        year1wintertable.GetControlFromPosition(2, j).Text = wint1list[i - 1].Name;
                        year1wintertable.GetControlFromPosition(3, j).Text = wint1list[i - 1].Credits;
                    }
                    else
                    {
                        year1wintertable.GetControlFromPosition(0, j).Text = "";
                        year1wintertable.GetControlFromPosition(1, j).Text = "";
                        year1wintertable.GetControlFromPosition(2, j).Text = "";
                        year1wintertable.GetControlFromPosition(3, j).Text = "";
                    }
                    j++;
                }
            }
            else { }
            year1wintertable.ResumeLayout();
        }

        private void wint1next_Click(object sender, EventArgs e)
        {
            year1wintertable.SuspendLayout();
            if (wint1list.Count > wint1page*7)
            {
                int j = 1;
                wint1page++;
                for (int i = ((wint1page - 1 )* 7) + 1; i <= (7 * wint1page); i++)
                {
                    if (wint1list.Count >= i)
                    {
                        year1wintertable.GetControlFromPosition(0, j).Text = wint1list[i - 1].Subject;
                        year1wintertable.GetControlFromPosition(1, j).Text = wint1list[i - 1].Number;
                        year1wintertable.GetControlFromPosition(2, j).Text = wint1list[i - 1].Name;
                        year1wintertable.GetControlFromPosition(3, j).Text = wint1list[i - 1].Credits;
                    }
                    else
                    {
                        year1wintertable.GetControlFromPosition(0, j).Text = "";
                        year1wintertable.GetControlFromPosition(1, j).Text = "";
                        year1wintertable.GetControlFromPosition(2, j).Text = "";
                        year1wintertable.GetControlFromPosition(3, j).Text = "";
                    }
                    j++;
                }
            }
            else { }
            year1wintertable.ResumeLayout();
        }

        private void fall2prev_Click(object sender, EventArgs e)
        {
            year2falltable.SuspendLayout();
            if (fall2page > 1)
            {
                int j = 1;
                fall2page--;
                for (int i = ((fall2page - 1 )* 7) + 1; i <= (7 * fall2page); i++)
                {
                    if (fall2list.Count >= i)
                    {
                        year2falltable.GetControlFromPosition(0, j).Text = fall2list[i - 1].Subject;
                        year2falltable.GetControlFromPosition(1, j).Text = fall2list[i - 1].Number;
                        year2falltable.GetControlFromPosition(2, j).Text = fall2list[i - 1].Name;
                        year2falltable.GetControlFromPosition(3, j).Text = fall2list[i - 1].Credits;
                    }
                    else
                    {
                        year2falltable.GetControlFromPosition(0, j).Text = "";
                        year2falltable.GetControlFromPosition(1, j).Text = "";
                        year2falltable.GetControlFromPosition(2, j).Text = "";
                        year2falltable.GetControlFromPosition(3, j).Text = "";
                    }
                    j++;
           
                }
            }
            else { }
            year2falltable.ResumeLayout();
        }

        private void fall2next_Click(object sender, EventArgs e)
        {
            year2falltable.SuspendLayout();
            if (fall2list.Count > fall2page*7)
            {
                int j = 1;
                fall2page++;
                for (int i = ((fall2page - 1 )* 7) + 1; i <= (7 * fall2page); i++)
                {
                    if (fall2list.Count >= i)
                    {
                        year2falltable.GetControlFromPosition(0, j).Text = fall2list[i - 1].Subject;
                        year2falltable.GetControlFromPosition(1, j).Text = fall2list[i - 1].Number;
                        year2falltable.GetControlFromPosition(2, j).Text = fall2list[i - 1].Name;
                        year2falltable.GetControlFromPosition(3, j).Text = fall2list[i - 1].Credits;
                    }
                    else
                    {
                        year2falltable.GetControlFromPosition(0, j).Text = "";
                        year2falltable.GetControlFromPosition(1, j).Text = "";
                        year2falltable.GetControlFromPosition(2, j).Text = "";
                        year2falltable.GetControlFromPosition(3, j).Text = "";
                    }
                    j++;
                }
            }
            else { }
            year2falltable.ResumeLayout();
        }

        private void wint2prev_Click(object sender, EventArgs e)
        {
            year2wintertable.SuspendLayout();
            if (wint2page > 1)
            {
                int j = 1;
                wint2page--;
                for (int i = ((wint2page - 1 )* 7) + 1; i <= (7 * wint2page); i++)
                {
                    if (wint2list.Count >= i)
                    {
                        year2wintertable.GetControlFromPosition(0, j).Text = wint2list[i - 1].Subject;
                        year2wintertable.GetControlFromPosition(1, j).Text = wint2list[i - 1].Number;
                        year2wintertable.GetControlFromPosition(2, j).Text = wint2list[i - 1].Name;
                        year2wintertable.GetControlFromPosition(3, j).Text = wint2list[i - 1].Credits;
                    }
                    else
                    {
                        year2wintertable.GetControlFromPosition(0, j).Text = "";
                        year2wintertable.GetControlFromPosition(1, j).Text = "";
                        year2wintertable.GetControlFromPosition(2, j).Text = "";
                        year2wintertable.GetControlFromPosition(3, j).Text = "";
                    }
                    j++;
                }
            }
            else { }
            year2wintertable.ResumeLayout();
        }

        private void prevcomp_Click(object sender, EventArgs e)
        {
            complementtable.SuspendLayout();
            if (comppage > 1)
            {
                int j = 1;
                comppage--;
                for (int i = ((comppage - 1 )* 7) + 1; i <= (7 * comppage); i++)
                {
                    if (complist.Count >= i)
                    {
                        complementtable.GetControlFromPosition(0, j).Text = complist[i - 1].Subject;
                        complementtable.GetControlFromPosition(1, j).Text = complist[i - 1].Number;
                        complementtable.GetControlFromPosition(2, j).Text = complist[i - 1].Name;
                        complementtable.GetControlFromPosition(3, j).Text = complist[i - 1].Credits;
                    }
                    else
                    {
                        complementtable.GetControlFromPosition(0, j).Text = "";
                        complementtable.GetControlFromPosition(1, j).Text = "";
                        complementtable.GetControlFromPosition(2, j).Text = "";
                        complementtable.GetControlFromPosition(3, j).Text = "";
                    }
                    j++;
                }
            }
            else { }
            complementtable.ResumeLayout();
        }

        private void nextcomp_Click(object sender, EventArgs e)
        {
            complementtable.SuspendLayout();
            if (complist.Count > comppage * 7)
            {
                int j = 1;
                comppage++;
                for (int i = ((comppage - 1) * 7) + 1; i <= (7 * comppage); i++)
                {
                    if (complist.Count >= i)
                    {
                        complementtable.GetControlFromPosition(0, j).Text = complist[i - 1].Subject;
                        complementtable.GetControlFromPosition(1, j).Text = complist[i - 1].Number;
                        complementtable.GetControlFromPosition(2, j).Text = complist[i - 1].Name;
                        complementtable.GetControlFromPosition(3, j).Text = complist[i - 1].Credits;
                    }
                    else
                    {
                        complementtable.GetControlFromPosition(0, j).Text = "";
                        complementtable.GetControlFromPosition(1, j).Text = "";
                        complementtable.GetControlFromPosition(2, j).Text = "";
                        complementtable.GetControlFromPosition(3, j).Text = "";
                    }
                    j++;
                }
            }
            else { }
            complementtable.ResumeLayout();
        }

        private void removebutton_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                NpgsqlCommand cmd;
                cmd = new NpgsqlCommand("update courses set lastusedyear = "+DateTime.Now.Year+" where coursesubject = '"+coursesubject.Text+"' and coursenumber = '"+coursenumber.Text+ "'", conn);
                cmd.ExecuteNonQuery();
                cmd = new NpgsqlCommand("update complementarycourses set lastusedyear = " + DateTime.Now.Year + " where coursesubject = '" + coursesubject.Text + "' and coursenumber = '" + coursenumber.Text + "'", conn);
                cmd.ExecuteNonQuery();

                conn.Close();
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }
        }

        private void wint2next_Click(object sender, EventArgs e)
        {
            year2wintertable.SuspendLayout();
            if (wint2list.Count > wint2page * 7)
            {
                int j = 1;
                wint2page++;
                for (int i = ((wint2page - 1) * 7) + 1; i <= (7 * wint2page); i++)
                {
                    if (wint2list.Count >= i)
                    {
                        year2wintertable.GetControlFromPosition(0, j).Text = wint2list[i - 1].Subject;
                        year2wintertable.GetControlFromPosition(1, j).Text = wint2list[i - 1].Number;
                        year2wintertable.GetControlFromPosition(2, j).Text = wint2list[i - 1].Name;
                        year2wintertable.GetControlFromPosition(3, j).Text = wint2list[i - 1].Credits;
                    }
                    else
                    {
                        year2wintertable.GetControlFromPosition(0, j).Text = "";
                        year2wintertable.GetControlFromPosition(1, j).Text = "";
                        year2wintertable.GetControlFromPosition(2, j).Text = "";
                        year2wintertable.GetControlFromPosition(3, j).Text = "";
                    }
                    j++;
                }
            }
            else { }
            year2wintertable.ResumeLayout();
        }

        private void fall3prev_Click(object sender, EventArgs e)
        {
            year3falltable.SuspendLayout();
            if (fall3page > 1)
            {
                int j = 1;
                fall3page--;
                for (int i = ((fall3page - 1) * 7) + 1; i <= (7 * fall3page); i++)
                {
                    if (fall3list.Count >= i)
                    {
                        year3falltable.GetControlFromPosition(0, j).Text = fall3list[i - 1].Subject;
                        year3falltable.GetControlFromPosition(1, j).Text = fall3list[i - 1].Number;
                        year3falltable.GetControlFromPosition(2, j).Text = fall3list[i - 1].Name;
                        year3falltable.GetControlFromPosition(3, j).Text = fall3list[i - 1].Credits;
                    }
                    else
                    {
                        year3falltable.GetControlFromPosition(0, j).Text = "";
                        year3falltable.GetControlFromPosition(1, j).Text = "";
                        year3falltable.GetControlFromPosition(2, j).Text = "";
                        year3falltable.GetControlFromPosition(3, j).Text = "";
                    }
                    j++;
                }
            }
            else { }
            year3falltable.ResumeLayout();
        }

        private void fall3next_Click(object sender, EventArgs e)
        {
            year3falltable.SuspendLayout();
            if (fall3list.Count > (fall3page * 7))
            {
                int j = 1;
                fall3page++;
                for (int i = ((fall3page - 1) * 7) + 1; i <= (7 * fall3page); i++)
                {
                    if (fall3list.Count >= i)
                    {
                        year3falltable.GetControlFromPosition(0, j).Text = fall3list[i - 1].Subject;
                        year3falltable.GetControlFromPosition(1, j).Text = fall3list[i - 1].Number;
                        year3falltable.GetControlFromPosition(2, j).Text = fall3list[i - 1].Name;
                        year3falltable.GetControlFromPosition(3, j).Text = fall3list[i - 1].Credits;
                    }
                    else
                    {
                        year3falltable.GetControlFromPosition(0, j).Text = "";
                        year3falltable.GetControlFromPosition(1, j).Text = "";
                        year3falltable.GetControlFromPosition(2, j).Text = "";
                        year3falltable.GetControlFromPosition(3, j).Text = "";
                    }
                    j++;
                }
            }
            else { }
            year3falltable.ResumeLayout();
        }

        private void wint3prev_Click(object sender, EventArgs e)
        {
            year3wintertable.SuspendLayout();
            if (wint3page > 1)
            {
                int j = 1;
                wint3page--;
                for (int i = ((wint3page - 1) * 7) + 1; i <= (7 * wint3page); i++)
                {
                    if (wint3list.Count >= i)
                    {
                        year3wintertable.GetControlFromPosition(0, j).Text = wint3list[i - 1].Subject;
                        year3wintertable.GetControlFromPosition(1, j).Text = wint3list[i - 1].Number;
                        year3wintertable.GetControlFromPosition(2, j).Text = wint3list[i - 1].Name;
                        year3wintertable.GetControlFromPosition(3, j).Text = wint3list[i - 1].Credits;
                    }
                    else
                    {
                        year3wintertable.GetControlFromPosition(0, j).Text = "";
                        year3wintertable.GetControlFromPosition(1, j).Text = "";
                        year3wintertable.GetControlFromPosition(2, j).Text = "";
                        year3wintertable.GetControlFromPosition(3, j).Text = "";
                    }
                    j++;
                }
            }
            else { }
            year3wintertable.ResumeLayout();
        }

        private void wint3next_Click(object sender, EventArgs e)
        {
            year3wintertable.SuspendLayout();
            if (wint3list.Count > wint3page * 7)
            {
                int j = 1;
                wint3page++;
                for (int i = ((wint3page - 1) * 7) + 1; i <= (7 * wint3page); i++)
                {
                    if (wint3list.Count >= i)
                    {
                        year3wintertable.GetControlFromPosition(0, j).Text = wint3list[i - 1].Subject;
                        year3wintertable.GetControlFromPosition(1, j).Text = wint3list[i - 1].Number;
                        year3wintertable.GetControlFromPosition(2, j).Text = wint3list[i - 1].Name;
                        year3wintertable.GetControlFromPosition(3, j).Text = wint3list[i - 1].Credits;
                    }
                    else
                    {
                        year3wintertable.GetControlFromPosition(0, j).Text = "";
                        year3wintertable.GetControlFromPosition(1, j).Text = "";
                        year3wintertable.GetControlFromPosition(2, j).Text = "";
                        year3wintertable.GetControlFromPosition(3, j).Text = "";
                    }
                    j++;
                }
            }
            else { }
            year3wintertable.ResumeLayout();
        }

        private void fall4prev_Click(object sender, EventArgs e)
        {
            year4falltable.SuspendLayout();
            if (fall4page > 1)
            {
                int j = 1;
                fall4page--;
                for (int i = ((fall4page - 1) * 7) + 1; i <= (7 * fall4page); i++)
                {
                    if (fall4list.Count >= i)
                    {
                        year4falltable.GetControlFromPosition(0, j).Text = fall4list[i - 1].Subject;
                        year4falltable.GetControlFromPosition(1, j).Text = fall4list[i - 1].Number;
                        year4falltable.GetControlFromPosition(2, j).Text = fall4list[i - 1].Name;
                        year4falltable.GetControlFromPosition(3, j).Text = fall4list[i - 1].Credits;
                    }
                    else
                    {
                        year4falltable.GetControlFromPosition(0, j).Text = "";
                        year4falltable.GetControlFromPosition(1, j).Text = "";
                        year4falltable.GetControlFromPosition(2, j).Text = "";
                        year4falltable.GetControlFromPosition(3, j).Text = "";
                    }
                    j++;
                }
            }
            else { }
            year4falltable.ResumeLayout();
        }

        private void fall4next_Click(object sender, EventArgs e)
        {
            year4falltable.SuspendLayout();
            if (fall4list.Count > (fall4page * 7))
            {
                int j = 1;
                fall4page++;
                for (int i = ((fall4page - 1) * 7) + 1; i <= (7 * fall4page); i++)
                {
                    if (fall4list.Count >= i)
                    {
                        year4falltable.GetControlFromPosition(0, j).Text = fall4list[i - 1].Subject;
                        year4falltable.GetControlFromPosition(1, j).Text = fall4list[i - 1].Number;
                        year4falltable.GetControlFromPosition(2, j).Text = fall4list[i - 1].Name;
                        year4falltable.GetControlFromPosition(3, j).Text = fall4list[i - 1].Credits;
                    }
                    else
                    {
                        year4falltable.GetControlFromPosition(0, j).Text = "";
                        year4falltable.GetControlFromPosition(1, j).Text = "";
                        year4falltable.GetControlFromPosition(2, j).Text = "";
                        year4falltable.GetControlFromPosition(3, j).Text = "";
                    }
                    j++;
                }
            }
            else { }
            year4falltable.ResumeLayout();
        }

        private void wint4prev_Click(object sender, EventArgs e)
        {
            year4wintertable.SuspendLayout();
            if (wint4page > 1)
            {
                int j = 1;
                wint4page--;
                for (int i = ((wint4page - 1) * 7) + 1; i <= (7 * wint4page); i++)
                {
                    if (wint4list.Count >= i)
                    {
                        year4wintertable.GetControlFromPosition(0, j).Text = wint4list[i - 1].Subject;
                        year4wintertable.GetControlFromPosition(1, j).Text = wint4list[i - 1].Number;
                        year4wintertable.GetControlFromPosition(2, j).Text = wint4list[i - 1].Name;
                        year4wintertable.GetControlFromPosition(3, j).Text = wint4list[i - 1].Credits;
                    }
                    else
                    {
                        year4wintertable.GetControlFromPosition(0, j).Text = "";
                        year4wintertable.GetControlFromPosition(1, j).Text = "";
                        year4wintertable.GetControlFromPosition(2, j).Text = "";
                        year4wintertable.GetControlFromPosition(3, j).Text = "";
                    }
                    j++;
                }
            }
            else { }
            year4wintertable.ResumeLayout();
        }

        private void wint4next_Click(object sender, EventArgs e)
        {
            year4wintertable.SuspendLayout();
            if (wint4list.Count > wint4page * 7)
            {
                int j = 1;
                wint4page++;
                for (int i = ((wint4page - 1) * 7) + 1; i <= (7 * wint4page); i++)
                {
                    if (wint4list.Count >= i)
                    {
                        year4wintertable.GetControlFromPosition(0, j).Text = wint4list[i - 1].Subject;
                        year4wintertable.GetControlFromPosition(1, j).Text = wint4list[i - 1].Number;
                        year4wintertable.GetControlFromPosition(2, j).Text = wint4list[i - 1].Name;
                        year4wintertable.GetControlFromPosition(3, j).Text = wint4list[i - 1].Credits;
                    }
                    else
                    {
                        year4wintertable.GetControlFromPosition(0, j).Text = "";
                        year4wintertable.GetControlFromPosition(1, j).Text = "";
                        year4wintertable.GetControlFromPosition(2, j).Text = "";
                        year4wintertable.GetControlFromPosition(3, j).Text = "";
                    }
                    j++;
                }
            }
            else { }
            year4wintertable.ResumeLayout();
        }

        public void loadtablefall2()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                NpgsqlCommand cmd;
                NpgsqlDataReader reader;
                //Fall year2
                cmd = new NpgsqlCommand("select coursesubject,coursenumber,coursename,credits FROM courses WHERE coursesection = 'Fall' AND yearlevel = 2 AND lastusedyear IS NULL", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    fall2list.Add(new viewtabletinfo { Subject = reader[0].ToString(), Number = reader[1].ToString(), Name = reader[2].ToString(), Credits = reader[3].ToString() });
                }
                cmd.Cancel();
                reader.Close();
                for (int i = 1; i <= 7; i++)
                {
                    if (fall2list.Count >= i)
                    {
                        SetControlPropertyThreadSafe(year2falltable.GetControlFromPosition(0, i), "Text", fall2list[i - 1].Subject);
                        SetControlPropertyThreadSafe(year2falltable.GetControlFromPosition(1, i), "Text", fall2list[i - 1].Number);
                        SetControlPropertyThreadSafe(year2falltable.GetControlFromPosition(2, i), "Text", fall2list[i - 1].Name);
                        SetControlPropertyThreadSafe(year2falltable.GetControlFromPosition(3, i), "Text", fall2list[i - 1].Credits);
                    }
                    else
                    {
                        SetControlPropertyThreadSafe(year2falltable.GetControlFromPosition(0, i), "Text", "");
                        SetControlPropertyThreadSafe(year2falltable.GetControlFromPosition(1, i), "Text", "");
                        SetControlPropertyThreadSafe(year2falltable.GetControlFromPosition(2, i), "Text", "");
                        SetControlPropertyThreadSafe(year2falltable.GetControlFromPosition(3, i), "Text", "");
                    }
                }

                conn.Close();
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }


            fallfin2 = true;
        }
        public void loadtablewinter2()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                NpgsqlCommand cmd;
                NpgsqlDataReader reader;
                //Winter year2
                cmd = new NpgsqlCommand("select coursesubject,coursenumber,coursename,credits FROM courses WHERE coursesection = 'Winter' AND yearlevel = 2 AND lastusedyear IS NULL", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    wint2list.Add(new viewtabletinfo { Subject = reader[0].ToString(), Number = reader[1].ToString(), Name = reader[2].ToString(), Credits = reader[3].ToString() });
                }
                cmd.Cancel();
                reader.Close();
                for (int i = 1; i <= 7; i++)
                {
                    if (wint2list.Count >= i)
                    {
                        SetControlPropertyThreadSafe(year2wintertable.GetControlFromPosition(0, i), "Text", wint2list[i - 1].Subject);
                        SetControlPropertyThreadSafe(year2wintertable.GetControlFromPosition(1, i), "Text", wint2list[i - 1].Number);
                        SetControlPropertyThreadSafe(year2wintertable.GetControlFromPosition(2, i), "Text", wint2list[i - 1].Name);
                        SetControlPropertyThreadSafe(year2wintertable.GetControlFromPosition(3, i), "Text", wint2list[i - 1].Credits);

                    }
                    else
                    {
                        SetControlPropertyThreadSafe(year2wintertable.GetControlFromPosition(0, i), "Text", "");
                        SetControlPropertyThreadSafe(year2wintertable.GetControlFromPosition(1, i), "Text", "");
                        SetControlPropertyThreadSafe(year2wintertable.GetControlFromPosition(2, i), "Text", "");
                        SetControlPropertyThreadSafe(year2wintertable.GetControlFromPosition(3, i), "Text", "");
                    }
                }
                conn.Close();
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }

            wintfin2 = true;
        }
        public void loadtablefall3()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                NpgsqlCommand cmd;
                NpgsqlDataReader reader;
                //Fall year3
                cmd = new NpgsqlCommand("select coursesubject,coursenumber,coursename,credits FROM courses WHERE coursesection = 'Fall' AND yearlevel = 3 AND lastusedyear IS NULL", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    fall3list.Add(new viewtabletinfo { Subject = reader[0].ToString(), Number = reader[1].ToString(), Name = reader[2].ToString(), Credits = reader[3].ToString() });
                }
                cmd.Cancel();
                reader.Close();
                for (int i = 1; i <= 7; i++)
                {
                    if (fall3list.Count >= i)
                    {
                        SetControlPropertyThreadSafe(year3falltable.GetControlFromPosition(0, i), "Text", fall3list[i - 1].Subject);
                        SetControlPropertyThreadSafe(year3falltable.GetControlFromPosition(1, i), "Text", fall3list[i - 1].Number);
                        SetControlPropertyThreadSafe(year3falltable.GetControlFromPosition(2, i), "Text", fall3list[i - 1].Name);
                        SetControlPropertyThreadSafe(year3falltable.GetControlFromPosition(3, i), "Text", fall3list[i - 1].Credits);
                    }
                    else
                    {
                        SetControlPropertyThreadSafe(year3falltable.GetControlFromPosition(0, i), "Text", "");
                        SetControlPropertyThreadSafe(year3falltable.GetControlFromPosition(1, i), "Text", "");
                        SetControlPropertyThreadSafe(year3falltable.GetControlFromPosition(2, i), "Text", "");
                        SetControlPropertyThreadSafe(year3falltable.GetControlFromPosition(3, i), "Text", "");
                    }
                }

                conn.Close();
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }

            fallfin3 = true;

        }
        public void loadtablewinter3()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                NpgsqlCommand cmd;
                NpgsqlDataReader reader;
                //Winter year3
                cmd = new NpgsqlCommand("select coursesubject,coursenumber,coursename,credits FROM courses WHERE coursesection = 'Winter' AND yearlevel = 3 AND lastusedyear IS NULL", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    wint3list.Add(new viewtabletinfo { Subject = reader[0].ToString(), Number = reader[1].ToString(), Name = reader[2].ToString(), Credits = reader[3].ToString() });
                }
                cmd.Cancel();
                reader.Close();
                for (int i = 1; i <= 7; i++)
                {
                    if (wint3list.Count >= i)
                    {
                        SetControlPropertyThreadSafe(year3wintertable.GetControlFromPosition(0, i), "Text", wint3list[i - 1].Subject);
                        SetControlPropertyThreadSafe(year3wintertable.GetControlFromPosition(1, i), "Text", wint3list[i - 1].Number);
                        SetControlPropertyThreadSafe(year3wintertable.GetControlFromPosition(2, i), "Text", wint3list[i - 1].Name);
                        SetControlPropertyThreadSafe(year3wintertable.GetControlFromPosition(3, i), "Text", wint3list[i - 1].Credits);

                    }
                    else
                    {
                        SetControlPropertyThreadSafe(year3wintertable.GetControlFromPosition(0, i), "Text", "");
                        SetControlPropertyThreadSafe(year3wintertable.GetControlFromPosition(1, i), "Text", "");
                        SetControlPropertyThreadSafe(year3wintertable.GetControlFromPosition(2, i), "Text", "");
                        SetControlPropertyThreadSafe(year3wintertable.GetControlFromPosition(3, i), "Text", "");
                    }
                }

                conn.Close();
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }

            wintfin3 = true;

        }
        public void loadtablefall4()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                NpgsqlCommand cmd;
                NpgsqlDataReader reader;
                //Fall year4
                cmd = new NpgsqlCommand("select coursesubject,coursenumber,coursename,credits FROM courses WHERE coursesection = 'Fall' AND yearlevel = 4 AND lastusedyear IS NULL", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    fall4list.Add(new viewtabletinfo { Subject = reader[0].ToString(), Number = reader[1].ToString(), Name = reader[2].ToString(), Credits = reader[3].ToString() });
                }
                cmd.Cancel();
                reader.Close();
                for (int i = 1; i <= 7; i++)
                {
                    if (fall4list.Count >= i)
                    {
                        SetControlPropertyThreadSafe(year4falltable.GetControlFromPosition(0, i), "Text", fall4list[i - 1].Subject);
                        SetControlPropertyThreadSafe(year4falltable.GetControlFromPosition(1, i), "Text", fall4list[i - 1].Number);
                        SetControlPropertyThreadSafe(year4falltable.GetControlFromPosition(2, i), "Text", fall4list[i - 1].Name);
                        SetControlPropertyThreadSafe(year4falltable.GetControlFromPosition(3, i), "Text", fall4list[i - 1].Credits);
                    }
                    else
                    {
                        SetControlPropertyThreadSafe(year4falltable.GetControlFromPosition(0, i), "Text", "");
                        SetControlPropertyThreadSafe(year4falltable.GetControlFromPosition(1, i), "Text", "");
                        SetControlPropertyThreadSafe(year4falltable.GetControlFromPosition(2, i), "Text", "");
                        SetControlPropertyThreadSafe(year4falltable.GetControlFromPosition(3, i), "Text", "");
                    }
                }

                conn.Close();
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }

            fallfin4 = true;

        }
        public void loadtablewinter4()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                NpgsqlCommand cmd;
                NpgsqlDataReader reader;
                //Winter year4
                cmd = new NpgsqlCommand("select coursesubject,coursenumber,coursename,credits FROM courses WHERE coursesection = 'Winter' AND yearlevel = 4 AND lastusedyear IS NULL", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    wint4list.Add(new viewtabletinfo { Subject = reader[0].ToString(), Number = reader[1].ToString(), Name = reader[2].ToString(), Credits = reader[3].ToString() });
                }
                cmd.Cancel();
                reader.Close();
                for (int i = 1; i <= 7; i++)
                {
                    if (wint4list.Count >= i)
                    {
                        SetControlPropertyThreadSafe(year4wintertable.GetControlFromPosition(0, i), "Text", wint4list[i - 1].Subject);
                        SetControlPropertyThreadSafe(year4wintertable.GetControlFromPosition(1, i), "Text", wint4list[i - 1].Number);
                        SetControlPropertyThreadSafe(year4wintertable.GetControlFromPosition(2, i), "Text", wint4list[i - 1].Name);
                        SetControlPropertyThreadSafe(year4wintertable.GetControlFromPosition(3, i), "Text", wint4list[i - 1].Credits);

                    }
                    else
                    {
                        SetControlPropertyThreadSafe(year4wintertable.GetControlFromPosition(0, i), "Text", "");
                        SetControlPropertyThreadSafe(year4wintertable.GetControlFromPosition(1, i), "Text", "");
                        SetControlPropertyThreadSafe(year4wintertable.GetControlFromPosition(2, i), "Text", "");
                        SetControlPropertyThreadSafe(year4wintertable.GetControlFromPosition(3, i), "Text", "");
                    }
                }
                conn.Close();
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }

            wintfin4 = true;

        }
        private delegate void SetControlPropertyThreadSafeDelegate(Control control,string propertyName, object propertyValue);
        public static void SetControlPropertyThreadSafe( Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate (SetControlPropertyThreadSafe),new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(propertyName,BindingFlags.SetProperty, null, control, new object[] { propertyValue });
            }
        }
   
    }
 }
