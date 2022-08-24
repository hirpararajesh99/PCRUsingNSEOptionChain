using System;
using System.Collections.Generic;
using System.Text;

namespace OptionChain.Model.Response
{
    public class CullPutDif
    {
        public string Name { get; set; }
        public double strikePrice { get; set; }
        public string time { get; set; }
        public long call { get; set; }
        public long put { get; set; }
        public long diff { get; set; }
    }
}
