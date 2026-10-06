using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace SRACMSApplication.Models

{
    public class about_amache_netrutvaModel
    {
        public long id { get; set; }
        public string sadasya_photo_location { get; set; }
        public string sadasya_name { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string address_line3 { get; set; }
        public string address_line4 { get; set; }
        public string contact { get; set; }
        public string email { get; set; }
        public string sadasya_name_eng { get; set; }
        public string designation_eng { get; set; }
        public string department_eng { get; set; }
        public string address_line1_eng { get; set; }
        public string address_line2_eng { get; set; }
        public string address_line3_eng { get; set; }
        public string address_line4_eng { get; set; }
        public string contact_eng { get; set; }
        public string email_eng { get; set; }
        public string active_status { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }

        [NotMapped]
        public List<about_amache_netrutva> AmacheNetrutvaList { get; set; }
        public List<ViewModel> lst_viewModel_amache_netruvta { get; set; }



    }
}