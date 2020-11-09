using CheeseManagement.DataType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using CheeseManagement.Calculation;

namespace CheeseManagement.Forms
{
    public partial class MainBrowser : Form
    {
        public BindingList<CheeseData> CheeseList = new BindingList<CheeseData>();

        public PriceCalculation CalEngine { get; private set; }

        public MainBrowser()
        {
            InitializeComponent();
            SetControl();
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = CheeseList.ToList();
            if (CheeseList==null ||CheeseList.Count <= 0)
            {
                label2.Text="No Cheese Information found, please load Cheese data from source";
                label2.ForeColor = Color.DarkRed;
                lblName.Visible = false;
                lblPrice.Visible = false;
            }

        }

        private void SetControl()
        {
            lblToday.Text = lblToday.Text + DateTime.Now.ToShortDateString();
            CalEngine = new PriceCalculation();
        }
        private void SourceFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open XML File";
            theDialog.Filter = "XML files|*.xml";
            theDialog.InitialDirectory = @"C:\temp";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ReadCheeseData(theDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ReadCheeseData(string filepath)
        {
            string datastring = filepath.Substring(filepath.IndexOf("_") + 1, 8);
            int day = int.Parse(datastring.Substring(0,2));
            int month = int.Parse(datastring.Substring(2, 2));
            int year = int.Parse(datastring.Substring(4,4));
            DateTime dateRec = new DateTime(year,month,day);
            try
            {
                CheeseList.Clear();
                XElement xelement = XElement.Load(filepath);
            IEnumerable<XElement> items = xelement.Elements();
            foreach (var item in items)
            {
                CheeseData currentcheese = new CheeseData();
                //Get the value of XML fields here
                currentcheese.Name =(!string.IsNullOrWhiteSpace(item.Element("Name").Value))? item.Element("Name").Value:null;
                string typename = (!string.IsNullOrWhiteSpace(item.Element("Type").Value))? item.Element("Type").Value:null;
        
                switch (typename)
                {
                    case "Standard":
                        currentcheese.Type = CheeseType.Standard;
                        break;
                    case "Aged":
                        currentcheese.Type = CheeseType.Aged;
                        break;
                    case "Unique":
                        currentcheese.Type = CheeseType.Unique;
                        break;
                    case "Fresh":
                        currentcheese.Type = CheeseType.Fresh;
                        break;
                    case "Special":
                        currentcheese.Type = CheeseType.Special;
                        break;
                    default:
                        currentcheese.Type = CheeseType.Standard;
                        break;
                }

                    currentcheese.DateReceived = dateRec;
                    int daysvalue;
                    currentcheese.DaysToSell = int.TryParse(item.Element("DaysToSell").Value, out daysvalue) == true ? daysvalue :0;
                    currentcheese.DaysToSell = CalEngine.UpdateDaysToSell(currentcheese);
                    currentcheese.BestBeforeDate = (!string.IsNullOrWhiteSpace(item.Element("BestBeforeDate").Value))?
                    DateTime.Parse(item.Element("BestBeforeDate").Value).Date:new DateTime();
                    double TempPrice;
                    currentcheese.Price=double.TryParse(item.Element("Price").Value, out TempPrice) == true ? TempPrice : 0;
                    currentcheese.Price=CalEngine.UpdatePrice(currentcheese,0);
                CheeseList.Add(currentcheese);
            }
                ControlAfter();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errors happened during the loading process, please check file or contact Support.");
            }
        }


        private void ControlAfter()
        {
            dataGridView1.DataSource = CheeseList.ToList();
            label2.Text="Cheese loaded, click on row to view detail.";
            lblName.Visible = false;
            lblPrice.Visible = false;
        }
       
        private void MainBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //CheeseData currentCheese=dataGridView1.se
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            CheeseData cheese = (CheeseData) selectedRow.DataBoundItem;
            lblName.Visible = true;
            lblPrice.Visible = true;
            lblName.Text = "Cheese Name:" +cheese.Name;
            double afterprice = CalEngine.UpdatePrice(cheese, 7);
            lblPrice.Text = "Price after 7 days:" + afterprice.ToString();

        }

    }
}
