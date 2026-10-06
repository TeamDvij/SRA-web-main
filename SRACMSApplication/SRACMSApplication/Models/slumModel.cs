using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRACMSApplication.Models
{
    public class slumModel
    {
        public long id { get; set; }
        //public string urlid { get; set; }

        public string pmc_pcmc { get; set; }
        public string ward { get; set; }
        public string prabhag { get; set; }
        public string slum_name { get; set; }
        public string hut_count { get; set; }
        public string slum_area { get; set; }
        public string declared_nondeclared { get; set; }
        public string slum_owner { get; set; }
        public string ward_eng { get; set; }
        public string prabhag_eng { get; set; }
        public string slum_name_eng { get; set; }
        public string hut_count_eng { get; set; }
        public string slum_area_eng { get; set; }
        public string declared_nondeclared_eng { get; set; }
        public string slum_owner_eng { get; set; }
        public string active_status { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }

        [NotMapped]
        public List<slum> pmc_slumList { get; set; }
        [NotMapped]
        public List<slum> pcmc_slumList { get; set; }

        public List<SelectListItem> typeList { get; set; }
      //  public List<tbl_urlId> tbl_urlIdList { get; set; }
        public string urlid { get; set; }
        public List<ViewModel> lst_viewModel_pmc { get; set; }
        public List<ViewModel> lst_viewModel_pcmc { get; set; }

    }
}