using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRACMSApplication.Models
{
    public class other_sra_rulesModel
    {
        public long id { get; set; }
        public string rule_name { get; set; }
        public string rule_year { get; set; }
        public string rule_name_eng { get; set; }
        public string rule_year_eng { get; set; }
        public string doc_name { get; set; }
        public string doc_location { get; set; }
        public System.DateTime rule_date { get; set; }
        public string rule_date1 { get; set; }

        public string active_status { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }
        [NotMapped]
        public List<other_sra_rules> sra_rulesList { get; set; }
        public List<ViewModel> lst_viewModel_sra_rules { get; set; }

    }
}