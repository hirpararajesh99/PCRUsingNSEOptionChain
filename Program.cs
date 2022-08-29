using RestSharp;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace OptionChain
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int s = 60 * 1000;
            int min = 5 * s;
            var timer = new Timer(callback, null,0, min);           
            Console.ReadKey();
        }
        private static void callback(object o)
        {
             print();
        }
        public static async Task print()
        {
            RestHelper restHelper = new RestHelper();
            var res = await restHelper.GetOpationchain("BANKNIFTY");
            var diff = await restHelper.CalculatePCR(res);
            await restHelper.InsertData(diff);
            Console.WriteLine($"Time : {diff.diff}  diff : {diff.time} CurrentTime :{DateTime.Now.ToString("dd-MM-yyyy hh:mm")} pcr:{diff.pcr}");
        }
    }
}
