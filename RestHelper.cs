using OptionChain.Model.Response;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        
    }
    public interface IRestHelper
    {
        Task<OpationchainResponse> GetOpationchain(string IndexName);
    }
}
