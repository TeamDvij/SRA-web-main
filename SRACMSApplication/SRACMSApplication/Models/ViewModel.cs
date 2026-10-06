using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRACMSApplication.Models
{
    public class ViewModel
    {
        public slum slum{get;set;}
      //  public tbl_urlId tbl_urlId { get; set; }
        public project project { get; set; }
        public mst_single_image_pdf mst_single_image_pdf { get; set; }
        public faq faq { get; set; }
        public GovtResolution govtresolution { get; set; }
        public galary galary { get; set; }
        public mst_role mst_role { get; set; }
        public mst_user mst_user { get; set; }
        public about_amache_netrutva about_amache_netrutva { get; set; }
        public abount_saksham_sadasya abount_saksham_sadasya { get; set; }
        public other_paripatrak other_paripatrak { get; set; }
        public other_developers other_developers { get; set; }
        public other_sra_rules other_sra_rules { get; set; }
        public other_jahir_prakatan_Preliminary_survey other_jahir_prakatan_Preliminary_survey { get; set; }
        public other_jahir_prakatan_primary_final_eligibility other_jahir_prakatan_primary_final_eligibility { get; set; }



    }
}