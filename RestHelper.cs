using OptionChain.Model.Response;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace OptionChain
{
    public class RestHelper : IRestHelper
    {
        public async Task<OpationchainResponse> GetOpationchain(string IndexName)
        {
            OpationchainResponse opationchainResponse;
            string url = string.Format("https://www.nseindia.com/api/option-chain-indices?symbol={0}", IndexName);
            using (var client = new RestClient(url))
            {
                var request = new RestRequest();
                request.Method = Method.Get;
                request.AddHeader("User-Agent", "PostmanRuntime/7.29.2");
                request.AddHeader("Accept", "*/*");
                request.AddHeader("Accept-Encoding", "gzip, deflate, br");
                request.AddHeader("Connection", "keep-alive");
                var response = await client.ExecuteAsync(request);
                var res = response.Content;
                opationchainResponse = JsonConvert.DeserializeObject<OpationchainResponse>(res.ToString());
            }
            return opationchainResponse;
        }
        public async Task<CullPutDif> CalculatePCR(OpationchainResponse opationchainResponse)
        {
            int record = DateTime.Now.DayOfWeek.ToString() switch { 
                "Monday" =>5,
                "Tuesday"=>4,
                "Wednesday" => 3,
                "Thursday" => 2,
                "Friday"=>5
            };
            var records = opationchainResponse.records;
            double currentPrice = Math.Round(records.underlyingValue / 100, 0) * 100;
            var price = opationchainResponse.filtered.data.Where(x => x.strikePrice == currentPrice).FirstOrDefault();
            var priceAbove = opationchainResponse.filtered.data.Where(x => x.strikePrice > currentPrice).Take(record).ToList();
            var priceBelow = opationchainResponse.filtered.data.Where(x => x.strikePrice < currentPrice).OrderByDescending(x => x.strikePrice).Take(record).ToList();
            long SumOfPutIO = 0;
            long SumOfCALLIO = 0;
            SumOfPutIO = SumOfPutIO + (price.PE.changeinOpenInterest);
            SumOfCALLIO = SumOfCALLIO + (price.CE.changeinOpenInterest);
            for (int i = 0; i < 4; i++)
            {
                var pa = priceAbove[i];
                var pb = priceBelow[i];
                SumOfPutIO = SumOfPutIO + (pa.PE.changeinOpenInterest + pb.PE.changeinOpenInterest);
                SumOfCALLIO = SumOfCALLIO + (pa.CE.changeinOpenInterest +  pb.CE.changeinOpenInterest);
            }
            long diff = (SumOfPutIO - SumOfCALLIO);
            decimal pcr =  (Convert.ToDecimal(SumOfPutIO) / Convert.ToDecimal(SumOfCALLIO));
            CullPutDif data = new CullPutDif
            {
                strikePrice = currentPrice,
                time = Convert.ToDateTime(records.timestamp).ToString("dd-MM-yyyy hh:mm"),
                call = SumOfCALLIO,
                put = SumOfPutIO,
                diff = diff,
                Name = "BankNifty",
                pcr = Math.Round(pcr, 2)
            };
            return data;
        }
        public async Task<int> InsertData(CullPutDif diff)
        {
            int result = 0;
            try
            {
                using (IDbConnection db = new SqlConnection("Data Source=SQL8004.site4now.net;Initial Catalog=db_a8c0de_metatrader;User Id=db_a8c0de_metatrader_admin;Password=******"))
                {
                    string insertQuery = @"INSERT INTO [dbo].[CallPutDifference]([Name], [Price], [SumOfPut], [SumOfCall], [Difference], [DateTime]) VALUES (@Name, @strikePrice, @put, @call, @diff, @time)";

                    result = await db.ExecuteAsync(insertQuery, new
                    {
                        diff.Name,
                        diff.strikePrice,
                        diff.put,               
                        diff.call,
                        diff.diff,
                        time = Convert.ToDateTime(diff.time)
                    }); ;
                }
            }
            catch (Exception e)
            {

            }
            return result;
        }


    }
    public interface IRestHelper
    {
        Task<OpationchainResponse> GetOpationchain(string IndexName);
        Task<CullPutDif> CalculatePCR(OpationchainResponse opationchainResponse);
        Task<int> InsertData(CullPutDif diff);
    }
}
