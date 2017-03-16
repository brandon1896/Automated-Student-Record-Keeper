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
        int tablepage = 1;
        public ViewGrade()
        {
            InitializeComponent();
            Fulltable1.Hide();
            Fulltable2.Hide();
            Fulltable3.Hide();
            Fulltable4.Hide();
            Fulltable5.Hide();
            Fulltable6.Hide();
            tableviewtable.Hide();


        }
        private void Calculate_Click(object sender, EventArgs e)
        {
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
                        Fulltable1.Hide();
                        Fulltable2.Hide();
                        Fulltable3.Hide();
                        Fulltable4.Hide();
                        Fulltable5.Hide();
                        Fulltable6.Hide();

                        MessageBox.Show(years.Count.ToString());

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
                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\"as n,(select coursesubject, coursenumber ,firstusedyear ,lastusedyear,credits from courses where coursenumber::int<3000 union (select distinct m.coursesubject, m.coursenumber ,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '" + StudentBox.Text + "') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=n.year and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc) as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(0, 0).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,(select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from courses where coursenumber::int >=3000 union select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from complementarycourses) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=n.year and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc)as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(0, 1).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select (sum(final.coursegrade * final.credits)/sum(final.credits))::int from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,(select coursesubject, coursenumber ,firstusedyear ,lastusedyear,credits from courses where coursenumber::int<3000 union (select distinct m.coursesubject, m.coursenumber ,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '" + StudentBox.Text + "') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year = :yearnum and(c.firstusedyear<=n.year and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc) as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(1, 0).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select (sum(final.coursegrade * final.credits)/sum(final.credits))::int from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,(select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from courses where coursenumber::int >=3000 union select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from complementarycourses) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year = :yearnum and(c.firstusedyear<=n.year and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc)as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(1, 1).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select (sum(final.coursegrade * final.credits)/sum(final.credits))::int from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,(select coursesubject, coursenumber ,firstusedyear ,lastusedyear,credits from courses where coursenumber::int<3000 union (select distinct m.coursesubject, m.coursenumber ,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '" + StudentBox.Text + "') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=n.year and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc) as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(2, 0).Text = (int.Parse(reader[0].ToString())).ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select (sum(final.coursegrade * final.credits)/sum(final.credits))::int from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,(select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from courses where coursenumber::int >=3000 union select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from complementarycourses) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=n.year and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc)as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(2, 1).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,(select coursesubject, coursenumber ,firstusedyear ,lastusedyear,credits from courses where coursenumber::int<3000 union (select distinct m.coursesubject, m.coursenumber ,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '" + StudentBox.Text + "') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=n.year and n.coursegrade>=50 and n.coursegrade<60 and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc) as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(3, 0).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,(select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from courses where coursenumber::int >=3000 union select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from complementarycourses) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and n.coursegrade >= 50 and n.coursegrade < 60 and(c.firstusedyear<=n.year and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc)as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(3, 1).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,(select coursesubject, coursenumber ,firstusedyear ,lastusedyear,credits from courses where coursenumber::int<3000 union (select distinct m.coursesubject, m.coursenumber ,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '" + StudentBox.Text + "') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=n.year and n.coursegrade<40 and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc) as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(4, 0).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,(select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from courses where coursenumber::int >=3000 union select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from complementarycourses) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and n.coursegrade < 40 and(c.firstusedyear<=n.year and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc)as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(4, 1).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,(select coursesubject, coursenumber ,firstusedyear ,lastusedyear,credits from courses where coursenumber::int<3000 union (select distinct m.coursesubject, m.coursenumber ,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '" + StudentBox.Text + "') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=n.year and n.coursegrade < 50 and n.coursegrade >= 40 and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc) as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(5, 0).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,(select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from courses where coursenumber::int >=3000 union select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from complementarycourses) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and n.coursegrade < 50 and n.coursegrade >= 40 and(c.firstusedyear<=n.year and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc)as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.GetControlFromPosition(5, 1).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select (sum(z.coursegrade * z.credits)/sum(z.credits))::int::int from (select distinct on (final.coursesubject,final.coursenumber) final.coursegrade , final.credits from ((select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,(select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from courses union select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from complementarycourses union (select distinct m.coursesubject, m.coursenumber ,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '" + StudentBox.Text + "') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year= :yearnum and(c.firstusedyear<=n.year and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc ) union select coursesubject,coursenumber,coursegrade,0.5 from \"" + StudentBox.Text + "\" where year = :yearnum )as final group by final.coursesubject, final.coursenumber,final.credits,final.coursegrade order by final.coursesubject,final.coursenumber,final.credits desc) as z", conn);
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
                cmd = new NpgsqlCommand("select * from (select distinct on (final.coursesubject,final.coursenumber) final.coursesubject,final.coursenumber,final.coursename,final.coursegrade , final.credits from ((select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,c.coursename,n.coursegrade ,c.credits from \""+studentnumber+"\" as n,(select coursesubject, coursenumber ,coursename,firstusedyear,lastusedyear ,credits from courses union select coursesubject, coursenumber,coursename ,firstusedyear,lastusedyear ,credits from complementarycourses union (select distinct m.coursesubject, m.coursenumber ,m.coursename,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '"+studentnumber+"') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.yearsection = '"+yearsection+"' and(c.firstusedyear<=n.year and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc ) union select coursesubject,coursenumber,coursename,coursegrade,0.5 from \""+studentnumber+"\" where yearsection = '"+yearsection+"' )as final group by final.coursesubject, final.coursenumber,final.coursename,final.credits,final.coursegrade order by final.coursesubject,final.coursenumber,final.credits desc) as z", conn);
                reader = cmd.ExecuteReader();
                tableviewlist.Clear();
                tablepage = 1;
                while (reader.Read())
                {
                    tableviewlist.Add(new viewgradetableinfo { Subject = reader[0].ToString(), Number = reader[1].ToString(), Name = reader[2].ToString(),Grade = reader[3].ToString(), Credits = reader[4].ToString() });
                }
                cmd.Cancel();
                reader.Close();
                for (int i = 1; i <= 8; i++)
                {
                    if (tableviewlist.Count >= i)
                    {
                        tableviewtable.GetControlFromPosition(0, i).Text = tableviewlist[i-1].Subject;
                        tableviewtable.GetControlFromPosition(1, i).Text = tableviewlist[i-1].Number;
                        tableviewtable.GetControlFromPosition(2, i).Text = tableviewlist[i-1].Name;
                        tableviewtable.GetControlFromPosition(3, i).Text = tableviewlist[i-1].Grade;
                        tableviewtable.GetControlFromPosition(4, i).Text = tableviewlist[i-1].Credits;

                    }
                    else
                    {
                        tableviewtable.GetControlFromPosition(0, i).Text = " ";
                        tableviewtable.GetControlFromPosition(1, i).Text = " ";
                        tableviewtable.GetControlFromPosition(2, i).Text = " ";
                        tableviewtable.GetControlFromPosition(3, i).Text = " ";
                        tableviewtable.GetControlFromPosition(4, i).Text = " ";
                    }
                }

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
                tableviewtable.Hide();
                Loadtablebyyear(yearbox.Text);
                tableviewtable.Show();
            }
        }

        private void tabelviewprev_Click(object sender, EventArgs e)
        {
            if (tablepage > 1)
            {
                tablepage--;
                tableviewtable.Hide();
                int j = 1;
                for (int i = ((tablepage - 1) * 8) + 1; i <= (8 * tablepage); i++)
                {
                    if (tableviewlist.Count >= i)
                    {
                        tableviewtable.GetControlFromPosition(0, j).Text = tableviewlist[i - 1].Subject;
                        tableviewtable.GetControlFromPosition(1, j).Text = tableviewlist[i - 1].Number;
                        tableviewtable.GetControlFromPosition(2, j).Text = tableviewlist[i - 1].Name;
                        tableviewtable.GetControlFromPosition(3, j).Text = tableviewlist[i - 1].Grade;
                        tableviewtable.GetControlFromPosition(4, j).Text = tableviewlist[i - 1].Credits;

                    }
                    else
                    {
                        tableviewtable.GetControlFromPosition(0, j).Text = " ";
                        tableviewtable.GetControlFromPosition(1, j).Text = " ";
                        tableviewtable.GetControlFromPosition(2, j).Text = " ";
                        tableviewtable.GetControlFromPosition(3, j).Text = " ";
                        tableviewtable.GetControlFromPosition(4, j).Text = " ";
                    }

                    j++;
                }
                tableviewtable.Show();
            }
            else { }
        }

        private void tableviewnext_Click(object sender, EventArgs e)
        {
            if (tableviewlist.Count > (tablepage* 8))
            {
                tableviewtable.Hide();
                int j = 1;
                tablepage++;
                for (int i = ((tablepage - 1) * 8) + 1; i <= (8 * tablepage); i++)
                {
                    if (tableviewlist.Count >= i)
                    {
                        tableviewtable.GetControlFromPosition(0, j).Text = tableviewlist[i - 1].Subject;
                        tableviewtable.GetControlFromPosition(1, j).Text = tableviewlist[i - 1].Number;
                        tableviewtable.GetControlFromPosition(2, j).Text = tableviewlist[i - 1].Name;
                        tableviewtable.GetControlFromPosition(3, j).Text = tableviewlist[i - 1].Grade;
                        tableviewtable.GetControlFromPosition(4, j).Text = tableviewlist[i - 1].Credits;

                    }
                    else
                    {
                        tableviewtable.GetControlFromPosition(0, j).Text = " ";
                        tableviewtable.GetControlFromPosition(1, j).Text = " ";
                        tableviewtable.GetControlFromPosition(2, j).Text = " ";
                        tableviewtable.GetControlFromPosition(3, j).Text = " ";
                        tableviewtable.GetControlFromPosition(4, j).Text = " ";
                    }

                    j++;
                }
                tableviewtable.Show();
            }
            else { }
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

