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

namespace SRACMSApplication.Controllers
{
    public class SocietyController : Controller
    {
        //
        // GET: /Society/
        private sra_website_cmsEntities db = new sra_website_cmsEntities();

        public ActionResult Society()
        {
            if (Session["sraLogo"] == null)
                return RedirectToAction("Home", "Home");

            Session["Page"] = "Society";
            Session["Controller"] = "Society";

            societyModel model = new societyModel();
            var lst1 = db.society.Where(x => x.active_status == "A").OrderByDescending(x => x.soc_registration_dt).ToList();
            model.society_List = lst1.ToList(); 
            return View(model);
        }
        public ActionResult cp_other_Society()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {

                societyModel model = new societyModel();
                var lst1 = db.society.Where(x => x.active_status == "A").OrderByDescending(x => x.id).ToList();
                model.society_List = lst1.ToList();
                return View(model);
            }
        }

        public ActionResult cp_other_society_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                societyModel model = new Models.societyModel();
                society obj = new society();
                if (id != 0 && id != null)
                {
                    obj = db.society.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {
                        model.id = obj.id;
                        model.soc_name_eng = obj.soc_name_eng;
                        model.soc_name = obj.soc_name;
                        model.soc_address = obj.soc_address;
                        model.soc_address_eng = obj.soc_address_eng;
                        model.soc_registration_no = obj.soc_registration_no;
                        model.soc_registration_no_eng = obj.soc_registration_no_eng;
                        model.soc_year = obj.soc_year;
                        model.soc_registration_dt1 = obj.soc_registration_dt.ToString("dd-MM-yyyy");
                    }
                    else
                        ViewBag.error = "Something went wrong.";
                }
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult cp_other_society_addedit(societyModel model)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    society obj = null;
                    if (model.id != 0)
                    {
                        obj = db.society.SingleOrDefault(x => x.id == model.id);
                        if (obj == null)
                        {
                            return View();
                        }
                        else
                        {
                            obj.updated_by = Convert.ToInt32(Session["UserID"]);
                            obj.updated_date = DateTime.Today;
                        }
                    }
                    else
                    {
                        obj = new society();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        obj.created_dt = DateTime.Today;
                    }
                    obj.soc_name = model.soc_name;
                    obj.soc_name_eng = model.soc_name_eng;

                    obj.soc_address = model.soc_address;
                    obj.soc_address_eng = model.soc_address_eng;

                    obj.soc_registration_no = model.soc_registration_no;
                    obj.soc_registration_no_eng = model.soc_registration_no_eng;


               
                    DateTime dateTime = new DateTime();
                     dateTime = Convert.ToDateTime(DateTime.ParseExact(model.soc_registration_dt1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
                     obj.soc_registration_dt = dateTime;

                    obj.soc_registration_dt = dateTime;

                    obj.soc_year = dateTime.Year.ToString();
                 


                    obj.created_by = 1;
                    obj.updated_by = 1;

                    obj.created_dt = dateTime;
                    obj.updated_date= dateTime;

                    obj.active_status = "A";
                    if (model.id == 0)
                        db.society.Add(obj);
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

        public ActionResult cp_other_society_activeDeactive(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    society obj = null;
                    if (id != 0)
                    {
                        obj = db.society.SingleOrDefault(x => x.id == id);
                        if (obj == null)
                        {
                            return View();
                        }
                        else
                        {
                            obj.updated_by = Convert.ToInt32(Session["UserID"]);
                            obj.updated_date = DateTime.Today;
                            //if (obj.updated_count == null)
                            //    obj.updated_count = 1;
                            //else
                            //    obj.updated_count += 1;
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


    }
}
