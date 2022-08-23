using System;
using System.Collections.Generic;
using System.Text;

namespace OptionChain.Model.Response
{

    public class OpationchainResponse
    {
        public Records records { get; set; }
        public Filtered filtered { get; set; }
    }
    public class CE
    {
        public int strikePrice { get; set; }
        public string expiryDate { get; set; }
        public string underlying { get; set; }
        public string identifier { get; set; }
        public int openInterest { get; set; }
        public int changeinOpenInterest { get; set; }
        public double pchangeinOpenInterest { get; set; }
        public int totalTradedVolume { get; set; }
        public double impliedVolatility { get; set; }
        public double lastPrice { get; set; }
        public double change { get; set; }
        public double pChange { get; set; }
        public int totalBuyQuantity { get; set; }
        public int totalSellQuantity { get; set; }
        public int bidQty { get; set; }
        public double bidprice { get; set; }
        public int askQty { get; set; }
        public double askPrice { get; set; }
        public double underlyingValue { get; set; }
        public int totOI { get; set; }
        public int totVol { get; set; }
    }

    public class Datum
    {
        public int strikePrice { get; set; }
        public string expiryDate { get; set; }
        public PE PE { get; set; }
        public CE CE { get; set; }
    }

    public class Filtered
    {
        public List<Datum> data { get; set; }
        public CE CE { get; set; }
        public PE PE { get; set; }
    }

    public class Index
    {
        public string key { get; set; }
        public string index { get; set; }
        public string indexSymbol { get; set; }
        public double last { get; set; }
        public double variation { get; set; }
        public double percentChange { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double previousClose { get; set; }
        public double yearHigh { get; set; }
        public double yearLow { get; set; }
        public string pe { get; set; }
        public string pb { get; set; }
        public string dy { get; set; }
        public string declines { get; set; }
        public string advances { get; set; }
        public string unchanged { get; set; }
    }

    public class PE
    {
        public int strikePrice { get; set; }
        public string expiryDate { get; set; }
        public string underlying { get; set; }
        public string identifier { get; set; }
        public int openInterest { get; set; }
        public int changeinOpenInterest { get; set; }
        public double pchangeinOpenInterest { get; set; }
        public int totalTradedVolume { get; set; }
        public double impliedVolatility { get; set; }
        public double lastPrice { get; set; }
        public double change { get; set; }
        public double pChange { get; set; }
        public int totalBuyQuantity { get; set; }
        public int totalSellQuantity { get; set; }
        public int bidQty { get; set; }
        public double bidprice { get; set; }
        public int askQty { get; set; }
        public double askPrice { get; set; }
        public double underlyingValue { get; set; }
        public int totOI { get; set; }
        public int totVol { get; set; }
    }

    public class Records
    {
        public List<string> expiryDates { get; set; }
        public List<Datum> data { get; set; }
        public string timestamp { get; set; }
        public double underlyingValue { get; set; }
        public List<int> strikePrices { get; set; }
        public Index index { get; set; }
    }


}
