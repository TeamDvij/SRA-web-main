using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRACMSApplication.Models
{
    public class DocumentSearchViewModel
    {
        public string TableName { get; set; }
        public string Source { get; set; }
        public string DocumentLink { get; set; }
        public DateTime dt1 { get; set; }
    }

}