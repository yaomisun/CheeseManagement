using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheeseManagement.DataType
{
    class Company
    {
        List<string> companies = new List<string>();

        public List<string> addressList = new List<string>();
        public CompanyUserPW curcompanyusers;
        public Dictionary<string, List<Tuple<string, string>>> alluserinfo= new Dictionary<string, List<Tuple<string, string>>>();
        public Company()
        {
            RegCompany();
            addressList.Add("");
            InitCompanyUserPW();
        }

        private void InitCompanyUserPW()
        {
            foreach (string curcompany in companies)
            {
                InitCompanyUser(curcompany);
            }
            
        }

        //all registered copanies, currently only have mother company
        private void RegCompany()
        {
            companies.Add("RustyDargon");
        }

        private void InitCompanyUser(string curCompany)
        {
            List<Tuple<string, string>> tempholder = new List<Tuple<string, string>>();
            tempholder.Add(Tuple.Create(curCompany + "_User1", "User1"));
            tempholder.Add(Tuple.Create(curCompany + "_User2", "User2"));
            alluserinfo.Add(curCompany, tempholder);
            
        }
    }
}
