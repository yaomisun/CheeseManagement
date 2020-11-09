using CheeseManagement.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheeseManagement.Calculation
{
    public class PriceCalculation
    {
        double normalrate = 0.05;
        DateTime currTime = DateTime.Now;
        public PriceCalculation()
        {

        }
       
        //public double UpdatePrice(CheeseData cheese, int adddays)
        //{
        //    double returnprice = 0.00;
        //    int toToday = (currTime - cheese.DateReceived).Days;
        //    switch (cheese.Type)
        //    {
        //        case CheeseType.Aged:
        //            if(cheese.BestBeforeDate<currTime)
        //                cheese.Price = cheese.Price * (1 + 2 * normalrate) * (toToday - 1);
        //            else
        //                cheese.Price = cheese.Price * (1 + normalrate) * (toToday - 1);
        //            break;
        //        case CheeseType.Fresh:
        //            if (cheese.BestBeforeDate < currTime)
        //                cheese.Price = cheese.Price * (1 - 4 * normalrate) * (toToday - 1);
        //            else
        //                cheese.Price = cheese.Price * (1 - 2*normalrate) * (toToday - 1);
        //            break;
        //        case CheeseType.Standard:
        //            if (cheese.BestBeforeDate < currTime)
        //                cheese.Price = cheese.Price * (1 - 2 * normalrate) * (toToday - 1);
        //            else
        //                cheese.Price = cheese.Price * (1 -normalrate) * (toToday - 1);
        //            break;
        //        case CheeseType.Special:
        //            if (cheese.BestBeforeDate < currTime)
        //            {
        //                if (cheese.DaysToSell <= 10)
        //                    cheese.Price = cheese.Price * (1 + normalrate) * (toToday - 1);
        //                else if (cheese.DaysToSell <= 5)
        //                    cheese.Price = cheese.Price * (1 + 2 * normalrate) * (toToday - 1);
        //            }
        //            else
        //                cheese.Price = cheese.Price * (1 -2* normalrate) * (toToday - 1);
        //            break;
        //        case CheeseType.Unique:
        //            break;
        //        default:
        //            if (cheese.BestBeforeDate < currTime)
        //                cheese.Price = cheese.Price * (1 - 2 * normalrate) * (toToday - 1);
        //            else
        //                cheese.Price = cheese.Price * (1 - normalrate) * (toToday - 1);
        //            break;
        //    }
        //    if (cheese.Price < 0)
        //        cheese.Price = 0.00;
        //    else if (cheese.Price > 20)
        //        cheese.Price = 20;
        //    Math.Round(cheese.Price, 2);
        //    return cheese.Price;
        //}


        public double UpdatePrice(CheeseData cheese, int adddays)
        {
            double returnprice = 0.00;
            int toToday = (currTime - cheese.DateReceived).Days;
            switch (cheese.Type)
            {
                case CheeseType.Aged:
                    if (cheese.BestBeforeDate < currTime)
                        returnprice = cheese.Price * (1 + 2 * normalrate) * (toToday - 1);
                    else
                        returnprice = cheese.Price * (1 + normalrate) * (toToday - 1);
                    break;
                case CheeseType.Fresh:
                    if (cheese.BestBeforeDate < currTime)
                        returnprice = cheese.Price * (1 - 4 * normalrate) * (toToday - 1);
                    else
                        returnprice = cheese.Price * (1 - 2 * normalrate) * (toToday - 1);
                    break;
                case CheeseType.Standard:
                    if (cheese.BestBeforeDate < currTime)
                        returnprice = cheese.Price * (1 - 2 * normalrate) * (toToday - 1);
                    else
                        returnprice = cheese.Price * (1 - normalrate) * (toToday - 1);
                    break;
                case CheeseType.Special:
                    if (cheese.BestBeforeDate < currTime)
                    {
                        if (cheese.DaysToSell <= 10)
                            returnprice = cheese.Price * (1 + normalrate) * (toToday - 1);
                        else if (cheese.DaysToSell <= 5)
                            returnprice = cheese.Price * (1 + 2 * normalrate) * (toToday - 1);
                    }
                    else
                        returnprice = cheese.Price * (1 - 2 * normalrate) * (toToday - 1);
                    break;
                case CheeseType.Unique:
                    returnprice = cheese.Price;
                    break;
                default:
                    if (cheese.BestBeforeDate < currTime)
                        returnprice = cheese.Price * (1 - 2 * normalrate) * (toToday - 1);
                    else
                        returnprice = cheese.Price * (1 - normalrate) * (toToday - 1);
                    break;
            }
            if (returnprice < 0)
                returnprice = 0.00;
            else if (returnprice > 20)
                returnprice = 20;
            returnprice=Math.Round(returnprice, 2);
            return returnprice;
        }

        public int UpdateDaysToSell(CheeseData cheese)
        {
            int toToday = (currTime - cheese.DateReceived).Days;
            return cheese.DaysToSell - toToday;
        }
    }
}
