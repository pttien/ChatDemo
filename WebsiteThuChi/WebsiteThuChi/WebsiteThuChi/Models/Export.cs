using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteThuChi.Models
{
    public class Export
    {
        public DateTime date { get; set; }
        public decimal inAsset { get; set; }
        public string inNote { get; set; }
        public string inCusName { get; set; }
        public decimal outAsset { get; set; }
        public string outNote { get; set; }
        public string outCusName { get; set; }
    }
}