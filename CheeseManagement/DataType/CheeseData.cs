using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CheeseManagement.DataType
{
    public class CheeseData
    {
        public string Name { get; set; }
        public CheeseType Type { get; set; }
        public double Price { get; set; }
        public int DaysToSell { get; set; }
        public DateTime BestBeforeDate { get; set; }
        public DateTime DateReceived { get; set; }
        public CheeseData()
        { }
    }

    public enum CheeseType
    {
        Standard,
        Aged,
        Unique,
        Fresh,
        Special
    }
}
