using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRACMSApplication.Models
{
    public class aboutModel
    {
        public long id { get; set; }

        public string line1 { get; set; }
        public string line_eng1 { get; set; }
        public string line2 { get; set; }
        public string line_eng2 { get; set; }
        public string line2_1 { get; set; }
        public string line_eng2_1 { get; set; }
        public string about_line_sthapana { get; set; }
        public string about_line_sthapana_eng { get; set; }
        
        public string line3 { get; set; }
        public string line_eng3 { get; set; }
        public string line4 { get; set; }
        public string line_eng4 { get; set; }
        public string line5 { get; set; }
        public string line_eng5 { get; set; }
        public string line6 { get; set; }
        public string line_eng6 { get; set; }
        public string line7 { get; set; }
        public string line_eng7 { get; set; }
        public string line7_1 { get; set; }
        public string line_eng7_1 { get; set; }
        public string line8 { get; set; }
        public string line_eng8 { get; set; }
        public string line9 { get; set; }
        public string line_eng9 { get; set; }
        public string line10 { get; set; }
        public string line_eng10 { get; set; }
        public string type { get; set; }
        public Nullable<int> col_srno { get; set; }
        public string col0 { get; set; }
        public string col1 { get; set; }
        public string col2 { get; set; }
        public string col3 { get; set; }
        public string col4 { get; set; }
        public string col5 { get; set; }
        public string col6 { get; set; }
        public string col7 { get; set; }
        public string col8 { get; set; }
        public string col9 { get; set; }
        public string col_eng0 { get; set; }
        public string col_eng1 { get; set; }
        public string col_eng2 { get; set; }
        public string col_eng3 { get; set; }
        public string col_eng4 { get; set; }
        public string col_eng5 { get; set; }
        public string col_eng6 { get; set; }
        public string col_eng7 { get; set; }
        public string col_eng8 { get; set; }
        public string col_eng9 { get; set; }
        public string active_status { get; set; }
        public string address1 { get; set; }
        public string address1_eng { get; set; }

        public string address2 { get; set; }
        public string address2_eng { get; set; }

        public string officeTime { get; set; }
        public string officeTime_eng { get; set; }
        public string officeContactNumber { get; set; }
        public string officeContactNumber_eng { get; set; }
        public string branchContactNumber { get; set; }
        public string branchContactNumber_eng { get; set; }
        public string office_email { get; set; }

        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }

        [NotMapped]
        public List<about> LawRegulationTableList { get; set; }
        [NotMapped]
        public List<about> SlumDetailsTableList { get; set; }
        [NotMapped]
        public List<about> OwnershipClassificationTableList { get; set; }
        [NotMapped]
        public List<about> SlumDensityTableList { get; set; }
        [NotMapped]
        public List<about> slumPlanTableList { get; set; }
        [NotMapped]
        public List<about> ContactOfficerTableList { get; set; }
        [NotMapped]
        public List<about> RTIContactOfficerTableList { get; set; }
    }
}