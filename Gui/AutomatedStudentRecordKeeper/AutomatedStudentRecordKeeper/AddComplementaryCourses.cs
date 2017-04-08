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
    public partial class AddComplementaryCourses : Form
    {
        string selectedInputFile;
        NpgsqlCommand cmd = new NpgsqlCommand();

        public AddComplementaryCourses()
        {
            InitializeComponent();
            int tempyear = DateTime.Now.Year;
            for (int i = 6; i >= -1; i--)
            {
                this.yeardropbox.Items.Add((tempyear - i).ToString() + "/" + (tempyear - i + 1).ToString());
            }
        }

        private void submittable_Click_1(object sender, EventArgs e)
        {
            CourseTable.Hide();
            int count = 0;
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {

                if (yeardropbox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a Year");
                }
                else if(listbox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a List");
                }
                else
                {
                    for (int j = 0; j < this.CourseTable.RowCount; j++)
                    {
                        if (string.IsNullOrWhiteSpace(CourseTable.GetControlFromPosition(0, j).Text) ||
                            string.IsNullOrWhiteSpace(CourseTable.GetControlFromPosition(1, j).Text) ||
                            string.IsNullOrWhiteSpace(CourseTable.GetControlFromPosition(2, j).Text) ||
                            string.IsNullOrWhiteSpace(CourseTable.GetControlFromPosition(3, j).Text))
                        {

                        }
                        else if (CourseTable.GetControlFromPosition(0, j).Text.Length != 4 || CourseTable.GetControlFromPosition(1, j).Text.Length != 4)
                        {

                        }
                        else
                        {
                            NpgsqlDataReader reader;
                            NpgsqlCommand cmd;
                            string checkifexists = "False";
                            cmd = new NpgsqlCommand("select exists(select true from courses where coursesubject =  :sub and coursenumber = :num and yearsection = :year)", conn);
                            cmd.Parameters.Add(new NpgsqlParameter("sub", CourseTable.GetControlFromPosition(0, j).Text));
                            cmd.Parameters.Add(new NpgsqlParameter("num", CourseTable.GetControlFromPosition(1, j).Text));
                            cmd.Parameters.Add(new NpgsqlParameter("year", yeardropbox.Text));
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                checkifexists = reader[0].ToString();
                            }
                            cmd.Cancel();
                            reader.Close();
                            if (checkifexists == "False")
                            {
                                string checktype = "";
                                if(listbox.Text == "A")
                                {
                                    checktype = "compa";
                                }
                                else
                                {
                                    checktype = "compb";
                                }
                                cmd = new NpgsqlCommand("insert into courses values(:sub, :num, NULL ,:name, :cred, NULL,:yearsec,:entyear,:type)", conn);
                                cmd.Parameters.Add(new NpgsqlParameter("sub", CourseTable.GetControlFromPosition(0, j).Text));
                                cmd.Parameters.Add(new NpgsqlParameter("num", CourseTable.GetControlFromPosition(1, j).Text));
                                cmd.Parameters.Add(new NpgsqlParameter("name", CourseTable.GetControlFromPosition(2, j).Text));
                                cmd.Parameters.Add(new NpgsqlParameter("cred", double.Parse(CourseTable.GetControlFromPosition(3, j).Text)));
                                cmd.Parameters.Add(new NpgsqlParameter("yearsec", yeardropbox.Text));
                                cmd.Parameters.Add(new NpgsqlParameter("entyear", int.Parse(yeardropbox.Text.Substring(0, 4))));
                                cmd.Parameters.Add(new NpgsqlParameter("type", checktype));
                                try
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                catch(NpgsqlException ex)
                                {

                                }
                            }
                            CourseTable.GetControlFromPosition(0, j).Text = "";
                            CourseTable.GetControlFromPosition(1, j).Text = "";
                            CourseTable.GetControlFromPosition(2, j).Text = "";
                            CourseTable.GetControlFromPosition(3, j).Text = "";
                            count++;
                        }
                    }
                    conn.Close();
                    MessageBox.Show(count.ToString() + " rows added to table, check formating if any field not cleared");
                }
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }
            CourseTable.Show();
        }

        private void richTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }


        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void importCSV_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                if (yeardropbox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a Year");
                }
                else
                {
                    OpenFileDialog choofdlog = new OpenFileDialog(); //opens file viewer
                    choofdlog.Filter = "csv Files (*.csv*)|*.csv"; //only shows html files
                    choofdlog.Title = "Select a CSV File";
                    choofdlog.FilterIndex = 1;
                    choofdlog.Multiselect = false; //one file at a time

                    if (choofdlog.ShowDialog() == DialogResult.OK)
                    {
                        selectedInputFile = choofdlog.FileName; //sets path
                        string html = System.IO.File.ReadAllText(selectedInputFile); //reads from path

                        //clean file here//
                        //Change names to coursecode
                        html = Regex.Replace(html, @"\s(engineering)(?=(\s[0-9]{4}))", "ENGI", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"\s(english)(?=(\s[0-9]{4}))", "ENGL", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(sociology)(?=(\s[0-9]{4}))", "SOCI", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(economics)(?=(\s[0-9]{4}))", "ECON", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(anthropology)(?=(\s[0-9]{4}))", "ANTH", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(criminology)(?=(\s[0-9]{4}))", "CRIM", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(geography)(?=(\s[0-9]{4}))", "GEOG", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(history)(?=(\s[0-9]{4}))", "HIST", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(indigenous learning)(?=(\s[0-9]{4}))", "INDI", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(finnish)(?=(\s[0-9]{4}))", "FINN", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(french)(?=(\s[0-9]{4}))", "FREN", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(italian)(?=(\s[0-9]{4}))", "ITAL", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(linguistics)(?=(\s[0-9]{4}))", "LING", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(music)(?=(\s[0-9]{4}))", "MUSI", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(philosophy)(?=(\s[0-9]{4}))", "PHIL", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(political science)(?=(\s[0-9]{4}))", "POLI", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(psychology)(?=(\s[0-9]{4}))", "PSYC", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(visual arts)(?=(\s[0-9]{4}))", "VISU", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(women's studies)(?=(\s[0-9]{4}))", "WOME", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(business)(?=(\s[0-9]{4}))", "BUSI", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(german)(?=(\s[0-9]{4}))", "GERM", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(language)(?=(\s[0-9]{4}))", "LANG", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(ojibwe)(?=(\s[0-9]{4}))", "OJIB", RegexOptions.IgnoreCase);
                        html = Regex.Replace(html, @"(spanish)(?=(\s[0-9]{4}))", "SPAN", RegexOptions.IgnoreCase);


                        html = Regex.Replace(html, @"\(Prerequisite:(.*?)\""", "\n");

                        html = Regex.Replace(html, @"\(.*?\w{2}\)", string.Empty, RegexOptions.Multiline); //removes most info in brackets

                        html = Regex.Replace(html, @"(?<=([A-Z]{4}))\s", "~"); //setting up delimiter
                        html = Regex.Replace(html, @"\b-\s", " - "); //setting up delimiter
                        html = Regex.Replace(html, @"\s-\s", "~"); //setting up delimiter
                        html = Regex.Replace(html, @"(?<=\d):\s", "~"); //setting up delimiter
                        html = Regex.Replace(html, @"\""�", string.Empty); //removes bullets
                        html = Regex.Replace(html, @"\s�\s", "~"); //setting up delimiter
                        html = Regex.Replace(html, @"OR", "\n"); //rare case of two courses on same line
                        html = Regex.Replace(html, @"\(credit weight 1.0\)|\(credit weight 1.0 \)", "~1.0"); //setting up delimiter
                        html = Regex.Replace(html, @"\(.*?\n", "\n");
                        html = Regex.Replace(html, @"\""", string.Empty); //removes remaining quotations

                        //creating list to prepare data for entering into dB
                        var courseCode = Regex.Matches(html, @"[A-Z]{4}~[0-9]{4}.*?[\n|\r]");
                        var courseCodeList = courseCode.Cast<Match>().Select(match => match.Value).ToList();

                        for (int i = 0; i < courseCodeList.Count; i++)
                        {
                            if (!courseCodeList[i].Contains("1.0"))
                            {
                                courseCodeList[i] += "~0.5~" + int.Parse(yeardropbox.Text.Substring(0, 4));
                                courseCodeList[i] = Regex.Replace(courseCodeList[i], @"[\n\r]+", string.Empty);
                            }
                            else
                            {
                                courseCodeList[i] += "~" + int.Parse(yeardropbox.Text.Substring(0, 4));
                                courseCodeList[i] = Regex.Replace(courseCodeList[i], @"[\n\r]+", string.Empty);
                            }
                        }

                        //creates file for bulk insert data into dB
                        System.IO.File.WriteAllLines(@"C:\Users\Public\csvtestCOURSE.txt", courseCodeList); //writes to text file

                        //-------------------------- temp fix ------------------------------
                        //drops table if exists
                        try
                        {
                            cmd = new NpgsqlCommand("drop table complementarycourses", conn);
                            cmd.ExecuteNonQuery();
                        }
                        catch
                        {
                            Debug.Write("Drop Table Failed, Table already Exists");
                        }

                        //recreates table
                        cmd = new NpgsqlCommand("CREATE TABLE complementarycourses( coursesubject text, coursenumber text, coursename text, credits double precision, firstusedyear integer, lastusedyear integer)", conn);
                        cmd.ExecuteNonQuery();

                        //bulk insert into dB
                        try
                        {
                            cmd = new NpgsqlCommand("COPY complementarycourses (coursesubject, coursenumber, coursename, credits, firstusedyear) FROM "
                            + @"'C:\Users\Public\csvtestCOURSE.txt' DELIMITER '~' CSV", conn);
                            cmd.ExecuteNonQuery();
                        }
                        catch
                        {
                            MessageBox.Show("Error Adding Complementary Courses to DataBase");
                            this.Close();
                        }

                        //MessageBox.Show("Complementary Courses Import Successful");

                        var result = MessageBox.Show("Importing of Complementary courses to the database was successful. Would you like to import another file?", "SUCCESS",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                            this.Activate();
                        if (result == DialogResult.No)
                            this.Close();

                    }
                    else
                        selectedInputFile = string.Empty;
                }
            }
        }

        private void AddComplementaryCourses_Load(object sender, EventArgs e)
        {

        }

        private void yeardropbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}