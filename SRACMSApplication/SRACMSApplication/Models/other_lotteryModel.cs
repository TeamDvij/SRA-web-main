using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRACMSApplication.Models
{
    public class other_lotteryModel
    {
        public long id { get; set; }
        public string proj_name { get; set; }
        public string developer { get; set; }
        public string lottery_year { get; set; }
        public System.DateTime flat_allotment_date { get; set; }
        public string flat_allotment_date1 { get; set; }
        public string total_flats { get; set; }
        public string allotted_flats { get; set; }
        public string proj_name_eng { get; set; }
        public string developer_eng { get; set; }
        public string lottery_year_eng { get; set; }
        public System.DateTime flat_allotment_date_eng { get; set; }
        public string total_flats_eng { get; set; }
        public string allotted_flats_eng { get; set; }
        public string doc_name { get; set; }
        public string doc_location { get; set; }
        public string video_url { get; set; }
        public string active_status { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }
        [NotMapped]
        public List<other_lottery> other_lotteryList { get; set; }
    }
}