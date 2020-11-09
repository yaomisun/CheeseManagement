using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheeseManagement.DataType
{
    class CompanyUserPW
    {
        private string mCompany;
        public string company
        {
            get { return mCompany; } set { mCompany = value; }
        }
        private string mUser;
        public string companyUser
        {
            get { return mUser; }
            set { mUser = value; }
        }
        private string mPW;
        public string companyUserPW
        {
            get { return mPW; }
            set { mPW = value; }
        }

        public Dictionary<string, List<Tuple<string, string>>> alluserinfo= new Dictionary<string, List<Tuple<string, string>>>();
       
        public CompanyUserPW(string curCompany)
        {
            InitCompanyUser(curCompany);
            InitInfo();
        }

        private void InitInfo()
        {
            
        }

        private void InitCompanyUser(string curCompany)
        {
            List<Tuple<string, string>> tempholder = new List<Tuple<string, string>>();
                    tempholder.Add(Tuple.Create(curCompany+"_User1", "User1"));
                    tempholder.Add(Tuple.Create(curCompany + "_User2", "User2"));
                    alluserinfo.Add(curCompany, tempholder);

            }

    }
}
