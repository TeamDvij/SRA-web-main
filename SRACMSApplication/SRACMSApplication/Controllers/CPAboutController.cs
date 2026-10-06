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
using System.IO;

namespace SRACMSApplication.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class CPAboutController : Controller
    {
        private sra_website_cmsEntities db = new sra_website_cmsEntities();

        //public int decreptid(string urlid)
        //{
        //    return Convert.ToInt32(db.tbl_urlId.Where(x => x.urlid == urlid).Select(x => x.id).SingleOrDefault());
        //}

        //आमच्या विषयी

        public ActionResult cp_about()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                aboutModel model = new aboutModel();
                //लागू कायदे व नियमावली
                var lst1 = db.about.Where(x=>x.type == "table1").ToList();
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
        }
        //Contact संपर्क
        
        public ActionResult cp_contact()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                aboutModel model = new aboutModel();
                //संपर्क-अधिकारी व कर्मचारी
                var lst1 = db.about.Where(x => x.type == "contact_officers").OrderBy(x=>x.col_srno).ToList();
                model.ContactOfficerTableList = lst1.ToList();
                //माहिती अधिकार संपर्क-अधिकारी व कर्मचारी
                var lst2 = db.about.Where(x => x.type == "rti_contact_officers").ToList();
                model.RTIContactOfficerTableList = lst2.ToList();
                return View(model);
            }
        }
        //update लागू कायदे व नियमावली / सन २०११ नुसार - कार्यक्षेत्रातील झोपडपट्टंयाचा तपशील / झोपडपट्टयांचे जमीन मालकीनिहाय वर्गीकरण /
        //संपर्क-अधिकारी व कर्मचारी //माहिती अधिकार संपर्क-अधिकारी व कर्मचारी
       
        public JsonResult SaveAboutTableList(List<about> obj_about)
        {
            about obj = null;
            //Loop and insert records.
            foreach (about objabout in obj_about)
            {
                obj = db.about.SingleOrDefault(x => x.id == objabout.id);
                if (obj == null)
                {
                    return Json("Something went wrong!");
                }
                obj.updated_by = Convert.ToInt32(Session["UserID"]);
                obj.updated_date = DateTime.Today;
                if (obj.updated_count == null)
                    obj.updated_count = 1;
                else
                    obj.updated_count += 1;
                obj.col_srno = objabout.col_srno;

                obj.col0 = objabout.col0;
                    obj.col1 = objabout.col1;
                    obj.col2 = objabout.col2;
                    obj.col3 = objabout.col3;
                    obj.col4 = objabout.col4;
                    obj.col5 = objabout.col5;
                    obj.col6 = objabout.col6;
                    obj.col7 = objabout.col7;
                    obj.col8 = objabout.col8;
                    obj.col9 = objabout.col9;
                    obj.col_eng0 = objabout.col_eng0;
                    obj.col_eng1 = objabout.col_eng1;
                    obj.col_eng2 = objabout.col_eng2;
                    obj.col_eng3 = objabout.col_eng3;
                    obj.col_eng4 = objabout.col_eng4;
                    obj.col_eng5 = objabout.col_eng5;
                    obj.col_eng6 = objabout.col_eng6;
                    obj.col_eng7 = objabout.col_eng7;
                    obj.col_eng8 = objabout.col_eng8;
                    obj.col_eng9 = objabout.col_eng9;
                obj.active_status = "A";
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.Message = "Record Submitted Successfully!";
            return Json("Record Updated Successfully!");
        }
        
        //आमचे नेतृत्व
        
        public ActionResult cp_netrutva()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                about_amache_netrutvaModel model = new about_amache_netrutvaModel();
                var lst = db.about_amache_netrutva.Where(x => x.active_status == "A").ToList();
                model.AmacheNetrutvaList = lst.ToList();
                return View(model);
            }
        }
        public ActionResult cp_netrutva_edit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                about_amache_netrutvaModel model = new Models.about_amache_netrutvaModel();
                about_amache_netrutva obj = new about_amache_netrutva();
                if (id != 0 && id != null)
                {
                    obj = db.about_amache_netrutva.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {
                        model.id = obj.id;
                        model.sadasya_photo_location = obj.sadasya_photo_location;
                        model.sadasya_name = obj.sadasya_name;
                        model.sadasya_name_eng = obj.sadasya_name_eng;
                        model.designation = obj.designation;
                        model.designation_eng = obj.designation_eng;
                        model.department = obj.department;
                        model.department_eng = obj.department_eng;
                        model.address_line1 = obj.address_line1;
                        model.address_line1_eng = obj.address_line1_eng;
                        model.address_line2 = obj.address_line2;
                        model.address_line2_eng = obj.address_line2_eng;
                        model.address_line3 = obj.address_line3;
                        model.address_line3_eng = obj.address_line3_eng;
                        model.address_line4 = obj.address_line4;
                        model.address_line4_eng = obj.address_line4_eng;
                        model.contact = obj.contact;
                        model.contact_eng = obj.contact_eng;
                        model.email = obj.email;
                        model.email_eng = obj.email_eng;
                        model.active_status = obj.active_status;
                    }
                    else
                        ViewBag.error = "Something went wrong.";
                }
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult cp_netrutva_edit(about_amache_netrutvaModel model, HttpPostedFileBase postedFile)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    string filename = "";
                    string extension = "";
                    if (postedFile != null)
                    {
                        filename = System.IO.Path.GetFileNameWithoutExtension(postedFile.FileName);
                        extension = System.IO.Path.GetExtension(postedFile.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        if (extension != ".jpeg" && extension != ".jpg")
                        {
                            ViewBag.fileExtensionMessage = "Please select jpg/jpeg file";
                            return View();
                        }
                    }
                    about_amache_netrutva obj = null;
                    if (model.id != 0)
                    {
                        obj = db.about_amache_netrutva.SingleOrDefault(x => x.id == model.id);
                        if (obj == null)
                        {
                            return View();
                        }
                        else
                        {
                            obj.updated_by = Convert.ToInt32(Session["UserID"]);
                            obj.updated_date = DateTime.Today;
                            if (obj.updated_count == null)
                                obj.updated_count = 1;
                            else
                                obj.updated_count += 1;
                            ////Delete Existing File
                            //if (obj.sadasya_photo_location != "" && filename != "")
                            //{
                            //    string file_name = System.IO.Path.Combine(Server.MapPath(obj.sadasya_photo_location), "");
                            //    FileInfo file = new FileInfo(file_name);
                            //    if (file.Exists)
                            //        file.Delete(); //Delete file at particular location
                            //}

                            obj.sadasya_name = model.sadasya_name;
                            obj.sadasya_name_eng = model.sadasya_name_eng;
                            obj.designation = model.designation;
                            obj.designation_eng = model.designation_eng;
                            obj.department = model.department;
                            obj.department_eng = model.department_eng;
                            obj.address_line1 = model.address_line1;
                            obj.address_line1_eng = model.address_line1_eng;
                            obj.address_line2 = model.address_line2;
                            obj.address_line2_eng = model.address_line2_eng;
                            obj.address_line3 = model.address_line3;
                            obj.address_line3_eng = model.address_line3_eng;
                            obj.address_line4 = model.address_line4;
                            obj.address_line4_eng = model.address_line4_eng;
                            obj.contact = model.contact;
                            obj.contact_eng = model.contact_eng;
                            obj.email = model.email;
                            obj.email_eng = model.email;
                            if (filename != "")
                            {
                                obj.sadasya_photo_location = "~/Content/images/AamacheNetrutva/" + filename;
                                filename = System.IO.Path.Combine(Server.MapPath("~/Content/images/AamacheNetrutva"), filename);
                                postedFile.SaveAs(filename); //Save file at particular location
                            }
                            obj.active_status = "A";
                            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            ViewBag.Message = "Record Submitted Successfully!";
                        }
                    }
                    return View(model);
                }
                catch (Exception)
                {
                    ViewBag.Message = "Something went wrong";
                    return View(model);
                }
            }
        }

        //सक्षम सदस्य

        public ActionResult cp_saksham_sadasya()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                abount_saksham_sadasyaModel model = new abount_saksham_sadasyaModel();
                var lst = db.abount_saksham_sadasya.Where(x => x.active_status == "A").ToList();
                model.abount_saksham_sadasyaList = lst.ToList();
                return View(model);
            }
            //if (Session["UserID"] == null)
            //{
            //    return RedirectToAction("Login", "CPAdmin");
            //}
            //else
            //{
            //    abount_saksham_sadasyaModel model = new abount_saksham_sadasyaModel();
            //    var lst = db.abount_saksham_sadasya.Where(x => x.active_status == "A").ToList();
            //    //model.abount_saksham_sadasyaList = lst.ToList();
            //    var lst_tblurlid = db.tbl_urlId.ToList();
            //    var lstnew = from s in lst
            //                 join u in lst_tblurlid on s.id equals u.id into table1
            //                 from u in table1.ToList()
            //                 select new ViewModel
            //                 {
            //                     abount_saksham_sadasya = s,
            //                     tbl_urlId = u
            //                 };
            //    model.lst_viewModel_saksham_sadasya = lstnew.ToList();
            //    return View(model);
            //}
        }
        
        public ActionResult cp_saksham_sadasya_edit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                abount_saksham_sadasyaModel model = new Models.abount_saksham_sadasyaModel();
                abount_saksham_sadasya obj = new abount_saksham_sadasya();
                if (id != 0 && id != null)
                {
                    obj = db.abount_saksham_sadasya.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {
                        model.id = obj.id;
                        model.name = obj.name;
                        model.name_eng = obj.name_eng;
                        model.designation = obj.designation;
                        model.designation_eng = obj.designation_eng;
                        model.active_status = obj.active_status;
                    }
                    else
                        ViewBag.error = "Something went wrong.";
                }
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult cp_saksham_sadasya_edit(abount_saksham_sadasyaModel model, HttpPostedFileBase postedFile)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    abount_saksham_sadasya obj = null;
                    if (model.id != 0)
                    {
                        obj = db.abount_saksham_sadasya.SingleOrDefault(x => x.id == model.id);
                        if (obj == null)
                        {
                            return View();
                        }
                        else
                        {
                            obj.updated_by = Convert.ToInt32(Session["UserID"]);
                            obj.updated_date = DateTime.Today;
                            if (obj.updated_count == null)
                                obj.updated_count = 1;
                            else
                                obj.updated_count += 1;
                            obj.name = model.name;
                            obj.name_eng = model.name_eng;
                            obj.designation = model.designation;
                            obj.designation_eng = model.designation_eng;
                            obj.active_status = "A";
                            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            ViewBag.Message = "Record Submitted Successfully!";
                        }
                    }
                            return View(model);
                }
                catch (Exception)
                {
                    ViewBag.Message = "Something went wrong";
                    return View(model);
                }
            }
        }

        //परिपत्रके
        
        public ActionResult cp_other_paripatrak()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_paripatrakModel model = new other_paripatrakModel();
                //Paripatrak lists
                var lst1 = db.other_paripatrak.OrderByDescending(x => x.id).ToList();
                model.other_paripatrakList = lst1.ToList();
                return View(model);
            }
        }
        
        public ActionResult cp_other_paripatrak_activeDeactive(long id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    other_paripatrak obj = null;
                    if (id != 0)
                    {
                        obj = db.other_paripatrak.SingleOrDefault(x => x.id == id);
                        if (obj == null)
                        {
                            return View();
                        }
                        else
                        {
                            obj.updated_by = Convert.ToInt32(Session["UserID"]);
                            obj.updated_date = DateTime.Today;
                            if (obj.updated_count == null)
                                obj.updated_count = 1;
                            else
                                obj.updated_count += 1;
                            if (obj.active_status == null)
                                obj.active_status = "A";
                            else if (obj.active_status == "A")
                            {
                                obj.active_status = "D";
                                ViewBag.activeDeactiveMessage = "Record Removed Successfully!";
                            }
                            else if (obj.active_status == "D")
                            {
                                obj.active_status = "A";
                                ViewBag.activeDeactiveMessage = "Record Added Successfully!";
                            }
                            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            return View();
                        }
                    }
                    else
                        return View();
                }
                catch (Exception)
                {
                    ViewBag.Message = "Something went wrong";
                    throw;
                }
            }
        }
        
        public ActionResult cp_other_paripatrak_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_paripatrakModel model = new Models.other_paripatrakModel();
                other_paripatrak obj = new other_paripatrak();

                //Department list
                var lst_dept = db.mst_department.ToList<mst_department>().Where(a => a.active_status == "A");
                model.mst_departmentList = lst_dept.ToList();

                if (id != 0 && id != null)
                {
                    obj = db.other_paripatrak.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {
                        model.id = obj.id;
                        model.dept_id = obj.dept_id;
                        model.subject = obj.subject;
                        model.subject_eng = obj.subject_eng;
                        model.circular_number = obj.circular_number;
                        model.circular_number_eng = obj.circular_number_eng;
                        model.subject_date1 = obj.subject_date.ToString("dd-MM-yyyy");
                        model.doc_name = obj.doc_name;
                        model.doc_location = obj.doc_location;
                        model.active_status = obj.active_status;
                    }
                    else
                        ViewBag.error = "Something went wrong.";
                }
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult cp_other_paripatrak_addedit(other_paripatrakModel model, HttpPostedFileBase postedFile)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    string filename = "";
                    string extension = "";
                    if (postedFile != null)
                    {
                        filename = System.IO.Path.GetFileNameWithoutExtension(postedFile.FileName);
                        string[] slipttext = filename.Split('.');
                        if (slipttext.Count() > 1)
                        {
                            ViewBag.fileExtensionMessage = "Invalid file!";
                            return View(model);
                        }
                        extension = System.IO.Path.GetExtension(postedFile.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        if (extension != ".pdf")
                        {
                            ViewBag.fileExtensionMessage = "Please select pdf file";
                            return View(model);
                        }
                    }
                    other_paripatrak obj = null;
                    if (model.id != 0)
                    {
                        obj = db.other_paripatrak.SingleOrDefault(x => x.id == model.id);
                        if (obj == null)
                        {
                            return View(model);
                        }
                        else
                        {
                            obj.updated_by = Convert.ToInt32(Session["UserID"]);
                            obj.updated_date = DateTime.Today;
                            if (obj.updated_count == null)
                                obj.updated_count = 1;
                            else
                                obj.updated_count += 1;
                            //Delete Existing File
                            if (obj.doc_name != "" && filename != "")
                            {
                                string file_name = System.IO.Path.Combine(Server.MapPath(obj.doc_location), obj.doc_name);
                                FileInfo file = new FileInfo(file_name);
                                if (file.Exists)
                                    file.Delete(); //Delete file at particular location
                            }
                        }
                    }
                    else
                    {
                        obj = new other_paripatrak();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        obj.created_date = DateTime.Today;
                    }
                    obj.dept_id = model.dept_id;
                    obj.subject = model.subject;
                    obj.subject_eng = model.subject_eng;
                    obj.circular_number = model.circular_number;
                    obj.circular_number_eng = model.circular_number_eng;
                    DateTime dateTime = new DateTime();
                    dateTime = Convert.ToDateTime(DateTime.ParseExact(model.subject_date1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    obj.subject_date = dateTime;
                    if (filename != "")
                    {
                        obj.doc_name = filename;
                        obj.doc_location = "~/Content/Documents/Paripatrak/";
                        filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Paripatrak"), obj.doc_name);
                        postedFile.SaveAs(filename); //Save file at particular location
                    }
                    obj.active_status = "A";
                    if (model.id == 0)
                        db.other_paripatrak.Add(obj);
                    else
                        db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    //Department list
                    var lst_dept = db.mst_department.ToList<mst_department>().Where(a => a.active_status == "A");
                    model.mst_departmentList = lst_dept.ToList();


                    ViewBag.Message = "Record Submitted Successfully!";
                    return View(model);
                }
                catch (Exception)
                {
                    ViewBag.Message = "Something went wrong";
                    return View(model);
                }
            }
        }

        //वार्षिक लेखा अहवाल
        
        public ActionResult CPAbout_varshik_ahval()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                mst_single_image_pdfModel model = new mst_single_image_pdfModel();
                //वार्षिक लेखा अहवाल lists
                var lst1 = db.mst_single_image_pdf.Where(x => x.content_type == "AnnualReport").OrderByDescending(x => x.id).ToList();
                model.lstContentType = lst1.ToList();
                return View(model);
            }
        }
        
        public ActionResult CPAbout_varshik_ahval_activeDeactive(long id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    mst_single_image_pdf obj = null;
                    if (id != 0)
                    {
                        obj = db.mst_single_image_pdf.SingleOrDefault(x => x.id == id);
                        if (obj == null)
                        {
                            return View();
                        }
                        else
                        {
                            obj.updated_by = Convert.ToInt32(Session["UserID"]);
                            obj.updated_date = DateTime.Today;
                            if (obj.updated_count == null)
                                obj.updated_count = 1;
                            else
                                obj.updated_count += 1;
                            if (obj.active_status == null)
                                obj.active_status = "A";
                            else if (obj.active_status == "A")
                            {
                                obj.active_status = "D";
                                ViewBag.activeDeactiveMessage = "Record Removed Successfully!";
                            }
                            else if (obj.active_status == "D")
                            {
                                obj.active_status = "A";
                                ViewBag.activeDeactiveMessage = "Record Added Successfully!";
                            }
                            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            return View();
                        }
                    }
                    else
                        return View();
                }
                catch (Exception)
                {
                    ViewBag.Message = "Something went wrong";
                    throw;
                }
            }
        }
        
        public ActionResult CPAbout_varshik_ahval_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                mst_single_image_pdfModel model = new Models.mst_single_image_pdfModel();
                mst_single_image_pdf obj = new mst_single_image_pdf();
                if (id != 0 && id != null)
                {
                    obj = db.mst_single_image_pdf.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {
                        model.id = obj.id;
                        model.title1 = obj.title;
                        model.title_eng = obj.title_eng;
                        model.doc_name = obj.doc_name;
                        model.doc_location = obj.doc_location;
                        model.active_status = obj.active_status;
                    }
                    else
                        ViewBag.error = "Something went wrong.";
                }
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult CPAbout_varshik_ahval_addedit(mst_single_image_pdfModel model, HttpPostedFileBase postedFile)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    string filename = "";
                    string extension = "";
                    if (postedFile != null)
                    {
                        filename = System.IO.Path.GetFileNameWithoutExtension(postedFile.FileName);
                        string[] slipttext = filename.Split('.');
                        if (slipttext.Count() > 1)
                        {
                            ViewBag.fileExtensionMessage = "Invalid file!";
                            return View(model);
                        }
                        extension = System.IO.Path.GetExtension(postedFile.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        if (extension != ".pdf")
                        {
                            ViewBag.fileExtensionMessage = "Please select pdf file";
                            return View(model);
                        }
                    }
                    mst_single_image_pdf obj = null;
                    if (model.id != 0)
                    {
                        obj = db.mst_single_image_pdf.SingleOrDefault(x => x.id == model.id);
                        if (obj == null)
                        {
                            return View(model);
                        }
                        else
                        {
                            obj.updated_by = Convert.ToInt32(Session["UserID"]);
                            obj.updated_date = DateTime.Today;
                            if (obj.updated_count == null)
                                obj.updated_count = 1;
                            else
                                obj.updated_count += 1;
                            //Delete Existing File
                            if (obj.doc_name != "" && filename != "")
                            {
                                string file_name = System.IO.Path.Combine(Server.MapPath(obj.doc_location), obj.doc_name);
                                FileInfo file = new FileInfo(file_name);
                                if (file.Exists)
                                    file.Delete(); //Delete file at particular location
                            }
                        }
                    }
                    else
                    {
                        obj = new mst_single_image_pdf();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        obj.created_date = DateTime.Today;
                    }
                    obj.file_type = "p";
                    obj.content_type = "AnnualReport";
                    obj.title = model.title1;
                    obj.title_eng = model.title_eng;
                    if (filename != "")
                    {
                        obj.doc_name = filename;
                        obj.doc_location = "~/Content/Documents/VarshikAhval/";
                        filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/VarshikAhval"), obj.doc_name);
                        postedFile.SaveAs(filename); //Save file at particular location
                    }
                    obj.active_status = "A";
                    if (model.id == 0)
                        db.mst_single_image_pdf.Add(obj);
                    else
                        db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Record Submitted Successfully!";
                    return View(model);
                }
                catch (Exception)
                {
                    ViewBag.Message = "Something went wrong";
                    return View(model);
                }
            }
        }

      
    }
}
