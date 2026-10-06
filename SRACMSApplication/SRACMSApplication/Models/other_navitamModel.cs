using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRACMSApplication.Models
{
    public class other_navitamModel
    {
        public long id { get; set; }
        public string new_tag { get; set; }
        public string new_title { get; set; }
        public string new_title_eng { get; set; }
        public System.DateTime new_date { get; set; }
        public string new_date1 { get; set; }
        public string doc_name { get; set; }
        public string doc_location { get; set; }
        public string active_status { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }

        [NotMapped]
        public List<other_navitam> other_navitamList { get; set; }
    }
}
