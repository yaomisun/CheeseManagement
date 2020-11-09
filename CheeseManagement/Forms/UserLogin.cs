using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CheeseManagement.DataType;
namespace CheeseManagement.Forms
{
    public partial class UserLogin : Form
    {

        public string CurCompany;
        public string CurUser;
        public UserLogin()
        {
            InitializeComponent();
            SetDefault();
        }

        private void SetDefault()
        {
            txtUser.Text = "RustyDargon_User1";
            txpwt.Text = "User1";
        }
        private void btnLog_Click(object sender, EventArgs e)
        {
            Company CompanySource = new Company();
            List<Tuple<string, string>> tempholder = new List<Tuple<string, string>>();
            foreach (string curcom in  CompanySource.alluserinfo.Keys)
            {
                tempholder = CompanySource.alluserinfo[curcom];
                Tuple<string, string> matchValue=tempholder.Find(x=>x.Item1== this.txtUser.Text&&x.Item2==this.txpwt.Text);
                if(matchValue ==null)
                {
                    var result=MessageBox.Show("The Login provided is not found. Retry?", "Login Window",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(result==DialogResult.No)
                    {
                        this.Close();
                    }
                }
                else
                {
                    string chooseuser = matchValue.Item1;
                    string curcompany = chooseuser.Substring(0, chooseuser.IndexOf("_"));
                    MainBrowser mainPanel = new MainBrowser();
                    mainPanel.Show();
                    this.Hide();
                    break;
                }

            }
        }
        
    }
}
