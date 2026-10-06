using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SRACMSApplication.Models;
using System.IO;
using System.Globalization;

namespace SRACMSApplication.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class cp_otherController : Controller
    {
        private sra_website_cmsEntities db = new sra_website_cmsEntities();
        //public int decreptid(string urlid)
        //{
        //    return Convert.ToInt32(db.tbl_urlId.Where(x => x.urlid == urlid).Select(x => x.id).SingleOrDefault());
        //}

        //विकसक
        public ActionResult cp_other_developer()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {

                other_developerModel model = new other_developerModel();
                //Developer lists
                var lst1 = db.other_developers.OrderByDescending(x => x.id).ToList();
                model.developerList = lst1.ToList();
                return View(model);
            }
        }
        public ActionResult cp_other_developer_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_developerModel model = new Models.other_developerModel();
                other_developers obj = new other_developers();
                if (id != 0 && id != null)
                {
                    obj = db.other_developers.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {
                        model.id = obj.id;
                        model.dev_name = obj.dev_name;
                        model.dev_name_eng = obj.dev_name_eng;
                        model.dev_address = obj.dev_address;
                        model.dev_address_eng = obj.dev_address_eng;
                        model.dev_contact = obj.dev_contact;
                        model.dev_contact_eng = obj.dev_contact_eng;
                        model.dev_class = obj.dev_class;
                        model.dev_class_eng = obj.dev_class_eng;
                        model.dev_registration_no = obj.dev_registration_no;
                        model.dev_registration_no_eng = obj.dev_registration_no_eng;
                        model.dev_registration_date1 = obj.dev_registration_date.ToString("dd-MM-yyyy");
                        model.completed_projects = obj.completed_projects;
                        model.completed_projects_eng = obj.completed_projects_eng;
                        model.current_projects = obj.current_projects;
                        model.current_projects_eng = obj.current_projects_eng;
                    }
                    else
                        ViewBag.error = "Something went wrong.";
                }
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult cp_other_developer_addedit(other_developerModel model)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    other_developers obj = null;
                    if (model.id != 0)
                    {
                        obj = db.other_developers.SingleOrDefault(x => x.id == model.id);
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
                        }
                    }
                    else
                    {
                        obj = new other_developers();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        obj.created_date = DateTime.Today;
                    }
                    obj.dev_name = model.dev_name;
                    obj.dev_name_eng = model.dev_name_eng;
                    obj.dev_address = model.dev_address;
                    obj.dev_address_eng = model.dev_address_eng;
                    obj.dev_contact = model.dev_contact;
                    obj.dev_contact_eng = model.dev_contact_eng;
                    obj.dev_class = model.dev_class;
                    obj.dev_class_eng = model.dev_class_eng;
                    obj.dev_registration_no = model.dev_registration_no;
                    obj.dev_registration_no_eng = model.dev_registration_no_eng;
                    DateTime dateTime = new DateTime();
                    dateTime = Convert.ToDateTime(DateTime.ParseExact(model.dev_registration_date1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    obj.dev_registration_date = dateTime;
                    //obj.dev_registration_date = model.dev_registration_date;
                    obj.completed_projects = model.completed_projects;
                    obj.completed_projects_eng = model.completed_projects_eng;
                    obj.current_projects = model.current_projects;
                    obj.current_projects_eng = model.current_projects_eng;
                    obj.active_status = "A";
                    if (model.id == 0)
                        db.other_developers.Add(obj);
                    else
                        db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Record Submitted Successfully!";

                    return View(model);
                }
                catch (Exception)
                {
                    ViewBag.Message = "Something went wrong";
                    throw;
                }
            }

        }

        public ActionResult cp_other_developer_activeDeactive(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    other_developers obj = null;
                    if (id != 0)
                    {
                        obj = db.other_developers.SingleOrDefault(x => x.id == id);
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

        //नियमावली
        public ActionResult cp_other_Niyamavali()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                List<other_sra_rulesModel> model = new List<other_sra_rulesModel>();
                //SRA Rules list
                List<other_sra_rules> obj = new List<other_sra_rules>();
                obj = db.other_sra_rules.OrderByDescending(x => x.id).ToList();
                foreach (var item in obj)
                {
                    other_sra_rulesModel model1 = new other_sra_rulesModel();
                    model1.id = item.id;
                    model1.rule_name = item.rule_name;
                    model1.rule_name_eng = item.rule_name_eng;
                    model1.rule_year_eng = item.rule_year_eng;
                    model1.rule_year = item.rule_year;
                    model1.doc_name = item.doc_name;
                    model1.doc_location = item.doc_location;
                    model1.active_status = item.active_status;
                    model1.rule_date1 = item.rule_date.ToString("dd-MM-yyyy");
                    model1.rule_date = item.rule_date;
                    model.Add(model1);
                }
                return View(model);
            }
        }
        public ActionResult cp_other_Niyamavali_addedit(long? id)
        {
            other_sra_rulesModel model = new Models.other_sra_rulesModel();
            other_sra_rules obj = new other_sra_rules();
            if (id != 0 && id != null)
            {
                obj = db.other_sra_rules.SingleOrDefault(x => x.id == id);
                if (obj != null)
                {
                    model.id = obj.id;
                    model.rule_name = obj.rule_name;
                    model.rule_name_eng = obj.rule_name_eng;
                    model.rule_year = obj.rule_year;
                    model.rule_year_eng = obj.rule_year_eng;
                    model.doc_name = obj.doc_name;
                    model.doc_location = obj.doc_location;
                    model.rule_date1 = obj.rule_date.ToString("dd-MM-yyyy");
                }
                else
                    ViewBag.error = "Something went wrong.";
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult cp_other_Niyamavali_addedit(other_sra_rulesModel model, HttpPostedFileBase postedFile)
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
                        if (extension != ".pdf")
                        {
                            ViewBag.fileExtensionMessage = "Please select pdf file";
                            return View();
                        }
                    }

                    other_sra_rules obj = null;
                    if (model.id != 0)
                    {
                        obj = db.other_sra_rules.SingleOrDefault(x => x.id == model.id);
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
                        }
                    }
                    else
                    {
                        obj = new other_sra_rules();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        obj.created_date = DateTime.Today;
                    }
                    obj.rule_name = model.rule_name;
                    obj.rule_name_eng = model.rule_name_eng;
                    obj.rule_year = model.rule_year;
                    obj.rule_year_eng = model.rule_year_eng;
                    DateTime dateTime = new DateTime();
                    dateTime = Convert.ToDateTime(DateTime.ParseExact(model.rule_date1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    obj.rule_date = dateTime;
                    if (filename != "")
                    {
                        obj.doc_name = filename;
                        obj.doc_location = "~/Content/Documents/sra_rules/";
                        filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/sra_rules"), filename);
                        postedFile.SaveAs(filename); //Save file at particular location
                    }
                    obj.active_status = "A";
                    if (model.id == 0)
                        db.other_sra_rules.Add(obj);
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
        public ActionResult cp_other_Niyamavali_activeDeactive(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    other_sra_rules obj = null;
                    if (id != 0)
                    {
                        obj = db.other_sra_rules.SingleOrDefault(x => x.id == id);
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

        //जाहीर प्रकटन
        public ActionResult cp_other_jahirPrakatan()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_jahirPrakatanModel model = new other_jahirPrakatanModel();
                //other_jahir_prakatan_Preliminary_survey
                var lst_jahirPrakatanPriliminary = db.other_jahir_prakatan_Preliminary_survey.OrderByDescending(x => x.id).ToList(); ;
                model.OtherJahirPrakatanList = lst_jahirPrakatanPriliminary.ToList();

                //primary notice list
                var lst_primary_Notice = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "P" && x.is_notice_list == "N").OrderByDescending(x => x.id).ToList(); ;
                model.primary_Notice_List = lst_primary_Notice.ToList();

                //primary list list
                var lst_primary_List = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "P" && x.is_notice_list == "L").OrderByDescending(x => x.id).ToList();
                model.primary_List_List = lst_primary_List.ToList();

                //final notice list
                var lst_final_Notice = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "F" && x.is_notice_list == "N").OrderByDescending(x => x.id).ToList();
                model.final_Notice_List = lst_final_Notice.ToList();

                //final list list
                var lst_final_List_List = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "F" && x.is_notice_list == "L").OrderByDescending(x => x.id).ToList();
                model.final_List_List = lst_final_List_List.ToList();
                return View(model);
            }
        }
        public ActionResult cp_other_jahirPrakatan_preliminary_activeDeactive(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    other_jahir_prakatan_Preliminary_survey obj = null;
                    if (id != 0)
                    {
                        obj = db.other_jahir_prakatan_Preliminary_survey.SingleOrDefault(x => x.id == id);
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
        public ActionResult cp_other_jahirPrakatan_primary_final_activeDeactive(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    other_jahir_prakatan_primary_final_eligibility obj = null;
                    if (id != 0)
                    {
                        obj = db.other_jahir_prakatan_primary_final_eligibility.SingleOrDefault(x => x.id == id);
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
        public ActionResult cp_other_jahirPrakatan_Preliminary_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_jahirPrakatanModel model = new Models.other_jahirPrakatanModel();
                other_jahir_prakatan_Preliminary_survey obj = new other_jahir_prakatan_Preliminary_survey();
                if (id != 0 && id != null)
                {
                    obj = db.other_jahir_prakatan_Preliminary_survey.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {
                        model.id = obj.id;
                        model.disclosure_number = obj.disclosure_number;
                        model.disclosure_number_eng = obj.disclosure_number_eng;
                        model.subject = obj.subject;
                        model.subject_eng = obj.subject_eng;
                        model.upload_date = obj.upload_date;
                        //model.upload_date_New1 = obj.upload_date_new.ToString("dd-MM-yyyy");//////////////////
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
        public ActionResult cp_other_jahirPrakatan_Preliminary_addedit(other_jahirPrakatanModel model, HttpPostedFileBase postedFile)
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
                        if (extension != ".pdf")
                        {
                            ViewBag.fileExtensionMessage = "Please select pdf file";
                            return View();
                        }
                    }
                    other_jahir_prakatan_Preliminary_survey obj = null;
                    if (model.id != 0)
                    {
                        obj = db.other_jahir_prakatan_Preliminary_survey.SingleOrDefault(x => x.id == model.id);
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
                            //Delete Existing File
                            if (obj.doc_name != "" && filename != "")
                            {
                                string file_name = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Public_Notices/Preliminary"), obj.doc_name);
                                FileInfo file = new FileInfo(file_name);
                                if (file.Exists)
                                    file.Delete(); //Delete file at particular location
                            }
                        }
                    }
                    else
                    {
                        obj = new other_jahir_prakatan_Preliminary_survey();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        obj.created_date = DateTime.Now;
                    }
                    obj.disclosure_number = model.disclosure_number;
                    obj.disclosure_number_eng = model.disclosure_number_eng;
                    obj.subject = model.subject;
                    obj.subject_eng = model.subject_eng;
                    DateTime dateTime = new DateTime();
                    dateTime = Convert.ToDateTime(DateTime.ParseExact(model.upload_date_New1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    /////obj.upload_date_new = dateTime;
                    if (filename != "")
                    {
                        obj.doc_name = filename;
                        obj.doc_location = "~/Content/Documents/Public_Notices/Preliminary/";
                        filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Public_Notices/Preliminary"), filename);
                        postedFile.SaveAs(filename); //Save file at particular location
                    }
                    obj.active_status = "A";
                    if (model.id == 0)
                        db.other_jahir_prakatan_Preliminary_survey.Add(obj);
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
        public ActionResult cp_other_jahirPrakatan_Primary_Final_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_jahirPrakatanModel model = new Models.other_jahirPrakatanModel();
                other_jahir_prakatan_primary_final_eligibility obj = new other_jahir_prakatan_primary_final_eligibility();
                List<SelectListItem> lst1 = new List<SelectListItem>();
                List<SelectListItem> lst2 = new List<SelectListItem>();
                lst1.Add(new SelectListItem
                {
                    Text = "प्राथमिक",
                    Value = "P"
                });
                lst1.Add(new SelectListItem
                {
                    Text = "अंतिम",
                    Value = "F"
                });
                model.typeList = lst1;
                lst2.Add(new SelectListItem
                {
                    Text = "नोटीस",
                    Value = "N"
                });
                lst2.Add(new SelectListItem
                {
                    Text = "यादी",
                    Value = "L"
                });
                model.notice_yadi_List = lst2;

                //सक्षम list
                var lst_bank = db.sys_config.ToList<sys_config>().Where(a => a.sys_type == "ca_dept" && a.active_status == "A");
                model.sys_configList = lst_bank.ToList();

                //योजना list
                var lst_yojna = db.project_new.ToList<project_new>().Where(a => a.active_status == "A" || a.active_status == "R").OrderBy(a=>a.Master_no);
                model.yojna_List = lst_yojna.ToList();

                if (id != 0 && id != null)
                {
                    obj = db.other_jahir_prakatan_primary_final_eligibility.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {
                        model.id = obj.id;
                        model.type = obj.type;
                        model.is_notice_list = obj.is_notice_list;
                        model.plan_name = obj.plan_name;
                        model.plan_name_eng = obj.plan_name_eng;
                        model.plan_date = obj.plan_date;
                        model.plan_date_New1 = obj.plan_date_New.ToString("dd-MM-yyyy");
                        model.doc_name = obj.doc_name;
                        model.doc_location = obj.doc_location;
                        model.active_status = obj.active_status;
                        model.ca_dept = obj.ca_dept;
                        model.ca_dept_id = obj.ca_dept_id;
                        model.master_no_id = obj.master_no_id;
                    }
                    else
                        ViewBag.error = "Something went wrong.";
                }
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult cp_other_jahirPrakatan_Primary_Final_addedit(other_jahirPrakatanModel model, HttpPostedFileBase postedFile)
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
                        if (extension != ".pdf")
                        {
                            ViewBag.fileExtensionMessage = "Please select pdf file";
                            return View();
                        }
                    }
                    other_jahir_prakatan_primary_final_eligibility obj = null;
                    if (model.id != 0)
                    {
                        obj = db.other_jahir_prakatan_primary_final_eligibility.SingleOrDefault(x => x.id == model.id);
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
                            //Delete Existing File
                            if (obj.doc_name != "" && filename != "")
                            {
                                string file_name = System.IO.Path.Combine(Server.MapPath(obj.doc_location), obj.doc_name);
                                FileInfo file = new FileInfo(file_name);
                                if (file.Exists)
                                    file.Delete(); //Delete file at particular location
                            }
                            //Move file if type or notice_yadi changed
                            else if (obj.type != model.type || obj.is_notice_list != model.is_notice_list)
                            {
                                string sourceFile = System.IO.Path.Combine(Server.MapPath(obj.doc_location), obj.doc_name);
                                string destinationFile = "";
                                if (model.type == "P" && model.is_notice_list == "N")
                                {
                                    obj.doc_location = "~/Content/Documents/Public_Notices/Primary_Notice/";
                                    destinationFile = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Public_Notices/Primary_Notice"), obj.doc_name);
                                }
                                else if (model.type == "P" && model.is_notice_list == "L")
                                {
                                    obj.doc_location = "~/Content/Documents/Public_Notices/Primary_List/";
                                    destinationFile = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Public_Notices/Primary_List"), obj.doc_name);
                                }
                                else if (model.type == "F" && model.is_notice_list == "N")
                                {
                                    obj.doc_location = "~/Content/Documents/Public_Notices/Final_Notice/";
                                    destinationFile = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Public_Notices/Final_Notice"), obj.doc_name);
                                }
                                else if (model.type == "F" && model.is_notice_list == "L")
                                {
                                    obj.doc_location = "~/Content/Documents/Public_Notices/Final_List/";
                                    destinationFile = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Public_Notices/Final_List"), obj.doc_name);
                                }
                                // System.IO.File.Move(sourceFile, destinationFile);
                                FileInfo file = new FileInfo(sourceFile);
                                if (file.Exists)
                                    file.MoveTo(destinationFile); //Move file at particular location
                            }
                        }
                    }
                    else
                    {
                        obj = new other_jahir_prakatan_primary_final_eligibility();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        obj.created_date = DateTime.Today;
                    }
                    obj.type = model.type;
                    obj.is_notice_list = model.is_notice_list;
                    obj.plan_name = model.plan_name;
                    obj.plan_name_eng = model.plan_name_eng;
                    DateTime dateTime = new DateTime();
                    dateTime = Convert.ToDateTime(DateTime.ParseExact(model.plan_date_New1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    obj.plan_date_New = dateTime;
                    if (filename != "")
                    {
                        obj.doc_name = filename;
                        if (obj.type == "P" && obj.is_notice_list == "N")
                        {
                            obj.doc_location = "~/Content/Documents/Public_Notices/Primary_Notice/";
                            filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Public_Notices/Primary_Notice"), filename);
                        }
                        else if (obj.type == "P" && obj.is_notice_list == "L")
                        {
                            obj.doc_location = "~/Content/Documents/Public_Notices/Primary_List/";
                            filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Public_Notices/Primary_List"), filename);
                        }
                        else if (obj.type == "F" && obj.is_notice_list == "N")
                        {
                            obj.doc_location = "~/Content/Documents/Public_Notices/Final_Notice/";
                            filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Public_Notices/Final_Notice"), filename);
                        }
                        else if (obj.type == "F" && obj.is_notice_list == "L")
                        {
                            obj.doc_location = "~/Content/Documents/Public_Notices/Final_List/";
                            filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Public_Notices/Final_List"), filename);
                        }
                        postedFile.SaveAs(filename); //Save file at particular location
                    }
                    obj.active_status = "A";
                    obj.ca_dept = model.ca_dept;
                    obj.ca_dept_id = model.ca_dept_id;
                    obj.master_no_id = model.master_no_id;

                    if (model.id == 0)
                        db.other_jahir_prakatan_primary_final_eligibility.Add(obj);
                    else
                        db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Record Submitted Successfully!";
                    List<SelectListItem> lst1 = new List<SelectListItem>();
                    List<SelectListItem> lst2 = new List<SelectListItem>();
                    lst1.Add(new SelectListItem
                    {
                        Text = "प्राथमिक",
                        Value = "P"
                    });
                    lst1.Add(new SelectListItem
                    {
                        Text = "अंतिम",
                        Value = "F"
                    });
                    model.typeList = lst1;
                    lst2.Add(new SelectListItem
                    {
                        Text = "नोटीस",
                        Value = "N"
                    });
                    lst2.Add(new SelectListItem
                    {
                        Text = "यादी",
                        Value = "L"
                    });
                    model.notice_yadi_List = lst2;

                    //सक्षम list
                    var lst_bank = db.sys_config.ToList<sys_config>().Where(a => a.sys_type == "ca_dept" && a.active_status == "A");
                    model.sys_configList = lst_bank.ToList();

                    //योजना list
                    var lst_yojna = db.project_new.ToList<project_new>().Where(a => a.active_status == "A" || a.active_status == "R").OrderBy(a => a.Master_no);
                    model.yojna_List = lst_yojna.ToList();

                    return View(model);
                }
                catch (Exception)
                {
                    ViewBag.Message = "Something went wrong";
                    return View(model);
                }
            }
        }

        //३क ३ड
        public ActionResult cp_other_sra3c3d()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_order_3c_3dModel model = new other_order_3c_3dModel();
                //3C3D lists
                //var lst1 = db.other_order_3c_3d.Where(x =>  x.active_status == "A").OrderByDescending(x => x.id).ToList();
                var lst1 = db.other_order_3c_3d.Where(x => x.order_type == "C" && x.notice_type == "A").OrderByDescending(x => x.id).ToList();
                model.other_order_3c_primaryList = lst1.ToList();
                var lst2 = db.other_order_3c_3d.Where(x => x.order_type == "C" && x.notice_type == "F").OrderByDescending(x => x.id).ToList();
                model.other_order_3c_FinalList = lst2.ToList();
                var lst3 = db.other_order_3c_3d.Where(x => x.order_type == "D" && x.notice_type == "A").OrderByDescending(x => x.id).ToList();
                model.other_order_3d_primaryList = lst3.ToList();
                var lst4 = db.other_order_3c_3d.Where(x => x.order_type == "D" && x.notice_type == "F").OrderByDescending(x => x.id).ToList();
                model.other_order_3d_finalList = lst4.ToList();
                return View(model);
            }
        }
        public ActionResult cp_other_sra3c3d_activeDeactive(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    other_order_3c_3d obj = null;
                    if (id != 0)
                    {
                        obj = db.other_order_3c_3d.SingleOrDefault(x => x.id == id);
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
        public ActionResult cp_other_sra3c3d_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_order_3c_3dModel model = new Models.other_order_3c_3dModel();
                other_order_3c_3d obj = new other_order_3c_3d();
                List<SelectListItem> lst1 = new List<SelectListItem>();
                List<SelectListItem> lst2 = new List<SelectListItem>();
                lst1.Add(new SelectListItem
                {
                    Text = "३क",
                    Value = "C"
                });
                lst1.Add(new SelectListItem
                {
                    Text = "३ड",
                    Value = "D"
                });
                model.order_typeList = lst1;
                lst2.Add(new SelectListItem
                {
                    Text = "पूर्व सूचना",
                    Value = "A"
                });
                lst2.Add(new SelectListItem
                {
                    Text = "अंतिम सूचना",
                    Value = "F"
                });
                model.notice_type_List = lst2;

                //सक्षम list
                var lst_bank = db.sys_config.ToList<sys_config>().Where(a => a.sys_type== "ca_dept" && a.active_status == "A");
                model.sys_configList = lst_bank.ToList();

                //योजना list
                var lst_yojna = db.project_new.ToList<project_new>().Where(a => a.active_status == "A" || a.active_status == "R").OrderBy(a => a.Master_no);
                model.yojna_List = lst_yojna.ToList();

                if (id != 0 && id != null)
                {
                    obj = db.other_order_3c_3d.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {

                        model.id = obj.id;
                        model.order_type = obj.order_type;
                        model.notice_type = obj.notice_type;
                        model.project_name = obj.project_name;
                        model.project_name_eng = obj.project_name_eng;
                        model.project_date_new1 = obj.project_date.ToString("dd-MM-yyyy");
                        model.doc_name = obj.doc_name;
                        model.doc_location = obj.doc_location;
                        model.active_status = obj.active_status;
                        model.ca_dept = obj.ca_dept;
                        model.ca_dept_id = obj.ca_dept_id;
                        model.master_no_id = obj.master_no_id;
                    }
                    else
                        ViewBag.error = "Something went wrong.";
                }
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult cp_other_sra3c3d_addedit(other_order_3c_3dModel model, HttpPostedFileBase postedFile)
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
                    other_order_3c_3d obj = null;
                    if (model.id != 0)
                    {
                        obj = db.other_order_3c_3d.SingleOrDefault(x => x.id == model.id);
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
                            //Move file if type or notice_yadi changed
                            else if (obj.order_type != model.order_type || obj.notice_type != model.notice_type)
                            {
                                string sourceFile = System.IO.Path.Combine(Server.MapPath(obj.doc_location), obj.doc_name);
                                string destinationFile = "";
                                if (model.order_type == "C" && model.notice_type == "A")
                                {
                                    obj.doc_location = "~/Content/Documents/SRA3C3D/3C/Primary/";
                                    destinationFile = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/SRA3C3D/3C/Primary"), obj.doc_name);
                                }
                                else if (model.order_type == "C" && model.notice_type == "F")
                                {
                                    obj.doc_location = "~/Content/Documents/SRA3C3D/3C/Final/";
                                    destinationFile = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/SRA3C3D/3C/Final"), obj.doc_name);
                                }
                                else if (model.order_type == "D" && model.notice_type == "A")
                                {
                                    obj.doc_location = "~/Content/Documents/SRA3C3D/3D/Primary/";
                                    destinationFile = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/SRA3C3D/3D/Primary"), obj.doc_name);
                                }
                                else if (model.order_type == "D" && model.notice_type == "F")
                                {
                                    obj.doc_location = "~/Content/Documents/SRA3C3D/3D/Final/";
                                    destinationFile = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/SRA3C3D/3D/Final"), obj.doc_name);
                                }
                                // System.IO.File.Move(sourceFile, destinationFile);
                                FileInfo file = new FileInfo(sourceFile);
                                if (file.Exists)
                                    file.MoveTo(destinationFile); //Move file at particular location
                            }
                        }
                    }
                    else
                    {
                        obj = new other_order_3c_3d();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        obj.created_date = DateTime.Today;
                    }
                    obj.order_type = model.order_type;
                    obj.notice_type = model.notice_type;
                    obj.project_name = model.project_name;
                    obj.project_name_eng = model.project_name_eng;
                    DateTime dateTime = new DateTime();
                    dateTime = Convert.ToDateTime(DateTime.ParseExact(model.project_date_new1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    obj.project_date = dateTime;

                    if (filename != "")
                    {
                        obj.doc_name = filename;
                        if (model.order_type == "C" && model.notice_type == "A")
                        {
                            obj.doc_location = "~/Content/Documents/SRA3C3D/3C/Primary/";
                            filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/SRA3C3D/3C/Primary"), obj.doc_name);
                        }
                        else if (model.order_type == "C" && model.notice_type == "F")
                        {
                            obj.doc_location = "~/Content/Documents/SRA3C3D/3C/Final/";
                            filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/SRA3C3D/3C/Final"), obj.doc_name);
                        }
                        else if (model.order_type == "D" && model.notice_type == "A")
                        {
                            obj.doc_location = "~/Content/Documents/SRA3C3D/3D/Primary/";
                            filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/SRA3C3D/3D/Primary"), obj.doc_name);
                        }
                        else if (model.order_type == "D" && model.notice_type == "F")
                        {
                            obj.doc_location = "~/Content/Documents/SRA3C3D/3D/Final/";
                            filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/SRA3C3D/3D/Final"), obj.doc_name);
                        }
                        postedFile.SaveAs(filename); //Save file at particular location
                    }
                    obj.active_status = "A";
                    obj.ca_dept = model.ca_dept;
                    obj.ca_dept_id = model.ca_dept_id;
                    obj.master_no_id = model.master_no_id;

                    if (model.id == 0)
                        db.other_order_3c_3d.Add(obj);
                    else
                        db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Record Submitted Successfully!";
                    List<SelectListItem> lst1 = new List<SelectListItem>();
                    List<SelectListItem> lst2 = new List<SelectListItem>();
                    lst1.Add(new SelectListItem
                    {
                        Text = "३क",
                        Value = "C"
                    });
                    lst1.Add(new SelectListItem
                    {
                        Text = "३ड",
                        Value = "D"
                    });
                    model.order_typeList = lst1;
                    lst2.Add(new SelectListItem
                    {
                        Text = "पूर्व सूचना",
                        Value = "A"
                    });
                    lst2.Add(new SelectListItem
                    {
                        Text = "अंतिम सूचना",
                        Value = "F"
                    });
                    model.notice_type_List = lst2;

                    //सक्षम list
                    var lst_bank = db.sys_config.ToList<sys_config>().Where(a => a.sys_type == "ca_dept" && a.active_status == "A");
                    model.sys_configList = lst_bank.ToList();

                    //योजना list
                    var lst_yojna = db.project_new.ToList<project_new>().Where(a => a.active_status == "A" || a.active_status == "R").OrderBy(a => a.Master_no);
                    model.yojna_List = lst_yojna.ToList();

                    return View(model);
                }
                catch (Exception)
                {
                    ViewBag.Message = "Something went wrong";
                    return View(model);
                }
            }
        }

        //SR 4(3) 4(5)
        public ActionResult cp_sr()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_srModel model = new other_srModel();
                //SR Prarup lists
                //var lst1 = db.other_sr.OrderByDescending(x => x.id).ToList();
                var lst1 = db.other_sr.Where(x => x.type == "P").OrderByDescending(x => x.subject_date).ToList();
                model.other_srList = lst1.ToList();

                //SR Antim lists
                var lst2 = db.other_sr.Where(x => x.type == "F").OrderByDescending(x => x.subject_date).ToList();
                model.other_srList2 = lst2.ToList();

                return View(model);
            }
        }
        public ActionResult cp_sr_activeDeactive(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    other_sr obj = null;
                    if (id != 0)
                    {
                        obj = db.other_sr.SingleOrDefault(x => x.id == id);
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
        public ActionResult cp_sr_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_srModel model = new Models.other_srModel();
                other_sr obj = new other_sr();
                model.doc_name = "";
                model.doc_name1 = "";
                model.doc_location = "";
                model.doc_location1 = "";

                List<SelectListItem> lst1 = new List<SelectListItem>();
                lst1.Add(new SelectListItem
                {
                    Text = "प्रारूप योजना मंजुरी",
                    Value = "P"
                });
                lst1.Add(new SelectListItem
                {
                    Text = "अंतिम योजना मंजुरी",
                    Value = "F"
                });
                model.typeList = lst1;

                //Technical dept list
                var lst_bank = db.sys_config.ToList<sys_config>().Where(a => a.sys_type == "ca_dept" && a.active_status == "A");
                model.sys_configList = lst_bank.ToList();

                //योजना list
                var lst_yojna = db.project_new.ToList<project_new>().Where(a => a.active_status == "A" || a.active_status == "R").OrderBy(a => a.Master_no);
                model.yojna_List = lst_yojna.ToList();


                if (id != 0 && id != null)
                {
                    obj = db.other_sr.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {

                        model.id = obj.id;
                        model.subject = obj.subject;
                        model.subject_eng = obj.subject_eng;
                        model.subject_date1 = obj.subject_date.ToString("dd-MM-yyyy");
                        model.doc_name = obj.doc_name;
                        model.doc_location = obj.doc_location;
                        model.doc_name1 = obj.doc_name1;
                        model.doc_location1 = obj.doc_location1;
                        model.active_status = obj.active_status;
                        model.dept_id = obj.dept_id;
                        model.type = obj.type;
                        model.master_no_id = obj.master_no_id;
                    }
                    else
                        ViewBag.error = "Something went wrong.";
                }
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult cp_sr_addedit(other_srModel model, HttpPostedFileBase postedFile, HttpPostedFileBase postedFile1)
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
                    string filename1 = "";
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
                    if (postedFile1 != null)
                    {
                        filename1 = System.IO.Path.GetFileNameWithoutExtension(postedFile1.FileName);
                        string[] slipttext = filename1.Split('.');
                        if (slipttext.Count() > 1)
                        {
                            ViewBag.fileExtensionMessage = "Invalid file!";
                            return View(model);
                        }
                        extension = System.IO.Path.GetExtension(postedFile1.FileName);
                        filename1 = filename1 + DateTime.Now.ToString("yymmssfff") + extension;
                        if (extension != ".pdf")
                        {
                            ViewBag.fileExtensionMessage = "Please select pdf file";
                            return View(model);
                        }
                    }
                    other_sr obj = null;
                    if (model.id != 0)
                    {
                        obj = db.other_sr.SingleOrDefault(x => x.id == model.id);
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
                            //Delete Existing File
                            if (obj.doc_name1 != "" && filename1 != "")
                            {
                                string file_name = System.IO.Path.Combine(Server.MapPath(obj.doc_location1), obj.doc_name1);
                                FileInfo file = new FileInfo(file_name);
                                if (file.Exists)
                                    file.Delete(); //Delete file at particular location
                            }
                        }
                    }
                    else
                    {
                        obj = new other_sr();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        obj.created_date = DateTime.Today;
                        obj.doc_name = "";
                        obj.doc_name1 = "";
                        obj.doc_location = "";
                        obj.doc_location1 = "";

                    }
                    obj.subject = model.subject;
                    obj.subject_eng = model.subject_eng;
                    DateTime dateTime = new DateTime();
                    dateTime = Convert.ToDateTime(DateTime.ParseExact(model.subject_date1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    obj.subject_date = dateTime;
                    if (filename != "")
                    {
                        obj.doc_name = filename;
                        obj.doc_location = "~/Content/Documents/SR/";
                        filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/SR"), obj.doc_name);
                        postedFile.SaveAs(filename); //Save file at particular location
                    }
                    //Add file 2 
                    // write code here for second pdf
                    if (filename1 != "")
                    {
                        obj.doc_name1 = filename1;
                        obj.doc_location1 = "~/Content/Documents/SR/";
                        filename1 = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/SR"), obj.doc_name1);
                        postedFile1.SaveAs(filename1); //Save file at particular location
                    }
                    /////
                    obj.active_status = "A";
                    obj.dept_id = model.dept_id;
                    obj.type = model.type;

                    obj.master_no_id = model.master_no_id;

                    if (model.id == 0)
                        db.other_sr.Add(obj);
                    else
                        db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Record Submitted Successfully!";

                    model.doc_name = "";
                    model.doc_name1 = "";
                    model.doc_location = "";
                    model.doc_location1 = "";
                    obj = db.other_sr.SingleOrDefault(x => x.id == obj.id);
                    if (obj != null)
                    {
                        model.id = obj.id;
                        model.subject = obj.subject;
                        model.subject_eng = obj.subject_eng;
                        model.subject_date1 = obj.subject_date.ToString("dd-MM-yyyy");
                        model.doc_name = obj.doc_name;
                        model.doc_location = obj.doc_location;
                        model.doc_name1 = obj.doc_name1;
                        model.doc_location1 = obj.doc_location1;
                        model.active_status = obj.active_status;
                    }

                    List<SelectListItem> lst1 = new List<SelectListItem>();
                    lst1.Add(new SelectListItem
                    {
                        Text = "प्रारूप योजना मंजुरी",
                        Value = "P"
                    });
                    lst1.Add(new SelectListItem
                    {
                        Text = "अंतिम योजना मंजुरी",
                        Value = "F"
                    });
                    model.typeList = lst1;

                    //Technical dept list
                    var lst_bank = db.sys_config.ToList<sys_config>().Where(a => a.sys_type == "ca_dept" && a.active_status == "A");
                    model.sys_configList = lst_bank.ToList();

                    //योजना list
                    var lst_yojna = db.project_new.ToList<project_new>().Where(a => a.active_status == "A" || a.active_status == "R").OrderBy(a => a.Master_no);
                    model.yojna_List = lst_yojna.ToList();

                    return View(model);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message + " Something went wrong";
                    return View(model);
                }
            }
        }

        //बांधकाम परवानगी व भोगवटा
        public ActionResult cp_other_buildingPermission_bhogvata()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_building_permission_bhogvataModel model = new other_building_permission_bhogvataModel();
                //Building Permission lists
                var lst1 = db.other_building_permission_bhogvata.Where(x => x.type == "B").OrderByDescending(x => x.id).ToList();
                model.other_building_permission_list = lst1.ToList();
                //Bhogvata lists
                var lst2 = db.other_building_permission_bhogvata.Where(x => x.type == "O").OrderByDescending(x => x.id).ToList();
                model.other_bhogvata_list = lst2.ToList();


                return View(model);
            }
        }
        public ActionResult cp_other_buildingPermission_bhogvata_activeDeactive(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    other_building_permission_bhogvata obj = null;
                    if (id != 0)
                    {
                        obj = db.other_building_permission_bhogvata.SingleOrDefault(x => x.id == id);
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
        public ActionResult cp_other_buildingPermission_bhogvata_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_building_permission_bhogvataModel model = new Models.other_building_permission_bhogvataModel();
                other_building_permission_bhogvata obj = new other_building_permission_bhogvata();
                List<SelectListItem> lst1 = new List<SelectListItem>();
                lst1.Add(new SelectListItem
                {
                    Text = "बाधकाम परवानगी",
                    Value = "B"
                });
                lst1.Add(new SelectListItem
                {
                    Text = "भोगवटा प्रमाणपत्र",
                    Value = "O"
                });
                model.typeList = lst1;

                //Technical dept list
                var lst_bank = db.sys_config.ToList<sys_config>().Where(a => a.sys_type == "ca_dept" && a.active_status == "A");
                model.sys_configList = lst_bank.ToList();

                //योजना list
                var lst_yojna = db.project_new.ToList<project_new>().Where(a => a.active_status == "A" || a.active_status == "R").OrderBy(a => a.Master_no);
                model.yojna_List = lst_yojna.ToList();

                if (id != 0 && id != null)
                {
                    obj = db.other_building_permission_bhogvata.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {
                        model.id = obj.id;
                        model.type = obj.type;
                        model.proj_name = obj.proj_name;
                        model.proj_name_eng = obj.proj_name_eng;
                        model.proj_date1 = obj.proj_date.ToString("dd-MM-yyyy");
                        model.doc_name = obj.doc_name;
                        model.doc_location = obj.doc_location;
                        model.active_status = obj.active_status;
                        model.dept_id = obj.dept_id;
                        model.master_no_id = obj.master_no_id;
                    }
                    else
                        ViewBag.error = "Something went wrong.";
                }
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult cp_other_buildingPermission_bhogvata_addedit(other_building_permission_bhogvataModel model, HttpPostedFileBase postedFile)
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
                    other_building_permission_bhogvata obj = null;
                    if (model.id != 0)
                    {
                        obj = db.other_building_permission_bhogvata.SingleOrDefault(x => x.id == model.id);
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
                            //Move file if type or notice_yadi changed
                            else if (obj.type != model.type)
                            {
                                string sourceFile = System.IO.Path.Combine(Server.MapPath(obj.doc_location), obj.doc_name);
                                string destinationFile = "";
                                if (model.type == "B")
                                {
                                    obj.doc_location = "~/Content/Documents/BuildingPermission/";
                                    destinationFile = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/BuildingPermission"), obj.doc_name);
                                }
                                else if (model.type == "O")
                                {
                                    obj.doc_location = "~/Content/Documents/Bhogvata/";
                                    destinationFile = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Bhogvata"), obj.doc_name);
                                }
                                // System.IO.File.Move(sourceFile, destinationFile);
                                FileInfo file = new FileInfo(sourceFile);
                                if (file.Exists)
                                    file.MoveTo(destinationFile); //Move file at particular location
                            }
                        }
                    }
                    else
                    {
                        obj = new other_building_permission_bhogvata();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        obj.created_date = DateTime.Today;
                    }
                    obj.type = model.type;
                    obj.proj_name = model.proj_name;
                    obj.proj_name_eng = model.proj_name_eng;
                    DateTime dateTime = new DateTime();
                    dateTime = Convert.ToDateTime(DateTime.ParseExact(model.proj_date1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    obj.proj_date = dateTime;
                    obj.proj_date_eng = dateTime;
                    if (filename != "")
                    {
                        obj.doc_name = filename;
                        if (model.type == "B")
                        {
                            obj.doc_location = "~/Content/Documents/BuildingPermission/";
                            filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/BuildingPermission"), obj.doc_name);
                        }
                        else if (model.type == "O")
                        {
                            obj.doc_location = "~/Content/Documents/Bhogvata/";
                            filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Bhogvata"), obj.doc_name);
                        }
                        postedFile.SaveAs(filename); //Save file at particular location
                    }
                    obj.active_status = "A";
                    obj.dept_id = model.dept_id;
                    obj.master_no_id = model.master_no_id;

                    if (model.id == 0)
                        db.other_building_permission_bhogvata.Add(obj);
                    else
                        db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Record Submitted Successfully!";
                    List<SelectListItem> lsttype = new List<SelectListItem>();
                    lsttype.Add(new SelectListItem
                    {
                        Text = "बाधकाम परवानगी",
                        Value = "B"
                    });
                    lsttype.Add(new SelectListItem
                    {
                        Text = "भोगवटा प्रमाणपत्र",
                        Value = "O"
                    });
                    model.typeList = lsttype;

                    //Technical dept list
                    var lst_bank = db.sys_config.ToList<sys_config>().Where(a => a.sys_type == "ca_dept" && a.active_status == "A");
                    model.sys_configList = lst_bank.ToList();

                    //योजना list
                    var lst_yojna = db.project_new.ToList<project_new>().Where(a => a.active_status == "A" || a.active_status == "R").OrderBy(a => a.Master_no);
                    model.yojna_List = lst_yojna.ToList();

                    return View(model);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Something went wrong";
                    return View(model);
                }
            }
        }

        //दरपत्रक

        public ActionResult cp_other_darpatrak()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_darpatrakModel model = new other_darpatrakModel();
                //Darpatrak lists
                var lst1 = db.other_darpatrak.Where(x=>x.type=="D").OrderByDescending(x => x.id).ToList();
                model.other_darpatrakList = lst1.ToList();
                return View(model);
            }
        }
        public ActionResult cp_other_darpatrak_activeDeactive(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    other_darpatrak obj = null;
                    if (id != 0)
                    {
                        obj = db.other_darpatrak.SingleOrDefault(x => x.id == id);
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
        public ActionResult cp_other_darpatrak_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_darpatrakModel model = new Models.other_darpatrakModel();
                other_darpatrak obj = new other_darpatrak();
                if (id != 0 && id != null)
                {
                    obj = db.other_darpatrak.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {

                        model.id = obj.id;
                        model.subject = obj.subject;
                        model.subject_eng = obj.subject_eng;
                        model.subject_date1 = obj.subject_date.ToString("dd-MM-yyyy");
                        model.subject_date_eng1 = obj.subject_date_eng.ToString("dd-MM-yyyy");
                        model.due_date1 = obj.due_date.ToString("dd-MM-yyyy"); ;
                        model.due_date_eng1 = obj.due_date_eng.ToString("dd-MM-yyyy"); ;
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
        public ActionResult cp_other_darpatrak_addedit(other_darpatrakModel model, HttpPostedFileBase postedFile)
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
                    other_darpatrak obj = null;
                    if (model.id != 0)
                    {
                        obj = db.other_darpatrak.SingleOrDefault(x => x.id == model.id);
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
                        obj = new other_darpatrak();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        obj.created_date = DateTime.Today;
                    }
                    obj.subject = model.subject;
                    obj.subject_eng = model.subject_eng;
                    DateTime dateTime = new DateTime();
                    dateTime = Convert.ToDateTime(DateTime.ParseExact(model.subject_date1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    obj.subject_date = dateTime;
                    obj.subject_date_eng = dateTime;
                    DateTime dateTime1 = new DateTime();
                    dateTime1 = Convert.ToDateTime(DateTime.ParseExact(model.due_date1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    obj.due_date = dateTime1;
                    obj.due_date_eng = dateTime1;
                    if (filename != "")
                    {
                        obj.doc_name = filename;
                        obj.doc_location = "~/Content/Documents/Darpatrak/";
                        filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Darpatrak"), obj.doc_name);
                        postedFile.SaveAs(filename); //Save file at particular location
                    }
                    obj.active_status = "A";
                    obj.type = "D"; //Type Darpatrak
                    if (model.id == 0)
                        db.other_darpatrak.Add(obj);
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
        //Tender
        public ActionResult cp_other_tender()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_darpatrakModel model = new other_darpatrakModel();
                //Tender lists
                var lst1 = db.other_darpatrak.Where(x => x.type == "T").OrderByDescending(x => x.id).ToList();
                model.other_darpatrakList = lst1.ToList();
                return View(model);
            }
        }
        public ActionResult cp_other_tender_activeDeactive(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    other_darpatrak obj = null;
                    if (id != 0)
                    {
                        obj = db.other_darpatrak.SingleOrDefault(x => x.id == id);
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
        public ActionResult cp_other_tender_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_darpatrakModel model = new Models.other_darpatrakModel();
                other_darpatrak obj = new other_darpatrak();
                if (id != 0 && id != null)
                {
                    obj = db.other_darpatrak.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {

                        model.id = obj.id;
                        model.subject = obj.subject;
                        model.subject_eng = obj.subject_eng;
                        model.subject_date1 = obj.subject_date.ToString("dd-MM-yyyy");
                        model.subject_date_eng1 = obj.subject_date_eng.ToString("dd-MM-yyyy");
                        model.due_date1 = obj.due_date.ToString("dd-MM-yyyy"); ;
                        model.due_date_eng1 = obj.due_date_eng.ToString("dd-MM-yyyy"); ;
                        model.doc_name = obj.doc_name;
                        model.doc_location = obj.doc_location;
                        model.active_status = obj.active_status;
                        model.tender_link = obj.tender_link;
                    }
                    else
                        ViewBag.error = "Something went wrong.";
                }
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult cp_other_tender_addedit(other_darpatrakModel model, HttpPostedFileBase postedFile)
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
                    other_darpatrak obj = null;
                    if (model.id != 0)
                    {
                        obj = db.other_darpatrak.SingleOrDefault(x => x.id == model.id);
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
                        obj = new other_darpatrak();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        obj.created_date = DateTime.Today;
                    }
                    obj.subject = model.subject;
                    obj.subject_eng = model.subject_eng;
                    DateTime dateTime = new DateTime();
                    dateTime = Convert.ToDateTime(DateTime.ParseExact(model.subject_date1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    obj.subject_date = dateTime;
                    obj.subject_date_eng = dateTime;
                    DateTime dateTime1 = new DateTime();
                    dateTime1 = Convert.ToDateTime(DateTime.ParseExact(model.due_date1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    obj.due_date = dateTime1;
                    obj.due_date_eng = dateTime1;
                    if (filename != "")
                    {
                        obj.doc_name = filename;
                        obj.doc_location = "~/Content/Documents/Darpatrak/";
                        filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Darpatrak"), obj.doc_name);
                        postedFile.SaveAs(filename); //Save file at particular location
                    }
                    obj.active_status = "A";
                    obj.tender_link = model.tender_link;
                    obj.type = "T"; //Type Tender
                    if (model.id == 0)
                        db.other_darpatrak.Add(obj);
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

       
        //लॉटरी 
        
        public ActionResult cp_other_lottery()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_lotteryModel model = new other_lotteryModel();
                //Lottery lists
                var lst1 = db.other_lottery.OrderByDescending(x => x.id).ToList();
                model.other_lotteryList = lst1.ToList();
                return View(model);
            }
        }
        public ActionResult cp_other_lottery_activeDeactive(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    other_lottery obj = null;
                    if (id != 0)
                    {
                        obj = db.other_lottery.SingleOrDefault(x => x.id == id);
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
        public ActionResult cp_other_lottery_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_lotteryModel model = new Models.other_lotteryModel();
                other_lottery obj = new other_lottery();
                if (id != 0 && id != null)
                {
                    obj = db.other_lottery.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {
                        model.id = obj.id;
                        model.proj_name = obj.proj_name;
                        model.proj_name_eng = obj.proj_name_eng;
                        model.developer = obj.developer;
                        model.developer_eng = obj.developer_eng;
                        model.lottery_year = obj.lottery_year;
                        model.lottery_year_eng = obj.lottery_year_eng;
                        model.flat_allotment_date1 = obj.flat_allotment_date.ToString("dd-MM-yyyy");
                        model.total_flats = obj.total_flats;
                        model.total_flats_eng = obj.total_flats_eng;
                        model.allotted_flats = obj.allotted_flats;
                        model.allotted_flats_eng = obj.allotted_flats_eng;
                        model.doc_name = obj.doc_name;
                        model.doc_location = obj.doc_location;
                        model.video_url = obj.video_url;
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
        public ActionResult cp_other_lottery_addedit(other_lotteryModel model, HttpPostedFileBase postedFile)
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
                        if(slipttext.Count()>1)
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
                    other_lottery obj = null;
                    if (model.id != 0)
                    {
                        obj = db.other_lottery.SingleOrDefault(x => x.id == model.id);
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
                        obj = new other_lottery();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        obj.created_date = DateTime.Today;
                    }
                    obj.proj_name = model.proj_name;
                    obj.proj_name_eng = model.proj_name_eng;
                    obj.developer = model.developer;
                    obj.developer_eng = model.developer_eng;
                    DateTime dateTime = new DateTime();
                    dateTime = Convert.ToDateTime(DateTime.ParseExact(model.flat_allotment_date1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    obj.flat_allotment_date = dateTime;
                    obj.flat_allotment_date_eng = dateTime;
                    obj.lottery_year = dateTime.Year.ToString();
                    obj.total_flats = model.total_flats;
                    obj.total_flats_eng = model.total_flats;// model.total_flats_eng;
                    obj.allotted_flats = model.allotted_flats;
                    obj.allotted_flats_eng = model.allotted_flats; //model.allotted_flats_eng;
                    if (filename != "")
                    {
                        obj.doc_name = filename;
                        obj.doc_location = "~/Content/Documents/Lottery/";
                        filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Lottery"), obj.doc_name);
                        postedFile.SaveAs(filename); //Save file at particular location
                    }
                    obj.video_url = model.video_url;
                    obj.active_status = "A";
                    if (model.id == 0)
                        db.other_lottery.Add(obj);
                    else
                        db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Record Submitted Successfully!";
                    return View(model);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Something went wrong" + ex.Message;
                    return View(model);
                }
            }
        }

        //विविध नमुने
        public ActionResult cp_other_vividhNamune()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_vividh_namuneModel model = new other_vividh_namuneModel();
                var lst1 = db.other_vividh_namune.OrderByDescending(x => x.id).ToList();
                model.vividhNamuneList = lst1.ToList();
                return View(model);
            }
        }
        public ActionResult cp_other_vividhNamune_activeDeactive(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    other_vividh_namune obj = null;
                    if (id != 0)
                    {
                        obj = db.other_vividh_namune.SingleOrDefault(x => x.id == id);
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
        public ActionResult cp_other_vividhNamune_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_vividh_namuneModel model = new Models.other_vividh_namuneModel();
                other_vividh_namune obj = new other_vividh_namune();
                if (id != 0 && id != null)
                {
                    obj = db.other_vividh_namune.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {

                        model.id = obj.id;
                        model.namuna_desc = obj.namuna_desc;
                        model.namuna_desc_eng = obj.namuna_desc_eng;
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
        public ActionResult cp_other_vividhNamune_addedit(other_vividh_namuneModel model, HttpPostedFileBase postedFile)
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
                    other_vividh_namune obj = null;
                    if (model.id != 0)
                    {
                        obj = db.other_vividh_namune.SingleOrDefault(x => x.id == model.id);
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
                        obj = new other_vividh_namune();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        obj.created_date = DateTime.Today;
                    }
                    obj.namuna_desc = model.namuna_desc;
                    obj.namuna_desc_eng = model.namuna_desc_eng;
                    if (filename != "")
                    {
                        obj.doc_name = filename;
                        obj.doc_location = "~/Content/Documents/vividh_namune/";
                        filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/vividh_namune"), obj.doc_name);
                        postedFile.SaveAs(filename); //Save file at particular location
                    }
                    obj.active_status = "A";
                    if (model.id == 0)
                        db.other_vividh_namune.Add(obj);
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

        //RTI 4C
        public ActionResult rti4c()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_rti_4cModel model = new other_rti_4cModel();
                var lst1 = db.other_rti_4c.OrderByDescending(x => x.id).ToList();
                model.rti_4cList = lst1.ToList();
                return View(model);
            }
        }
        public ActionResult rti4c_activeDeactive(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    other_rti_4c obj = null;
                    if (id != 0)
                    {
                        obj = db.other_rti_4c.SingleOrDefault(x => x.id == id);
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
        public ActionResult rti4c_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_rti_4cModel model = new Models.other_rti_4cModel();
                other_rti_4c obj = new other_rti_4c();
                if (id != 0 && id != null)
                {
                    obj = db.other_rti_4c.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {

                        model.id = obj.id;
                        model.year = obj.year;
                        model.year_eng = obj.year_eng;
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
        public ActionResult rti4c_addedit(other_rti_4cModel model, HttpPostedFileBase postedFile)
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
                    other_rti_4c obj = null;
                    if (model.id != 0)
                    {
                        obj = db.other_rti_4c.SingleOrDefault(x => x.id == model.id);
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
                        obj = new other_rti_4c();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        obj.created_date = DateTime.Today;
                    }
                    obj.year = model.year;
                    obj.year_eng = model.year_eng;
                    if (filename != "")
                    {
                        obj.doc_name = filename;
                        obj.doc_location = "~/Content/Documents/rti_4c/";
                        filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/rti_4c"), obj.doc_name);
                        postedFile.SaveAs(filename); //Save file at particular location
                    }
                    obj.active_status = "A";
                    if (model.id == 0)
                        db.other_rti_4c.Add(obj);
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
        
        


        ////विकसक

        //public ActionResult cp_other_developer()
        //{
        //    if (Session["UserID"] == null)
        //    {
        //        return RedirectToAction("Login", "CPAdmin");
        //    }
        //    else
        //    {

        //        other_developerModel model = new other_developerModel();
        //        //Developer lists
        //        var lst1 = db.other_developers.OrderByDescending(x => x.id).ToList();
        //        //model.developerList = lst1.ToList();
        //        var lst_tblurlid = db.tbl_urlId.ToList();
        //        var lstnew = from s in lst1
        //                     join u in lst_tblurlid on s.id equals u.id into table1
        //                     from u in table1.ToList()
        //                     select new ViewModel
        //                     {
        //                         other_developers = s,
        //                         tbl_urlId = u
        //                     };
        //        model.lst_viewModel_developer = lstnew.ToList();
        //        return View(model);
        //    }
        //}

        //public ActionResult cp_other_developer_addedit(string urlid)
        //{
        //    if (Session["UserID"] == null)
        //    {
        //        return RedirectToAction("Login", "CPAdmin");
        //    }
        //    else
        //    {
        //        other_developerModel model = new Models.other_developerModel();
        //        other_developers obj = new other_developers();
        //        int id = decreptid(urlid);
        //        if (id != 0 && id != null)
        //        {
        //            obj = db.other_developers.SingleOrDefault(x => x.id == id);
        //            if (obj != null)
        //            {
        //                model.id = obj.id;
        //                model.dev_name = obj.dev_name;
        //                model.dev_name_eng = obj.dev_name_eng;
        //                model.dev_address = obj.dev_address;
        //                model.dev_address_eng = obj.dev_address_eng;
        //                model.dev_contact = obj.dev_contact;
        //                model.dev_contact_eng = obj.dev_contact_eng;
        //                model.dev_class = obj.dev_class;
        //                model.dev_class_eng = obj.dev_class_eng;
        //                model.dev_registration_no = obj.dev_registration_no;
        //                model.dev_registration_no_eng = obj.dev_registration_no_eng;
        //                model.dev_registration_date1 = obj.dev_registration_date.ToString("dd-MM-yyyy");
        //                model.completed_projects = obj.completed_projects;
        //                model.completed_projects_eng = obj.completed_projects_eng;
        //                model.current_projects = obj.current_projects;
        //                model.current_projects_eng = obj.current_projects_eng;
        //            }
        //            else
        //                ViewBag.error = "Something went wrong.";
        //        }
        //        return View(model);
        //    }
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public ActionResult cp_other_developer_addedit(other_developerModel model)
        //{
        //    if (Session["UserID"] == null)
        //    {
        //        return RedirectToAction("Login", "CPAdmin");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            other_developers obj = null;
        //            if (model.id != 0)
        //            {
        //                obj = db.other_developers.SingleOrDefault(x => x.id == model.id);
        //                if (obj == null)
        //                {
        //                    return View();
        //                }
        //                else
        //                {
        //                    obj.updated_by = Convert.ToInt32(Session["UserID"]);
        //                    obj.updated_date = DateTime.Today;
        //                    if (obj.updated_count == null)
        //                        obj.updated_count = 1;
        //                    else
        //                        obj.updated_count += 1;
        //                }
        //            }
        //            else
        //            {
        //                obj = new other_developers();
        //                obj.created_by = Convert.ToInt32(Session["UserID"]);
        //                obj.created_date = DateTime.Today;
        //            }
        //            obj.dev_name = model.dev_name;
        //            obj.dev_name_eng = model.dev_name_eng;
        //            obj.dev_address = model.dev_address;
        //            obj.dev_address_eng = model.dev_address_eng;
        //            obj.dev_contact = model.dev_contact;
        //            obj.dev_contact_eng = model.dev_contact_eng;
        //            obj.dev_class = model.dev_class;
        //            obj.dev_class_eng = model.dev_class_eng;
        //            obj.dev_registration_no = model.dev_registration_no;
        //            obj.dev_registration_no_eng = model.dev_registration_no_eng;
        //            DateTime dateTime = new DateTime();
        //            dateTime = Convert.ToDateTime(DateTime.ParseExact(model.dev_registration_date1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
        //            obj.dev_registration_date = dateTime;
        //            //obj.dev_registration_date = model.dev_registration_date;
        //            obj.completed_projects = model.completed_projects;
        //            obj.completed_projects_eng = model.completed_projects_eng;
        //            obj.current_projects = model.current_projects;
        //            obj.current_projects_eng = model.current_projects_eng;
        //            obj.active_status = "A";
        //            if (model.id == 0)
        //                db.other_developers.Add(obj);
        //            else
        //                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        //            db.SaveChanges();
        //            ViewBag.Message = "Record Submitted Successfully!";

        //            return View(model);
        //        }
        //        catch (Exception)
        //        {
        //            ViewBag.Message = "Something went wrong";
        //            throw;
        //        }
        //    }

        //}

        //public ActionResult cp_other_developer_activeDeactive(string urlid)
        //{
        //    if (Session["UserID"] == null)
        //    {
        //        return RedirectToAction("Login", "CPAdmin");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            other_developers obj = null;
        //            int id = decreptid(urlid);
        //            if (id != 0)
        //            {
        //                obj = db.other_developers.SingleOrDefault(x => x.id == id);
        //                if (obj == null)
        //                {
        //                    return View();
        //                }
        //                else
        //                {
        //                    obj.updated_by = Convert.ToInt32(Session["UserID"]);
        //                    obj.updated_date = DateTime.Today;
        //                    if (obj.updated_count == null)
        //                        obj.updated_count = 1;
        //                    else
        //                        obj.updated_count += 1;
        //                    if (obj.active_status == null)
        //                        obj.active_status = "A";
        //                    else if (obj.active_status == "A")
        //                    {
        //                        obj.active_status = "D";
        //                        ViewBag.activeDeactiveMessage = "Record Removed Successfully!";
        //                    }
        //                    else if (obj.active_status == "D")
        //                    {
        //                        obj.active_status = "A";
        //                        ViewBag.activeDeactiveMessage = "Record Added Successfully!";
        //                    }
        //                    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        //                    db.SaveChanges();
        //                    return View();
        //                }
        //            }
        //            else
        //                return View();
        //        }
        //        catch (Exception)
        //        {
        //            ViewBag.Message = "Something went wrong";
        //            throw;
        //        }
        //    }

        //}

        ////नियमावली

        //public ActionResult cp_other_Niyamavali()
        //{
        //    if (Session["UserID"] == null)
        //    {
        //        return RedirectToAction("Login", "CPAdmin");
        //    }
        //    else
        //    {
        //        //List<other_sra_rulesModel> model = new List<other_sra_rulesModel>();
        //        ////SRA Rules list
        //        //List<other_sra_rules> obj = new List<other_sra_rules>();
        //        //obj = db.other_sra_rules.OrderByDescending(x => x.id).ToList();
        //        //foreach (var item in obj)
        //        //{
        //        //    other_sra_rulesModel model1 = new other_sra_rulesModel();
        //        //    model1.id = item.id;
        //        //    model1.rule_name = item.rule_name;
        //        //    model1.rule_name_eng = item.rule_name_eng;
        //        //    model1.rule_year_eng = item.rule_year_eng;
        //        //    model1.rule_year = item.rule_year;
        //        //    model1.doc_name = item.doc_name;
        //        //    model1.doc_location = item.doc_location;
        //        //    model1.active_status = item.active_status;
        //        //    model1.rule_date1 = item.rule_date.ToString("dd-MM-yyyy");
        //        //    model1.rule_date = item.rule_date;
        //        //    model.Add(model1);
        //        //}

        //        other_sra_rulesModel model = new other_sra_rulesModel();
        //        //Developer lists
        //        var lst1 = db.other_sra_rules.OrderByDescending(x => x.id).ToList();
        //        var lst_tblurlid = db.tbl_urlId.ToList();
        //        var lstnew = from s in lst1
        //                     join u in lst_tblurlid on s.id equals u.id into table1
        //                     from u in table1.ToList()
        //                     select new ViewModel
        //                     {
        //                         other_sra_rules = s,
        //                         tbl_urlId = u
        //                     };
        //        model.lst_viewModel_sra_rules = lstnew.ToList();
        //        return View(model);
        //    }
        //}

        //public ActionResult cp_other_Niyamavali_addedit(string urlid)
        //{
        //    other_sra_rulesModel model = new Models.other_sra_rulesModel();
        //    other_sra_rules obj = new other_sra_rules();
        //    int id = decreptid(urlid);
        //    if (id != 0 && id != null)
        //    {
        //        obj = db.other_sra_rules.SingleOrDefault(x => x.id == id);
        //        if (obj != null)
        //        {
        //            model.id = obj.id;
        //            model.rule_name = obj.rule_name;
        //            model.rule_name_eng = obj.rule_name_eng;
        //            model.rule_year = obj.rule_year;
        //            model.rule_year_eng = obj.rule_year_eng;
        //            model.doc_name = obj.doc_name;
        //            model.doc_location = obj.doc_location;
        //            model.rule_date1 = obj.rule_date.ToString("dd-MM-yyyy");
        //        }
        //        else
        //            ViewBag.error = "Something went wrong.";
        //    }
        //    return View(model);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public ActionResult cp_other_Niyamavali_addedit(other_sra_rulesModel model, HttpPostedFileBase postedFile)
        //{

        //    if (Session["UserID"] == null)
        //    {
        //        return RedirectToAction("Login", "CPAdmin");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            string filename = "";
        //            string extension = "";

        //            if (postedFile != null)
        //            {
        //                filename = System.IO.Path.GetFileNameWithoutExtension(postedFile.FileName);
        //                string[] slipttext = filename.Split('.');
        //                if (slipttext.Count() > 1)
        //                {
        //                    ViewBag.fileExtensionMessage = "Invalid file!";
        //                    return View(model);
        //                }
        //                extension = System.IO.Path.GetExtension(postedFile.FileName);
        //                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
        //                if (extension != ".pdf")
        //                {
        //                    ViewBag.fileExtensionMessage = "Please select pdf file";
        //                    return View(model);
        //                }
        //            }

        //            other_sra_rules obj = null;
        //            if (model.id != 0)
        //            {
        //                obj = db.other_sra_rules.SingleOrDefault(x => x.id == model.id);
        //                if (obj == null)
        //                {
        //                    return View(model);
        //                }
        //                else
        //                {
        //                    obj.updated_by = Convert.ToInt32(Session["UserID"]);
        //                    obj.updated_date = DateTime.Today;
        //                    if (obj.updated_count == null)
        //                        obj.updated_count = 1;
        //                    else
        //                        obj.updated_count += 1;
        //                }
        //            }
        //            else
        //            {
        //                obj = new other_sra_rules();
        //                obj.created_by = Convert.ToInt32(Session["UserID"]);
        //                obj.created_date = DateTime.Today;
        //            }
        //            obj.rule_name = model.rule_name;
        //            obj.rule_name_eng = model.rule_name_eng;
        //            obj.rule_year = model.rule_year;
        //            obj.rule_year_eng = model.rule_year_eng;
        //            DateTime dateTime = new DateTime();
        //            dateTime = Convert.ToDateTime(DateTime.ParseExact(model.rule_date1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
        //            obj.rule_date = dateTime;
        //            if (filename != "")
        //            {
        //                obj.doc_name = filename;
        //                obj.doc_location = "~/Content/Documents/sra_rules/";
        //                filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/sra_rules"), filename);
        //                postedFile.SaveAs(filename); //Save file at particular location
        //            }
        //            obj.active_status = "A";
        //            if (model.id == 0)
        //                db.other_sra_rules.Add(obj);
        //            else
        //                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        //            db.SaveChanges();
        //            ViewBag.Message = "Record Submitted Successfully!";
        //            return View(model);
        //        }
        //        catch (Exception)
        //        {
        //            ViewBag.Message = "Something went wrong";
        //            return View(model);
        //        }
        //    }

        //}

        //public ActionResult cp_other_Niyamavali_activeDeactive(string urlid)
        //{
        //    if (Session["UserID"] == null)
        //    {
        //        return RedirectToAction("Login", "CPAdmin");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            other_sra_rules obj = null;
        //            int id = decreptid(urlid);
        //            if (id != 0)
        //            {
        //                obj = db.other_sra_rules.SingleOrDefault(x => x.id == id);
        //                if (obj == null)
        //                {
        //                    return View();
        //                }
        //                else
        //                {
        //                    obj.updated_by = Convert.ToInt32(Session["UserID"]);
        //                    obj.updated_date = DateTime.Today;
        //                    if (obj.updated_count == null)
        //                        obj.updated_count = 1;
        //                    else
        //                        obj.updated_count += 1;
        //                    if (obj.active_status == null)
        //                        obj.active_status = "A";
        //                    else if (obj.active_status == "A")
        //                    {
        //                        obj.active_status = "D";
        //                        ViewBag.activeDeactiveMessage = "Record Removed Successfully!";
        //                    }
        //                    else if (obj.active_status == "D")
        //                    {
        //                        obj.active_status = "A";
        //                        ViewBag.activeDeactiveMessage = "Record Added Successfully!";
        //                    }
        //                    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        //                    db.SaveChanges();
        //                    return View();
        //                }
        //            }
        //            else
        //                return View();
        //        }
        //        catch (Exception)
        //        {
        //            ViewBag.Message = "Something went wrong";
        //            throw;
        //        }
        //    }

        //}

        ////जाहीर प्रकटन

        //public ActionResult cp_other_jahirPrakatan()
        //{
        //    if (Session["UserID"] == null)
        //    {
        //        return RedirectToAction("Login", "CPAdmin");
        //    }
        //    else
        //    {
        //        other_jahirPrakatanModel model = new other_jahirPrakatanModel();
        //        //other_jahir_prakatan_Preliminary_survey
        //        var lst_jahirPrakatanPriliminary = db.other_jahir_prakatan_Preliminary_survey.OrderByDescending(x => x.id).ToList(); ;
        //        //model.OtherJahirPrakatanList = lst_jahirPrakatanPriliminary.ToList();
        //        var lst_tblurlid = db.tbl_urlId.ToList();
        //        var lstnew = from s in lst_jahirPrakatanPriliminary
        //                     join u in lst_tblurlid on s.id equals u.id into table1
        //                     from u in table1.ToList()
        //                     select new ViewModel
        //                     {
        //                         other_jahir_prakatan_Preliminary_survey = s,
        //                         tbl_urlId = u
        //                     };
        //        model.lst_viewModel_JahirPrakatan = lstnew.ToList();

        //        //primary notice list
        //        var lst_primary_Notice = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "P" && x.is_notice_list == "N").OrderByDescending(x => x.id).ToList(); ;
        //        //model.primary_Notice_List = lst_primary_Notice.ToList();
        //        var lstnew1 = from s in lst_primary_Notice
        //                      join u in lst_tblurlid on s.id equals u.id into table1
        //                      from u in table1.ToList()
        //                      select new ViewModel
        //                      {
        //                          other_jahir_prakatan_primary_final_eligibility = s,
        //                          tbl_urlId = u
        //                      };
        //        model.lst_viewModel_primary_Notice = lstnew1.ToList();

        //        //primary list list
        //        var lst_primary_List = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "P" && x.is_notice_list == "L").OrderByDescending(x => x.id).ToList();
        //        //model.primary_List_List = lst_primary_List.ToList();
        //        var lstnew2 = from s in lst_primary_List
        //                      join u in lst_tblurlid on s.id equals u.id into table1
        //                      from u in table1.ToList()
        //                      select new ViewModel
        //                      {
        //                          other_jahir_prakatan_primary_final_eligibility = s,
        //                          tbl_urlId = u
        //                      };
        //        model.lst_viewModel_primary_List = lstnew2.ToList();

        //        //final notice list
        //        var lst_final_Notice = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "F" && x.is_notice_list == "N").OrderByDescending(x => x.id).ToList();
        //        //model.final_Notice_List = lst_final_Notice.ToList();
        //        var lstnew3 = from s in lst_final_Notice
        //                      join u in lst_tblurlid on s.id equals u.id into table1
        //                      from u in table1.ToList()
        //                      select new ViewModel
        //                      {
        //                          other_jahir_prakatan_primary_final_eligibility = s,
        //                          tbl_urlId = u
        //                      };
        //        model.lst_viewModel_final_Notice = lstnew3.ToList();

        //        //final list list
        //        var lst_final_List_List = db.other_jahir_prakatan_primary_final_eligibility.Where(x => x.type == "F" && x.is_notice_list == "L").OrderByDescending(x => x.id).ToList();
        //        //model.final_List_List = lst_final_List_List.ToList();
        //        var lstnew4 = from s in lst_final_List_List
        //                      join u in lst_tblurlid on s.id equals u.id into table1
        //                      from u in table1.ToList()
        //                      select new ViewModel
        //                      {
        //                          other_jahir_prakatan_primary_final_eligibility = s,
        //                          tbl_urlId = u
        //                      };
        //        model.lst_viewModel_final_List = lstnew4.ToList();

        //        return View(model);
        //    }
        //}

        //public ActionResult cp_other_jahirPrakatan_preliminary_activeDeactive(string urlid)
        //{
        //    if (Session["UserID"] == null)
        //    {
        //        return RedirectToAction("Login", "CPAdmin");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            other_jahir_prakatan_Preliminary_survey obj = null;
        //            int id = decreptid(urlid);
        //            if (id != 0)
        //            {
        //                obj = db.other_jahir_prakatan_Preliminary_survey.SingleOrDefault(x => x.id == id);
        //                if (obj == null)
        //                {
        //                    return View();
        //                }
        //                else
        //                {
        //                    obj.updated_by = Convert.ToInt32(Session["UserID"]);
        //                    obj.updated_date = DateTime.Today;
        //                    if (obj.updated_count == null)
        //                        obj.updated_count = 1;
        //                    else
        //                        obj.updated_count += 1;
        //                    if (obj.active_status == null)
        //                        obj.active_status = "A";
        //                    else if (obj.active_status == "A")
        //                    {
        //                        obj.active_status = "D";
        //                        ViewBag.activeDeactiveMessage = "Record Removed Successfully!";
        //                    }
        //                    else if (obj.active_status == "D")
        //                    {
        //                        obj.active_status = "A";
        //                        ViewBag.activeDeactiveMessage = "Record Added Successfully!";
        //                    }
        //                    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        //                    db.SaveChanges();
        //                    return View();
        //                }
        //            }
        //            else
        //                return View();
        //        }
        //        catch (Exception)
        //        {
        //            ViewBag.Message = "Something went wrong";
        //            throw;
        //        }
        //    }
        //}

        //public ActionResult cp_other_jahirPrakatan_primary_final_activeDeactive(string urlid)
        //{
        //    if (Session["UserID"] == null)
        //    {
        //        return RedirectToAction("Login", "CPAdmin");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            other_jahir_prakatan_primary_final_eligibility obj = null;
        //            int id = decreptid(urlid);
        //            if (id != 0)
        //            {
        //                obj = db.other_jahir_prakatan_primary_final_eligibility.SingleOrDefault(x => x.id == id);
        //                if (obj == null)
        //                {
        //                    return View();
        //                }
        //                else
        //                {
        //                    obj.updated_by = Convert.ToInt32(Session["UserID"]);
        //                    obj.updated_date = DateTime.Today;
        //                    if (obj.updated_count == null)
        //                        obj.updated_count = 1;
        //                    else
        //                        obj.updated_count += 1;
        //                    if (obj.active_status == null)
        //                        obj.active_status = "A";
        //                    else if (obj.active_status == "A")
        //                    {
        //                        obj.active_status = "D";
        //                        ViewBag.activeDeactiveMessage = "Record Removed Successfully!";
        //                    }
        //                    else if (obj.active_status == "D")
        //                    {
        //                        obj.active_status = "A";
        //                        ViewBag.activeDeactiveMessage = "Record Added Successfully!";
        //                    }
        //                    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        //                    db.SaveChanges();
        //                    return View();
        //                }
        //            }
        //            else
        //                return View();
        //        }
        //        catch (Exception)
        //        {
        //            ViewBag.Message = "Something went wrong";
        //            throw;
        //        }
        //    }

        //}

        //public ActionResult cp_other_jahirPrakatan_Preliminary_addedit(string urlid)
        //{
        //    if (Session["UserID"] == null)
        //    {
        //        return RedirectToAction("Login", "CPAdmin");
        //    }
        //    else
        //    {
        //        other_jahirPrakatanModel model = new Models.other_jahirPrakatanModel();
        //        other_jahir_prakatan_Preliminary_survey obj = new other_jahir_prakatan_Preliminary_survey();
        //        int id = decreptid(urlid);
        //        if (id != 0 && id != null)
        //        {
        //            obj = db.other_jahir_prakatan_Preliminary_survey.SingleOrDefault(x => x.id == id);
        //            if (obj != null)
        //            {
        //                model.id = obj.id;
        //                model.disclosure_number = obj.disclosure_number;
        //                model.disclosure_number_eng = obj.disclosure_number_eng;
        //                model.subject = obj.subject;
        //                model.subject_eng = obj.subject_eng;
        //                model.upload_date = obj.upload_date;
        //                model.upload_date_New1 = obj.upload_date_new.ToString("dd-MM-yyyy");
        //                model.doc_name = obj.doc_name;
        //                model.doc_location = obj.doc_location;
        //                model.active_status = obj.active_status;
        //            }
        //            else
        //                ViewBag.error = "Something went wrong.";
        //        }
        //        return View(model);
        //    }
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public ActionResult cp_other_jahirPrakatan_Preliminary_addedit(other_jahirPrakatanModel model, HttpPostedFileBase postedFile)
        //{
        //    if (Session["UserID"] == null)
        //    {
        //        return RedirectToAction("Login", "CPAdmin");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            string filename = "";
        //            string extension = "";

        //            if (postedFile != null)
        //            {
        //                filename = System.IO.Path.GetFileNameWithoutExtension(postedFile.FileName);
        //                string[] slipttext = filename.Split('.');
        //                if (slipttext.Count() > 1)
        //                {
        //                    ViewBag.fileExtensionMessage = "Invalid file!";
        //                    return View(model);
        //                }
        //                extension = System.IO.Path.GetExtension(postedFile.FileName);
        //                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
        //                if (extension != ".pdf")
        //                {
        //                    ViewBag.fileExtensionMessage = "Please select pdf file";
        //                    return View(model);
        //                }
        //            }
        //            other_jahir_prakatan_Preliminary_survey obj = null;
        //            if (model.id != 0)
        //            {
        //                obj = db.other_jahir_prakatan_Preliminary_survey.SingleOrDefault(x => x.id == model.id);
        //                if (obj == null)
        //                {
        //                    return View(model);
        //                }
        //                else
        //                {
        //                    obj.updated_by = Convert.ToInt32(Session["UserID"]);
        //                    obj.updated_date = DateTime.Today;
        //                    if (obj.updated_count == null)
        //                        obj.updated_count = 1;
        //                    else
        //                        obj.updated_count += 1;
        //                    //Delete Existing File
        //                    if (obj.doc_name != "" && filename != "")
        //                    {
        //                        string file_name = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Public_Notices/Preliminary"), obj.doc_name);
        //                        FileInfo file = new FileInfo(file_name);
        //                        if (file.Exists)
        //                            file.Delete(); //Delete file at particular location
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                obj = new other_jahir_prakatan_Preliminary_survey();
        //                obj.created_by = Convert.ToInt32(Session["UserID"]);
        //                obj.created_date = DateTime.Now;
        //            }
        //            obj.disclosure_number = model.disclosure_number;
        //            obj.disclosure_number_eng = model.disclosure_number_eng;
        //            obj.subject = model.subject;
        //            obj.subject_eng = model.subject_eng;
        //            DateTime dateTime = new DateTime();
        //            dateTime = Convert.ToDateTime(DateTime.ParseExact(model.upload_date_New1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
        //            obj.upload_date_new = dateTime;
        //            if (filename != "")
        //            {
        //                obj.doc_name = filename;
        //                obj.doc_location = "~/Content/Documents/Public_Notices/Preliminary/";
        //                filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Public_Notices/Preliminary"), filename);
        //                postedFile.SaveAs(filename); //Save file at particular location
        //            }
        //            obj.active_status = "A";
        //            if (model.id == 0)
        //                db.other_jahir_prakatan_Preliminary_survey.Add(obj);
        //            else
        //                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        //            db.SaveChanges();
        //            ViewBag.Message = "Record Submitted Successfully!";
        //            return View(model);
        //        }
        //        catch (Exception)
        //        {
        //            ViewBag.Message = "Something went wrong";
        //            return View(model);
        //        }
        //    }
        //}
        //public ActionResult cp_other_jahirPrakatan_Primary_Final_addedit(string urlid)
        //{
        //    if (Session["UserID"] == null)
        //    {
        //        return RedirectToAction("Login", "CPAdmin");
        //    }
        //    else
        //    {
        //        other_jahirPrakatanModel model = new Models.other_jahirPrakatanModel();
        //        other_jahir_prakatan_primary_final_eligibility obj = new other_jahir_prakatan_primary_final_eligibility();
        //        List<SelectListItem> lst1 = new List<SelectListItem>();
        //        List<SelectListItem> lst2 = new List<SelectListItem>();
        //        lst1.Add(new SelectListItem
        //        {
        //            Text = "प्राथमिक",
        //            Value = "P"
        //        });
        //        lst1.Add(new SelectListItem
        //        {
        //            Text = "अंतिम",
        //            Value = "F"
        //        });
        //        model.typeList = lst1;
        //        lst2.Add(new SelectListItem
        //        {
        //            Text = "नोटीस",
        //            Value = "N"
        //        });
        //        lst2.Add(new SelectListItem
        //        {
        //            Text = "यादी",
        //            Value = "L"
        //        });
        //        model.notice_yadi_List = lst2;
        //        int id = decreptid(urlid);
        //        if (id != 0 && id != null)
        //        {
        //            obj = db.other_jahir_prakatan_primary_final_eligibility.SingleOrDefault(x => x.id == id);
        //            if (obj != null)
        //            {

        //                model.id = obj.id;
        //                model.type = obj.type;
        //                model.is_notice_list = obj.is_notice_list;
        //                model.plan_name = obj.plan_name;
        //                model.plan_name_eng = obj.plan_name_eng;
        //                model.plan_date = obj.plan_date;
        //                model.plan_date_New1 = obj.plan_date_New.ToString("dd-MM-yyyy");
        //                model.doc_name = obj.doc_name;
        //                model.doc_location = obj.doc_location;
        //                model.active_status = obj.active_status;
        //            }
        //            else
        //                ViewBag.error = "Something went wrong.";
        //        }
        //        return View(model);
        //    }
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public ActionResult cp_other_jahirPrakatan_Primary_Final_addedit(other_jahirPrakatanModel model, HttpPostedFileBase postedFile)
        //{
        //    if (Session["UserID"] == null)
        //    {
        //        return RedirectToAction("Login", "CPAdmin");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            string filename = "";
        //            string extension = "";

        //            if (postedFile != null)
        //            {
        //                filename = System.IO.Path.GetFileNameWithoutExtension(postedFile.FileName);
        //                string[] slipttext = filename.Split('.');
        //                if (slipttext.Count() > 1)
        //                {
        //                    ViewBag.fileExtensionMessage = "Invalid file!";
        //                    return View(model);
        //                }
        //                extension = System.IO.Path.GetExtension(postedFile.FileName);
        //                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
        //                if (extension != ".pdf")
        //                {
        //                    ViewBag.fileExtensionMessage = "Please select pdf file";
        //                    return View(model);
        //                }
        //            }
        //            other_jahir_prakatan_primary_final_eligibility obj = null;
        //            if (model.id != 0)
        //            {
        //                obj = db.other_jahir_prakatan_primary_final_eligibility.SingleOrDefault(x => x.id == model.id);
        //                if (obj == null)
        //                {
        //                    return View(model);
        //                }
        //                else
        //                {
        //                    obj.updated_by = Convert.ToInt32(Session["UserID"]);
        //                    obj.updated_date = DateTime.Today;
        //                    if (obj.updated_count == null)
        //                        obj.updated_count = 1;
        //                    else
        //                        obj.updated_count += 1;
        //                    //Delete Existing File
        //                    if (obj.doc_name != "" && filename != "")
        //                    {
        //                        string file_name = System.IO.Path.Combine(Server.MapPath(obj.doc_location), obj.doc_name);
        //                        FileInfo file = new FileInfo(file_name);
        //                        if (file.Exists)
        //                            file.Delete(); //Delete file at particular location
        //                    }
        //                    //Move file if type or notice_yadi changed
        //                    else if (obj.type != model.type || obj.is_notice_list != model.is_notice_list)
        //                    {
        //                        string sourceFile = System.IO.Path.Combine(Server.MapPath(obj.doc_location), obj.doc_name);
        //                        string destinationFile = "";
        //                        if (model.type == "P" && model.is_notice_list == "N")
        //                        {
        //                            obj.doc_location = "~/Content/Documents/Public_Notices/Primary_Notice/";
        //                            destinationFile = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Public_Notices/Primary_Notice"), obj.doc_name);
        //                        }
        //                        else if (model.type == "P" && model.is_notice_list == "L")
        //                        {
        //                            obj.doc_location = "~/Content/Documents/Public_Notices/Primary_List/";
        //                            destinationFile = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Public_Notices/Primary_List"), obj.doc_name);
        //                        }
        //                        else if (model.type == "F" && model.is_notice_list == "N")
        //                        {
        //                            obj.doc_location = "~/Content/Documents/Public_Notices/Final_Notice/";
        //                            destinationFile = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Public_Notices/Final_Notice"), obj.doc_name);
        //                        }
        //                        else if (model.type == "F" && model.is_notice_list == "L")
        //                        {
        //                            obj.doc_location = "~/Content/Documents/Public_Notices/Final_List/";
        //                            destinationFile = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Public_Notices/Final_List"), obj.doc_name);
        //                        }
        //                        // System.IO.File.Move(sourceFile, destinationFile);
        //                        FileInfo file = new FileInfo(sourceFile);
        //                        if (file.Exists)
        //                            file.MoveTo(destinationFile); //Move file at particular location
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                obj = new other_jahir_prakatan_primary_final_eligibility();
        //                obj.created_by = Convert.ToInt32(Session["UserID"]);
        //                obj.created_date = DateTime.Today;
        //            }
        //            obj.type = model.type;
        //            obj.is_notice_list = model.is_notice_list;
        //            obj.plan_name = model.plan_name;
        //            obj.plan_name_eng = model.plan_name_eng;
        //            DateTime dateTime = new DateTime();
        //            dateTime = Convert.ToDateTime(DateTime.ParseExact(model.plan_date_New1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
        //            obj.plan_date_New = dateTime;
        //            if (filename != "")
        //            {
        //                obj.doc_name = filename;
        //                if (obj.type == "P" && obj.is_notice_list == "N")
        //                {
        //                    obj.doc_location = "~/Content/Documents/Public_Notices/Primary_Notice/";
        //                    filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Public_Notices/Primary_Notice"), filename);
        //                }
        //                else if (obj.type == "P" && obj.is_notice_list == "L")
        //                {
        //                    obj.doc_location = "~/Content/Documents/Public_Notices/Primary_List/";
        //                    filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Public_Notices/Primary_List"), filename);
        //                }
        //                else if (obj.type == "F" && obj.is_notice_list == "N")
        //                {
        //                    obj.doc_location = "~/Content/Documents/Public_Notices/Final_Notice/";
        //                    filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Public_Notices/Final_Notice"), filename);
        //                }
        //                else if (obj.type == "F" && obj.is_notice_list == "L")
        //                {
        //                    obj.doc_location = "~/Content/Documents/Public_Notices/Final_List/";
        //                    filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Public_Notices/Final_List"), filename);
        //                }
        //                postedFile.SaveAs(filename); //Save file at particular location
        //            }
        //            obj.active_status = "A";
        //            if (model.id == 0)
        //                db.other_jahir_prakatan_primary_final_eligibility.Add(obj);
        //            else
        //                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        //            db.SaveChanges();
        //            ViewBag.Message = "Record Submitted Successfully!";
        //            List<SelectListItem> lst1 = new List<SelectListItem>();
        //            List<SelectListItem> lst2 = new List<SelectListItem>();
        //            lst1.Add(new SelectListItem
        //            {
        //                Text = "प्राथमिक",
        //                Value = "P"
        //            });
        //            lst1.Add(new SelectListItem
        //            {
        //                Text = "अंतिम",
        //                Value = "F"
        //            });
        //            model.typeList = lst1;
        //            lst2.Add(new SelectListItem
        //            {
        //                Text = "नोटीस",
        //                Value = "N"
        //            });
        //            lst2.Add(new SelectListItem
        //            {
        //                Text = "यादी",
        //                Value = "L"
        //            });
        //            model.notice_yadi_List = lst2;
        //            return View(model);
        //        }
        //        catch (Exception)
        //        {
        //            ViewBag.Message = "Something went wrong";
        //            return View(model);
        //        }
        //    }
        //}

    }
}
