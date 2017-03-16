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
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                cmd = new NpgsqlCommand("select exists (select true from pg_tables where tablename = '" + StudentNumber.Text + "')", conn);
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
                            cmd.Parameters.Add(new NpgsqlParameter("yr", int.Parse(yeardropbox.Text.Substring(0, 4))));
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
                    result = Regex.Replace(result, @"(Degree/CCD:).*?(?<=END OF)", string.Empty, RegexOptions.Singleline);
                    result = Regex.Replace(result, @"INTERNAL USE ONLY", string.Empty); //this line causes issues with courseCodeList values because of ONLY
                    result = Regex.Replace(result, "[-]", "\n");
                    result = Regex.Replace(result, @"(MATH Average:)|(ENGI Average:)", string.Empty, RegexOptions.Singleline);

                    result = Regex.Replace(result, @"(Spring/Summer:).*?(?<=CreditGradeRep)", string.Empty, RegexOptions.Singleline);

                    result = Regex.Replace(result, @"(name:)\s+", "Name: ", RegexOptions.IgnoreCase); //puts student name on same line
                    result = Regex.Replace(result, @"(student number:)\s+", "Student Number: ", RegexOptions.IgnoreCase); //puts student number on same line
                    string[] students = result.Split(new string[] { "Name:" }, StringSplitOptions.RemoveEmptyEntries);

                    for (int j = 0; j < students.Length; j++)
                    {
                        string[] files = string.Join(string.Empty, students[j]).Split(new string[] { "Fall/Winter" }, StringSplitOptions.RemoveEmptyEntries);

                        var studentNumber = Regex.Matches(students[j], @"(?<=Student Number: )\d+"); //extracts student number --needs tweaking--                       
                        var studentNumberList = studentNumber.Cast<Match>().Select(match => match.Value).ToList();

                        //drops table
                        //seems to be working now
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

                        //Seems to double the time... consider other options...
                        //var studentName = Regex.Matches(students[j], @".*?(Student Number:)", RegexOptions.Singleline); //extracts student name
                        //var studentNameList = studentName.Cast<Match>().Select(match => match.Value).ToList();
                        // -- still has "Student Number:" in it??? Whyyyyyy... FIX IT
                        // -- NEED TO ADD STUDENT TO STUDENT TABLE!!!
                        //System.IO.File.AppendAllLines(@"C:\Users\Shawn\Documents\studentNames.txt", studentNameList); //writes to text file

                        //trying to grab year level -- only one year level on transcript --NOT WORKINGNGNGNSKGNDSKJGNSDKGNDS
                        //var yearLevel = Regex.Matches(students[j], @"(?<=Year Level: )\s\d\s", RegexOptions.Singleline);
                        //var yearLevelList = yearLevel.Cast<Match>().Select(match => match.Value).ToList();
                        //System.IO.File.AppendAllLines(@"C:\Users\Shawn\Documents\yearLevel.txt", yearLevelList); //writes to text file

                        Debug.Write("Files Length: " + files.Length);

                        for (int i = 0; i < files.Length; i++) //doesnt seem to be going through all files (possibly fixed)
                        {

                            Debug.Write("Number of Students: j: " + j);

                            {
                                //probably not needed --> most likely not needed
                                /*
                                cmd = new NpgsqlCommand("select exists (select true from pg_tables where tablename = '" + studentNumberList + "')", conn); //studentNumberList[0] when re-adding create table
                                string checknum = "f";
                                NpgsqlDataReader reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    checknum = reader[0].ToString();
                                }
                                cmd.Cancel();
                                reader.Close();
                                if (checknum == "False")
                                */
                                //{
                                //write to dB

                                /* -- this probably needs tweaking --
                                //trying to grab year level -- only one year level on transcript
                                var yearLevel = Regex.Matches(files[i], @"(?<=Year Level: )\s[0-9]\s", RegexOptions.Singleline);
                                var yearLevelList = yearLevel.Cast<Match>().Select(match => match.Value).ToList();

                                //trying to grab dates
                                var yearDate = Regex.Matches(files[i], @"(?<=Fall/Winter)\s\d+/\d+\s|(?<=Spring/Summer)\s\d+\s", RegexOptions.Singleline);
                                var yearDateList = yearLevel.Cast<Match>().Select(match => match.Value).ToList();
                                */

                                //trying to grab all uppercase 4 letter words, seems to work as planned --132 is the magic number
                                var courseCode = Regex.Matches(files[i], @"\s[A-Z]{4}\s");
                                var courseCodeList = courseCode.Cast<Match>().Select(match => match.Value).ToList();

                                //trying to grab all 4 digit numbers, seems to work as planned --132 is the magic number
                                var courseNumber = Regex.Matches(files[i], @"\s[0-9]{4}\s");
                                var courseNumberList = courseNumber.Cast<Match>().Select(match => match.Value).ToList();

                                //trying to grab all grades, -- 132 is the magic number
                                var studentGrade = Regex.Matches(files[i], @"\s[0-9]{2,3}\s|\s(IP)\s");
                                var studentGradeList = studentGrade.Cast<Match>().Select(match => match.Value).ToList();

                                //if (courseCodeList.Count == courseNumberList.Count && courseCodeList.Count == studentGradeList.Count)
                                int count = courseCodeList.Count;
                                Debug.Write("Number of Courses: " + count);

                                //System.IO.File.WriteAllText(@"C:\Users\Shawn\Documents\test" + i + ".txt", files[i]); //writes to text file
                                //string year = files[i]; //need to convert to date? spring and summer courses cause an issue here

                                Debug.Write("j" + j);

                                //consider bulk insert, may be quicker (though this executes quickly as is)
                                for (int n = 0; n < count; n++) //need the list size
                                {
                                    string cSubject = courseCodeList[n];
                                    cSubject = Regex.Replace(cSubject, @"\s", string.Empty);
                                    string cNumber = courseNumberList[n];
                                    cNumber = Regex.Replace(cNumber, @"\s", string.Empty);

                                    int sGrade = 0;
                                    int.TryParse(studentGradeList[n], out sGrade); //trys to convert list values to integers

                                    Debug.Write("Attempting to write to DB");

                                    //write to dB --this is questionable--
                                    cmd = new NpgsqlCommand("insert into \"" + studentNumberList[0] + "\"(coursesubject,coursenumber,coursegrade) values(:sub, :num, :grade)", conn);
                                    cmd.Parameters.Add(new NpgsqlParameter("sub", cSubject));
                                    cmd.Parameters.Add(new NpgsqlParameter("num", cNumber));
                                    if (sGrade == 0)
                                        cmd.Parameters.Add(new NpgsqlParameter("grade", DBNull.Value));
                                    else
                                        cmd.Parameters.Add(new NpgsqlParameter("grade", sGrade));
                                    cmd.Parameters.Add(new NpgsqlParameter("yrsec", DBNull.Value));
                                    cmd.Parameters.Add(new NpgsqlParameter("yr", DBNull.Value));
                                    cmd.ExecuteNonQuery(); //currently broken!!!!!!!!!!!!!!!

                                }
                                /*
                                //delete duplicates from table --might work? -- not working --syntax error
                                //wont need if we keep the drop and recreate table
                                //keep just in case for now
                                cmd = new NpgsqlCommand("delete from " + studentNumberList[0] +
                                    "where id in (select id " +
                                    "from(SELECT id, ROW_NUMBER() OVER(partition BY column1, column2," +
                                    " column3 ORDER BY id) AS rnum FROM " + studentNumberList[0] + ") t WHERE t.rnum > 1)", conn);
                                cmd.ExecuteNonQuery();
                                */
                                //}
                            }
                        }
                    }

                }
                else
                    sSelectedFile = string.Empty;
            }
        }

    }

}

