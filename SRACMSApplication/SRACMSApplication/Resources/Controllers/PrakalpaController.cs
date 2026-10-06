using SRACMSApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRACMSApplication.Controllers
{
    public class PrakalpaController : Controller
    {
        private sra_website_cmsEntities db = new sra_website_cmsEntities();

        //
        // GET: /Prakalpa/
        public ActionResult Prakalpa()
        {
            if (Session["sraLogo"] == null)
                return RedirectToAction("Home", "Home");
            if (Session["Lang"] == null)
                Session["Lang"] = "mr";
            Session["Page"] = "Prakalpa";
            Session["Controller"] = "Prakalpa";
            projectModel model = new projectModel();

            //झोपडपट्टी पुनर्वसन योजनांचा गोषवारा
            var lst5 = db.about.Where(x => x.type == "table5").ToList();
            model.slumPlanTableList = lst5.ToList();

            int val1 = db.projects.Where(x => x.active_status == "A").ToList().Count;
            int val2 =Convert.ToInt32(db.projects.Where(x => x.active_status == "A").Sum(x=>x.total_huts));
            int val3 = db.projects.Where(x => x.project_status == 1 && x.active_status == "A").ToList().Count;
            int val4 = Convert.ToInt32(db.projects.Where(x => x.project_status==1 && x.active_status == "A").Sum(x => x.total_huts));
            int val5 = db.projects.Where(x => x.project_status == 2 && x.active_status == "A").ToList().Count;
            int val6 = Convert.ToInt32(db.projects.Where(x => x.project_status == 2 && x.active_status == "A").Sum(x => x.total_huts));
            int val7 = val1 - val3 - val5;
            int val8 = val2 - val4 - val6;
            int val9 = db.projects.Where(x => x.project_status == 3 && x.active_status == "A").ToList().Count;
            int val10 = Convert.ToInt32(db.projects.Where(x => x.project_status == 3 && x.active_status == "A").Sum(x => x.total_huts));
            int val11 = db.projects.Where(x => x.project_status == 4 &&  x.active_status == "A").ToList().Count;
            int val12= Convert.ToInt32(db.projects.Where(x => x.project_status == 4 &&   x.active_status == "A").Sum(x => x.total_huts));
            int val13= db.projects.Where(x => x.project_status == 5 && x.active_status == "A").ToList().Count;
            int val14 = Convert.ToInt32(db.projects.Where(x => x.project_status == 5 && x.active_status == "A").Sum(x => x.total_huts));
            int val15 = db.projects.Where(x => x.project_status == 6 &&  x.active_status == "A").ToList().Count;
            int val16 = Convert.ToInt32(db.projects.Where(x => x.project_status == 6 &&  x.active_status == "A").Sum(x => x.total_huts));
            int val17 = db.projects.Where(x => x.project_status == 7 && x.active_status == "A").ToList().Count;
            int val18 = Convert.ToInt32(db.projects.Where(x => x.project_status == 7 && x.active_status == "A").Sum(x => x.total_huts));
            int val19 = db.projects.Where(x => x.project_status == 8 &&  x.active_status == "A").ToList().Count;
            int val20 = Convert.ToInt32(db.projects.Where(x => x.project_status == 8 &&  x.active_status == "A").Sum(x => x.total_huts));
            int val21 = db.projects.Where(x => x.project_status == 9 && x.active_status == "A").ToList().Count;
            int val22 = Convert.ToInt32(db.projects.Where(x => x.project_status == 9 && x.active_status == "A").Sum(x => x.total_huts));
            int val23 = db.projects.Where(x => x.project_status == 10 &&  x.active_status == "A").ToList().Count;
            int val24 = Convert.ToInt32(db.projects.Where(x => x.project_status == 10 &&  x.active_status == "A").Sum(x => x.total_huts));
            int val25 = db.projects.Where(x => x.project_status == 11 && x.active_status == "A").ToList().Count;
            int val26 = Convert.ToInt32(db.projects.Where(x => x.project_status == 11 && x.active_status == "A").Sum(x => x.total_huts));
            int val27 = db.projects.Where(x => x.project_status == 12 &&  x.active_status == "A").ToList().Count;
            int val28 = Convert.ToInt32(db.projects.Where(x => x.project_status == 12 &&  x.active_status == "A").Sum(x => x.total_huts));
            int val29 = db.projects.Where(x => x.project_status == 13 && x.active_status == "A").ToList().Count;
            int val30 = Convert.ToInt32(db.projects.Where(x => x.project_status == 13 && x.active_status == "A").Sum(x => x.total_huts));
            int val31 = db.projects.Where(x => x.project_status == 14 &&  x.active_status == "A").ToList().Count;
            int val32 = Convert.ToInt32(db.projects.Where(x => x.project_status == 14 &&  x.active_status == "A").Sum(x => x.total_huts));
            int val33 = val11 + val13 + val15 + val17+val19;
            int val34 = val12 + val14 + val16+val18+val20;
            int val35 = val21 + val23 + val25 + val27 + val29 ;
            int val36 = val22 + val24 + val26 + val28 + val30 ;
            int val37 = val7 - val31;
            int val38 = val8 - val32;

            int[] my_array = new int[] { val1, val2,val3, val4, val5, val6, val7, val8, val9, val10
            , val11, val12, val13, val14, val15, val16, val17, val18, val19, val20
            , val21, val22, val23, val24, val25, val26, val27, val28, val29, val30, val31, val32,val33,val34,val35,val36,val37,val38};

            ViewBag.schems = my_array;


            //Completed Project list
            //var lst_completed = db.projects.Where(x => x.project_type == "C" && x.active_status == "A").OrderByDescending(x => x.id).ToList();
            var lst_completed = db.projects.Where(x => x.project_status1.project_type == "C" && x.active_status == "A").ToList();
            model.completed_projectList = lst_completed.ToList();
            //Running Project List
            //var lst_running = db.projects.Where(x => x.project_type == "R" && x.active_status == "A").OrderByDescending(x => x.id).ToList();
            var lst_running = db.projects.Where(x => x.project_status1.project_type == "R" && x.active_status == "A").ToList();
            model.running_projectList = lst_running.ToList();
            //Running Project List
            //var lst_running = db.projects.Where(x => x.project_type == "R" && x.active_status == "A").OrderByDescending(x => x.id).ToList();
            var lst_running1 = db.projects.Where(x => x.project_status1.project_type == "S" && x.active_status == "A").ToList();
            model.running_pending_projectList = lst_running1.ToList();
            //New Project List
            //var lst_new = db.projects.Where(x => x.project_type == "N" && x.active_status == "A").OrderByDescending(x => x.id).ToList();
            var lst_new = db.projects.Where(x => x.project_status1.project_type == "N" && x.active_status == "A").ToList();
            model.new_projectList = lst_new.ToList();


           

            return View(model);
        }
        public ActionResult cp_other_Prakalpa()
        {
            if (Session["sraLogo"] == null)
                return RedirectToAction("Home", "Home");

            Session["Page"] = "Prakalpa";
            Session["Controller"] = "Prakalpa";

            projectModel model = new projectModel();
            var lst1 = db.projects.Where(x => x.active_status == "A").OrderByDescending(x => x.id).ToList();
            model.tot_projectList = lst1.ToList();
            return View(model);

            
      

          
        }


        public ActionResult cp_other_prakalpa_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                other_developers model1 = new other_developers();
                var lst2 = db.other_developers.Select(x => x.dev_name).OrderBy(x => x).ToList();
                //model1.developerList = lst2.ToList();


                projectModel model = new Models.projectModel();
                project obj = new project();
                if (id != 0 && id != null)
                {
                    obj = db.projects.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {
                        model.id = obj.id;
                        //model.soc_name_eng = obj.soc_name_eng;
                        //model.soc_name = obj.soc_name;
                        //model.soc_address = obj.soc_address;
                        //model.soc_address_eng = obj.soc_address_eng;
                        //model.soc_registration_no = obj.soc_registration_no;
                        //model.soc_registration_no_eng = obj.soc_registration_no_eng;
                        //model.soc_year = obj.soc_year;
                        //model.soc_registration_dt1 = obj.soc_registration_dt.ToString("dd-MM-yyyy");
                    }
                    else
                        ViewBag.error = "Something went wrong.";
                }
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult cp_other_prakalpa_addedit(project model)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    project obj = null;
                    if (model.id != 0)
                    {
                        obj = db.projects.SingleOrDefault(x => x.id == model.id);
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
                        obj = new project();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        //obj.created_dt = DateTime.Today;
                    }
                    //obj.soc_name = model.soc_name;
                    //obj.soc_name_eng = model.soc_name_eng;

                    //obj.soc_address = model.soc_address;
                    //obj.soc_address_eng = model.soc_address_eng;

                    //obj.soc_registration_no = model.soc_registration_no;
                    //obj.soc_registration_no_eng = model.soc_registration_no_eng;



                    DateTime dateTime = new DateTime();
                    //dateTime = Convert.ToDateTime(DateTime.ParseExact(model.soc_registration_dt1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    



                    obj.created_by = 1;
                    obj.updated_by = 1;

                   
                    obj.updated_date = dateTime;

                    obj.active_status = "A";
                    if (model.id == 0)
                        // db.society.Add(obj);
                        obj.updated_date = dateTime;
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

        public ActionResult cp_other_prakalpa_activeDeactive(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    project obj = null;
                    if (id != 0)
                    {
                        obj = db.projects.SingleOrDefault(x => x.id == id);
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
