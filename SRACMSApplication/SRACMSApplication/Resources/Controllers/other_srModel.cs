using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRACMSApplication.Models
{
    public class other_srModel
    {
        public long id { get; set; }
        public string subject { get; set; }
        public string subject_eng { get; set; }
        public System.DateTime subject_date { get; set; }
        public string subject_date1 { get; set; }
        public string doc_name { get; set; }
        public string doc_location { get; set; }
        public string doc_name1 { get; set; }
        public string doc_location1 { get; set; }
        public string active_status { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }
        [NotMapped]
        public List<other_sr> other_srList { get; set; }
        public List<other_sr> other_srList2 { get; set; }

    }
}