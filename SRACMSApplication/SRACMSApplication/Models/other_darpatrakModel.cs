using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRACMSApplication.Models
{
    public class other_darpatrakModel
    {
        public long id { get; set; }
        public string subject { get; set; }
        public System.DateTime subject_date { get; set; }
        public System.DateTime due_date { get; set; }
        public string subject_date1 { get; set; }
        public string due_date1 { get; set; }

        public string subject_eng { get; set; }
        public System.DateTime subject_date_eng { get; set; }
        public System.DateTime due_date_eng { get; set; }
        public string subject_date_eng1 { get; set; }
        public string due_date_eng1 { get; set; }
        public string doc_name { get; set; }
        public string doc_location { get; set; }
        public string active_status { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }
        [NotMapped]
        public List<other_darpatrak> other_darpatrakList { get; set; }
        [NotMapped]
        public List<other_darpatrak> other_tenderList { get; set; }
        public string tender_link { get; set; }
        public string type { get; set; }


    }
}