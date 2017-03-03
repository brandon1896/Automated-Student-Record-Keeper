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

namespace AutomatedStudentRecordKeeper
{
    public partial class AddGrade : Form
    {
        string sSelectedFile;
        //string sSelectedFolder;
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
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                NpgsqlCommand cmd = new NpgsqlCommand("select exists (select true from pg_tables where tablename = '" + StudentNumber.Text + "')", conn);
                string checknum = "f";
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
                    for (int j = 0; j < this.CourseTable.RowCount; j++)
                    {
                        if (string.IsNullOrWhiteSpace(CourseTable.GetControlFromPosition(0, j).Text) ||
                            string.IsNullOrWhiteSpace(CourseTable.GetControlFromPosition(1, j).Text) ||
                            string.IsNullOrWhiteSpace(CourseTable.GetControlFromPosition(2, j).Text))
                        {

                        }
                        else
                        {
                            cmd = new NpgsqlCommand("insert into \"" + StudentNumber.Text + "\"(coursesubject,coursenumber,coursegrade,yearsection, year) values(:sub, :num, :grade, :yrsec , :yr)", conn);
                            cmd.Parameters.Add(new NpgsqlParameter("sub", CourseTable.GetControlFromPosition(0, j).Text));
                            cmd.Parameters.Add(new NpgsqlParameter("num", CourseTable.GetControlFromPosition(1, j).Text));
                            cmd.Parameters.Add(new NpgsqlParameter("grade", int.Parse(CourseTable.GetControlFromPosition(2, j).Text)));
                            cmd.Parameters.Add(new NpgsqlParameter("yrsec", yeardropbox.Text));
                            cmd.Parameters.Add(new NpgsqlParameter("yr", int.Parse(yeardropbox.Text.Substring(0,4))));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }
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
                choofdlog.FilterIndex = 1;
                choofdlog.Multiselect = false; //one file at a time

                if (choofdlog.ShowDialog() == DialogResult.OK)
                {
                    sSelectedFile = choofdlog.FileName; //sets path
                    string html = System.IO.File.ReadAllText(sSelectedFile); //reads from path

                    //clean file here//
                    string result = Regex.Replace(html, @" ?\<.*?\>", string.Empty); //removes everything between <>

                    result = result.Substring(result.IndexOf("Name")); //removes irrelevant info at beginning of html file
                    result = result.Substring(0, result.LastIndexOf("Term Average:")); //removes irrelevant info at bottom of html file
                    result = Regex.Replace(result, @"(Term Standing:).*?(>)", string.Empty, RegexOptions.Singleline);
                    result = Regex.Replace(result, @"(Full Time).*?(>)", string.Empty, RegexOptions.Singleline);
                    result = Regex.Replace(result, @"(Degree/CCD:).*?(?<=END OF)", string.Empty, RegexOptions.Singleline);
                    result = Regex.Replace(result, @"(Printed on:).*?(\))", string.Empty, RegexOptions.Singleline);
                    //result = Regex.Replace(result, "[%|+]", string.Empty); //might not be needed
                    result = Regex.Replace(result, "[-]", "\n");
                    result = Regex.Replace(result, @"(MATH Average:)|(ENGI Average:)", string.Empty, RegexOptions.Singleline);

                    result = Regex.Replace(result, @"(name:)\s+", "Name: ", RegexOptions.IgnoreCase); //puts student name on same line
                    result = Regex.Replace(result, @"(student number:)\s+", "Student Number: ", RegexOptions.IgnoreCase); //puts student number on same line
                    string[] files = result.Split(new string[] { "ACADEMIC TRANSCRIPT" }, StringSplitOptions.RemoveEmptyEntries);
                    //splits into multiple files 

                    int i = 0;
                    while (i < files.Length)
                    {
                        var studentNumber = Regex.Matches(files[i], @"(?<=Student Number: )\d+"); //extracts student name --needs tweaking--
                        var studentNumberList = studentNumber.Cast<Match>().Select(match => match.Value).ToList();

                        NpgsqlCommand cmd = new NpgsqlCommand("select exists (select true from pg_tables where tablename = '" + studentNumberList + "')", conn);
                        string checknum = "f";
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
                            //write to dB
                            var studentName = Regex.Matches(files[i], @"(?<=Name: ).*?(\.)", RegexOptions.Singleline); //extracts student name --needs tweaking--
                            //this only works if every name ends with a period. Not sure how all students appear in file
                            var studentNameList = studentName.Cast<Match>().Select(match => match.Value).ToList();

                            //trying to grab all uppercase 4 letter words, seems to work as planned --132 is the magic number
                            var courseCode = Regex.Matches(files[i], @"\s[A-Z]{4}\s");
                            var courseCodeList = courseCode.Cast<Match>().Select(match => match.Value).ToList();

                            //trying to grab all 4 digit numbers, seems to work as planned --132 is the magic number
                            var courseNumber = Regex.Matches(files[i], @"\s[0-9]{4}\s");
                            var courseNumberList = courseNumber.Cast<Match>().Select(match => match.Value).ToList();

                            //some dates snuck through, 2014/2015, 2016/2017 possibly only in my half of the file? didnt seem to happen before
                            //possibly a non issue

                            //trying to grab all grades, -- 132 is the magic number
                            var studentGrade = Regex.Matches(files[i], @"\s[0-9]{2,3}\s|\s(IP)\s");
                            var studentGradeList = studentGrade.Cast<Match>().Select(match => match.Value).ToList();
                        }

                        //System.IO.File.WriteAllLines(@"C:\Users\Shawn\Documents\WriteLines" + i + ".txt", studentGradeList); //writes to text file
                        //System.Diagnostics.Process.Start(@"C:\Users\Shawn\Documents\WriteLines" + i + ".txt"); //opens file

                        i++;
                    }
                }
                else
                    sSelectedFile = string.Empty;
            }
        }

    }
    
}

