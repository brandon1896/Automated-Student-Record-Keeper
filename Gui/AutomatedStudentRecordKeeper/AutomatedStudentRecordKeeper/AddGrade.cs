﻿using System;
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
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace AutomatedStudentRecordKeeper
{
    public partial class AddGrade : Form
    {
        string sSelectedFile;
        NpgsqlCommand cmd = new NpgsqlCommand();

        public AddGrade()
        {
            InitializeComponent();
            int tempyear = DateTime.Now.Year;
            for (int i = 6; i >= 0; i--)
            {
                this.yeardropbox.Items.Add((tempyear - i).ToString() + "/" + (tempyear - i + 1).ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CourseTable.Hide();
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                if (StudentNumber.Text.Length != 7)
                {
                    MessageBox.Show("Please enter valid student number");
                }
                else if (yeardropbox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a year");
                }
                else
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select exists (select true from pg_tables where tablename = '" + StudentNumber.Text + "')", conn);
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
                        MessageBox.Show("Student Doesnt exist");
                    }
                    else
                    {
                        int count = 0;
                        for (int j = 0; j < this.CourseTable.RowCount; j++)
                        {
                            if (string.IsNullOrWhiteSpace(CourseTable.GetControlFromPosition(0, j).Text) ||
                                string.IsNullOrWhiteSpace(CourseTable.GetControlFromPosition(1, j).Text) ||
                                string.IsNullOrWhiteSpace(CourseTable.GetControlFromPosition(2, j).Text))
                            {

                            }
                            else if (CourseTable.GetControlFromPosition(0, j).Text.Length != 4 || CourseTable.GetControlFromPosition(1, j).Text.Length != 4
                                || int.Parse(CourseTable.GetControlFromPosition(2, j).Text) < 0 || int.Parse(CourseTable.GetControlFromPosition(2, j).Text) > 100)
                            {

                            }
                            else
                            {

                                cmd = new NpgsqlCommand("insert into \"" + StudentNumber.Text + "\"(coursesubject,coursenumber,coursegrade,yearsection, year) values(:sub, :num, :grade, :yrsec , :yr)", conn);
                                cmd.Parameters.Add(new NpgsqlParameter("sub", CourseTable.GetControlFromPosition(0, j).Text));
                                cmd.Parameters.Add(new NpgsqlParameter("num", CourseTable.GetControlFromPosition(1, j).Text));
                                cmd.Parameters.Add(new NpgsqlParameter("grade", int.Parse(CourseTable.GetControlFromPosition(2, j).Text)));
                                cmd.Parameters.Add(new NpgsqlParameter("yrsec", yeardropbox.Text));
                                cmd.Parameters.Add(new NpgsqlParameter("yr", int.Parse(yeardropbox.Text.Substring(0, 4))));
                                cmd.ExecuteNonQuery();
                                CourseTable.GetControlFromPosition(0, j).Text = "";
                                CourseTable.GetControlFromPosition(1, j).Text = "";
                                CourseTable.GetControlFromPosition(2, j).Text = "";
                                count++;
                            }
                        }
                        conn.Close();
                        MessageBox.Show(count.ToString() + " rows added, if not cleared check formating");
                    }
                }
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }
            CourseTable.Show();
            }

        private void yeardropbox_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void importHTML_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                OpenFileDialog choofdlog = new OpenFileDialog(); //opens file viewer
                choofdlog.Filter = "html Files (*.html*)|*.html"; //only shows html files
                choofdlog.Title = "Select an HTML File";
                choofdlog.FilterIndex = 1;
                choofdlog.Multiselect = false; //one file at a time

                if (choofdlog.ShowDialog() == DialogResult.OK)
                {
                    sSelectedFile = choofdlog.FileName; //sets path
                    string html = System.IO.File.ReadAllText(sSelectedFile); //reads from path

                    //add a check to make sure html is valid file
                    //for example... if it starts with lakehead? regex.match

                    //clean file here//
                    string result = Regex.Replace(html, @" ?\<.*?\>", string.Empty); //removes everything between <>
                    result = result.Substring(result.IndexOf("Name:")); //removes irrelevant info at beginning of html file
                    result = result.Substring(0, result.LastIndexOf("Term Average:")); //removes irrelevant info at bottom of html file
                    result = Regex.Replace(result, @"(Degree/CCD:).*?(?<=END OF)", string.Empty, RegexOptions.Singleline);
                    result = Regex.Replace(result, @"INTERNAL USE ONLY", string.Empty); //this line causes issues with courseCodeList values because of ONLY
                    result = Regex.Replace(result, "[-]", "\n");
                    result = Regex.Replace(result, @"(MATH Average:)|(ENGI Average:)", string.Empty, RegexOptions.Singleline);

                    result = Regex.Replace(result, @"(Spring/Summer:).*?(?<=CreditGradeRep)", string.Empty, RegexOptions.Singleline);

                    result = Regex.Replace(result, @"(name:)\s+", "Name: ", RegexOptions.IgnoreCase); //puts student name on same line
                    result = Regex.Replace(result, @"(student number:)\s+", "Student Number: ", RegexOptions.IgnoreCase); //puts student number on same line
                    result = Regex.Replace(result, @"(year level:)\s+", "Year Level: ", RegexOptions.IgnoreCase); //puts year level on same line
                    string[] students = result.Split(new string[] { "Name:" }, StringSplitOptions.RemoveEmptyEntries);

                    //grab student names
                    var studentName = Regex.Matches(result, @"(?<=Name: ).*?(?<=\n)", RegexOptions.Singleline); //extracts student name
                    var studentNameList = studentName.Cast<Match>().Select(match => match.Value).ToList();

                    //grabs year level
                    var yearLevel = Regex.Matches(result, @"(?<=Year Level: ).*?(?<=\n)", RegexOptions.Singleline);
                    var yearLevelList = yearLevel.Cast<Match>().Select(match => match.Value).ToList();

                    for (int j = 0; j < students.Length; j++)
                    {
                        string[] files = string.Join(string.Empty, students[j]).Split(new string[] { "Fall/Winter" }, StringSplitOptions.RemoveEmptyEntries);

                        var studentNumber = Regex.Matches(students[j], @"(?<=Student Number: )\d+"); //extracts student number --needs tweaking--                       
                        var studentNumberList = studentNumber.Cast<Match>().Select(match => match.Value).ToList();

                        //drops table if exists
                        try
                        {
                            cmd = new NpgsqlCommand("drop table \"" + studentNumberList[0] + "\"", conn);
                            cmd.ExecuteNonQuery();
                        }
                        catch
                        {
                        }

                        //recreates table
                        cmd = new NpgsqlCommand("create table \"" + studentNumberList[0] + "\"(coursesubject text, coursenumber text, coursename text, coursegrade integer, yearsection text, year integer)", conn);
                        cmd.ExecuteNonQuery();

                        // to add student to students table as well
                        cmd = new NpgsqlCommand("insert into student (name, studentid, yearlevel, yearsection, year) select :name, :num, :yrlvl, :yrsec, :entyear " +
                            "WHERE NOT EXISTS (SELECT studentid FROM student WHERE studentid = '" + studentNumberList[0] + "') LIMIT 1; ", conn);
                        cmd.Parameters.Add(new NpgsqlParameter("name", studentNameList[j]));
                        cmd.Parameters.Add(new NpgsqlParameter("num", studentNumberList[0]));
                        cmd.Parameters.Add(new NpgsqlParameter("yrlvl", int.Parse(yearLevelList[j])));
                        cmd.Parameters.Add(new NpgsqlParameter("yrsec", (DateTime.Now.Year - (files.Length - 1)).ToString() + "/" + (DateTime.Now.Year - (files.Length - 2)).ToString()));
                        cmd.Parameters.Add(new NpgsqlParameter("entyear", DateTime.Now.Year - (files.Length - 1)));
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < files.Length; i++) //doesnt seem to be going through all files (possibly fixed)
                        {

                            Debug.Write("Number of Students: j: " + j);

                            {
                                //trying to grab all uppercase 4 letter words, seems to work as planned --132 is the magic number
                                var courseCode = Regex.Matches(files[i], @"\s[A-Z]{4}\s");
                                var courseCodeList = courseCode.Cast<Match>().Select(match => match.Value).ToList();

                                //trying to grab all 4 digit numbers, seems to work as planned --132 is the magic number
                                var courseNumber = Regex.Matches(files[i], @"\s[0-9]{4}\s");
                                var courseNumberList = courseNumber.Cast<Match>().Select(match => match.Value).ToList();

                                //trying to grab all grades, -- 132 is the magic number
                                var studentGrade = Regex.Matches(files[i], @"\s[0-9]{2,3}\s|\s(IP)\s");
                                var studentGradeList = studentGrade.Cast<Match>().Select(match => match.Value).ToList();

                                int count = courseCodeList.Count;

                                for (int n = 0; n < count; n++) //need the list size
                                {
                                    string cSubject = courseCodeList[n];
                                    cSubject = Regex.Replace(cSubject, @"\s", string.Empty);
                                    string cNumber = courseNumberList[n];
                                    cNumber = Regex.Replace(cNumber, @"\s", string.Empty);

                                    int sGrade = 0;
                                    int.TryParse(studentGradeList[n], out sGrade); //trys to convert list values to integers

                                    Debug.Write("Attempting to write to DB");

                                    //write to dB
                                    cmd = new NpgsqlCommand("insert into \"" + studentNumberList[0] + "\"(coursesubject,coursenumber,coursegrade, yearsection, year) values(:sub, :num, :grade, :yrsec, :yr)", conn);
                                    cmd.Parameters.Add(new NpgsqlParameter("sub", cSubject));
                                    cmd.Parameters.Add(new NpgsqlParameter("num", cNumber));
                                    if (sGrade == 0)
                                        cmd.Parameters.Add(new NpgsqlParameter("grade", DBNull.Value));
                                    else
                                        cmd.Parameters.Add(new NpgsqlParameter("grade", sGrade));

                                    cmd.Parameters.Add(new NpgsqlParameter("yrsec", (DateTime.Now.Year - ((files.Length - i))).ToString() + "/" + (DateTime.Now.Year - (files.Length - i) + 1).ToString()));
                                    cmd.Parameters.Add(new NpgsqlParameter("yr", DateTime.Now.Year - (files.Length - i)));

                                    cmd.ExecuteNonQuery();

                                }
                            }
                        }
                    }
                    MessageBox.Show("All students Added Succesfully. Import Complete.");
                }
                else
                    sSelectedFile = string.Empty;
            }
            conn.Close();
        }

        private void richTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (Char.IsDigit(ch))
            {
                e.Handled = true;
            }
            else
            {
                e.KeyChar = Char.ToUpper(ch);
            }
        }

        private void richTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }
    }
    
}

