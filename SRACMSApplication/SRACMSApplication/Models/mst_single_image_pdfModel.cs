using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRACMSApplication.Models
{
    public class mst_single_image_pdfModel
    {
        public long id { get; set; }
        public string file_type { get; set; }
        public string content_type { get; set; }
        public string title1 { get; set; }
        public string title_eng { get; set; }
        public string doc_name { get; set; }
        public string doc_location { get; set; }
        public string active_status { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }
        public List<mst_single_image_pdf> lstContentType { get; set; }
        [NotMapped]
        public List<mst_single_image_pdf> chalughadamodi_List { get; set; }
        public List<SelectListItem> newsYearList { get; set; }

        public List<SelectListItem> DistinctYear { get; set; }
        public List<ViewModel> lst_viewModel_banner { get; set; }


    }
}