using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRACMSApplication.Models

{
    public class faqModel
    {
        public long id { get; set; }
        public string faq1 { get; set; }
        public string answer { get; set; }
        public string faq_eng { get; set; }
        public string answer_eng { get; set; }
        public string active_status { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }
        [NotMapped]
        public List<faq> faq_list { get; set; }
        public List<ViewModel> lst_viewModel_faq { get; set; }

    }
}