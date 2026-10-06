using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRACMSApplication.Models
{
    public class govResolutionsModel
    {
        public long id { get; set; }
        public string resolution_subject { get; set; }
        public System.DateTime resolution_date { get; set; }
        public string resolution_date1 { get; set; }
        public string resolution_subject_eng { get; set; }
        public Nullable<System.DateTime> resolution_date_eng { get; set; }
        public string doc_name { get; set; }
        public string doc_location { get; set; }
        public string active_status { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }

        [NotMapped]
        public List<GovtResolution> GovtResolutionList { get; set; }
        public List<ViewModel> lst_viewModel_govtres { get; set; }

    }
}