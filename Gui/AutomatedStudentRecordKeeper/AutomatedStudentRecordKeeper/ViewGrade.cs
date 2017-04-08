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
        string studentnumber="";
        bool dataloaded= false;
        List<viewgradetableinfo> tableviewlist = new List<viewgradetableinfo>();
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
            tableviewlist.Clear();
            yearbox.Items.Clear();
            dataloaded = false;
            List<string> years = new List<string>();
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                if (StudentBox.Text.Length != 7)
                {
                    MessageBox.Show("Please enter valid student number");
                }
                else
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select exists (select true from pg_tables where tablename = '" + StudentBox.Text + "')", conn);
                    string checknum = "False";
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        checknum = reader[0].ToString();
                    }
                    cmd.Cancel();
                    reader.Close();
                    if (checknum == "False")
                    {
                        MessageBox.Show("Student Doesn't exist");
                    }
                    else
                    {
                        waitScreen waitscrn = new waitScreen();
                        waitscrn.Show();
                        Application.DoEvents();
                        cmd = new NpgsqlCommand("select distinct yearsection from \"" + StudentBox.Text + "\" order by yearsection asc", conn);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            years.Add(reader[0].ToString());
                            yearbox.Items.Add(reader[0].ToString());
                        }
                        cmd.Cancel();
                        reader.Close();

                        Fulltable1.Hide();
                        Fulltable2.Hide();
                        Fulltable3.Hide();
                        Fulltable4.Hide();
                        Fulltable5.Hide();
                        Fulltable6.Hide();
                        
                        switch (years.Count)
                        {
                            case 1:
                                date1.Text = years[0];
                                Loadtable(table1, date1, SCA1);
                                Fulltable1.Show();
                                break;
                            case 2:
                                date1.Text = years[0];
                                date2.Text = years[1];
                                Loadtable(table1, date1, SCA1);
                                Loadtable(table2, date2, SCA2);
                                Fulltable1.Show();
                                Fulltable2.Show();
                                break;
                            case 3:
                                date1.Text = years[0];
                                date2.Text = years[1];
                                date3.Text = years[2];
                                Loadtable(table1, date1, SCA1);
                                Loadtable(table2, date2, SCA2);
                                Loadtable(table3, date3, SCA3);
                                Fulltable1.Show();
                                Fulltable2.Show();
                                Fulltable3.Show();
                                break;
                            case 4:
                                date1.Text = years[0];
                                date2.Text = years[1];
                                date3.Text = years[2];
                                date4.Text = years[3];
                                Loadtable(table1, date1, SCA1);
                                Loadtable(table2, date2, SCA2);
                                Loadtable(table3, date3, SCA3);
                                Loadtable(table4, date4, SCA4);
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
                                Loadtable(table1, date1, SCA1);
                                Loadtable(table2, date2, SCA2);
                                Loadtable(table3, date3, SCA3);
                                Loadtable(table4, date4, SCA4);
                                Loadtable(table5, date5, SCA5);
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
                                Loadtable(table1, date1, SCA1);
                                Loadtable(table2, date2, SCA2);
                                Loadtable(table3, date3, SCA3);
                                Loadtable(table4, date4, SCA4);
                                Loadtable(table5, date5, SCA5);
                                Loadtable(table6, date6, SCA6);
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

                        dataloaded = true;
                        studentnumber = StudentBox.Text;
                        conn.Close();
                        waitscrn.Hide();
                    }
                }
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }

        }
        public void Loadtable(TableLayoutPanel table, Control date, Control SCA)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                NpgsqlCommand cmd;
                NpgsqlDataReader reader;
                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\"as n,student as s,(select coursesubject, coursenumber ,firstusedyear ,lastusedyear,credits from courses where coursenumber::int<3000 union (select distinct m.coursesubject, m.coursenumber ,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '" + StudentBox.Text + "') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=s.year and (c.lastusedyear>=s.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc) as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(0, 0).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,student as s,(select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from courses where coursenumber::int >=3000 union select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from complementarycourses) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=s.year and (c.lastusedyear>=s.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc)as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(0, 1).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select (sum(final.coursegrade * final.credits)/sum(final.credits))::int from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,student as s,(select coursesubject, coursenumber ,firstusedyear ,lastusedyear,credits from courses where coursenumber::int<3000 union (select distinct m.coursesubject, m.coursenumber ,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '" + StudentBox.Text + "') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year = :yearnum and(c.firstusedyear<=s.year and (c.lastusedyear>=s.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc) as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(1, 0).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select (sum(final.coursegrade * final.credits)/sum(final.credits))::int from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,student as s,(select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from courses where coursenumber::int >=3000 union select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from complementarycourses) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year = :yearnum and(c.firstusedyear<=s.year and (c.lastusedyear>=s.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc)as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(1, 1).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select (sum(final.coursegrade * final.credits)/sum(final.credits))::int from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,student as s,(select coursesubject, coursenumber ,firstusedyear ,lastusedyear,credits from courses where coursenumber::int<3000 union (select distinct m.coursesubject, m.coursenumber ,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '" + StudentBox.Text + "') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=s.year and (c.lastusedyear>=s.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc) as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(2, 0).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select (sum(final.coursegrade * final.credits)/sum(final.credits))::int from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,student as s,(select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from courses where coursenumber::int >=3000 union select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from complementarycourses) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=s.year and (c.lastusedyear>=s.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc)as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(2, 1).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,student as s,(select coursesubject, coursenumber ,firstusedyear ,lastusedyear,credits from courses where coursenumber::int<3000 union (select distinct m.coursesubject, m.coursenumber ,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '" + StudentBox.Text + "') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=s.year and n.coursegrade>=50 and n.coursegrade<60 and (c.lastusedyear>=s.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc) as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(3, 0).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,student as s,(select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from courses where coursenumber::int >=3000 union select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from complementarycourses) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and n.coursegrade >= 50 and n.coursegrade < 60 and(c.firstusedyear<=s.year and (c.lastusedyear>=s.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc)as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(3, 1).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,student as s,(select coursesubject, coursenumber ,firstusedyear ,lastusedyear,credits from courses where coursenumber::int<3000 union (select distinct m.coursesubject, m.coursenumber ,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '" + StudentBox.Text + "') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=s.year and n.coursegrade<40 and (c.lastusedyear>=s.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc) as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(4, 0).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,student as s,(select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from courses where coursenumber::int >=3000 union select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from complementarycourses) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and n.coursegrade < 40 and(c.firstusedyear<=s.year and (c.lastusedyear>=s.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc)as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(4, 1).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,student as s,(select coursesubject, coursenumber ,firstusedyear ,lastusedyear,credits from courses where coursenumber::int<3000 union (select distinct m.coursesubject, m.coursenumber ,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '" + StudentBox.Text + "') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=s.year and n.coursegrade < 50 and n.coursegrade >= 40 and (c.lastusedyear>=s.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc) as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(5, 0).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,student as s,(select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from courses where coursenumber::int >=3000 union select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from complementarycourses) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and n.coursegrade < 50 and n.coursegrade >= 40 and(c.firstusedyear<=s.year and (c.lastusedyear>=s.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc)as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(5, 1).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select (sum(z.coursegrade * z.credits)/sum(z.credits))::int from (select distinct on (final.coursesubject,final.coursenumber) final.coursegrade , final.credits from ((select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,student as s,(select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from courses union select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from complementarycourses union (select distinct m.coursesubject, m.coursenumber ,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '" + StudentBox.Text + "') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year= :yearnum and(c.firstusedyear<=s.year and (c.lastusedyear>=s.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc ))as final group by final.coursesubject, final.coursenumber,final.credits,final.coursegrade order by final.coursesubject,final.coursenumber,final.credits desc) as z", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SCA.Text = reader[0].ToString();
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
        
        public void Loadtablebyyear(string yearsection)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {

                
                NpgsqlCommand cmd;
                NpgsqlDataReader reader;
                cmd = new NpgsqlCommand("select g.coursesubject, g.coursenumber, c.coursename, g.grade, c.credits from courses as c, grades as g where c.coursesubject = g.coursesubject and c.coursenumber = g.coursenumber and g.studentnumber = :snum and g.yeartaken = :year ", conn);
                cmd.Parameters.Add(new NpgsqlParameter("snum", studentnumber));
                cmd.Parameters.Add(new NpgsqlParameter("year", int.Parse(yearsection.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                tableviewlist.Clear();
                while (reader.Read())
                {
                    tableviewlist.Add(new viewgradetableinfo { Subject = reader[0].ToString(), Number = reader[1].ToString(), Name = reader[2].ToString(),Grade = reader[3].ToString(), Credits = reader[4].ToString() });
                }
                cmd.Cancel();
                reader.Close();
                gradetable.DataSource = tableviewlist;
                gradetable.Refresh();


                conn.Close();
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }

        }

        private void CourseTable_Paint(object sender, PaintEventArgs e)
        {

        }

        private void yearbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataloaded == true)
            {
                Loadtablebyyear(yearbox.Text);
            }
        }
        
        private void StudentBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }
    }
  
}

