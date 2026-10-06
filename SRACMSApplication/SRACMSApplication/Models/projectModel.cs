using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRACMSApplication.Models
{
    public class projectModel
    {
        public long id { get; set; }
        public string project_type { get; set; }
        public string survey_no { get; set; }
        public string project_name { get; set; }
        public string developer_name { get; set; }
        public string architect_name { get; set; }
        public string survey_no_eng { get; set; }
        public string project_name_eng { get; set; }
        public string developer_name_eng { get; set; }
        public string architect_name_eng { get; set; }
        public string active_status { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }
        public long occupied_flats { get; set; }
        public System.DateTime completion_date { get; set; }
        public string completion_date1 { get; set; }

        [NotMapped]
        public List<project> completed_projectList { get; set; }
        [NotMapped]
        public List<project> running_projectList { get; set; }
        [NotMapped]
        public List<project> running_pending_projectList { get; set; }
        [NotMapped]
        public List<project> new_projectList { get; set; }
        [NotMapped]
        public List<project> tot_projectList { get; set; }

        public List<SelectListItem> typeList { get; set; }
        public List<ViewModel> lst_viewModel_project_completed { get; set; }
        public List<ViewModel> lst_viewModel_project_running1 { get; set; }
        public List<ViewModel> lst_viewModel_project_running2 { get; set; }

        public List<ViewModel> lst_viewModel_project_new { get; set; }

        [NotMapped]
        public List<about> slumPlanTableList { get; set; }

        public string project_no { get; set; }
        public long project_status { get; set; }
        public Nullable<System.DateTime> submission_date { get; set; }
        public string submission_date1 { get; set; }

        public string corporation { get; set; }
        public long ward { get; set; }
        public string slum_name { get; set; }
        public string proprietary_rights { get; set; }
        public Nullable<long> total_huts { get; set; }
        public Nullable<double> area_sqmtr { get; set; }
        public string remark_ca { get; set; }
        public string remark_technical { get; set; }
        public string corporation_eng { get; set; }
        public string ward_eng { get; set; }
        public string slum_name_eng { get; set; }
        public string proprietary_rights_eng { get; set; }
        public string remark_ca_eng { get; set; }
        public string remark_technical_eng { get; set; }

        public virtual mst_ward mst_ward { get; set; }
        public virtual project_status project_status1 { get; set; }

        public List<int> schemes { get; set; }
        public List<int> slumholders { get; set; }

        [NotMapped]
        public List<project_new> archieved_projectList { get; set; }
        [NotMapped]
        public List<project_new> completed_projectList1 { get; set; }
        [NotMapped]
        public List<project_new> running_projectList1 { get; set; }
        [NotMapped]
        public List<project_new> running_pending_projectList1 { get; set; }
        [NotMapped]
        public List<project_new> new_projectList1 { get; set; }

        [NotMapped]
        public List<project_new> yojna_list { get; set; }

        public double Master_no { get; set; }
        public int ca_dept_id { get; set; }

        [NotMapped]
        public List<project_status> lst_yojna_status { get; set; }

        [NotMapped]
        public List<mst_ward> lst_ward { get; set; }

        public List<sys_config> sys_configList { get; set; }



    }
}