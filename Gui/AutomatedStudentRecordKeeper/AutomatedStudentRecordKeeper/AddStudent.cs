using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomatedStudentRecordKeeper
{
    public partial class AddStudent : Form
    {
        public AddStudent()
        {
            InitializeComponent();
        }

        private void enterbutton_Click(object sender, EventArgs e)
        {
            if (fnamebox.Text == "" && lnamebox.Text == "" && snumberbox.Text == "")
            {
                MessageBox.Show("Please Enter First Name, Last Name, and Student Number");
            }
            else if(fnamebox.Text == "")
            {
                MessageBox.Show("Please Enter First Name");
            }
            else if (lnamebox.Text == "")
            {
                MessageBox.Show("Please Enter Last Name");
            }
            else if (snumberbox.Text == "")
            {
                MessageBox.Show("Please Enter Student Number");
            }
            else
            {
                MessageBox.Show("Student Added");
                this.Close();
            }
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
