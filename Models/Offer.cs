using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace trial2.Models
{
    public class Offer
    {
        public decimal? Id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public string image { get; set; }
        public HttpPostedFileBase photo { get; set; }
        public List<Tbl_offer> View_offer { get; set; }
    }
}