using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trial2.Models
{
    public class login_1
    {
        public decimal? Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public List<Tbl_login> AdminLoginData { get; set; }
    }
}