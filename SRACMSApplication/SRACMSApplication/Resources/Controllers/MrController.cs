using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Reflection;
using SRACMSApplication.Models;
using System.Net.Mail;
using System.Net;
using System.Data.Entity;

namespace SRACMSApplication.Controllers
{
    public class MrController : Controller
    {
        private sra_website_cmsEntities db = new sra_website_cmsEntities();

        ResourceManager rm;
        CultureInfo ci;
        public void shared_method()
        {
            if (Session["Lang"] == null)
                Session["Lang"] = "mr";

            if (Session["Layout"] == null)
                Session["Layout"] = "~/Views/Shared/Master7.cshtml";

            Session["Controller"] = "Mr";

            mst_content_single obj = new mst_content_single();
            mst_single_image_pdf obj1 = new mst_single_image_pdf();

            obj = db.mst_content_single.Where(x => x.content_type == "Top Header" && x.active_status == "A").FirstOrDefault();
            if (Session["Lang"].ToString() == "mr") Session["topHeader"] = obj.description; else Session["topHeader"] = obj.description_eng; 
            obj = db.mst_content_single.Where(x => x.content_type == "Title Line1" && x.active_status == "A").FirstOrDefault();
            if (Session["Lang"].ToString() == "mr") Session["titleLine1"] = obj.description; else Session["titleLine1"] = obj.description_eng;
            obj = db.mst_content_single.Where(x => x.content_type == "Title Line2" && x.active_status == "A").FirstOrDefault();
            if (Session["Lang"].ToString() == "mr") Session["titleLine2"] = obj.description; else Session["titleLine2"] = obj.description_eng;
            obj = db.mst_content_single.Where(x => x.content_type == "Address1" && x.active_status == "A").FirstOrDefault();
            if (Session["Lang"].ToString() == "mr") Session["address1"] = obj.description; else Session["address1"] = obj.description_eng;
            obj = db.mst_content_single.Where(x => x.content_type == "Address2" && x.active_status == "A").FirstOrDefault();
            if (Session["Lang"].ToString() == "mr") Session["address2"] = obj.description; else Session["address2"] = obj.description_eng;
            obj = db.mst_content_single.Where(x => x.content_type == "Office Contact Number" && x.active_status == "A").FirstOrDefault();
            if (Session["Lang"].ToString() == "mr") Session["officeContactNumber"] = obj.description; else Session["officeContactNumber"] = obj.description_eng;
            obj = db.mst_content_single.Where(x => x.content_type == "office eMail" && x.active_status == "A").FirstOrDefault();
            if (Session["Lang"].ToString() == "mr") Session["eMail"] = obj.description; else Session["eMail"] = obj.description_eng;
            obj = db.mst_content_single.Where(x => x.content_type == "Branch Contact Number" && x.active_status == "A").FirstOrDefault();
            if (Session["Lang"].ToString() == "mr") Session["branchContactNumber"] = obj.description; else Session["branchContactNumber"] = obj.description_eng;


            //झो.पु.प्रा. logo & satyamev logo
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "sra_logo" && x.active_status == "A").FirstOrDefault();
            Session["sraLogo"] = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "satyamev_logo" && x.active_status == "A").FirstOrDefault();
            Session["satyamevLogo"] = obj1.doc_location + obj1.doc_name;

            //झो.पु.प्रा.योजनेचे टप्पे
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "yojaneche_tappe" && x.active_status == "A").FirstOrDefault();
            Session["yojanecheTappe"] = obj1.doc_location + obj1.doc_name;

            //विकसक नोंदणी निकष
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "nondani_nikash" && x.active_status == "A").FirstOrDefault();
            Session["viksak_nodni_nikash"] = obj1.doc_location + obj1.doc_name;

            //योजनेची माहिती पुस्तिका
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "mahiti_pustika" && x.active_status == "A").FirstOrDefault();
            Session["mahiti_pustika"] = obj1.doc_location + obj1.doc_name;

            //सूचिबध्द संस्था
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "listed_institutions" && x.active_status == "A").FirstOrDefault();
            Session["listed_institutions"] = obj1.doc_location + obj1.doc_name;

            //सार संग्रह पुस्तिका
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "sar_sangrah_pustika" && x.active_status == "A").FirstOrDefault();
            Session["sar_sangrah_pustika"] = obj1.doc_location + obj1.doc_name;

            //सहकारी गृहनिर्माण संस्था नोंदणी कार्यपद्धती
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "coop_hsg_society" && x.active_status == "A").FirstOrDefault();
            Session["coop_hsg_society"] = obj1.doc_location + obj1.doc_name;

            //झोपुप्रा प्रस्ताव दाखल कार्यपद्धती
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "prastav_dakhal" && x.active_status == "A").FirstOrDefault();
            Session["prastav_dakhal"] = obj1.doc_location + obj1.doc_name;

            //माहितीचा अधिकार कायदा २००५ 
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "RTI2005" && x.active_status == "A").FirstOrDefault();
            Session["RTI2005"] = obj1.doc_location + obj1.doc_name;
            // नागरिकांची सनद
             obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "nagarikachi_sanad" && x.active_status == "A").FirstOrDefault();
            Session["nagarikachi_sanad"] = obj1.doc_location + obj1.doc_name;
            // लोकसेवा हक्क अधिनियम अंतर्गत सेवा
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "lokseva_hakka_adhiniyam" && x.active_status == "A").FirstOrDefault();
            Session["loksevaHakka"] = obj1.doc_location + obj1.doc_name;
            //सी.ई.ओ. अधिकार
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "ceo_adhikar" && x.active_status == "A").FirstOrDefault();
            Session["ceo_adhikar"] = obj1.doc_location + obj1.doc_name;
            //माहितीचा अधिकार कायदा ४ क
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "RTI4C" && x.active_status == "A").FirstOrDefault();
            if (obj1 != null)
                Session["RTI4C"] = obj1.doc_location + obj1.doc_name;
            else
                Session["RTI4C"]=null;
            //झोपुप्रा कायदा
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "slum_act" && x.active_status == "A").FirstOrDefault();
            Session["slum_act"] = obj1.doc_location + obj1.doc_name;

            //Get Visitor Count
            ViewBag.visitorCount = db.VisitorLogs.Select(x => (int?)x.count).DefaultIfEmpty(0).Sum().Value + 1000;

        }
        public ActionResult Home()
        {
            shared_method();
            Session["Page"] = "Home";

            homeModel model = new homeModel();
            mst_content_single obj = new mst_content_single();
            mst_single_image_pdf obj1 = new mst_single_image_pdf();


            //SRA Slogan in Middle
            obj = db.mst_content_single.Where(x => x.content_type == "Middle Line").FirstOrDefault();
            model.middleLine = obj.description;
            model.middleLine_eng = obj.description_eng;

            //Ceo Message
            obj = db.mst_content_single.Where(x => x.content_type == "CEO Message").FirstOrDefault();
            model.ceoMessage = obj.description;
            model.ceoMessage_eng = obj.description_eng;

            //Ceo Name
            obj = db.mst_content_single.Where(x => x.content_type == "CEO Name").FirstOrDefault();
            model.ceoName = obj.description;
            model.ceoName_eng = obj.description_eng;

            //ceo Photo
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "ceo photo").FirstOrDefault();
            model.ceoPhoto = obj1.doc_location+ obj1.doc_name;

            ////Completed Project list
            //var lst_completed = db.projects.Where(x => x.project_type == "C" && x.active_status == "A");
            //model.completed_projectList = lst_completed.ToList();
            ////Running Project List
            //var lst_running = db.projects.Where(x => (x.project_type == "R" || x.project_type == "S") && x.active_status == "A");
            //model.running_projectList = lst_running.ToList();
            ////New Project List
            //var lst_new = db.projects.Where(x => x.project_type == "N" && x.active_status == "A");
            //model.new_projectList = lst_new.ToList();

            //Banner List
            var lst1 = db.mst_single_image_pdf.Where(x => x.content_type == "Banners" && x.active_status == "A");
            model.banner_List = lst1.ToList();

            //Honerable Ministers
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister1" && x.active_status == "A").FirstOrDefault();
            model.minister1_Name = obj1.title;
            model.minister1_Name_eng = obj1.title_eng;
            model.minister1_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister2" && x.active_status == "A").FirstOrDefault();
            model.minister2_Name = obj1.title;
            model.minister2_Name_eng = obj1.title_eng;
            model.minister2_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister3" && x.active_status == "A").FirstOrDefault();
            model.minister3_Name = obj1.title;
            model.minister3_Name_eng = obj1.title_eng;
            model.minister3_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister4" && x.active_status == "A").FirstOrDefault();
            model.minister4_Name = obj1.title;
            model.minister4_Name_eng = obj1.title_eng;
            model.minister4_Photo = obj1.doc_location + obj1.doc_name;



            mst_single_image_pdf obj11 = new mst_single_image_pdf();
            obj11 = db.mst_single_image_pdf.Where(x => x.content_type == "sra_map").FirstOrDefault();
            model.sra_map = obj11.doc_location + obj11.doc_name;


            //Add Visitor Count
            VisitorLogs objVisitor = null;
            var IPAddress = UserInfoHelper.GetClientIp();
            var BrowserInfo = UserInfoHelper.GetBrowserInfo();

            

            //check already visit
            string localIP = Dns.GetHostEntry(Dns.GetHostName())
                                 .AddressList
                                 .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?
                                 .ToString();
            objVisitor = db.VisitorLogs.SingleOrDefault(x =>x.IPAddress == IPAddress.ToString() && x.localIP==localIP && x.BrowserInfo == BrowserInfo &&
                         DbFunctions.TruncateTime(x.VisitDate) == DbFunctions.TruncateTime(DateTime.Today));
            if (objVisitor == null)
            {
                objVisitor = new VisitorLogs();
                objVisitor.IPAddress = UserInfoHelper.GetClientIp();
                objVisitor.VisitDate = DateTime.Now;
                objVisitor.lastVisitDateTime = objVisitor.VisitDate;
                objVisitor.BrowserInfo = UserInfoHelper.GetBrowserInfo();
                objVisitor.OsInfo = UserInfoHelper.GetOS();
                objVisitor.localIP = localIP;
                objVisitor.count = 1;
                db.VisitorLogs.Add(objVisitor);
            }
            else
            {
                objVisitor.count += 1;
                objVisitor.lastVisitDateTime = DateTime.Now;
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            //Get Visitor Count
            ViewBag.visitorCount = db.VisitorLogs.Select(x => (int?)x.count).DefaultIfEmpty(0).Sum().Value + 1000;

            return View(model);
        }
        public ActionResult PradhikarnachiMahiti()
        {
            shared_method();
            Session["Page"] = "PradhikarnachiMahiti";

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
            shared_method();
            Session["Page"] = "PradhikaranachaAarakhada";
            //Aarakhada
            homeModel model = new homeModel();
            mst_single_image_pdf obj1 = new mst_single_image_pdf();

            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "aarakhada").FirstOrDefault();
            model.aarakhada = obj1.doc_location + obj1.doc_name;

            return View(model);
        }
        public ActionResult AmcheNetrutva()
        {
            shared_method();
            Session["Page"] = "AmcheNetrutva";
            about_amache_netrutvaModel model = new about_amache_netrutvaModel();
            var lst = db.about_amache_netrutva.Where(x => x.active_status == "A").ToList();
            model.AmacheNetrutvaList = lst.ToList();
            return View(model);
        }
        public ActionResult SakshamSadasya()
        {
            shared_method();
            Session["Page"] = "SakshamSadasya";
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
        public ActionResult WorkAreaMap()
        {
            //MAp
            shared_method();
            Session["Page"] = "WorkAreaMap";
            homeModel model = new homeModel();
            mst_single_image_pdf obj1 = new mst_single_image_pdf();
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "sra_map").FirstOrDefault();
            model.sra_map = obj1.doc_location + obj1.doc_name;
            return View(model);
        }
        public ActionResult FAQ()
        {
            shared_method();
            Session["Page"] = "FAQ";
            faqModel model = new faqModel();
            var lst1 = db.faqs.Where(x =>  x.active_status == "A").ToList();
            model.faq_list = lst1.ToList();
            return View(model);
        }
        public ActionResult Gallery(string desc)
        {
            shared_method();
            Session["Page"] = "Gallery";
            galaryModel model = new galaryModel();

            //var lst1 = new SelectList(db.galaries.Where(x => x.photo_video == "P").Select(x => x.album_desc).Distinct().ToList(), "album_desc", "album_desc");
            var lst1 = new SelectList(db.galaries.Where(x => x.photo_video == "P" && x.active_status == "A").Select(x => x.album_desc).Distinct().ToList());
            var lst2 = new SelectList(db.galaries.Where(x => x.photo_video == "P" && x.active_status == "A").Select(x => x.album_desc_eng).Distinct().ToList());
            if (Session["Lang"].ToString() == "mr")
                model.DistinctPhotoAlbum = lst1.ToList();
            else
                model.DistinctPhotoAlbum = lst2.ToList();

            string firstAlbum = "";
            foreach (var item in model.DistinctPhotoAlbum)
            {
                firstAlbum = item.Text;
                Session["selectedAlbum"] = firstAlbum;
                break;
            }
            //var lst = db.galaries.Where(x => x.photo_video == "P").OrderByDescending(x => x.id).ToList();
            var lst = db.galaries.Where(x => x.photo_video == "P" && x.active_status == "A" && x.album_desc == firstAlbum).ToList();
            if (desc != null)
            {
                lst = db.galaries.Where(x => x.photo_video == "P" && x.active_status == "A" && x.album_desc == desc).ToList();
                Session["selectedAlbum"] = desc;
            }

            model.PhotoGalleryList = lst.ToList();

            // model.PhotoAlbum = lst1;

            //model.PhotoAlbum = lst1.ToList();
            //var lst2 = db.galaries.Where(x => x.photo_video == "V").OrderByDescending(x => x.id).ToList();
            //model.VideoGalleryList = lst2.ToList();
            //var lst3 = db.galaries.Where(x => x.photo_video == "V").Select(x => x.album_desc).Distinct();
            //model.PhotoAlbum = lst3;
            return View(model);
        }

        public ActionResult ChaluGhadamodi(string desc)
        {
            shared_method();
            Session["Page"] = "ChaluGhadamodi";
            mst_single_image_pdfModel model = new mst_single_image_pdfModel();
           
            //Year list
            var lstyear = new SelectList(db.mst_single_image_pdf.Where(x => x.active_status == "A" && x.content_type == "News").OrderByDescending(x => x.id).Select(x => x.title_eng).Distinct().ToList());
            model.DistinctYear = lstyear.ToList();
            string defaultYear = "";
            foreach (var item in model.DistinctYear)
            {
                defaultYear = item.Text;
                Session["defaultYear"] = defaultYear;
                break;
            }
            var lst1 = db.mst_single_image_pdf.Where(x => x.content_type == "News" && x.active_status == "A" && x.title == defaultYear).OrderByDescending(x => x.id).ToList();
            if (desc != null)
            {
                lst1 = db.mst_single_image_pdf.Where(x => x.content_type == "News" && x.active_status == "A" && x.title == desc).OrderByDescending(x => x.id).ToList();
                Session["defaultYear"] = desc;
            }
            model.chalughadamodi_List = lst1.ToList();
            //ViewBag.YearListItems = new SelectList(db.mst_single_image_pdf.Where(x => x.active_status == "A" && x.content_type == "News").Select(m => m.title_eng).Distinct());
            return View(model);
        }

        public ActionResult Contact()
        {
            shared_method();
            Session["Page"] = "Contact";
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
            var lst1 = db.about.Where(x => x.type == "contact_officers" && x.active_status == "A").OrderBy(x => x.col_srno).ToList();
            model.ContactOfficerTableList = lst1.ToList();
            //माहिती अधिकार संपर्क-अधिकारी व कर्मचारी
            var lst2 = db.about.Where(x => x.type == "rti_contact_officers").OrderBy(x => x.id).ToList();
            model.RTIContactOfficerTableList = lst2.ToList();

            return View(model);
        }

   
        [HttpGet]
        public void sendMail(string name, string mobile, string email, string remark)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(email));  // replace with valid value 
                    message.From = new MailAddress("umeshpotdar@priyatech.in");  // replace with valid value
                    message.Subject = "अभिप्राय";
                    message.Body = remark;
                    message.IsBodyHtml = true;

                    using (var smtp = new SmtpClient())
                    {
                        var credential = new NetworkCredential
                        {
                            UserName = "umeshpotdar@priyatech.in",  // replace with valid value
                            Password = "pROkABADDI@2019"  // replace with valid value
                        };
                        smtp.Credentials = credential;
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = false;
                        //smtp.EnableSsl = true;
                        smtp.Credentials = new System.Net.NetworkCredential("umeshpotdar@priyatech.in", "pROkABADDI@2019"); // ***use valid credentials***
                        smtp.Send(message);
                        ViewBag.Message = "Record Successfully Updated";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
        }

      
        public ActionResult NewHome()
        {
            if (Session["Lang"] == null)
                Session["Lang"] = "mr";

            shared_method();
            Session["Page"] = "Home";
            Session["Controller"] = "Home";

            homeModel model = new homeModel();
            mst_content_single obj = new mst_content_single();
            mst_single_image_pdf obj1 = new mst_single_image_pdf();

            //SRA Slogan in Middle
            obj = db.mst_content_single.Where(x => x.content_type == "Middle Line").FirstOrDefault();
            model.middleLine = obj.description;
            model.middleLine_eng = obj.description_eng;

            //Ceo Message
            obj = db.mst_content_single.Where(x => x.content_type == "CEO Message").FirstOrDefault();
            model.ceoMessage = obj.description;
            model.ceoMessage_eng = obj.description_eng;

            //Ceo Name
            obj = db.mst_content_single.Where(x => x.content_type == "CEO Name").FirstOrDefault();
            model.ceoName = obj.description;
            model.ceoName_eng = obj.description_eng;

            //ceo Photo
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "ceo photo").FirstOrDefault();
            model.ceoPhoto = obj1.doc_location + obj1.doc_name;

            ////Completed Project list
            //var lst_completed = db.projects.Where(x => x.project_type == "C" && x.active_status == "A");
            //model.completed_projectList = lst_completed.ToList();
            ////Running Project List
            //var lst_running = db.projects.Where(x => (x.project_type == "R" || x.project_type == "S") && x.active_status == "A");
            //model.running_projectList = lst_running.ToList();
            ////New Project List
            //var lst_new = db.projects.Where(x => x.project_type == "N" && x.active_status == "A");
            //model.new_projectList = lst_new.ToList();

            //Banner List
            var lst1 = db.mst_single_image_pdf.Where(x => x.content_type == "Banners" && x.active_status == "A");
            model.banner_List = lst1.ToList();

            //Honerable Ministers
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister1" && x.active_status == "A").FirstOrDefault();
            model.minister1_Name = obj1.title;
            model.minister1_Name_eng = obj1.title_eng;
            model.minister1_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister2" && x.active_status == "A").FirstOrDefault();
            model.minister2_Name = obj1.title;
            model.minister2_Name_eng = obj1.title_eng;
            model.minister2_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister3" && x.active_status == "A").FirstOrDefault();
            model.minister3_Name = obj1.title;
            model.minister3_Name_eng = obj1.title_eng;
            model.minister3_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister4" && x.active_status == "A").FirstOrDefault();
            model.minister4_Name = obj1.title;
            model.minister4_Name_eng = obj1.title_eng;
            model.minister4_Photo = obj1.doc_location + obj1.doc_name;



            mst_single_image_pdf obj11 = new mst_single_image_pdf();
            obj11 = db.mst_single_image_pdf.Where(x => x.content_type == "sra_map").FirstOrDefault();
            model.sra_map = obj11.doc_location + obj11.doc_name;


            //Darpatrak lists
            var lst_darpatrak = db.other_darpatrak.Where(x => x.type == "D").OrderByDescending(x => x.id).Take(5).ToList();
            model.other_darpatrakList = lst_darpatrak.ToList();

            ////Tender lists
            //var lst2 = db.other_darpatrak.Where(x => x.active_status == "A" && x.type == "T" && x.due_date_eng >= DateTime.Today).OrderByDescending(x => x.id).ToList();
            //model.other_tenderList = lst2.ToList();

            //Shashan Nirnay lists
            var lst_pmc = db.GovtResolutions.Where(x => x.active_status == "A").OrderByDescending(x => x.resolution_date).Take(5).ToList();
            model.GovtResolutionList = lst_pmc.ToList();


            //jahir prakatn 
            var lst_final_List_List = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "P" && (x.is_notice_list == "N" || x.is_notice_list == "L") && x.active_status == "A").OrderByDescending(x => x.plan_date_New).Take(5).ToList();
            model.final_List_List = lst_final_List_List.ToList();

            //patrata yadi list
            var lst21 = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "F" && (x.is_notice_list == "N" || x.is_notice_list == "L") && x.active_status == "A").OrderByDescending(x => x.plan_date_New).Take(5).ToList();
            model.final_List_2 = lst21.ToList();

            //3C list
            var lst22 = db.other_order_3c_3d.Where(x => x.order_type == "C" && (x.notice_type == "A" || x.notice_type == "F") && x.active_status == "A").OrderByDescending(x => x.project_date).Take(5).ToList();
            model.other_order_3c_list = lst22.ToList();

            //3D list
            var lst23 = db.other_order_3c_3d.Where(x => x.order_type == "D" && (x.notice_type == "A" || x.notice_type == "F") && x.active_status == "A").OrderByDescending(x => x.project_date).Take(5).ToList();
            model.other_order_3d_list = lst23.ToList();

            //building Permission
            var lst24 = db.other_building_permission_bhogvata.Where(x => x.type == "B" && x.active_status == "A").OrderByDescending(x => x.proj_date).Take(5).ToList();
            model.other_building_permission_list = lst24.ToList();

            //SR3 4
            var lst25 = db.other_sr.Where(x => x.active_status == "A" && x.doc_name != "").OrderByDescending(x => x.id).Take(5).ToList();
            model.other_sr_list = lst25.ToList();

            //occupacy Permission
            var lst26 = db.other_building_permission_bhogvata.Where(x => x.type == "O" && x.active_status == "A").OrderByDescending(x => x.proj_date).Take(5).ToList();
            model.other_building_bhogvata_list = lst26.ToList();

            //Darpatrak
            var lst27 = db.other_darpatrak.Where(x => x.active_status == "A").OrderByDescending(x => x.due_date).Take(5).ToList();
            model.other_darpatrak_list = lst27.ToList();

            //Lottery lists
            //var lst1 = db.other_lottery.Where(x => x.active_status == "A").OrderByDescending(x => x.id).ToList();
            var lotteryList = db.other_lottery.Where(x => x.active_status == "A").OrderByDescending(x => x.flat_allotment_date).Take(5).ToList();
            model.other_lotteryList = lotteryList.ToList();

            return View(model);
        }
        public ActionResult template2()
        {
            if (Session["Lang"] == null)
                Session["Lang"] = "mr";

            shared_method();
            Session["Page"] = "Home";
            Session["Controller"] = "Home";

            homeModel model = new homeModel();
            mst_content_single obj = new mst_content_single();
            mst_single_image_pdf obj1 = new mst_single_image_pdf();

            //SRA Slogan in Middle
            obj = db.mst_content_single.Where(x => x.content_type == "Middle Line").FirstOrDefault();
            model.middleLine = obj.description;
            model.middleLine_eng = obj.description_eng;

            //Ceo Message
            obj = db.mst_content_single.Where(x => x.content_type == "CEO Message").FirstOrDefault();
            model.ceoMessage = obj.description;
            model.ceoMessage_eng = obj.description_eng;

            //Ceo Name
            obj = db.mst_content_single.Where(x => x.content_type == "CEO Name").FirstOrDefault();
            model.ceoName = obj.description;
            model.ceoName_eng = obj.description_eng;

            //ceo Photo
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "ceo photo").FirstOrDefault();
            model.ceoPhoto = obj1.doc_location + obj1.doc_name;

            ////Completed Project list
            //var lst_completed = db.projects.Where(x => x.project_type == "C" && x.active_status == "A");
            //model.completed_projectList = lst_completed.ToList();
            ////Running Project List
            //var lst_running = db.projects.Where(x => (x.project_type == "R" || x.project_type == "S") && x.active_status == "A");
            //model.running_projectList = lst_running.ToList();
            ////New Project List
            //var lst_new = db.projects.Where(x => x.project_type == "N" && x.active_status == "A");
            //model.new_projectList = lst_new.ToList();

            //Banner List
            var lst1 = db.mst_single_image_pdf.Where(x => x.content_type == "Banners" && x.active_status == "A");
            model.banner_List = lst1.ToList();

            //Honerable Ministers
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister1" && x.active_status == "A").FirstOrDefault();
            model.minister1_Name = obj1.title;
            model.minister1_Name_eng = obj1.title_eng;
            model.minister1_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister2" && x.active_status == "A").FirstOrDefault();
            model.minister2_Name = obj1.title;
            model.minister2_Name_eng = obj1.title_eng;
            model.minister2_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister3" && x.active_status == "A").FirstOrDefault();
            model.minister3_Name = obj1.title;
            model.minister3_Name_eng = obj1.title_eng;
            model.minister3_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister4" && x.active_status == "A").FirstOrDefault();
            model.minister4_Name = obj1.title;
            model.minister4_Name_eng = obj1.title_eng;
            model.minister4_Photo = obj1.doc_location + obj1.doc_name;



            mst_single_image_pdf obj11 = new mst_single_image_pdf();
            obj11 = db.mst_single_image_pdf.Where(x => x.content_type == "sra_map").FirstOrDefault();
            model.sra_map = obj11.doc_location + obj11.doc_name;


            //Darpatrak lists
            var lst_darpatrak = db.other_darpatrak.Where(x => x.type == "D").OrderByDescending(x => x.id).Take(5).ToList();
            model.other_darpatrakList = lst_darpatrak.ToList();

            ////Tender lists
            //var lst2 = db.other_darpatrak.Where(x => x.active_status == "A" && x.type == "T" && x.due_date_eng >= DateTime.Today).OrderByDescending(x => x.id).ToList();
            //model.other_tenderList = lst2.ToList();

            //Shashan Nirnay lists
            var lst_pmc = db.GovtResolutions.Where(x => x.active_status == "A").OrderByDescending(x => x.resolution_date).Take(5).ToList();
            model.GovtResolutionList = lst_pmc.ToList();


            //jahir prakatn 
            var lst_final_List_List = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "P" && (x.is_notice_list == "N" || x.is_notice_list == "L") && x.active_status == "A").OrderByDescending(x => x.plan_date_New).Take(5).ToList();
            model.final_List_List = lst_final_List_List.ToList();

            //patrata yadi list
            var lst21 = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "F" && (x.is_notice_list == "N" || x.is_notice_list == "L") && x.active_status == "A").OrderByDescending(x => x.plan_date_New).Take(5).ToList();
            model.final_List_2 = lst21.ToList();

            //3C list
            var lst22 = db.other_order_3c_3d.Where(x => x.order_type == "C" && (x.notice_type == "A" || x.notice_type == "F") && x.active_status == "A").OrderByDescending(x => x.project_date).Take(5).ToList();
            model.other_order_3c_list = lst22.ToList();

            //3D list
            var lst23 = db.other_order_3c_3d.Where(x => x.order_type == "D" && (x.notice_type == "A" || x.notice_type == "F") && x.active_status == "A").OrderByDescending(x => x.project_date).Take(5).ToList();
            model.other_order_3d_list = lst23.ToList();

            //building Permission
            var lst24 = db.other_building_permission_bhogvata.Where(x => x.type == "B" && x.active_status == "A").OrderByDescending(x => x.proj_date).Take(5).ToList();
            model.other_building_permission_list = lst24.ToList();

            //SR3 4
            var lst25 = db.other_sr.Where(x => x.active_status == "A" && x.doc_name != "").OrderByDescending(x => x.id).Take(5).ToList();
            model.other_sr_list = lst25.ToList();

            //occupacy Permission
            var lst26 = db.other_building_permission_bhogvata.Where(x => x.type == "O" && x.active_status == "A").OrderByDescending(x => x.proj_date).Take(5).ToList();
            model.other_building_bhogvata_list = lst26.ToList();

            //Darpatrak
            var lst27 = db.other_darpatrak.Where(x => x.active_status == "A").OrderByDescending(x => x.due_date).Take(5).ToList();
            model.other_darpatrak_list = lst27.ToList();

            //Lottery lists
            //var lst1 = db.other_lottery.Where(x => x.active_status == "A").OrderByDescending(x => x.id).ToList();
            var lotteryList = db.other_lottery.Where(x => x.active_status == "A").OrderByDescending(x => x.flat_allotment_date).Take(5).ToList();
            model.other_lotteryList = lotteryList.ToList();

            return View(model);
        }
        public ActionResult template4()
        {
            if (Session["Lang"] == null)
                Session["Lang"] = "mr";

            shared_method();
            Session["Page"] = "Home";
            Session["Controller"] = "Home";

            homeModel model = new homeModel();
            mst_content_single obj = new mst_content_single();
            mst_single_image_pdf obj1 = new mst_single_image_pdf();

            //SRA Slogan in Middle
            obj = db.mst_content_single.Where(x => x.content_type == "Middle Line").FirstOrDefault();
            model.middleLine = obj.description;
            model.middleLine_eng = obj.description_eng;

            //Ceo Message
            obj = db.mst_content_single.Where(x => x.content_type == "CEO Message").FirstOrDefault();
            model.ceoMessage = obj.description;
            model.ceoMessage_eng = obj.description_eng;

            //Ceo Name
            obj = db.mst_content_single.Where(x => x.content_type == "CEO Name").FirstOrDefault();
            model.ceoName = obj.description;
            model.ceoName_eng = obj.description_eng;

            //ceo Photo
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "ceo photo").FirstOrDefault();
            model.ceoPhoto = obj1.doc_location + obj1.doc_name;

            ////Completed Project list
            //var lst_completed = db.projects.Where(x => x.project_type == "C" && x.active_status == "A");
            //model.completed_projectList = lst_completed.ToList();
            ////Running Project List
            //var lst_running = db.projects.Where(x => (x.project_type == "R" || x.project_type == "S") && x.active_status == "A");
            //model.running_projectList = lst_running.ToList();
            ////New Project List
            //var lst_new = db.projects.Where(x => x.project_type == "N" && x.active_status == "A");
            //model.new_projectList = lst_new.ToList();

            //Banner List
            var lst1 = db.mst_single_image_pdf.Where(x => x.content_type == "Banners" && x.active_status == "A");
            model.banner_List = lst1.ToList();

            //Honerable Ministers
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister1" && x.active_status == "A").FirstOrDefault();
            model.minister1_Name = obj1.title;
            model.minister1_Name_eng = obj1.title_eng;
            model.minister1_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister2" && x.active_status == "A").FirstOrDefault();
            model.minister2_Name = obj1.title;
            model.minister2_Name_eng = obj1.title_eng;
            model.minister2_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister3" && x.active_status == "A").FirstOrDefault();
            model.minister3_Name = obj1.title;
            model.minister3_Name_eng = obj1.title_eng;
            model.minister3_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister4" && x.active_status == "A").FirstOrDefault();
            model.minister4_Name = obj1.title;
            model.minister4_Name_eng = obj1.title_eng;
            model.minister4_Photo = obj1.doc_location + obj1.doc_name;



            mst_single_image_pdf obj11 = new mst_single_image_pdf();
            obj11 = db.mst_single_image_pdf.Where(x => x.content_type == "sra_map").FirstOrDefault();
            model.sra_map = obj11.doc_location + obj11.doc_name;


            //Darpatrak lists
            var lst_darpatrak = db.other_darpatrak.Where(x => x.type == "D").OrderByDescending(x => x.id).Take(5).ToList();
            model.other_darpatrakList = lst_darpatrak.ToList();

            ////Tender lists
            //var lst2 = db.other_darpatrak.Where(x => x.active_status == "A" && x.type == "T" && x.due_date_eng >= DateTime.Today).OrderByDescending(x => x.id).ToList();
            //model.other_tenderList = lst2.ToList();

            //Shashan Nirnay lists
            var lst_pmc = db.GovtResolutions.Where(x => x.active_status == "A").OrderByDescending(x => x.resolution_date).Take(5).ToList();
            model.GovtResolutionList = lst_pmc.ToList();


            //jahir prakatn 
            var lst_final_List_List = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "P" && (x.is_notice_list == "N" || x.is_notice_list == "L") && x.active_status == "A").OrderByDescending(x => x.plan_date_New).Take(5).ToList();
            model.final_List_List = lst_final_List_List.ToList();

            //patrata yadi list
            var lst21 = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "F" && (x.is_notice_list == "N" || x.is_notice_list == "L") && x.active_status == "A").OrderByDescending(x => x.plan_date_New).Take(5).ToList();
            model.final_List_2 = lst21.ToList();

            //3C list
            var lst22 = db.other_order_3c_3d.Where(x => x.order_type == "C" && (x.notice_type == "A" || x.notice_type == "F") && x.active_status == "A").OrderByDescending(x => x.project_date).Take(5).ToList();
            model.other_order_3c_list = lst22.ToList();

            //3D list
            var lst23 = db.other_order_3c_3d.Where(x => x.order_type == "D" && (x.notice_type == "A" || x.notice_type == "F") && x.active_status == "A").OrderByDescending(x => x.project_date).Take(5).ToList();
            model.other_order_3d_list = lst23.ToList();

            //building Permission
            var lst24 = db.other_building_permission_bhogvata.Where(x => x.type == "B" && x.active_status == "A").OrderByDescending(x => x.proj_date).Take(5).ToList();
            model.other_building_permission_list = lst24.ToList();

            //SR3 4
            var lst25 = db.other_sr.Where(x => x.active_status == "A" && x.doc_name != "").OrderByDescending(x => x.id).Take(5).ToList();
            model.other_sr_list = lst25.ToList();

            //occupacy Permission
            var lst26 = db.other_building_permission_bhogvata.Where(x => x.type == "O" && x.active_status == "A").OrderByDescending(x => x.proj_date).Take(5).ToList();
            model.other_building_bhogvata_list = lst26.ToList();

            //Darpatrak
            var lst27 = db.other_darpatrak.Where(x => x.active_status == "A").OrderByDescending(x => x.due_date).Take(5).ToList();
            model.other_darpatrak_list = lst27.ToList();

            //Lottery lists
            //var lst1 = db.other_lottery.Where(x => x.active_status == "A").OrderByDescending(x => x.id).ToList();
            var lotteryList = db.other_lottery.Where(x => x.active_status == "A").OrderByDescending(x => x.flat_allotment_date).Take(5).ToList();
            model.other_lotteryList = lotteryList.ToList();

            return View(model);
        }
        public ActionResult template5()
        {
            if (Session["Lang"] == null)
                Session["Lang"] = "mr";

            shared_method();
            Session["Page"] = "Home";
            Session["Controller"] = "Home";

            homeModel model = new homeModel();
            mst_content_single obj = new mst_content_single();
            mst_single_image_pdf obj1 = new mst_single_image_pdf();

            //SRA Slogan in Middle
            obj = db.mst_content_single.Where(x => x.content_type == "Middle Line").FirstOrDefault();
            model.middleLine = obj.description;
            model.middleLine_eng = obj.description_eng;

            //Ceo Message
            obj = db.mst_content_single.Where(x => x.content_type == "CEO Message").FirstOrDefault();
            model.ceoMessage = obj.description;
            model.ceoMessage_eng = obj.description_eng;

            //Ceo Name
            obj = db.mst_content_single.Where(x => x.content_type == "CEO Name").FirstOrDefault();
            model.ceoName = obj.description;
            model.ceoName_eng = obj.description_eng;

            //ceo Photo
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "ceo photo").FirstOrDefault();
            model.ceoPhoto = obj1.doc_location + obj1.doc_name;

            ////Completed Project list
            //var lst_completed = db.projects.Where(x => x.project_type == "C" && x.active_status == "A");
            //model.completed_projectList = lst_completed.ToList();
            ////Running Project List
            //var lst_running = db.projects.Where(x => (x.project_type == "R" || x.project_type == "S") && x.active_status == "A");
            //model.running_projectList = lst_running.ToList();
            ////New Project List
            //var lst_new = db.projects.Where(x => x.project_type == "N" && x.active_status == "A");
            //model.new_projectList = lst_new.ToList();

            //Banner List
            var lst1 = db.mst_single_image_pdf.Where(x => x.content_type == "Banners" && x.active_status == "A");
            model.banner_List = lst1.ToList();

            //Honerable Ministers
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister1" && x.active_status == "A").FirstOrDefault();
            model.minister1_Name = obj1.title;
            model.minister1_Name_eng = obj1.title_eng;
            model.minister1_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister2" && x.active_status == "A").FirstOrDefault();
            model.minister2_Name = obj1.title;
            model.minister2_Name_eng = obj1.title_eng;
            model.minister2_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister3" && x.active_status == "A").FirstOrDefault();
            model.minister3_Name = obj1.title;
            model.minister3_Name_eng = obj1.title_eng;
            model.minister3_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister4" && x.active_status == "A").FirstOrDefault();
            model.minister4_Name = obj1.title;
            model.minister4_Name_eng = obj1.title_eng;
            model.minister4_Photo = obj1.doc_location + obj1.doc_name;



            mst_single_image_pdf obj11 = new mst_single_image_pdf();
            obj11 = db.mst_single_image_pdf.Where(x => x.content_type == "sra_map").FirstOrDefault();
            model.sra_map = obj11.doc_location + obj11.doc_name;


            //Darpatrak lists
            var lst_darpatrak = db.other_darpatrak.Where(x => x.type == "D").OrderByDescending(x => x.id).Take(5).ToList();
            model.other_darpatrakList = lst_darpatrak.ToList();

            ////Tender lists
            //var lst2 = db.other_darpatrak.Where(x => x.active_status == "A" && x.type == "T" && x.due_date_eng >= DateTime.Today).OrderByDescending(x => x.id).ToList();
            //model.other_tenderList = lst2.ToList();

            //Shashan Nirnay lists
            var lst_pmc = db.GovtResolutions.Where(x => x.active_status == "A").OrderByDescending(x => x.resolution_date).Take(5).ToList();
            model.GovtResolutionList = lst_pmc.ToList();


            //jahir prakatn 
            var lst_final_List_List = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "P" && (x.is_notice_list == "N" || x.is_notice_list == "L") && x.active_status == "A").OrderByDescending(x => x.plan_date_New).Take(5).ToList();
            model.final_List_List = lst_final_List_List.ToList();

            //patrata yadi list
            var lst21 = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "F" && (x.is_notice_list == "N" || x.is_notice_list == "L") && x.active_status == "A").OrderByDescending(x => x.plan_date_New).Take(5).ToList();
            model.final_List_2 = lst21.ToList();

            //3C list
            var lst22 = db.other_order_3c_3d.Where(x => x.order_type == "C" && (x.notice_type == "A" || x.notice_type == "F") && x.active_status == "A").OrderByDescending(x => x.project_date).Take(5).ToList();
            model.other_order_3c_list = lst22.ToList();

            //3D list
            var lst23 = db.other_order_3c_3d.Where(x => x.order_type == "D" && (x.notice_type == "A" || x.notice_type == "F") && x.active_status == "A").OrderByDescending(x => x.project_date).Take(5).ToList();
            model.other_order_3d_list = lst23.ToList();

            //building Permission
            var lst24 = db.other_building_permission_bhogvata.Where(x => x.type == "B" && x.active_status == "A").OrderByDescending(x => x.proj_date).Take(5).ToList();
            model.other_building_permission_list = lst24.ToList();

            //SR3 4
            var lst25 = db.other_sr.Where(x => x.active_status == "A" && x.doc_name != "").OrderByDescending(x => x.id).Take(5).ToList();
            model.other_sr_list = lst25.ToList();

            //occupacy Permission
            var lst26 = db.other_building_permission_bhogvata.Where(x => x.type == "O" && x.active_status == "A").OrderByDescending(x => x.proj_date).Take(5).ToList();
            model.other_building_bhogvata_list = lst26.ToList();

            //Darpatrak
            var lst27 = db.other_darpatrak.Where(x => x.active_status == "A").OrderByDescending(x => x.due_date).Take(5).ToList();
            model.other_darpatrak_list = lst27.ToList();

            //Lottery lists
            //var lst1 = db.other_lottery.Where(x => x.active_status == "A").OrderByDescending(x => x.id).ToList();
            var lotteryList = db.other_lottery.Where(x => x.active_status == "A").OrderByDescending(x => x.flat_allotment_date).Take(5).ToList();
            model.other_lotteryList = lotteryList.ToList();

            return View(model);
        }
        public ActionResult Niyamavali1()
        {
            shared_method();
            Session["Page"] = "Niyamavali";
            Session["Controller"] = "Home";
            other_sra_rulesModel model = new other_sra_rulesModel();
            //SRA rules list / नियमावली 
            var lst1 = db.other_sra_rules.Where(x => x.active_status == "A").OrderByDescending(x => x.rule_date).ToList(); ;
            model.sra_rulesList = lst1.ToList();
            return View(model);
        }

        public ActionResult SRA3C1()
        {
            shared_method();
            Session["Page"] = "SRA3C";
            Session["Controller"] = "Home";
            other_order_3c_3dModel model = new other_order_3c_3dModel();
            //3C3D lists
            //var lst1 = db.other_order_3c_3d.Where(x =>  x.active_status == "A").OrderByDescending(x => x.id).ToList();
            var lst1 = db.other_order_3c_3d.Where(x => x.order_type == "C" && x.notice_type == "A" && x.active_status == "A").OrderByDescending(x => x.project_date).ToList();
            model.other_order_3c_primaryList = lst1.ToList();
            var lst2 = db.other_order_3c_3d.Where(x => x.order_type == "C" && x.notice_type == "F" && x.active_status == "A").OrderByDescending(x => x.project_date).ToList();
            model.other_order_3c_FinalList = lst2.ToList();
            //var lst3 = db.other_order_3c_3d.Where(x => x.order_type == "D" && x.notice_type == "A" && x.active_status == "A").OrderByDescending(x => x.project_date).ToList();
            //model.other_order_3d_primaryList = lst3.ToList();
            //var lst4 = db.other_order_3c_3d.Where(x => x.order_type == "D" && x.notice_type == "F" && x.active_status == "A").OrderByDescending(x => x.project_date).ToList();
            //model.other_order_3d_finalList = lst4.ToList();
            return View(model);
        }

        public ActionResult BuildingPermission1()
        {
            shared_method();
            Session["Page"] = "BuildingPermission1";
            Session["Controller"] = "Home";
            other_building_permission_bhogvataModel model = new other_building_permission_bhogvataModel();
            //Building Permission lists
            var lst1 = db.other_building_permission_bhogvata.Where(x => x.type == "B" && x.active_status == "A").OrderByDescending(x => x.id).Take(5).ToList();
            model.other_building_permission_list = lst1.ToList();
            ////Bhogvata lists
            //var lst2 = db.other_building_permission_bhogvata.Where(x => x.type == "O" && x.active_status == "A").OrderByDescending(x => x.id).ToList();
            //model.other_bhogvata_list = lst2.ToList();
            return View(model);
        }
        public ActionResult website()
        {
            return View();
        }

        public ActionResult Niyamavali2()
        {
            shared_method();
            Session["Page"] = "Niyamavali";
            Session["Controller"] = "Home";
            other_sra_rulesModel model = new other_sra_rulesModel();
            //SRA rules list / नियमावली 
            var lst1 = db.other_sra_rules.Where(x => x.active_status == "A").OrderByDescending(x => x.rule_date).ToList(); ;
            model.sra_rulesList = lst1.ToList();
            return View(model);
        }

        public ActionResult abstractCA()
        {
            shared_method();
            Session["Page"] = "abstractCA";
            Session["Controller"] = "Home";

            GroupedOrderCAModel model = new GroupedOrderCAModel();
            //CA1
            model.CA1_3C_1 = db.other_order_3c_3d.Where(x => x.order_type == "C" && x.notice_type == "A" && x.active_status == "A" && x.ca_dept_id==1).ToList().Count();
            model.CA1_3C_2 = db.other_order_3c_3d.Where(x => x.order_type == "C" && x.notice_type == "F" && x.active_status == "A" && x.ca_dept_id == 1).ToList().Count();
            model.CA1_3D_1 = db.other_order_3c_3d.Where(x => x.order_type == "D" && x.notice_type == "A" && x.active_status == "A" && x.ca_dept_id == 1).ToList().Count();
            model.CA1_3D_2 = db.other_order_3c_3d.Where(x => x.order_type == "D" && x.notice_type == "F" && x.active_status == "A" && x.ca_dept_id == 1).ToList().Count();
            model.CA1_yadi_1 = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "P" && x.is_notice_list == "L" && x.active_status == "A" && x.ca_dept_id == 1).ToList().Count(); 
            model.CA1_yadi_2 = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "F" && x.is_notice_list == "L" && x.active_status == "A" && x.ca_dept_id == 1).ToList().Count(); 

            //CA2
            model.CA2_3C_1 = db.other_order_3c_3d.Where(x => x.order_type == "C" && x.notice_type == "A" && x.active_status == "A" && x.ca_dept_id == 2).ToList().Count();
            model.CA2_3C_2 = db.other_order_3c_3d.Where(x => x.order_type == "C" && x.notice_type == "F" && x.active_status == "A" && x.ca_dept_id == 2).ToList().Count();
            model.CA2_3D_1 = db.other_order_3c_3d.Where(x => x.order_type == "D" && x.notice_type == "A" && x.active_status == "A" && x.ca_dept_id == 2).ToList().Count();
            model.CA2_3D_2 = db.other_order_3c_3d.Where(x => x.order_type == "D" && x.notice_type == "F" && x.active_status == "A" && x.ca_dept_id == 2).ToList().Count();
            model.CA2_yadi_1 = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "P" && x.is_notice_list == "L" && x.active_status == "A" && x.ca_dept_id == 2).ToList().Count(); 
            model.CA2_yadi_2 = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "F" && x.is_notice_list == "L" && x.active_status == "A" && x.ca_dept_id == 2).ToList().Count(); 

            //CA3
            model.CA3_3C_1 = db.other_order_3c_3d.Where(x => x.order_type == "C" && x.notice_type == "A" && x.active_status == "A" && x.ca_dept_id == 3).ToList().Count();
            model.CA3_3C_2 = db.other_order_3c_3d.Where(x => x.order_type == "C" && x.notice_type == "F" && x.active_status == "A" && x.ca_dept_id == 3).ToList().Count();
            model.CA3_3D_1 = db.other_order_3c_3d.Where(x => x.order_type == "D" && x.notice_type == "A" && x.active_status == "A" && x.ca_dept_id == 3).ToList().Count();
            model.CA3_3D_2 = db.other_order_3c_3d.Where(x => x.order_type == "D" && x.notice_type == "F" && x.active_status == "A" && x.ca_dept_id == 3).ToList().Count();
            model.CA3_yadi_1 = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "P" && x.is_notice_list == "L" && x.active_status == "A" && x.ca_dept_id == 3).ToList().Count(); 
            model.CA3_yadi_2 = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "F" && x.is_notice_list == "L" && x.active_status == "A" && x.ca_dept_id == 3).ToList().Count();


            //Tech1
            model.tech1_permission_1 = db.other_sr.Where(x => x.type == "P" && x.active_status == "A" && x.dept_id == 8).ToList().Count();
            model.tech1_permission_2 = db.other_sr.Where(x => x.type == "F" && x.active_status == "A" && x.dept_id == 8).ToList().Count();
            model.tech1_cc = db.other_building_permission_bhogvata.Where(x => x.type == "B" && x.active_status == "A" && x.dept_id == 8).ToList().Count();
            model.tech1_occup = db.other_building_permission_bhogvata.Where(x => x.type == "O" && x.active_status == "A" && x.dept_id == 8).ToList().Count();

            //Tech2
            model.tech2_permission_1 = db.other_sr.Where(x => x.type == "P" && x.active_status == "A" && x.dept_id == 9).ToList().Count();
            model.tech2_permission_2 = db.other_sr.Where(x => x.type == "F" && x.active_status == "A" && x.dept_id == 9).ToList().Count();
            model.tech2_cc = db.other_building_permission_bhogvata.Where(x => x.type == "B" && x.active_status == "A" && x.dept_id == 9).ToList().Count();
            model.tech2_occup = db.other_building_permission_bhogvata.Where(x => x.type == "O" && x.active_status == "A" && x.dept_id == 9).ToList().Count();

            //Tech3
            model.tech3_permission_1 = db.other_sr.Where(x => x.type == "P" && x.active_status == "A" && x.dept_id == 10).ToList().Count();
            model.tech3_permission_2 = db.other_sr.Where(x => x.type == "F" && x.active_status == "A" && x.dept_id == 10).ToList().Count();
            model.tech3_cc = db.other_building_permission_bhogvata.Where(x => x.type == "B" && x.active_status == "A" && x.dept_id == 10).ToList().Count();
            model.tech3_occup = db.other_building_permission_bhogvata.Where(x => x.type == "O" && x.active_status == "A" && x.dept_id == 10).ToList().Count();

            return View(model);
        }

        public ActionResult template7()
        {
            if (Session["Lang"] == null)
                Session["Lang"] = "mr";

            shared_method();
            Session["Page"] = "Home";
            Session["Controller"] = "Home";

            homeModel model = new homeModel();
            mst_content_single obj = new mst_content_single();
            mst_single_image_pdf obj1 = new mst_single_image_pdf();

            //SRA Slogan in Middle
            obj = db.mst_content_single.Where(x => x.content_type == "Middle Line").FirstOrDefault();
            model.middleLine = obj.description;
            model.middleLine_eng = obj.description_eng;

            //Ceo Message
            obj = db.mst_content_single.Where(x => x.content_type == "CEO Message").FirstOrDefault();
            model.ceoMessage = obj.description;
            model.ceoMessage_eng = obj.description_eng;

            //Ceo Name
            obj = db.mst_content_single.Where(x => x.content_type == "CEO Name").FirstOrDefault();
            model.ceoName = obj.description;
            model.ceoName_eng = obj.description_eng;

            //ceo Photo
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "ceo photo").FirstOrDefault();
            model.ceoPhoto = obj1.doc_location + obj1.doc_name;

            ////Completed Project list
            //var lst_completed = db.projects.Where(x => x.project_type == "C" && x.active_status == "A");
            //model.completed_projectList = lst_completed.ToList();
            ////Running Project List
            //var lst_running = db.projects.Where(x => (x.project_type == "R" || x.project_type == "S") && x.active_status == "A");
            //model.running_projectList = lst_running.ToList();
            ////New Project List
            //var lst_new = db.projects.Where(x => x.project_type == "N" && x.active_status == "A");
            //model.new_projectList = lst_new.ToList();

            //Banner List
            var lst1 = db.mst_single_image_pdf.Where(x => x.content_type == "Banners" && x.active_status == "A");
            model.banner_List = lst1.ToList();

            //Honerable Ministers
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister1" && x.active_status == "A").FirstOrDefault();
            model.minister1_Name = obj1.title;
            model.minister1_Name_eng = obj1.title_eng;
            model.minister1_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister2" && x.active_status == "A").FirstOrDefault();
            model.minister2_Name = obj1.title;
            model.minister2_Name_eng = obj1.title_eng;
            model.minister2_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister3" && x.active_status == "A").FirstOrDefault();
            model.minister3_Name = obj1.title;
            model.minister3_Name_eng = obj1.title_eng;
            model.minister3_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister4" && x.active_status == "A").FirstOrDefault();
            model.minister4_Name = obj1.title;
            model.minister4_Name_eng = obj1.title_eng;
            model.minister4_Photo = obj1.doc_location + obj1.doc_name;



            mst_single_image_pdf obj11 = new mst_single_image_pdf();
            obj11 = db.mst_single_image_pdf.Where(x => x.content_type == "sra_map").FirstOrDefault();
            model.sra_map = obj11.doc_location + obj11.doc_name;


            //Darpatrak lists
            var lst_darpatrak = db.other_darpatrak.Where(x => x.type == "D").OrderByDescending(x => x.id).Take(5).ToList();
            model.other_darpatrakList = lst_darpatrak.ToList();

            ////Tender lists
            //var lst2 = db.other_darpatrak.Where(x => x.active_status == "A" && x.type == "T" && x.due_date_eng >= DateTime.Today).OrderByDescending(x => x.id).ToList();
            //model.other_tenderList = lst2.ToList();

            //Shashan Nirnay lists
            var lst_pmc = db.GovtResolutions.Where(x => x.active_status == "A").OrderByDescending(x => x.resolution_date).Take(5).ToList();
            model.GovtResolutionList = lst_pmc.ToList();


            //jahir prakatn 
            var lst_final_List_List = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "P" && (x.is_notice_list == "N" || x.is_notice_list == "L") && x.active_status == "A").OrderByDescending(x => x.plan_date_New).Take(5).ToList();
            model.final_List_List = lst_final_List_List.ToList();

            //patrata yadi list
            var lst21 = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "F" && (x.is_notice_list == "N" || x.is_notice_list == "L") && x.active_status == "A").OrderByDescending(x => x.plan_date_New).Take(5).ToList();
            model.final_List_2 = lst21.ToList();

            //3C list
            var lst22 = db.other_order_3c_3d.Where(x => x.order_type == "C" && (x.notice_type == "A" || x.notice_type == "F") && x.active_status == "A").OrderByDescending(x => x.project_date).Take(5).ToList();
            model.other_order_3c_list = lst22.ToList();

            //3D list
            var lst23 = db.other_order_3c_3d.Where(x => x.order_type == "D" && (x.notice_type == "A" || x.notice_type == "F") && x.active_status == "A").OrderByDescending(x => x.project_date).Take(5).ToList();
            model.other_order_3d_list = lst23.ToList();

            //building Permission
            var lst24 = db.other_building_permission_bhogvata.Where(x => x.type == "B" && x.active_status == "A").OrderByDescending(x => x.proj_date).Take(5).ToList();
            model.other_building_permission_list = lst24.ToList();

            //SR3 4
            var lst25 = db.other_sr.Where(x => x.active_status == "A" && x.doc_name != "").OrderByDescending(x => x.id).Take(5).ToList();
            model.other_sr_list = lst25.ToList();

            //occupacy Permission
            var lst26 = db.other_building_permission_bhogvata.Where(x => x.type == "O" && x.active_status == "A").OrderByDescending(x => x.proj_date).Take(5).ToList();
            model.other_building_bhogvata_list = lst26.ToList();

            //Darpatrak
            var lst27 = db.other_darpatrak.Where(x => x.active_status == "A").OrderByDescending(x => x.due_date).Take(5).ToList();
            model.other_darpatrak_list = lst27.ToList();

            //Lottery lists
            //var lst1 = db.other_lottery.Where(x => x.active_status == "A").OrderByDescending(x => x.id).ToList();
            var lotteryList = db.other_lottery.Where(x => x.active_status == "A").OrderByDescending(x => x.flat_allotment_date).Take(5).ToList();
            model.other_lotteryList = lotteryList.ToList();

            return View(model);
        }

        public ActionResult template12()
        {
            if (Session["Lang"] == null)
                Session["Lang"] = "mr";

            shared_method();
            Session["Page"] = "Home";
            Session["Controller"] = "Home";

            homeModel model = new homeModel();
            mst_content_single obj = new mst_content_single();
            mst_single_image_pdf obj1 = new mst_single_image_pdf();

            //SRA Slogan in Middle
            obj = db.mst_content_single.Where(x => x.content_type == "Middle Line").FirstOrDefault();
            model.middleLine = obj.description;
            model.middleLine_eng = obj.description_eng;

            //Ceo Message
            obj = db.mst_content_single.Where(x => x.content_type == "CEO Message").FirstOrDefault();
            model.ceoMessage = obj.description;
            model.ceoMessage_eng = obj.description_eng;

            //Ceo Name
            obj = db.mst_content_single.Where(x => x.content_type == "CEO Name").FirstOrDefault();
            model.ceoName = obj.description;
            model.ceoName_eng = obj.description_eng;

            //ceo Photo
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "ceo photo").FirstOrDefault();
            model.ceoPhoto = obj1.doc_location + obj1.doc_name;

            ////Completed Project list
            //var lst_completed = db.projects.Where(x => x.project_type == "C" && x.active_status == "A");
            //model.completed_projectList = lst_completed.ToList();
            ////Running Project List
            //var lst_running = db.projects.Where(x => (x.project_type == "R" || x.project_type == "S") && x.active_status == "A");
            //model.running_projectList = lst_running.ToList();
            ////New Project List
            //var lst_new = db.projects.Where(x => x.project_type == "N" && x.active_status == "A");
            //model.new_projectList = lst_new.ToList();

            //Banner List
            var lst1 = db.mst_single_image_pdf.Where(x => x.content_type == "Banners" && x.active_status == "A");
            model.banner_List = lst1.ToList();

            //Honerable Ministers
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister1" && x.active_status == "A").FirstOrDefault();
            model.minister1_Name = obj1.title;
            model.minister1_Name_eng = obj1.title_eng;
            model.minister1_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister2" && x.active_status == "A").FirstOrDefault();
            model.minister2_Name = obj1.title;
            model.minister2_Name_eng = obj1.title_eng;
            model.minister2_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister3" && x.active_status == "A").FirstOrDefault();
            model.minister3_Name = obj1.title;
            model.minister3_Name_eng = obj1.title_eng;
            model.minister3_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister4" && x.active_status == "A").FirstOrDefault();
            model.minister4_Name = obj1.title;
            model.minister4_Name_eng = obj1.title_eng;
            model.minister4_Photo = obj1.doc_location + obj1.doc_name;



            mst_single_image_pdf obj11 = new mst_single_image_pdf();
            obj11 = db.mst_single_image_pdf.Where(x => x.content_type == "sra_map").FirstOrDefault();
            model.sra_map = obj11.doc_location + obj11.doc_name;


            //Darpatrak lists
            var lst_darpatrak = db.other_darpatrak.Where(x => x.type == "D").OrderByDescending(x => x.id).Take(5).ToList();
            model.other_darpatrakList = lst_darpatrak.ToList();

            ////Tender lists
            //var lst2 = db.other_darpatrak.Where(x => x.active_status == "A" && x.type == "T" && x.due_date_eng >= DateTime.Today).OrderByDescending(x => x.id).ToList();
            //model.other_tenderList = lst2.ToList();

            //Shashan Nirnay lists
            var lst_pmc = db.GovtResolutions.Where(x => x.active_status == "A").OrderByDescending(x => x.resolution_date).Take(5).ToList();
            model.GovtResolutionList = lst_pmc.ToList();


            //jahir prakatn 
            var lst_final_List_List = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "P" && (x.is_notice_list == "N" || x.is_notice_list == "L") && x.active_status == "A").OrderByDescending(x => x.plan_date_New).Take(5).ToList();
            model.final_List_List = lst_final_List_List.ToList();

            //patrata yadi list
            var lst21 = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "F" && (x.is_notice_list == "N" || x.is_notice_list == "L") && x.active_status == "A").OrderByDescending(x => x.plan_date_New).Take(5).ToList();
            model.final_List_2 = lst21.ToList();

            //3C list
            var lst22 = db.other_order_3c_3d.Where(x => x.order_type == "C" && (x.notice_type == "A" || x.notice_type == "F") && x.active_status == "A").OrderByDescending(x => x.project_date).Take(5).ToList();
            model.other_order_3c_list = lst22.ToList();

            //3D list
            var lst23 = db.other_order_3c_3d.Where(x => x.order_type == "D" && (x.notice_type == "A" || x.notice_type == "F") && x.active_status == "A").OrderByDescending(x => x.project_date).Take(5).ToList();
            model.other_order_3d_list = lst23.ToList();

            //building Permission
            var lst24 = db.other_building_permission_bhogvata.Where(x => x.type == "B" && x.active_status == "A").OrderByDescending(x => x.proj_date).Take(5).ToList();
            model.other_building_permission_list = lst24.ToList();

            //SR3 4
            var lst25 = db.other_sr.Where(x => x.active_status == "A" && x.doc_name != "").OrderByDescending(x => x.id).Take(5).ToList();
            model.other_sr_list = lst25.ToList();

            //occupacy Permission
            var lst26 = db.other_building_permission_bhogvata.Where(x => x.type == "O" && x.active_status == "A").OrderByDescending(x => x.proj_date).Take(5).ToList();
            model.other_building_bhogvata_list = lst26.ToList();

            //Darpatrak
            var lst27 = db.other_darpatrak.Where(x => x.active_status == "A").OrderByDescending(x => x.due_date).Take(5).ToList();
            model.other_darpatrak_list = lst27.ToList();

            //Lottery lists
            //var lst1 = db.other_lottery.Where(x => x.active_status == "A").OrderByDescending(x => x.id).ToList();
            var lotteryList = db.other_lottery.Where(x => x.active_status == "A").OrderByDescending(x => x.flat_allotment_date).Take(5).ToList();
            model.other_lotteryList = lotteryList.ToList();

            return View(model);
        }

        public ActionResult template11()
        {
            if (Session["Lang"] == null)
                Session["Lang"] = "mr";

            shared_method();
            Session["Page"] = "Home";
            Session["Controller"] = "Home";

            homeModel model = new homeModel();
            mst_content_single obj = new mst_content_single();
            mst_single_image_pdf obj1 = new mst_single_image_pdf();

            //SRA Slogan in Middle
            obj = db.mst_content_single.Where(x => x.content_type == "Middle Line").FirstOrDefault();
            model.middleLine = obj.description;
            model.middleLine_eng = obj.description_eng;

            //Ceo Message
            obj = db.mst_content_single.Where(x => x.content_type == "CEO Message").FirstOrDefault();
            model.ceoMessage = obj.description;
            model.ceoMessage_eng = obj.description_eng;

            //Ceo Name
            obj = db.mst_content_single.Where(x => x.content_type == "CEO Name").FirstOrDefault();
            model.ceoName = obj.description;
            model.ceoName_eng = obj.description_eng;

            //ceo Photo
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "ceo photo").FirstOrDefault();
            model.ceoPhoto = obj1.doc_location + obj1.doc_name;

            ////Completed Project list
            //var lst_completed = db.projects.Where(x => x.project_type == "C" && x.active_status == "A");
            //model.completed_projectList = lst_completed.ToList();
            ////Running Project List
            //var lst_running = db.projects.Where(x => (x.project_type == "R" || x.project_type == "S") && x.active_status == "A");
            //model.running_projectList = lst_running.ToList();
            ////New Project List
            //var lst_new = db.projects.Where(x => x.project_type == "N" && x.active_status == "A");
            //model.new_projectList = lst_new.ToList();

            //Banner List
            var lst1 = db.mst_single_image_pdf.Where(x => x.content_type == "Banners" && x.active_status == "A");
            model.banner_List = lst1.ToList();

            //Honerable Ministers
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister1" && x.active_status == "A").FirstOrDefault();
            model.minister1_Name = obj1.title;
            model.minister1_Name_eng = obj1.title_eng;
            model.minister1_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister2" && x.active_status == "A").FirstOrDefault();
            model.minister2_Name = obj1.title;
            model.minister2_Name_eng = obj1.title_eng;
            model.minister2_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister3" && x.active_status == "A").FirstOrDefault();
            model.minister3_Name = obj1.title;
            model.minister3_Name_eng = obj1.title_eng;
            model.minister3_Photo = obj1.doc_location + obj1.doc_name;
            obj1 = db.mst_single_image_pdf.Where(x => x.content_type == "Minister4" && x.active_status == "A").FirstOrDefault();
            model.minister4_Name = obj1.title;
            model.minister4_Name_eng = obj1.title_eng;
            model.minister4_Photo = obj1.doc_location + obj1.doc_name;



            mst_single_image_pdf obj11 = new mst_single_image_pdf();
            obj11 = db.mst_single_image_pdf.Where(x => x.content_type == "sra_map").FirstOrDefault();
            model.sra_map = obj11.doc_location + obj11.doc_name;


            //Darpatrak lists
            var lst_darpatrak = db.other_darpatrak.Where(x => x.type == "D").OrderByDescending(x => x.id).Take(5).ToList();
            model.other_darpatrakList = lst_darpatrak.ToList();

            ////Tender lists
            //var lst2 = db.other_darpatrak.Where(x => x.active_status == "A" && x.type == "T" && x.due_date_eng >= DateTime.Today).OrderByDescending(x => x.id).ToList();
            //model.other_tenderList = lst2.ToList();

            //Shashan Nirnay lists
            var lst_pmc = db.GovtResolutions.Where(x => x.active_status == "A").OrderByDescending(x => x.resolution_date).Take(5).ToList();
            model.GovtResolutionList = lst_pmc.ToList();


            //jahir prakatn 
            var lst_final_List_List = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "P" && (x.is_notice_list == "N" || x.is_notice_list == "L") && x.active_status == "A").OrderByDescending(x => x.plan_date_New).Take(5).ToList();
            model.final_List_List = lst_final_List_List.ToList();

            //patrata yadi list
            var lst21 = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "F" && (x.is_notice_list == "N" || x.is_notice_list == "L") && x.active_status == "A").OrderByDescending(x => x.plan_date_New).Take(5).ToList();
            model.final_List_2 = lst21.ToList();

            //3C list
            var lst22 = db.other_order_3c_3d.Where(x => x.order_type == "C" && (x.notice_type == "A" || x.notice_type == "F") && x.active_status == "A").OrderByDescending(x => x.project_date).Take(5).ToList();
            model.other_order_3c_list = lst22.ToList();

            //3D list
            var lst23 = db.other_order_3c_3d.Where(x => x.order_type == "D" && (x.notice_type == "A" || x.notice_type == "F") && x.active_status == "A").OrderByDescending(x => x.project_date).Take(5).ToList();
            model.other_order_3d_list = lst23.ToList();

            //building Permission
            var lst24 = db.other_building_permission_bhogvata.Where(x => x.type == "B" && x.active_status == "A").OrderByDescending(x => x.proj_date).Take(5).ToList();
            model.other_building_permission_list = lst24.ToList();

            //SR3 4
            var lst25 = db.other_sr.Where(x => x.active_status == "A" && x.doc_name != "").OrderByDescending(x => x.id).Take(5).ToList();
            model.other_sr_list = lst25.ToList();

            //occupacy Permission
            var lst26 = db.other_building_permission_bhogvata.Where(x => x.type == "O" && x.active_status == "A").OrderByDescending(x => x.proj_date).Take(5).ToList();
            model.other_building_bhogvata_list = lst26.ToList();

            //Darpatrak
            var lst27 = db.other_darpatrak.Where(x => x.active_status == "A").OrderByDescending(x => x.due_date).Take(5).ToList();
            model.other_darpatrak_list = lst27.ToList();

            //Lottery lists
            //var lst1 = db.other_lottery.Where(x => x.active_status == "A").OrderByDescending(x => x.id).ToList();
            var lotteryList = db.other_lottery.Where(x => x.active_status == "A").OrderByDescending(x => x.flat_allotment_date).Take(5).ToList();
            model.other_lotteryList = lotteryList.ToList();

            return View(model);
        }


        //Tabs

        public ActionResult Yojana()
        {
            shared_method();
            Session["Page"] = "Yojana";
            projectModel model = new projectModel();

            //झोपडपट्टी पुनर्वसन योजनांचा गोषवारा
            var lst5 = db.about.Where(x => x.type == "table5").ToList();
            model.slumPlanTableList = lst5.ToList();

            int val1 = db.project_new.Where(x => x.active_status == "A").ToList().Count;
            int val2 = Convert.ToInt32(db.project_new.Where(x => x.active_status == "A").Sum(x => x.total_huts));
            int val3 = db.project_new.Where(x => x.project_status == 1 && x.active_status == "A").ToList().Count;
            int val4 = Convert.ToInt32(db.project_new.Where(x => x.project_status == 1 && x.active_status == "A").Sum(x => x.total_huts));
            int val5 = db.project_new.Where(x => x.project_status == 2 && x.active_status == "A").ToList().Count;
            int val6 = Convert.ToInt32(db.project_new.Where(x => x.project_status == 2 && x.active_status == "A").Sum(x => x.total_huts));
            int val7 = val1 - val3 - val5;
            int val8 = val2 - val4 - val6;
            int val9 = db.project_new.Where(x => x.project_status == 3 && x.active_status == "A").ToList().Count;
            int val10 = Convert.ToInt32(db.project_new.Where(x => x.project_status == 3 && x.active_status == "A").Sum(x => x.total_huts));
            int val11 = db.project_new.Where(x => x.project_status == 4 && x.active_status == "A").ToList().Count;
            int val12 = Convert.ToInt32(db.project_new.Where(x => x.project_status == 4 && x.active_status == "A").Sum(x => x.total_huts));
            int val13 = db.project_new.Where(x => x.project_status == 5 && x.active_status == "A").ToList().Count;
            int val14 = Convert.ToInt32(db.project_new.Where(x => x.project_status == 5 && x.active_status == "A").Sum(x => x.total_huts));
            int val15 = db.project_new.Where(x => x.project_status == 6 && x.active_status == "A").ToList().Count;
            int val16 = Convert.ToInt32(db.project_new.Where(x => x.project_status == 6 && x.active_status == "A").Sum(x => x.total_huts));
            int val17 = db.project_new.Where(x => x.project_status == 7 && x.active_status == "A").ToList().Count;
            int val18 = Convert.ToInt32(db.project_new.Where(x => x.project_status == 7 && x.active_status == "A").Sum(x => x.total_huts));
            int val19 = db.project_new.Where(x => x.project_status == 8 && x.active_status == "A").ToList().Count;
            int val20 = Convert.ToInt32(db.project_new.Where(x => x.project_status == 8 && x.active_status == "A").Sum(x => x.total_huts));
            int val21 = db.project_new.Where(x => x.project_status == 9 && x.active_status == "A").ToList().Count;
            int val22 = Convert.ToInt32(db.project_new.Where(x => x.project_status == 9 && x.active_status == "A").Sum(x => x.total_huts));
            int val23 = db.project_new.Where(x => x.project_status == 10 && x.active_status == "A").ToList().Count;
            int val24 = Convert.ToInt32(db.project_new.Where(x => x.project_status == 10 && x.active_status == "A").Sum(x => x.total_huts));
            int val25 = db.project_new.Where(x => x.project_status == 11 && x.active_status == "A").ToList().Count;
            int val26 = Convert.ToInt32(db.project_new.Where(x => x.project_status == 11 && x.active_status == "A").Sum(x => x.total_huts));
            int val27 = db.project_new.Where(x => x.project_status == 12 && x.active_status == "A").ToList().Count;
            int val28 = Convert.ToInt32(db.project_new.Where(x => x.project_status == 12 && x.active_status == "A").Sum(x => x.total_huts));
            int val29 = db.project_new.Where(x => x.project_status == 13 && x.active_status == "A").ToList().Count;
            int val30 = Convert.ToInt32(db.project_new.Where(x => x.project_status == 13 && x.active_status == "A").Sum(x => x.total_huts));
            int val31 = db.project_new.Where(x => x.project_status == 14 && x.active_status == "A").ToList().Count;
            int val32 = Convert.ToInt32(db.project_new.Where(x => x.project_status == 14 && x.active_status == "A").Sum(x => x.total_huts));
            int val33 = val11 + val13 + val15 + val17 + val19;
            int val34 = val12 + val14 + val16 + val18 + val20;
            int val35 = val21 + val23 + val25 + val27 + val29;
            int val36 = val22 + val24 + val26 + val28 + val30;
            int val37 = val7 - val31;
            int val38 = val8 - val32;
            int val39 = db.project_new.Where(x => x.active_status == "A" && x.project_status == 2).ToList().Count;//61;//दप्तरी दाखल 
            int val40 = val1;//Total Yojna 
            int val41 = val2;//Total Huts
            int val42 = db.project_new.Where(x => (x.project_status1.project_type == "S" || x.project_status1.project_type == "R") && x.active_status == "A").ToList().Count;
            int val43 = Convert.ToInt32(db.project_new.Where(x => (x.project_status1.project_type == "S" || x.project_status1.project_type == "R") && x.active_status == "A").Sum(x => x.total_huts));

            int val46 = Convert.ToInt32(db.project_new.Where(x => x.project_status == 2 && x.active_status == "A").Sum(x => x.total_huts)); // 23778;//दप्तरी दाखल 
            int val44 = val31 + val42 + val39;//Total Huts
            int val45 = val32 + val43 + val46;//Total Huts

            int val47 = val11 + val13 + val15 + val17 + val19; // Total Schema सक्षम प्राधिकारी विभाग
            int val48 = val12 + val14 + val16 + val18 + val20; //Total huts सक्षम प्राधिकारी विभाग

            int val49 = val21 + val23 + val25;// Total Schema नगर रचना विभाग
            int val50 = val22 + val24 + val26;//Total huts नगर रचना विभाग

            int val51 = db.project_new.Where(x => x.project_status == 15 && x.active_status == "A").ToList().Count;
            int val52 = Convert.ToInt32(db.project_new.Where(x => x.project_status == 15 && x.active_status == "A").Sum(x => x.total_huts));

            int val53 = val27 + val29 + val51;// Total Schema बांधकाम  विभाग
            int val54 = val28 + val30 + val52;// Total huts बांधकाम  विभाग

            int[] my_array = new int[] { val1, val2,val3, val4, val5, val6, val7, val8, val9, val10
            , val11, val12, val13, val14, val15, val16, val17, val18, val19, val20
            , val21, val22, val23, val24, val25, val26, val27, val28, val29, val30, val31, val32,val33
            ,val34,val35,val36,val37,val38,val39,val40,val41,val42,val43,val44,val45,val46,val47,val48
            ,val49,val50,val51,val52,val53,val54};

            ViewBag.schems = my_array;


            //Archived Project list
            var lst_archived = db.project_new.Where(x => x.project_status1.project_type == "A" && x.active_status == "A").OrderBy(x => x.Master_no).ToList();
            model.archieved_projectList = lst_archived.ToList();

            //Completed Project list
            //var lst_completed = db.project_new.Where(x => x.project_type == "C" && x.active_status == "A").OrderByDescending(x => x.id).ToList();
            //var lst_completed = db.project_new.Where(x => x.project_status1.project_type == "C" && x.active_status == "A").OrderByDescending(x => x.submission_date).ToList();
            var lst_completed = db.project_new.Where(x => x.project_status1.project_type == "C" && x.active_status == "A").OrderBy(x => x.Master_no).ToList();
            model.completed_projectList1 = lst_completed.ToList();
            //Running Project List
            //var lst_running = db.project_new.Where(x => x.project_type == "R" && x.active_status == "A").OrderByDescending(x => x.id).ToList();
            //var lst_running = db.project_new.Where(x => x.project_status1.project_type != "C" && x.project_status1.project_type != "N" && x.active_status == "A").OrderByDescending(x => x.submission_date).ToList();
            var lst_running = db.project_new.Where(x => x.project_status1.project_type != "A" && x.project_status1.project_type != "C" && x.project_status1.project_type != "N" && x.active_status == "A").OrderBy(x => x.Master_no).ToList();
            model.running_projectList1 = lst_running.ToList();
            ////Running Project List
            ////var lst_running = db.project_new.Where(x => x.project_type == "R" && x.active_status == "A").OrderByDescending(x => x.id).ToList();
            //var lst_running1 = db.project_new.Where(x => x.project_status1.project_type == "S" && x.active_status == "A").ToList();
            //model.running_pending_projectList1 = lst_running.ToList();
            //New Project List
            //var lst_new = db.project_new.Where(x => x.project_type == "N" && x.active_status == "A").OrderByDescending(x => x.id).ToList();
            var lst_new = db.project_new.Where(x => x.project_status1.project_type == "N" && x.active_status == "A").OrderByDescending(x => x.submission_date).ToList();
            model.new_projectList1 = lst_new.ToList();

            return View(model);
        }
        public ActionResult JahirPrakatan()
        {
            shared_method();
            Session["Page"] = "JahirPrakatan";
            other_jahirPrakatanModel model = new other_jahirPrakatanModel();
            //other_jahir_prakatan_Preliminary_survey
            var lst_jahirPrakatanPriliminary = db.other_jahir_prakatan_Preliminary_survey.Where(x => x.active_status == "A").OrderByDescending(x => x.upload_date).ToList(); ;
            model.OtherJahirPrakatanList = lst_jahirPrakatanPriliminary.ToList();

            //primary notice list
            var lst_primary_Notice = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "P" && x.is_notice_list == "N" && x.active_status == "A").OrderByDescending(x => x.plan_date_New).ToList(); ;
            model.primary_Notice_List = lst_primary_Notice.ToList();

            //primary list list
            var lst_primary_List = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "P" && x.is_notice_list == "L" && x.active_status == "A").OrderByDescending(x => x.plan_date_New).ToList(); ;
            model.primary_List_List = lst_primary_List.ToList();

            //final notice list
            var lst_final_Notice = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "F" && x.is_notice_list == "N" && x.active_status == "A").OrderByDescending(x => x.plan_date_New).ToList(); ;
            model.final_Notice_List = lst_final_Notice.ToList();

            //final list list
            var lst_final_List_List = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "F" && x.is_notice_list == "L" && x.active_status == "A").OrderByDescending(x => x.plan_date_New).ToList(); ;
            model.final_List_List = lst_final_List_List.ToList();
            return View(model);
        }
        public ActionResult SRA3C()
        {
            shared_method();
            Session["Page"] = "SRA3C";
            other_order_3c_3dModel model = new other_order_3c_3dModel();
            //3C3D lists
            //var lst1 = db.other_order_3c_3d.Where(x =>  x.active_status == "A").OrderByDescending(x => x.id).ToList();
            var lst1 = db.other_order_3c_3d.Where(x => x.order_type == "C" && x.notice_type == "A" && x.active_status == "A").OrderByDescending(x => x.project_date).ToList();
            model.other_order_3c_primaryList = lst1.ToList();
            var lst2 = db.other_order_3c_3d.Where(x => x.order_type == "C" && x.notice_type == "F" && x.active_status == "A").OrderByDescending(x => x.project_date).ToList();
            model.other_order_3c_FinalList = lst2.ToList();
            //var lst3 = db.other_order_3c_3d.Where(x => x.order_type == "D" && x.notice_type == "A" && x.active_status == "A").OrderByDescending(x => x.project_date).ToList();
            //model.other_order_3d_primaryList = lst3.ToList();
            //var lst4 = db.other_order_3c_3d.Where(x => x.order_type == "D" && x.notice_type == "F" && x.active_status == "A").OrderByDescending(x => x.project_date).ToList();
            //model.other_order_3d_finalList = lst4.ToList();
            return View(model);
        }
        public ActionResult PatrataYadi()
        {
            shared_method();
            Session["Page"] = "PatrataYadi";
            other_jahirPrakatanModel model = new other_jahirPrakatanModel();
            //other_jahir_prakatan_Preliminary_survey
            var lst_jahirPrakatanPriliminary = db.other_jahir_prakatan_Preliminary_survey.Where(x => x.active_status == "A").OrderByDescending(x => x.upload_date).ToList(); ;
            model.OtherJahirPrakatanList = lst_jahirPrakatanPriliminary.ToList();

            //primary notice list
            var lst_primary_Notice = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "P" && x.is_notice_list == "N" && x.active_status == "A").OrderByDescending(x => x.plan_date_New).ToList(); ;
            model.primary_Notice_List = lst_primary_Notice.ToList();

            //primary list list
            var lst_primary_List = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "P" && x.is_notice_list == "L" && x.active_status == "A").OrderByDescending(x => x.plan_date_New).ToList(); ;
            model.primary_List_List = lst_primary_List.ToList();

            //final notice list
            var lst_final_Notice = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "F" && x.is_notice_list == "N" && x.active_status == "A").OrderByDescending(x => x.plan_date_New).ToList(); ;
            model.final_Notice_List = lst_final_Notice.ToList();

            //final list list
            var lst_final_List_List = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "F" && x.is_notice_list == "L" && x.active_status == "A").OrderByDescending(x => x.plan_date_New).ToList(); ;
            model.final_List_List = lst_final_List_List.ToList();
            return View(model);
        }
        public ActionResult SR4()
        {
            shared_method();
            Session["Page"] = "SR4";
            other_srModel model = new other_srModel();
            //SR 4(3) 4(5) lists
            //var lst1 = db.other_sr.Where(x => x.active_status == "A" &&  x.doc_name != "").OrderByDescending(x => x.id).ToList();
            var lst1 = db.other_sr.Where(x => x.active_status == "A" && x.type == "P").OrderByDescending(x => x.subject_date).ToList();
            model.other_srList = lst1.ToList();
            //var lst2 = db.other_sr.Where(x => x.active_status == "A" &&  x.doc_name1 !="" ).OrderByDescending(x => x.id).ToList();
            var lst2 = db.other_sr.Where(x => x.active_status == "A" && x.type == "F").OrderByDescending(x => x.subject_date).ToList();
            model.other_srList2 = lst2.ToList();
            return View(model);
        }
        public ActionResult SRA3D()
        {
            shared_method();
            Session["Page"] = "SRA3D";
            other_order_3c_3dModel model = new other_order_3c_3dModel();
            //3C3D lists
            //var lst1 = db.other_order_3c_3d.Where(x =>  x.active_status == "A").OrderByDescending(x => x.id).ToList();
            //var lst1 = db.other_order_3c_3d.Where(x => x.order_type == "C" && x.notice_type == "A" && x.active_status == "A").OrderByDescending(x => x.project_date).ToList();
            //model.other_order_3c_primaryList = lst1.ToList();
            //var lst2 = db.other_order_3c_3d.Where(x => x.order_type == "C" && x.notice_type == "F" && x.active_status == "A").OrderByDescending(x => x.project_date).ToList();
            //model.other_order_3c_FinalList = lst2.ToList();
            var lst3 = db.other_order_3c_3d.Where(x => x.order_type == "D" && x.notice_type == "A" && x.active_status == "A").OrderByDescending(x => x.project_date).ToList();
            model.other_order_3d_primaryList = lst3.ToList();
            var lst4 = db.other_order_3c_3d.Where(x => x.order_type == "D" && x.notice_type == "F" && x.active_status == "A").OrderByDescending(x => x.project_date).ToList();
            model.other_order_3d_finalList = lst4.ToList();
            return View(model);
        }
        public ActionResult Darpatrak()
        {
            shared_method();
            Session["Page"] = "Darpatrak";
            other_darpatrakModel model = new other_darpatrakModel();
            //Darpatrak lists
            var lst1 = db.other_darpatrak.Where(x => x.active_status == "A" && x.type == "D" && x.due_date_eng >= DateTime.Today).OrderByDescending(x => x.id).ToList();
            model.other_darpatrakList = lst1.ToList();

            //Tender lists
            var lst2 = db.other_darpatrak.Where(x => x.active_status == "A" && x.type == "T" && x.due_date_eng >= DateTime.Today).OrderByDescending(x => x.id).ToList();
            model.other_tenderList = lst2.ToList();
            return View(model);
        }
        public ActionResult SlumList()
        {
            shared_method();
            Session["Page"] = "SlumList";
            homeModel model = new homeModel();
            //PMC Slum list
            //var lst_pmc = db.slums.Where(x => x.pmc_pcmc == "PMC" && x.active_status == "A").OrderByDescending(x => x.id).ToList(); ;
            var lst_pmc = db.slums.Where(x => x.pmc_pcmc == "PMC" && x.active_status == "A").OrderBy(x => x.ward).ToList(); ;
            model.pmc_slumList = lst_pmc.ToList();

            //PCMC Slum list
            //var lst_pcmc = db.slums.Where(x => x.pmc_pcmc == "PCMC" && x.active_status == "A").OrderByDescending(x => x.id).ToList(); ;
            var lst_pcmc = db.slums.Where(x => x.pmc_pcmc == "PCMC" && x.active_status == "A").OrderBy(x => x.ward).ToList(); ;
            model.pcmc_slumList = lst_pcmc.ToList();

            ViewBag.totalSlums = model.pcmc_slumList.Count + model.pmc_slumList.Count;

            return View(model);
        }

        //Single Tables

        public ActionResult VarshikAhaval()
        {
            shared_method();
            Session["Page"] = "VarshikAhaval";
            mst_single_image_pdfModel model = new mst_single_image_pdfModel();
            //वार्षिक लेखा अहवाल lists
            var lst1 = db.mst_single_image_pdf.Where(x => x.content_type == "AnnualReport" && x.active_status == "A").OrderByDescending(x => x.id).ToList();
            model.lstContentType = lst1.ToList();
            return View(model);
        }
        public ActionResult Paripatrak()
        {
            shared_method();
            Session["Page"] = "Paripatrak";
            other_paripatrakModel model = new other_paripatrakModel();
            //Paripatrake lists
            //var lst1 = db.other_paripatrak.Where(x => x.active_status == "A").OrderByDescending(x => x.id).ToList();
            var lst1 = db.other_paripatrak.Where(x => x.active_status == "A").OrderByDescending(x => x.subject_date).ToList();
            model.other_paripatrakList = lst1.ToList();
            return View(model);
        }
        public ActionResult ShasanNirnay()
        {
            shared_method();
            Session["Page"] = "ShasanNirnay";
            govResolutionsModel model = new govResolutionsModel();
            //PMC Slum list
            //var lst_pmc = db.GovtResolutions.Where(x => x.active_status == "A").OrderByDescending(x=>x.id);
            var lst_pmc = db.GovtResolutions.Where(x => x.active_status == "A").OrderByDescending(x => x.resolution_date).ToList();
            model.GovtResolutionList = lst_pmc.ToList();
            return View(model);
        }
        public ActionResult RTI_4C()
        {
            shared_method();
            Session["Page"] = "RTI_4C";
            other_rti_4cModel model = new other_rti_4cModel();
            var lst1 = db.other_rti_4c.Where(x => x.active_status == "A").OrderByDescending(x => x.id).ToList();
            model.rti_4cList = lst1.ToList();
            return View(model);
        }
        public ActionResult Niyamavali()
        {
            shared_method();
            Session["Page"] = "Niyamavali";
            other_sra_rulesModel model = new other_sra_rulesModel();
            //SRA rules list / नियमावली 
            var lst1 = db.other_sra_rules.Where(x => x.active_status == "A").OrderByDescending(x => x.rule_date).ToList(); ;
            model.sra_rulesList = lst1.ToList();
            return View(model);
        }
        public ActionResult VividhNamune()
        {
            shared_method();
            Session["Page"] = "VividhNamune";
            other_vividh_namuneModel model = new other_vividh_namuneModel();
            var lst1 = db.other_vividh_namune.Where(x => x.active_status == "A").OrderByDescending(x => x.id).ToList();
            model.vividhNamuneList = lst1.ToList();
            return View(model);
        }
        public ActionResult VikasakList()
        {
            shared_method();
            Session["Page"] = "VikasakList";
            other_developerModel model = new other_developerModel();
            //Developer list
            //var lst_dev = db.other_developers.Where(x => x.active_status == "A").OrderByDescending(x => x.id).ToList(); 
            var lst_dev = db.other_developers.Where(x => x.active_status == "A").OrderByDescending(x => x.dev_registration_date).ToList();

            model.developerList = lst_dev.ToList();
            return View(model);
        }
        public ActionResult BuildingPermission()
        {
            shared_method();
            Session["Page"] = "BuildingPermission";
            other_building_permission_bhogvataModel model = new other_building_permission_bhogvataModel();
            //Building Permission lists
            var lst1 = db.other_building_permission_bhogvata.Where(x => x.type == "B" && x.active_status == "A").OrderByDescending(x => x.id).ToList();
            model.other_building_permission_list = lst1.ToList();
            ////Bhogvata lists
            //var lst2 = db.other_building_permission_bhogvata.Where(x => x.type == "O" && x.active_status == "A").OrderByDescending(x => x.id).ToList();
            //model.other_bhogvata_list = lst2.ToList();
            return View(model);
        }
        public ActionResult Occupancy()
        {
            shared_method();
            Session["Page"] = "Occupancy";
            other_building_permission_bhogvataModel model = new other_building_permission_bhogvataModel();
            ////Building Permission lists
            //var lst1 = db.other_building_permission_bhogvata.Where(x => x.type == "B" && x.active_status == "A").OrderByDescending(x => x.id).ToList();
            //model.other_building_permission_list = lst1.ToList();
            //Bhogvata lists
            var lst2 = db.other_building_permission_bhogvata.Where(x => x.type == "O" && x.active_status == "A").OrderByDescending(x => x.id).ToList();
            model.other_bhogvata_list = lst2.ToList();
            return View(model);
        }
        public ActionResult Society()
        {
            shared_method();
            Session["Page"] = "Society";
            societyModel model = new societyModel();
            var lst1 = db.society.Where(x => x.active_status == "A").OrderByDescending(x => x.soc_registration_dt).ToList();
            model.society_List = lst1.ToList();
            return View(model);
        }
        public ActionResult Lottery()
        {
            shared_method();
            Session["Page"] = "Lottery";
            other_lotteryModel model = new other_lotteryModel();
            //Lottery lists
            //var lst1 = db.other_lottery.Where(x => x.active_status == "A").OrderByDescending(x => x.id).ToList();
            var lst1 = db.other_lottery.Where(x => x.active_status == "A").OrderByDescending(x => x.flat_allotment_date).ToList();
            model.other_lotteryList = lst1.ToList();
            return View(model);
        }

        //योजना निहाय माहिती
        public ActionResult projectDetails(float masterNo_id)
        {
            shared_method();
            Session["Page"] = "projectDetails";
            homeModel model = new homeModel();
            //project details
            var lst = db.project_new.Where(x => x.id == masterNo_id).SingleOrDefault();
            model.master_no = lst.Master_no.ToString();
            model.yojana = lst.project_name.ToString();

            //get 3c details primary
            model.other_order_3c_primaryList = db.other_order_3c_3d.Where(x => x.master_no_id == masterNo_id && x.order_type == "C" && x.notice_type == "A" && x.active_status == "A").OrderByDescending(x => x.project_date).ToList();
            //get 3c details final
            model.other_order_3c_FinalList = db.other_order_3c_3d.Where(x => x.master_no_id == masterNo_id && x.order_type == "C" && x.notice_type == "F" && x.active_status == "A").OrderByDescending(x => x.project_date).ToList();
            //get 3d details primary
            model.other_order_3d_primaryList = db.other_order_3c_3d.Where(x => x.master_no_id == masterNo_id && x.order_type == "D" && x.notice_type == "A" && x.active_status == "A").OrderByDescending(x => x.project_date).ToList();
            //get 3d details final
            model.other_order_3d_primaryList = db.other_order_3c_3d.Where(x => x.master_no_id == masterNo_id && x.order_type == "D" && x.notice_type == "F" && x.active_status == "A").OrderByDescending(x => x.project_date).ToList();

            //primary list list
            model.primary_List_List = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.master_no_id == masterNo_id && x.type == "P" && x.is_notice_list == "L" && x.active_status == "A").OrderByDescending(x => x.plan_date_New).ToList(); ;

            //final list list
            model.final_List_List = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.master_no_id == masterNo_id && x.type == "F" && x.is_notice_list == "L" && x.active_status == "A").OrderByDescending(x => x.plan_date_New).ToList(); ;

            //Primary SR
            model.other_srList = db.other_sr.Where(x => x.master_no_id == masterNo_id && x.active_status == "A" && x.type == "P").OrderByDescending(x => x.subject_date).ToList();
            //Final SR
            model.other_srList2 = db.other_sr.Where(x => x.master_no_id == masterNo_id && x.active_status == "A" && x.type == "F").OrderByDescending(x => x.subject_date).ToList();

            //Building Permission lists
            model.other_building_permission_list = db.other_building_permission_bhogvata.Where(x => x.master_no_id == masterNo_id && x.type == "B" && x.active_status == "A").OrderByDescending(x => x.proj_date).ToList();
            ////Bhogvata lists
            model.other_bhogvata_list = db.other_building_permission_bhogvata.Where(x => x.master_no_id == masterNo_id && x.type == "O" && x.active_status == "A").OrderByDescending(x => x.proj_date).ToList();
            return View(model);
        }
    }
}