using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRACMSApplication.Models
{
    public class mst_content_singleModel
    {
        public long id { get; set; }
        public string content_type { get; set; }
        public string description { get; set; }
        public string description_eng { get; set; }
        public string active_status { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }

        public List<mst_content_single> lstContentType { get; set; }
    }
}