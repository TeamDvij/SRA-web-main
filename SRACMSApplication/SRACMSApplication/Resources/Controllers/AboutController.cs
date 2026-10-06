using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SRACMSApplication.Models;

namespace SRACMSApplication.Controllers
{
    public class AboutController : Controller
    {
        private sra_website_cmsEntities db = new sra_website_cmsEntities();
        public ActionResult PradhikarnachiMahiti()
        {
            if (Session["sraLogo"]==null)
                return RedirectToAction("Home", "Home");
            Session["Page"] = "PradhikarnachiMahiti";
            Session["Controller"] = "About";
            aboutModel model = new aboutModel();
            mst_content_single obj = new mst_content_single();
            //About Line 1
            obj = db.mst_content_single.Where(x => x.content_type == "about_line1").FirstOrDefault();
            model.line1 = obj.description;
            model.line_eng1 = obj.description_eng;
            //About Line 2
            obj = db.mst_content_single.Where(x => x.content_type == "about_line2").FirstOrDefault();
            model.line2 = obj.description;
            model.line_eng2 = obj.description_eng;
            //About Line 2_1
            obj = db.mst_content_single.Where(x => x.content_type == "about_line2_1").FirstOrDefault();
            model.line2_1 = obj.description;
            model.line_eng2_1 = obj.description_eng;
            //About Line sthapana
            obj = db.mst_content_single.Where(x => x.content_type == "about_line_sthapana").FirstOrDefault();
            model.about_line_sthapana = obj.description;
            model.about_line_sthapana_eng = obj.description_eng;
            
            //About Line 3
            obj = db.mst_content_single.Where(x => x.content_type == "about_line3").FirstOrDefault();
            model.line3 = obj.description;
            model.line_eng3 = obj.description_eng;
            //About Line 4
            obj = db.mst_content_single.Where(x => x.content_type == "about_line4").FirstOrDefault();
            model.line4 = obj.description;
            model.line_eng4 = obj.description_eng;
            //About Line 5
            obj = db.mst_content_single.Where(x => x.content_type == "about_line5").FirstOrDefault();
            model.line5 = obj.description;
            model.line_eng5 = obj.description_eng;
            //About Line 6
            obj = db.mst_content_single.Where(x => x.content_type == "about_line6").FirstOrDefault();
            model.line6 = obj.description;
            model.line_eng6 = obj.description_eng;
            //About Line 7
            obj = db.mst_content_single.Where(x => x.content_type == "about_line7").FirstOrDefault();
            model.line7 = obj.description;
            model.line_eng7 = obj.description_eng;
            //About Line 7_1
            obj = db.mst_content_single.Where(x => x.content_type == "about_line7_1").FirstOrDefault();
            model.line7_1 = obj.description;
            model.line_eng7_1 = obj.description_eng;
            //About Line 8
            obj = db.mst_content_single.Where(x => x.content_type == "about_line8").FirstOrDefault();
            model.line8 = obj.description;
            model.line_eng8 = obj.description_eng;
            //About Line 9
            obj = db.mst_content_single.Where(x => x.content_type == "about_line9").FirstOrDefault();
            model.line9 = obj.description;
            model.line_eng9 = obj.description_eng;
            //About Line 9
            obj = db.mst_content_single.Where(x => x.content_type == "about_line10").FirstOrDefault();
            model.line10 = obj.description;
            model.line_eng10 = obj.description_eng;
            //लागू कायदे व नियमावली
            var lst1 = db.about.Where(x => x.type == "table1").ToList();
            model.LawRegulationTableList = lst1.ToList();
            //सन २०११ नुसार - कार्यक्षेत्रातील झोपडपट्टंयाचा तपशील
            var lst2 = db.about.Where(x => x.type == "table2").ToList();
            model.SlumDetailsTableList = lst2.ToList();
            //झोपडपट्टयांचे जमीन मालकीनिहाय वर्गीकरण
            var lst3 = db.about.Where(x => x.type == "table3").ToList();
            model.OwnershipClassificationTableList = lst3.ToList();
            //कार्यक्षेत्रातील झोपड्यांची घनता
            var lst4 = db.about.Where(x => x.type == "table4").ToList();
            model.SlumDensityTableList = lst4.ToList();
            //झोपडपट्टी पुनर्वसन योजनांचा गोषवारा
            var lst5 = db.about.Where(x => x.type == "table5").ToList();
            model.slumPlanTableList = lst5.ToList();

            return View(model);
        }
     
        public ActionResult PradhikaranachaAarakhada()
        {
            if (Session["sraLogo"] == null)
                return RedirectToAction("Home", "Home");
            Session["Page"] = "PradhikaranachaAarakhada";
            Session["Controller"] = "About";
            //Aarakhada
            homeModel model = new homeModel();
            mst_single_image_pdf obj1 = new mst_single_image_pdf();

            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "aarakhada").FirstOrDefault();
            model.aarakhada = obj1.doc_location + obj1.doc_name;

            return View(model);
        }
        public ActionResult AmcheNetrutva()
        {
            if (Session["sraLogo"] == null)
                return RedirectToAction("Home", "Home");
            Session["Page"] = "AmcheNetrutva";
            Session["Controller"] = "About";
            about_amache_netrutvaModel model = new about_amache_netrutvaModel();
            var lst = db.about_amache_netrutva.Where(x=>x.active_status=="A").ToList();
            model.AmacheNetrutvaList = lst.ToList();
            return View(model);
        }
        public ActionResult SakshamSadasya()
        {
            if (Session["sraLogo"] == null)
                return RedirectToAction("Home", "Home");
            Session["Page"] = "SakshamSadasya";
            Session["Controller"] = "About";
            abount_saksham_sadasyaModel model = new abount_saksham_sadasyaModel();
            mst_content_single obj = new mst_content_single();
            //About Line 1
            obj = db.mst_content_single.Where(x => x.content_type == "saksham_sadasya_line1").FirstOrDefault();
            model.line1 = obj.description;
            model.line1_eng = obj.description_eng;
            //About Line 2
            obj = db.mst_content_single.Where(x => x.content_type == "saksham_sadasya_line2").FirstOrDefault();
            model.line2 = obj.description;
            model.line2_eng = obj.description_eng;
            var lst = db.abount_saksham_sadasya.Where(x => x.active_status == "A").ToList();
            model.abount_saksham_sadasyaList = lst.ToList();
            return View(model);
        }
        public ActionResult VarshikAhaval()
        {
            if (Session["sraLogo"] == null)
                return RedirectToAction("Home", "Home");
            Session["Page"] = "VarshikAhaval";
            Session["Controller"] = "About";
            mst_single_image_pdfModel model = new mst_single_image_pdfModel();
            //वार्षिक लेखा अहवाल lists
            var lst1 = db.mst_single_image_pdf.Where(x => x.content_type == "AnnualReport" && x.active_status == "A").OrderByDescending(x => x.id).ToList();
            model.lstContentType = lst1.ToList();
            return View(model);
        }
        public ActionResult Paripatrak()
        {
            if (Session["sraLogo"] == null)
                return RedirectToAction("Home", "Home");
            Session["Page"] = "Paripatrak";
            Session["Controller"] = "About";
            other_paripatrakModel model = new other_paripatrakModel();
            //Paripatrake lists
            //var lst1 = db.other_paripatrak.Where(x => x.active_status == "A").OrderByDescending(x => x.id).ToList();
            var lst1 = db.other_paripatrak.Where(x => x.active_status == "A").OrderByDescending(x=>x.subject_date).ToList();
            model.other_paripatrakList = lst1.ToList();
            return View(model);
        }
        public ActionResult Contact()
        {
            if (Session["sraLogo"] == null)
                return RedirectToAction("Home", "Home");
            Session["Page"] = "Contact";
            Session["Controller"] = "About";
            aboutModel model = new aboutModel();
            mst_content_single obj = new mst_content_single();
            obj = db.mst_content_single.Where(x => x.content_type == "Address1" && x.active_status == "A").FirstOrDefault();
            model.address1 = obj.description;
            model.address1_eng = obj.description_eng;
            obj = db.mst_content_single.Where(x => x.content_type == "Address2" && x.active_status == "A").FirstOrDefault();
            model.address2 = obj.description;
            model.address2_eng = obj.description_eng;
            obj = db.mst_content_single.Where(x => x.content_type == "Office Time" && x.active_status == "A").FirstOrDefault();
            model.officeTime = obj.description;
            model.officeTime_eng = obj.description_eng;
            obj = db.mst_content_single.Where(x => x.content_type == "Office Contact Number" && x.active_status == "A").FirstOrDefault();
            model.officeContactNumber = obj.description;
            model.officeContactNumber_eng = obj.description_eng;
            obj = db.mst_content_single.Where(x => x.content_type == "office eMail" && x.active_status == "A").FirstOrDefault();
            model.office_email = obj.description;
            obj = db.mst_content_single.Where(x => x.content_type == "Branch Contact Number" && x.active_status == "A").FirstOrDefault();
            model.branchContactNumber = obj.description;
            model.branchContactNumber_eng = obj.description_eng;
            

            //संपर्क-अधिकारी व कर्मचारी
            var lst1 = db.about.Where(x => x.type == "contact_officers" && x.active_status=="A").OrderBy(x=>x.col_srno).ToList();
            model.ContactOfficerTableList = lst1.ToList();
            //माहिती अधिकार संपर्क-अधिकारी व कर्मचारी
            var lst2 = db.about.Where(x => x.type == "rti_contact_officers").OrderBy(x => x.id).ToList();
            model.RTIContactOfficerTableList = lst2.ToList();
            
            return View(model);
        }
        
    }
}
