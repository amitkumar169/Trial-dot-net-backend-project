using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trial2.Models
{
    public class Contact
    {
        public decimal? Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
        public string message { get; set; }

        public List<Tbl_contact> ContactList { get; set; }
    }
}