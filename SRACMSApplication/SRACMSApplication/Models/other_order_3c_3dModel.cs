using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRACMSApplication.Models
{
    public class other_order_3c_3dModel
    {
      
        public long id { get; set; }
        public string order_type { get; set; }
        public string notice_type { get; set; }
        public string project_name { get; set; }
        public string project_date_new1 { get; set; }
        public System.DateTime project_date { get; set; }
        public string project_name_eng { get; set; }
        public string doc_name { get; set; }
        public string doc_location { get; set; }
        public string active_status { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }

        public string ca_dept { get; set; }
        public int ca_dept_id { get; set; }

        [NotMapped]
        public List<sys_config> sys_configList { get; set; }

        [NotMapped]
        public List<other_order_3c_3d> other_order_3c_primaryList { get; set; }
        [NotMapped]
        public List<other_order_3c_3d> other_order_3c_FinalList { get; set; }
        [NotMapped]
        public List<other_order_3c_3d> other_order_3d_primaryList { get; set; }
        [NotMapped]
        public List<other_order_3c_3d> other_order_3d_finalList { get; set; }

        public List<SelectListItem> order_typeList { get; set; }
        public List<SelectListItem> notice_type_List { get; set; }

        [NotMapped]
        public List<project_new> yojna_List { get; set; }
        public long master_no_id { get; set; }
        public float master_no { get; set; }
        public string remark_yojna { get; set; }

    }
}