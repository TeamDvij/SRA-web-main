using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRACMSApplication.Models
{
    public class societyModel
    {
        public long id { get; set; }
        public string soc_name { get; set; }
        public string soc_address { get; set; }
        public string soc_registration_no { get; set; }
        public string soc_name_eng { get; set; }
        public string soc_address_eng { get; set; }
        public string soc_registration_no_eng { get; set; }
        public string soc_registration_dt1 { get; set; }
        public System.DateTime soc_registration_dt { get; set; }
        public string soc_year { get; set; }
        public string active_status { get; set; }
        public Nullable<double> created_by { get; set; }
        public Nullable<System.DateTime> created_dt { get; set; }
        public Nullable<double> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }

        [NotMapped]
        public List<society> society_List { get; set; }

    }
}