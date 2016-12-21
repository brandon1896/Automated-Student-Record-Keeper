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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void enterbutton_Click(object sender, EventArgs e)
        {
            if (userbox.Text == "" && userbox.Text == "")
            {
                MessageBox.Show("Please Enter a Username and Password");
            }
            else if (userbox.Text == "")
            {
                MessageBox.Show("Please Enter a Username");
            }
            else if (passbox.Text == "")
            {
                MessageBox.Show("Please Enter a Password");
            }
            else
            {
                MessageBox.Show("Login Successful");
                this.Close();
            }
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
