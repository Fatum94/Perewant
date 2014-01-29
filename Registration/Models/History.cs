using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Registration.Models
{
    public class History
    {
        public int Id { get; set; }
        public string Uname { get; set; }
        public string LastActivity { get; set; }
        public string ActivityType { get; set; }
    }
}
