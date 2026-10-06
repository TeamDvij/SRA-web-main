using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRACMSApplication.Models
{
    public class homeModel : projectModel
    {
        public string middleLine { get; set; }
        public string middleLine_eng { get; set; }

        public string ceoMessage { get; set; }
        public string ceoMessage_eng { get; set; }

        public string ceoName { get; set; }
        public string ceoName_eng { get; set; }
        public string ceoPhoto { get; set; }
        public string yojanecheTappe { get; set; }
        public string sra_map { get; set; }
        public string aarakhada { get; set; }

        public string minister1_Name { get; set; }
        public string minister1_Name_eng { get; set; }
        public string minister1_Photo { get; set; }
        public string minister2_Name { get; set; }
        public string minister2_Name_eng { get; set; }
        public string minister2_Photo { get; set; }
        public string minister3_Name { get; set; }
        public string minister3_Name_eng { get; set; }
        public string minister3_Photo { get; set; }
        public string minister4_Name { get; set; }
        public string minister4_Name_eng { get; set; }
        public string minister4_Photo { get; set; }

        [NotMapped]
        public List<slum> pmc_slumList { get; set; }
        [NotMapped]
        public List<slum> pcmc_slumList { get; set; }

        [NotMapped]
        public List<mst_single_image_pdf> banner_List { get; set; }
        [NotMapped]
        public List<other_darpatrak> other_darpatrakList { get; set; }
        [NotMapped]
        public List<GovtResolution> GovtResolutionList { get; set; }

        //[NotMapped]
        //public List<other_jahir_prakatan_primary_final_eligibility> final_List_List { get; set; }
        [NotMapped]
        public List<other_jahir_prakatan_primary_final_eligibility> final_List_2 { get; set; }
        [NotMapped]
        public List<other_order_3c_3d> other_order_3c_list { get; set; }
        [NotMapped]
        public List<other_order_3c_3d> other_order_3d_list { get; set; }
        //[NotMapped]
        //public List<other_building_permission_bhogvata> other_building_permission_list { get; set; }
        [NotMapped]
        public List<other_sr> other_sr_list { get; set; }
        [NotMapped]
        public List<other_building_permission_bhogvata> other_building_bhogvata_list { get; set; }
        [NotMapped]
        public List<other_darpatrak> other_darpatrak_list { get; set; }
        [NotMapped]
        public List<other_lottery> other_lotteryList { get; set; }

        //Project Details
        public string master_no { get; set; }
        public string yojana { get; set; }

        [NotMapped]
        public List<other_order_3c_3d> other_order_3c_primaryList { get; set; }
        [NotMapped]
        public List<other_order_3c_3d> other_order_3c_FinalList { get; set; }
        [NotMapped]
        public List<other_order_3c_3d> other_order_3d_primaryList { get; set; }
        [NotMapped]
        public List<other_order_3c_3d> other_order_3d_FinalList { get; set; }

        [NotMapped]
        public List<other_jahir_prakatan_primary_final_eligibility> primary_List_List { get; set; }
        [NotMapped]
        public List<other_jahir_prakatan_primary_final_eligibility> final_List_List { get; set; }
        [NotMapped]
        public List<other_sr> other_srList { get; set; }
        [NotMapped]
        public List<other_sr> other_srList2 { get; set; }
        [NotMapped]
        public List<other_building_permission_bhogvata> other_building_permission_list { get; set; }
        [NotMapped]
        public List<other_building_permission_bhogvata> other_bhogvata_list { get; set; }

    }
}