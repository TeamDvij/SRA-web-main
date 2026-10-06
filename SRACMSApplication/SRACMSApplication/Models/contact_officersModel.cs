using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRACMSApplication.Models

{
    public class contact_officersModel
    {
        public long id { get; set; }
        public int officer_seqence_no { get; set; }
        public string officer_name { get; set; }
        public string officer_desgnation { get; set; }
        public string officer_contact_no { get; set; }
        public string officer_name_eng { get; set; }
        public string officer_desgnation_eng { get; set; }
        public string officer_contact_no_eng { get; set; }
        public string active_status { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }

    }
}