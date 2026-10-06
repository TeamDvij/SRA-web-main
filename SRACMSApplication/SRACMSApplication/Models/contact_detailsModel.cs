using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRACMSApplication.Models

{
    public class contact_detailsModel
    {
        public long id { get; set; }
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string office_time { get; set; }
        public string contact_number { get; set; }
        public string address_line1_eng { get; set; }
        public string address_line2_eng { get; set; }
        public string office_time_eng { get; set; }
        public string contact_number_eng { get; set; }
        public string office_website { get; set; }
        public string office_email { get; set; }
        public string active_status { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }
    }
}