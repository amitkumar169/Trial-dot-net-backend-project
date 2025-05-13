using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trial2.Models
{
    public class Slider
    {
        public decimal? Id { get; set; }
        public string image { get; set; }
        public HttpPostedFileBase photo { get; set; }
        public List<Tbl_slider> View_slider { get; set; }
    }
}