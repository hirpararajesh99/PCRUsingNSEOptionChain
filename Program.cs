using RestSharp;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;


namespace OptionChain
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int price=38600;
            Console.Write("Please enter strike price :");
            price = Convert.ToInt32(Console.ReadLine());
            RestHelper restHelper = new RestHelper();
            var res =  await restHelper.GetOpationchain("BANKNIFTY");
            var priceAbove = res.filtered.data.Where(x => x.strikePrice > price).Take(5).ToList();
            var priceBelow = res.filtered.data.Where(x => x.strikePrice < price).OrderByDescending(x=>x.strikePrice).Take(5).ToList();
            decimal SumOfPutIO = 0;
            decimal SumOfCALLIO = 0;
            decimal PCR = 0;
            for (int i = 0; i < 4; i++)
            {
                var  pa = priceAbove[i];
                var pb = priceBelow[i];
                SumOfPutIO = SumOfPutIO + (pa.PE.openInterest + pb.PE.openInterest);
                SumOfCALLIO = SumOfCALLIO + (pa.CE.openInterest + pb.CE.openInterest);
            }
            PCR = (SumOfPutIO / SumOfCALLIO);
            Console.WriteLine("PCR : {0}", Math.Round(PCR,2));
            Console.ReadKey();
        }
    }
}
