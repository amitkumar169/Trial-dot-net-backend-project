using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trial2.Models
{
    public class Index
    {
        public decimal? id { get; set; }
        public string name { get; set; }
        public List<Tbl_product> productlist { get; set; }
        public List<Tbl_offer> Offerlist { get; set; }
        public List<Tbl_blog> Bloglist { get; set; }
        public List<Tbl_slider> Sliderlist { get; set; }
    }
}