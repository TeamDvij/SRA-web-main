using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRACMSApplication.Models
{
    public class other_building_permission_bhogvataModel
    {
        public long id { get; set; }
        public string type { get; set; }
        public string proj_name { get; set; }
        public string proj_name_eng { get; set; }
        public System.DateTime proj_date { get; set; }
        public string proj_date1 { get; set; }
        public System.DateTime proj_date_eng { get; set; }
        public string doc_name { get; set; }
        public string doc_location { get; set; }
        public string active_status { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }
        [NotMapped]
        public List<other_building_permission_bhogvata> other_building_permission_list { get; set; }
        [NotMapped]
        public List<other_building_permission_bhogvata> other_bhogvata_list { get; set; }
        public List<SelectListItem> typeList { get; set; }
        public int dept_id { get; set; }
        [NotMapped]
        public List<sys_config> sys_configList { get; set; }

        [NotMapped]
        public List<project_new> yojna_List { get; set; }
        public long master_no_id { get; set; }
        public float master_no { get; set; }
        public string remark_yojna { get; set; }

        public string strmaster_no { get; set; }
        public string department { get; set; }


    }
}