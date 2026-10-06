using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRACMSApplication.Models
{
    public class abount_saksham_sadasyaModel
    {

        public long id { get; set; }
        public string name { get; set; }
        public string designation { get; set; }
        public string name_eng { get; set; }
        public string designation_eng { get; set; }
        public string active_status { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string line1_eng { get; set; }
        public string line2_eng { get; set; }
        [NotMapped]
        public List<abount_saksham_sadasya> abount_saksham_sadasyaList { get; set; }
        public List<ViewModel> lst_viewModel_saksham_sadasya { get; set; }


    }
}