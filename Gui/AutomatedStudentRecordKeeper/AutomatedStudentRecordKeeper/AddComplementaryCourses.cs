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
    public partial class AddComplementaryCourses : Form
    {
        public AddComplementaryCourses()
        {
            InitializeComponent();
        }

        private void browsebutton_Click(object sender, EventArgs e)
        {

        }

        private void submitfile_Click(object sender, EventArgs e)
        {
         
        }

        private void submittable_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=Localhost; Port=5432; Database=studentrecordkeeper; User Id=postgres; Password=;");
            //connect to database
            conn.Open();
            NpgsqlCommand cmd;
            
        }
    }
}
