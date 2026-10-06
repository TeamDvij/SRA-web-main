using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SRACMSApplication.Models
{
    public class other_jahirPrakatanModel
    {
        public long id { get; set; }
        public string disclosure_number { get; set; }
        public string subject { get; set; }
        public string upload_date { get; set; }
        public System.DateTime upload_date_New { get; set; }
        public string upload_date_New1 { get; set; }

        public string disclosure_number_eng { get; set; }
        public string subject_eng { get; set; }
        public string active_status { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }
        public string type { get; set; }
        public string is_notice_list { get; set; }
        public string plan_name { get; set; }
        public string plan_date { get; set; }
        public System.DateTime plan_date_New { get; set; }
        public string plan_date_New1 { get; set; }
        public string plan_name_eng { get; set; }
        public string doc_name { get; set; }
        public string doc_location { get; set; }

        public string ca_dept { get; set; }
        public int ca_dept_id { get; set; }


        [NotMapped]
        public List<sys_config> sys_configList { get; set; }

        [NotMapped]
        public List<other_jahir_prakatan_Preliminary_survey> OtherJahirPrakatanList { get; set; }
        [NotMapped]
        public List<other_jahir_prakatan_primary_final_eligibility> primary_Notice_List { get; set; }
        [NotMapped]
        public List<other_jahir_prakatan_primary_final_eligibility> primary_List_List { get; set; }
        [NotMapped]
        public List<other_jahir_prakatan_primary_final_eligibility> final_Notice_List { get; set; }
        [NotMapped]
        public List<other_jahir_prakatan_primary_final_eligibility> final_List_List { get; set; }

        //Added by Umesh on 23.04.2026
        [NotMapped]
        public List<other_jahir_prakatan_primary_final_eligibility> appeal_Notice { get; set; }
        [NotMapped]
        public List<other_jahir_prakatan_primary_final_eligibility> appeal_List { get; set; }
        //upto this by Umesh on 23.04.2026

        //Added by Umesh on 03.06.2026
        [NotMapped]
        public List<other_jahir_prakatan_primary_final_eligibility> supplementary_Notice { get; set; }
        [NotMapped]
        public List<other_jahir_prakatan_primary_final_eligibility> supplementary_List { get; set; }
        //upto this by Umesh on 03.06.2026

        public List<SelectListItem> typeList { get; set; }
        public List<SelectListItem> notice_yadi_List { get; set; }

        public List<ViewModel> lst_viewModel_JahirPrakatan { get; set; }
        public List<ViewModel> lst_viewModel_primary_Notice { get; set; }
        public List<ViewModel> lst_viewModel_primary_List { get; set; }
        public List<ViewModel> lst_viewModel_final_Notice { get; set; }
        public List<ViewModel> lst_viewModel_final_List { get; set; }

        [NotMapped]
        public List<project_new> yojna_List { get; set; }
        public long master_no_id { get; set; }
        public float master_no { get; set; }
        public string remark_yojna { get; set; }

    }
}