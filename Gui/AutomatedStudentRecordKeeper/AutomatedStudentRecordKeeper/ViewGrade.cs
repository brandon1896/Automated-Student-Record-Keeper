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
                        Loadtable1();
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
        public void Loadtable1()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            { 
                NpgsqlCommand cmd;
                NpgsqlDataReader reader;
                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \""+ StudentBox.Text + "\"as n,(select coursesubject, coursenumber ,firstusedyear ,lastusedyear,credits from courses where coursenumber::int<3000 union (select distinct m.coursesubject, m.coursenumber ,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '"+ StudentBox.Text + "') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=n.year and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc) as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum",int.Parse(date1.Text.Substring(0,4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   table1.GetControlFromPosition(0,0).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \""+ StudentBox.Text +"\" as n,(select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from courses where coursenumber::int >=3000 union select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from complementarycourses) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=n.year and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc)as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date1.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table1.GetControlFromPosition(0,1).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select avg(final.coursegrade * final.credits *2) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \""+ StudentBox.Text +"\" as n,(select coursesubject, coursenumber ,firstusedyear ,lastusedyear,credits from courses where coursenumber::int<3000 union (select distinct m.coursesubject, m.coursenumber ,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '"+ StudentBox.Text +"') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year = :yearnum and(c.firstusedyear<=n.year and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc) as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date1.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table1.GetControlFromPosition(1,0).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select avg(final.coursegrade * final.credits *2) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \""+StudentBox.Text+"\" as n,(select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from courses where coursenumber::int >=3000 union select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from complementarycourses) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year = :yearnum and(c.firstusedyear<=n.year and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc)as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date1.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table1.GetControlFromPosition(1, 1).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select avg(final.coursegrade * final.credits *2) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \""+StudentBox.Text+"\" as n,(select coursesubject, coursenumber ,firstusedyear ,lastusedyear,credits from courses where coursenumber::int<3000 union (select distinct m.coursesubject, m.coursenumber ,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '"+StudentBox.Text+"') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=n.year and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc) as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date1.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table1.GetControlFromPosition(2, 0).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select avg(final.coursegrade * final.credits *2) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \""+StudentBox.Text+"\" as n,(select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from courses where coursenumber::int >=3000 union select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from complementarycourses) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=n.year and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc)as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date1.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table1.GetControlFromPosition(2, 1).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \""+StudentBox.Text+"\" as n,(select coursesubject, coursenumber ,firstusedyear ,lastusedyear,credits from courses where coursenumber::int<3000 union (select distinct m.coursesubject, m.coursenumber ,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '"+StudentBox.Text+"') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=n.year and n.coursegrade>=50 and n.coursegrade<60 and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc) as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date1.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table1.GetControlFromPosition(3, 0).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \""+StudentBox.Text+"\" as n,(select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from courses where coursenumber::int >=3000 union select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from complementarycourses) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and n.coursegrade >= 50 and n.coursegrade < 60 and(c.firstusedyear<=n.year and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc)as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date1.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table1.GetControlFromPosition(3, 1).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,(select coursesubject, coursenumber ,firstusedyear ,lastusedyear,credits from courses where coursenumber::int<3000 union (select distinct m.coursesubject, m.coursenumber ,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '" + StudentBox.Text + "') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=n.year and n.coursegrade<50 and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc) as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date1.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table1.GetControlFromPosition(4, 0).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select count(*) from (select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \"" + StudentBox.Text + "\" as n,(select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from courses where coursenumber::int >=3000 union select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from complementarycourses) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and n.coursegrade < 50 and(c.firstusedyear<=n.year and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc)as final", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date1.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table1.GetControlFromPosition(4, 1).Text = reader[0].ToString();
                }
                cmd.Cancel();
                reader.Close();

                cmd = new NpgsqlCommand("select distinct on (final.coursesubject,final.coursenumber) avg(final.coursegrade * final.credits * 2) from ((select distinct on (n.coursesubject,n.coursenumber) n.coursesubject,n.coursenumber,n.coursegrade ,c.credits from \""+StudentBox.Text+"\" as n,(select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from courses union select coursesubject, coursenumber ,firstusedyear,lastusedyear ,credits from complementarycourses union (select distinct m.coursesubject, m.coursenumber ,s.year,null::int,m.credits from makeupcourses as m,student as s where m.studentid = '"+StudentBox.Text+"') ) as c where c.coursesubject=n.coursesubject and c.coursenumber = n.coursenumber and n.year<= :yearnum and(c.firstusedyear<=n.year and (c.lastusedyear>=n.year or c.lastusedyear is null)) order by n.coursesubject, n.coursenumber, n.year desc ) union select coursesubject,coursenumber,coursegrade,0.5 from \""+StudentBox.Text+"\" where year <= :yearnum)as final group by final.coursesubject, final.coursenumber,final.credits order by final.coursesubject,final.coursenumber,final.credits desc", conn);
                cmd.Parameters.Add(new NpgsqlParameter("yearnum", int.Parse(date1.Text.Substring(0, 4))));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SCA1.Text = reader[0].ToString().Substring(0,4);
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
        public void Loadtable2()
        {

        }
        public void Loadtable3()
        {

        }
        public void Loadtable4()
        {

        }
        public void Loadtable5()
        {

        }
        public void Loadtable6()
        {

        }
    }
  
}

