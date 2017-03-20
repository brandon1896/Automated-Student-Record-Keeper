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
using Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices.ComTypes;

namespace AutomatedStudentRecordKeeper
{
    public partial class AddCourse : Form
    {
        string sSelectedFile;
        NpgsqlCommand cmd = new NpgsqlCommand();

        public AddCourse()
        {
            InitializeComponent();
            int tempyear = DateTime.Now.Year;
            for (int i = 6; i >= -1; i--)
            {
                this.yeardropbox.Items.Add((tempyear - i).ToString() + "/" + (tempyear - i + 1).ToString());
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            AddCourseTable.Hide();
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                if (Yearleveldropbox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a Year level");

                }
                else if (sectiondropbox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a section");
                }
                else if (yeardropbox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a Year");
                }
                else
                {
                    int count = 0;
                    for (int j = 0; j < this.AddCourseTable.RowCount; j++)
                    {
                        if (string.IsNullOrWhiteSpace(AddCourseTable.GetControlFromPosition(0, j).Text) ||
                            string.IsNullOrWhiteSpace(AddCourseTable.GetControlFromPosition(1, j).Text) ||
                            string.IsNullOrWhiteSpace(AddCourseTable.GetControlFromPosition(2, j).Text) ||
                            string.IsNullOrWhiteSpace(AddCourseTable.GetControlFromPosition(3, j).Text))
                        {

                        }
                        else if (AddCourseTable.GetControlFromPosition(0, j).Text.Length != 4 || AddCourseTable.GetControlFromPosition(1, j).Text.Length != 4)
                        {

                        }
                        else
                        {
                            NpgsqlDataReader reader;
                            NpgsqlCommand cmd;
                            string checkifexists = "False";
                            cmd = new NpgsqlCommand("select exists(select true from courses where coursesubject = :sub and coursenumber = :num  and lastusedyear is null)", conn);
                            cmd.Parameters.Add(new NpgsqlParameter("sub", AddCourseTable.GetControlFromPosition(0, j).Text));
                            cmd.Parameters.Add(new NpgsqlParameter("num", AddCourseTable.GetControlFromPosition(1, j).Text));
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                checkifexists = reader[0].ToString();
                            }
                            cmd.Cancel();
                            reader.Close();
                            if (checkifexists == "False")
                            {
                                cmd = new NpgsqlCommand("insert into courses values(:sub, :num, :sec, :name, :cred, :yrlvl, :yrsec, :entyear)", conn);
                                cmd.Parameters.Add(new NpgsqlParameter("sub", AddCourseTable.GetControlFromPosition(0, j).Text));
                                cmd.Parameters.Add(new NpgsqlParameter("num", AddCourseTable.GetControlFromPosition(1, j).Text));
                                cmd.Parameters.Add(new NpgsqlParameter("sec", sectiondropbox.Text));
                                cmd.Parameters.Add(new NpgsqlParameter("name", AddCourseTable.GetControlFromPosition(2, j).Text));
                                cmd.Parameters.Add(new NpgsqlParameter("cred", double.Parse(AddCourseTable.GetControlFromPosition(3, j).Text)));
                                cmd.Parameters.Add(new NpgsqlParameter("yrlvl", int.Parse(Yearleveldropbox.Text)));
                                cmd.Parameters.Add(new NpgsqlParameter("yrsec", yeardropbox.Text));
                                cmd.Parameters.Add(new NpgsqlParameter("entyear", int.Parse(yeardropbox.Text.Substring(0, 4))));
                                cmd.ExecuteNonQuery();

                            }
                            AddCourseTable.GetControlFromPosition(0, j).Text = "";
                            AddCourseTable.GetControlFromPosition(1, j).Text = "";
                            AddCourseTable.GetControlFromPosition(2, j).Text = "";
                            AddCourseTable.GetControlFromPosition(3, j).Text = "";
                            count++;
                        }
                    }
                    conn.Close();
                    MessageBox.Show(count.ToString() + " rows added to table, check formating if form not cleared");
                }
            }
            else
            {
                MessageBox.Show("Connection error to database");
            }
            AddCourseTable.Show();
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

        private void richTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void import_courses_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                OpenFileDialog choofdlog = new OpenFileDialog(); //opens file viewer
                choofdlog.Filter = "doc files (*.docx*)|*.docx|(*.doc*)|*.doc"; //only shows doc files
                choofdlog.Title = "Select a Document File";
                choofdlog.FilterIndex = 1;
                choofdlog.Multiselect = false; //one file at a time

                if (choofdlog.ShowDialog() == DialogResult.OK)
                {
                    sSelectedFile = choofdlog.FileName; //sets path

                    //work in progress.... almost done i think..
                    Debug.Write("START");
                    List<string> data = new List<string>();
                    Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                    Document document = word.Documents.Open(sSelectedFile, ReadOnly: true);

                    foreach (Paragraph objParagraph in document.Paragraphs)
                        data.Add(objParagraph.Range.Text.Trim());

                    string totaltext = string.Join(string.Empty, data.ToArray());

                    ((_Document)document).Close();
                    ((_Application)word).Quit();

                    totaltext = totaltext.Substring(totaltext.IndexOf("YEAR")); //removes irrelevant info at beginning of doc file
                    totaltext = Regex.Replace(totaltext, @"\a", string.Empty);
                    totaltext = Regex.Replace(totaltext, @"(Lab)\r|(Lec)\r", string.Empty);
                    totaltext = Regex.Replace(totaltext, @"\d\.\d", string.Empty);
                    totaltext = Regex.Replace(totaltext, @"\b\d{1}\s|\b\d{2}\s", Environment.NewLine);
                    totaltext = Regex.Replace(totaltext, @"YEAR|FALL|WINTER", string.Empty, RegexOptions.Singleline);
                    totaltext = Regex.Replace(totaltext, @"SECOND|THIRD|FOURTH", string.Empty);
                    totaltext = Regex.Replace(totaltext, @"(Total).*?(Hours)", string.Empty);
                    totaltext = Regex.Replace(totaltext, @"(Sociology 2755).*?\n", string.Empty);
                    totaltext = Regex.Replace(totaltext, Environment.NewLine, string.Empty);
                    totaltext = Regex.Replace(totaltext, @"\r+", "\n");
                    totaltext = Regex.Replace(totaltext, @"(?<=([\d]{4}))\s+", "~", RegexOptions.Singleline);
                    totaltext = Regex.Replace(totaltext, @"(One half course from Science Elective Course List).*?\n", string.Empty, RegexOptions.Singleline);
                    totaltext = Regex.Replace(totaltext, @"(One half course from Engineering Elective Course List).*?\n", string.Empty, RegexOptions.Singleline);
                    totaltext = Regex.Replace(totaltext, @"(One complementary).*?\n", string.Empty, RegexOptions.Singleline);

                    string[] year = totaltext.Split(new string[] { " TERM " }, StringSplitOptions.RemoveEmptyEntries);

                    for (int j = 0; j < year.Length; j++)
                    {
                        year[j] = Regex.Replace(year[j], @"(?<=([A-Z]{4}))\s+", "~");
                        year[j] = Regex.Replace(year[j], @"(- ).*?(Year\n)", string.Empty);

                        var lines = year[j].Split('\n')
                            .Where(s => !string.IsNullOrWhiteSpace(s));

                        year[j] = string.Join("\n", lines);

                        var courseNumber = Regex.Matches(year[j], @"[A-Z]{4}.*?\n", RegexOptions.Singleline);
                        var courseNumberList = courseNumber.Cast<Match>().Select(match => match.Value).ToList();
                        /*
                        for (int i = 0; i < courseNumberList.Count; i++)
                        {
                                courseNumberList[i] += "~0.5~" + j + DateTime.Now.Year;
                                courseNumberList[i] = Regex.Replace(courseNumberList[i], @"[\n\r]+", string.Empty);
                        }
                        */

                        System.IO.File.WriteAllText(@"C:\Users\Public\COURSESyear" + j + ".txt", year[j]);

                        cmd = new NpgsqlCommand("COPY courses (coursesubject, coursenumber, coursename) FROM "
                            + @"'C:\Users\Public\COURSESyear" + j + ".txt' DELIMITER '~' CSV", conn);

                        //cmd = new NpgsqlCommand("COPY courses (coursesubject, coursenumber, coursename, credits, yearLevel, firstusedyear) FROM "
                        //    + @"'C:\Users\Public\COURSESyear" + j + ".txt' DELIMITER '~' CSV", conn);
                        cmd.ExecuteNonQuery();

                    }
                    Debug.Write("FINISH");
                    //System.IO.File.WriteAllLines(@"C:\Users\Shawn\Documents\COURSES.txt", courseNumberList);
                    //System.IO.File.WriteAllText(@"C:\Users\Shawn\Documents\COURSES.txt", html); //writes to text file
                }
                else
                    sSelectedFile = string.Empty;
            }
        }
    }
}
