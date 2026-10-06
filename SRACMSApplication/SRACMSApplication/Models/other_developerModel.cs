using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRACMSApplication.Models
{
    public class other_developerModel
    {
        public long id { get; set; }
        public string dev_name { get; set; }
        public string dev_address { get; set; }
        public string dev_contact { get; set; }
        public string dev_class { get; set; }
        public string dev_registration_no { get; set; }
        public string dev_registration_date1 { get; set; }
        public System.DateTime dev_registration_date { get; set; }
        public string dev_name_eng { get; set; }
        public string dev_address_eng { get; set; }
        public string dev_contact_eng { get; set; }
        public string dev_class_eng { get; set; }
        public string dev_registration_no_eng { get; set; }
        public string active_status { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }
        public string completed_projects { get; set; }
        public string current_projects { get; set; }
        public string completed_projects_eng { get; set; }
        public string current_projects_eng { get; set; }
        [NotMapped]
        public List<other_developers> developerList { get; set; }

        public List<ViewModel> lst_viewModel_developer { get; set; }

    }
}