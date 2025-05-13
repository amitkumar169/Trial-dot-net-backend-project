using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trial2.Models
{
    public class Home_product
    {
        public decimal? id {  get; set; }
        public string name { get; set; }
        public List<Tbl_product> Productlist { get; set; }
    }
}