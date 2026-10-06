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
using System.Text;
using System.Security.Cryptography;
using System.Web.Security;

namespace SRACMSApplication.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class CPAdminController : Controller
    {
        private sra_website_cmsEntities db = new sra_website_cmsEntities();
        //
        // GET: /CPAdmin/
        //Login
        public ActionResult Login(int? logout)
        {
            if (logout==0 && Session["UserID"] != null)
                return RedirectToAction("Default", "CPAdmin");

            if (logout == 1)
            {
                Session["UserID"] = null;
                Session["UserName"] = null;
                Session["User_role_id"] = null;
                Session.Clear();
                FormsAuthentication.SignOut();
            }
            return View();
        }
        //Login Button Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(mst_userModel model)
        {
            //if (ModelState.IsValid)
            //{
           //  var obj = db.mst_user.Where(a => a.user_name.Equals(model.user_name) && a.user_password.Equals(model.user_password)).FirstOrDefault();
            string pswd = Encrypt(model.user_password);
            string pswd1 =  Decrypt(pswd);
           var obj = db.mst_user.Where(a => a.user_name== model.user_name && a.user_password==pswd && a.active_status=="A").FirstOrDefault();

            if (obj != null)
            {
                Session["UserID"] = obj.user_id.ToString();
                Session["UserName"] = obj.user_name.ToString();
                Session["User_role_id"] = obj.user_role_id.ToString();
                Session["User_Name"] = obj.user_fname.ToString().ToUpper()+" "+ obj.user_lname2.ToString().ToUpper();
                return RedirectToAction("Default", "CPAdmin");
            }
            else
            {
                ViewBag.Message = "Wrong username or password. Try again.";
                return View(model);
            }
            //}
            //else
            //    ViewBag.Message = "Wrong username or password. Try again.";
        }

        public string Encrypt(string clearText)
        {
            string EncryptionKey = "SRAWebSiteCMS@2021";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "SRAWebSiteCMS@2021";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        //this function Convert to Encord your Password 
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        } //this function Convert to Decord your Password
        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
        
        public ActionResult Default()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            return View();
        }

        //Change Password
        public ActionResult ChangePassword()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            mst_userModel model = new mst_userModel();
            mst_user obj = new mst_user();
            Int32 uid = Convert.ToInt32( Session["UserID"]);
            obj = db.mst_user.SingleOrDefault(x => x.user_id == uid);
            if (obj != null)
            {
                model.user_id = obj.user_id;
               // model.user_password = obj.user_password;
                model.user_name = obj.user_name;
                model.user_fname = obj.user_fname;
                model.user_lname2 = obj.user_lname2;
            }
            else
                ViewBag.error = "Something went wrong.";
        
                return View(model);
    }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(mst_userModel model)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    mst_user obj = null;
                    if (model.user_id != 0)
                    {
                        //encrypt password
                        string pswd = Encrypt(model.user_old_password);
                        obj = db.mst_user.SingleOrDefault(x => x.user_id == model.user_id && x.user_password == pswd);
                        if (obj == null)
                        {
                            ViewBag.Message = "Invalid Old Password!";
                            //Session["msg"]= "Invalid Old Password!";
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

                            obj.user_fname = model.user_fname;
                            obj.user_lname2 = model.user_lname2;
                            obj.user_password = Encrypt(model.user_create_password);
                            obj.active_status = "A";
                            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            ViewBag.Message = "Password Changed Successfully!";
                        }
                    }
                    return View();

                }
                catch (Exception)
                {
                    ViewBag.Message = "Something went wrong";
                    throw;
                }
            }

        }

        //public int decreptid(string urlid)
        //{
        //    return Convert.ToInt32(db.tbl_urlId.Where(x => x.urlid == urlid).Select(x => x.id).SingleOrDefault());
        //}

        //प्रकल्प
        public ActionResult cp_project()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                projectModel model = new projectModel();
                //Completed Project list // पूर्ण
                var lst_completed = db.projects.Where(x => x.project_status1.project_type == "C").OrderByDescending(x => x.id).ToList();
                model.completed_projectList = lst_completed.ToList();
                //Running Project List // चालू बांधकाम चालू असलेले 
                var lst_running = db.projects.Where(x => x.project_status1.project_type == "R").OrderByDescending(x => x.id).ToList();
                model.running_projectList = lst_running.ToList();
                //Running Project List// चालू बांधकाम चालू नसलेले 
                var lst_running1 = db.projects.Where(x => x.project_status1.project_type == "S").OrderByDescending(x => x.id).ToList();
                model.running_pending_projectList = lst_running1.ToList();
                //New Project List // नव्याने दाखल 
                var lst_new = db.projects.Where(x => x.project_status1.project_type == "N").OrderByDescending(x => x.id).ToList();
                model.new_projectList = lst_new.ToList();
                return View(model);
            }
        }
        
        public ActionResult cp_project_activeDeactive(long? id)
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
        
        public ActionResult cp_project_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                projectModel model = new Models.projectModel();
                project obj = new project();
                List<SelectListItem> lst1 = new List<SelectListItem>();
                lst1.Add(new SelectListItem
                {
                    Text = "पूर्ण",
                    Value = "C"
                });
                lst1.Add(new SelectListItem
                {
                    Text = "चालू - बांधकाम चालू",
                    Value = "R"
                });
                lst1.Add(new SelectListItem
                {
                    Text = "चालू - बांधकाम चालू नाही",
                    Value = "S"
                });
                lst1.Add(new SelectListItem
                {
                    Text = "नव्याने दाखल",
                    Value = "N"
                });
                model.typeList = lst1;
                if (id != 0 && id != null)
                {
                    obj = db.projects.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {
                        model.id = obj.id;
                        model.project_type = obj.project_status1.project_type;
                        model.project_name = obj.project_name;
                        model.project_name_eng = obj.project_name_eng;
                        model.survey_no = obj.survey_no;
                        model.survey_no_eng = obj.survey_no_eng;
                        model.developer_name = obj.developer_name;
                        model.developer_name_eng = obj.developer_name_eng;
                        model.architect_name = obj.architect_name;
                        model.architect_name_eng = obj.architect_name_eng;
                        //model.completion_date1 = obj.completion_date.ToString("dd-MM-yyyy");
                        //model.occupied_flats = obj.occupied_flats;
                    }
                    else
                        ViewBag.error = "Something went wrong.";
                }
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult cp_project_addedit(projectModel model)
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
                            if (obj.updated_count == null)
                                obj.updated_count = 1;
                            else
                                obj.updated_count += 1;
                        }
                    }
                    else
                    {
                        obj = new project();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        obj.created_date = DateTime.Today;
                    }
                    obj.project_status1.project_type = model.project_type;
                    obj.survey_no = model.survey_no;
                    obj.survey_no_eng = model.survey_no_eng;
                    obj.project_name = model.project_name;
                    obj.project_name_eng = model.project_name_eng;
                    obj.developer_name = model.developer_name;
                    obj.developer_name_eng = model.developer_name_eng;
                    obj.architect_name = model.architect_name;
                    obj.architect_name_eng = model.architect_name_eng;
                    DateTime dateTime = new DateTime();
                    dateTime = Convert.ToDateTime(DateTime.ParseExact(model.completion_date1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    //obj.completion_date = dateTime;
                    //obj.occupied_flats = model.occupied_flats;
                    obj.active_status = "A";
                    if (model.id == 0)
                        db.projects.Add(obj);
                    else
                        db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Record Submitted Successfully!";
                    List<SelectListItem> lst1 = new List<SelectListItem>();
                    lst1.Add(new SelectListItem
                    {
                        Text = "पूर्ण",
                        Value = "C"
                    });
                    lst1.Add(new SelectListItem
                    {
                        Text = "चालू - बांधकाम चालू",
                        Value = "R"
                    });
                    lst1.Add(new SelectListItem
                    {
                        Text = "चालू - बांधकाम चालू नाही",
                        Value = "S"
                    });
                    lst1.Add(new SelectListItem
                    {
                        Text = "नव्याने दाखल",
                        Value = "N"
                    });
                    model.typeList = lst1;
                    return View(model);
                }
                catch (Exception)
                {
                    ViewBag.Message = "Something went wrong";
                    throw;
                }
            }

        }

        //झोपडपट्टी यादी

        public ActionResult cp_slums()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                slumModel model = new slumModel();
                //PMC Slum list
                var lst_pmc = db.slums.Where(x => x.pmc_pcmc == "PMC").OrderBy(x => x.ward).ToList(); ;
                model.pmc_slumList = lst_pmc.ToList();

                //PCMC Slum list
                var lst_pcmc = db.slums.Where(x => x.pmc_pcmc == "PCMC").OrderBy(x => x.ward).ToList(); ;
                model.pcmc_slumList = lst_pcmc.ToList();

                ViewBag.totalSlums = model.pcmc_slumList.Count + model.pmc_slumList.Count;

                return View(model);
            }
        }
        public ActionResult cp_slums_activeDeactive(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    slum obj = null;
                    if (id != 0)
                    {
                        obj = db.slums.SingleOrDefault(x => x.id == id);
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
                    {
                    ViewBag.Message = "Something went wrong";
                        return View();
                    }
                }
                catch (Exception)
                {
                    ViewBag.Message = "Something went wrong";
                    throw;
                }
            }

        }
        
        public ActionResult cp_slums_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                slumModel model = new Models.slumModel();
                slum obj = new slum();
                List<SelectListItem> lst1 = new List<SelectListItem>();
                lst1.Add(new SelectListItem
                {
                    Text = "PMC",
                    Value = "PMC"
                });
                lst1.Add(new SelectListItem
                {
                    Text = "PCMC",
                    Value = "PCMC"
                });
                model.typeList = lst1;
                //int id =  Convert.ToInt32(Decrypt(urlid));
                if (id != 0 && id != null)
                {
                    obj = db.slums.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {
                        model.id = obj.id;
                        model.pmc_pcmc = obj.pmc_pcmc;
                        model.ward = obj.ward;
                        model.ward_eng = obj.ward_eng;
                        model.prabhag = obj.prabhag;
                        model.prabhag_eng = obj.prabhag_eng;
                        model.slum_name = obj.slum_name;
                        model.slum_name_eng = obj.slum_name_eng;
                        model.hut_count = obj.hut_count;
                        model.hut_count_eng = obj.hut_count_eng;
                        model.slum_area = obj.slum_area;
                        model.slum_area_eng = obj.slum_area_eng;
                        model.declared_nondeclared = obj.declared_nondeclared;
                        model.declared_nondeclared_eng = obj.declared_nondeclared_eng;
                        model.slum_owner = obj.slum_owner;
                        model.slum_owner_eng = obj.slum_owner_eng;
                    }
                    else
                        ViewBag.error = "Something went wrong.";
                }
                else
                {
                    ViewBag.error = "Something went wrong.";
                    return RedirectToAction("cp_slums", "CPAdmin");
                }
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult cp_slums_addedit(slumModel model)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    slum obj = null;
                    if (model.id != 0)
                    {
                        obj = db.slums.SingleOrDefault(x => x.id == model.id);
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
                        obj = new slum();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        obj.created_date = DateTime.Today;
                    }
                    obj.pmc_pcmc = model.pmc_pcmc;
                    obj.ward = model.ward;
                    obj.ward_eng = model.ward_eng;
                    obj.prabhag = model.prabhag;
                    obj.prabhag_eng = model.prabhag_eng;
                    obj.slum_name = model.slum_name;
                    obj.slum_name_eng = model.slum_name_eng;
                    obj.hut_count = model.hut_count;
                    obj.hut_count_eng = model.hut_count_eng;
                    obj.slum_area = model.slum_area;
                    obj.slum_area_eng = model.slum_area_eng;
                    obj.declared_nondeclared = model.declared_nondeclared;
                    obj.declared_nondeclared_eng = model.declared_nondeclared_eng;
                    obj.slum_owner = model.slum_owner;
                    obj.slum_owner_eng = model.slum_owner_eng;
                    obj.active_status = "A";
                    if (model.id == 0)
                        db.slums.Add(obj);
                    else
                        db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Record Submitted Successfully!";
                    List<SelectListItem> lst1 = new List<SelectListItem>();
                    lst1.Add(new SelectListItem
                    {
                        Text = "PMC",
                        Value = "PMC"
                    });
                    lst1.Add(new SelectListItem
                    {
                        Text = "PCMC",
                        Value = "PCMC"
                    });
                    model.typeList = lst1;
                    return View(model);
                }
                catch (Exception)
                {
                    ViewBag.Message = "Something went wrong";
                    throw;
                }
            }

        }

        //SIngle pdf or Image
        
        public ActionResult single_image_pdf(string type)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                mst_single_image_pdfModel model = new mst_single_image_pdfModel();
                List<mst_single_image_pdf> lstobj = db.mst_single_image_pdf.Where(x => x.active_status == "A").ToList();
                model.lstContentType = lstobj;
                mst_single_image_pdf obj = db.mst_single_image_pdf.SingleOrDefault(x => x.content_type == type && x.active_status == "A");
                if (obj != null)
                {
                    model.id = obj.id;
                    model.file_type = obj.file_type;
                    model.content_type = obj.content_type;
                    model.title1 = obj.title;
                    model.title_eng = obj.title_eng;
                    model.doc_name = obj.doc_name;
                    model.doc_location = obj.doc_location;
                }
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult single_image_pdf(mst_single_image_pdfModel model, HttpPostedFileBase postedFile)
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
                        if (extension == ".pdf" && model.file_type != "p")
                        {
                                ViewBag.fileExtensionMessage = "Please select pdf file";
                                return View(model);
                        }
                        else if ((extension == ".jpg" || extension == ".jpeg") && model.file_type != "i")
                        {
                                ViewBag.fileExtensionMessage = "Please select jpg/jpeg file";
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
                            obj.file_type = model.file_type;
                            obj.content_type = model.content_type;
                            obj.title = model.title1;
                            obj.title_eng = model.title_eng;
                            //Delete Existing File
                            if (obj.doc_name != "" && filename != "")
                            {
                                string file_name = System.IO.Path.Combine(Server.MapPath(obj.doc_location), obj.doc_name);
                                FileInfo file = new FileInfo(file_name);
                                if (file.Exists)
                                    file.Delete(); //Delete file at particular location
                            }
                            if (filename != "")
                            {
                                obj.doc_name = filename;
                                filename = System.IO.Path.Combine(Server.MapPath(obj.doc_location), filename);
                                postedFile.SaveAs(filename); //Save file at particular location
                            }
                            obj.active_status = "A";
                            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            ViewBag.Message = "Record Submitted Successfully!";
                                }
                    }
                    List<mst_single_image_pdf> lstobj = db.mst_single_image_pdf.Where(x => x.active_status == "A").ToList();
                    model.lstContentType = lstobj;
                    if (obj != null)
                    {
                        model.id = obj.id;
                        model.file_type = obj.file_type;
                        model.content_type = obj.content_type;
                        model.title1 = obj.title;
                        model.title_eng = obj.title_eng;
                        model.doc_name = obj.doc_name;
                        model.doc_location = obj.doc_location;
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
        
        public JsonResult get_single_image_pdf_details(string contentType)
        {
            mst_single_image_pdf obj = db.mst_single_image_pdf.SingleOrDefault(x => x.content_type == contentType && x.active_status == "A");
            mst_single_image_pdfModel model = new mst_single_image_pdfModel
            {
                id = obj.id,
                content_type=obj.content_type,
                title1 = obj.title,
                title_eng = obj.title_eng,
                doc_name = obj.doc_name,
                doc_location=obj.doc_location,
                file_type=obj.file_type
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //SIngle content
        
        public ActionResult single_content(string type)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                mst_content_singleModel model = new mst_content_singleModel();
                List<mst_content_single> lstobj = db.mst_content_single.Where(x => x.active_status == "A").ToList();
                model.lstContentType = lstobj;
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult single_content(mst_content_singleModel model)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    mst_content_single obj = null;
                    if (model.id != 0)
                    {
                        obj = db.mst_content_single.SingleOrDefault(x => x.id == model.id);
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
                            obj.content_type = model.content_type;
                            obj.description = model.description;
                            obj.description_eng = model.description_eng;
                            obj.active_status = "A";
                            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            ViewBag.Message = "Record Submitted Successfully!";
                        }
                    }
                    List<mst_content_single> lstobj = db.mst_content_single.Where(x => x.active_status == "A").ToList();
                    model.lstContentType = lstobj;
                    if (obj != null)
                    {
                        model.id = obj.id;
                        model.content_type = obj.content_type;
                        model.description = obj.description;
                        model.description_eng = obj.description_eng;
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
        public JsonResult get_single_content_details(string contentType)
        {
            mst_content_single obj = db.mst_content_single.SingleOrDefault(x => x.content_type == contentType && x.active_status == "A");
            mst_content_singleModel model = new mst_content_singleModel
            {
                id = obj.id,
                description = obj.description,
                description_eng = obj.description_eng
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //Banner

        public ActionResult banners()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                List<mst_single_image_pdfModel> model = new List<mst_single_image_pdfModel>();
                //Banner list
                List<mst_single_image_pdf> obj = new List<mst_single_image_pdf>();
                obj = db.mst_single_image_pdf.Where(x => x.content_type == "Banners").OrderByDescending(x => x.id).ToList();
                foreach (var item in obj)
                {
                    mst_single_image_pdfModel model1 = new mst_single_image_pdfModel();
                    model1.id = item.id;
                    model1.content_type = item.content_type;
                    model1.title1 = item.title;
                    model1.title_eng = item.title_eng;
                    model1.doc_name = item.doc_name;
                    model1.doc_location = item.doc_location;
                    model1.active_status = item.active_status;
                    model.Add(model1);
                }
                return View(model);
            }
        }
        public ActionResult banners_add()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "CPAdmin");
            else
                return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult banners_add(mst_single_image_pdfModel model, HttpPostedFileBase postedFile)
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
                        if (extension != ".jpg" && extension != ".jpeg")
                        {
                            ViewBag.fileExtensionMessage = "Please select jpeg/jpg file";
                            return View(model);
                        }
                    }

                    mst_single_image_pdf obj = new mst_single_image_pdf(); 
                    obj.created_by = Convert.ToInt32(Session["UserID"]);
                    obj.created_date = DateTime.Today;
                    obj.content_type = "Banners";
                    obj.title = model.title1;
                    obj.title_eng = model.title_eng;
                    obj.file_type = "i";
                    if (filename != "")
                    {
                        obj.doc_name = filename;
                        obj.doc_location = "~/Content/Images/Banners/";
                        filename = System.IO.Path.Combine(Server.MapPath("~/Content/Images/Banners"), filename);
                        postedFile.SaveAs(filename); //Save file at particular location
                    }
                    obj.active_status = "A";
                    db.mst_single_image_pdf.Add(obj);
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
        
        public ActionResult banners_activeDeactive(long? id)
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

        //FAQ

        public ActionResult cp_faq()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                faqModel model = new faqModel();
                var lst = db.faqs.ToList();
                model.faq_list = lst.ToList();
                return View(model);
            }
        }
        public ActionResult cp_faq_activeDeactive(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    faq obj = null;
                    if (id != 0)
                    {
                        obj = db.faqs.SingleOrDefault(x => x.id == id);
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
        
        public ActionResult cp_faq_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                faqModel model = new Models.faqModel();
                faq obj = new faq();
                if (id != 0 && id != null)
                {
                    obj = db.faqs.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {
                        model.id = obj.id;
                        model.faq1 = obj.faq1;
                        model.faq_eng = obj.faq_eng;
                        model.answer = obj.answer;
                        model.answer_eng = obj.answer_eng;
                    }
                    else
                        ViewBag.error = "Something went wrong.";
                }
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult cp_faq_addedit(faqModel model)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    faq obj = null;
                    if (model.id != 0)
                    {
                        obj = db.faqs.SingleOrDefault(x => x.id == model.id);
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
                        obj = new faq();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        obj.created_date = DateTime.Today;
                    }
                    obj.faq1 = model.faq1;
                    obj.faq_eng = model.faq_eng;
                    obj.answer = model.answer;
                    obj.answer_eng = model.answer_eng;
                    obj.active_status = "A";
                    if (model.id == 0)
                        db.faqs.Add(obj);
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

        //शासन निर्णय

        public ActionResult cp_gr()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                govResolutionsModel model = new govResolutionsModel();
                var lst = db.GovtResolutions.OrderByDescending(x => x.id).ToList();
                model.GovtResolutionList = lst.ToList();
                return View(model);
            }
        }
        public ActionResult cp_gr_activeDeactive(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    GovtResolution obj = null;
                    if (id != 0)
                    {
                        obj = db.GovtResolutions.SingleOrDefault(x => x.id == id);
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
        
        public ActionResult cp_gr_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                govResolutionsModel model = new Models.govResolutionsModel();
                GovtResolution obj = new GovtResolution();

                if (id != 0 && id != null)
                {
                    obj = db.GovtResolutions.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {

                        model.id = obj.id;
                        model.resolution_subject = obj.resolution_subject;
                        model.resolution_subject_eng = obj.resolution_subject_eng;
                        model.resolution_date1 = obj.resolution_date.ToString("dd-MM-yyyy");
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
        
        public ActionResult cp_gr_addedit(govResolutionsModel model, HttpPostedFileBase postedFile)
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
                    GovtResolution obj = null;
                    if (model.id != 0)
                    {
                        obj = db.GovtResolutions.SingleOrDefault(x => x.id == model.id);
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
                        obj = new GovtResolution();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        obj.created_date = DateTime.Today;
                    }
                    obj.resolution_subject = model.resolution_subject;
                    obj.resolution_subject_eng = model.resolution_subject_eng;
                    DateTime dateTime = new DateTime();
                    dateTime = Convert.ToDateTime(DateTime.ParseExact(model.resolution_date1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    obj.resolution_date = dateTime;
                    obj.resolution_date_eng = dateTime;
                    if (filename != "")
                    {
                        obj.doc_name = filename;
                        obj.doc_location = "~/Content/Documents/GR/";
                        filename = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/GR"), obj.doc_name);
                        postedFile.SaveAs(filename); //Save file at particular location
                    }
                    obj.active_status = "A";
                    if (model.id == 0)
                        db.GovtResolutions.Add(obj);
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

        //Gallery

        public ActionResult cp_gallery()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                galaryModel model = new galaryModel();
                var lst = db.galaries.Where(x => x.photo_video == "P").OrderByDescending(x => x.id).ToList();
                model.PhotoGalleryList = lst.ToList();
                var lst1 = db.galaries.Where(x => x.photo_video == "V").OrderByDescending(x => x.id).ToList();
                model.VideoGalleryList = lst1.ToList();
                return View(model);
            }
        }
        public ActionResult cp_gallery_add()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "CPAdmin");
            else
            {
                galaryModel model = new galaryModel();
                List<SelectListItem> lst1 = new List<SelectListItem>();
                lst1.Add(new SelectListItem
                {
                    Text = "Photo Gallery",
                    Value = "P"
                });
                lst1.Add(new SelectListItem
                {
                    Text = "Video Gallery",
                    Value = "V"
                });
                model.photo_video_List = lst1;
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult cp_gallery_add(galaryModel model, HttpPostedFileBase[] postedFile)
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
                    foreach (HttpPostedFileBase file in postedFile)
                    {
                        //Checking file is available to save.  
                        if (file != null)
                        {
                            filename = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                            string[] slipttext = filename.Split('.');
                            if (slipttext.Count() > 1)
                            {
                                ViewBag.fileExtensionMessage = "Invalid file!";
                                return View(model);
                            }
                            extension = System.IO.Path.GetExtension(file.FileName);
                            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                            List<SelectListItem> lst2 = new List<SelectListItem>();
                            lst2.Add(new SelectListItem
                            {
                                Text = "Photo Gallery",
                                Value = "P"
                            });
                            lst2.Add(new SelectListItem
                            {
                                Text = "Video Gallery",
                                Value = "V"
                            });
                            model.photo_video_List = lst2;
                            if (model.photo_video=="P" && extension.ToString().ToUpper() != ".JPG" && extension.ToString().ToUpper() != ".JPEG")
                            {
                                ViewBag.fileExtensionMessage = "Please select jpeg/jpg file";
                                return View(model);
                            }
                            if (model.photo_video == "V"  && extension.ToString().ToUpper() != ".MP4")
                            {
                                ViewBag.fileExtensionMessage = "Please select mp4 file";
                                return View(model);
                            }
                        }
                    }
                            foreach (HttpPostedFileBase file in postedFile)
                    {
                        //Checking file is available to save.  
                        if (file != null)
                        {
                            filename = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                            extension = System.IO.Path.GetExtension(file.FileName);
                            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                            var InputFileName = Path.GetFileName(filename);
                            var ServerSavePath = Path.Combine(Server.MapPath("~/Content/images/Gallery/") + InputFileName);
                            //Save file to server folder  
                            file.SaveAs(ServerSavePath);

                            galary obj = new galary();
                            obj.created_by = Convert.ToInt32(Session["UserID"]);
                            obj.created_date = DateTime.Today;

                            obj.photo_video = model.photo_video;
                            obj.album_desc = model.album_desc;
                            obj.album_desc_eng = model.album_desc_eng;
                            obj.doc_name = filename;
                            obj.doc_location = "~/Content/Images/Gallery/";
                            obj.active_status = "A";
                            db.galaries.Add(obj);
                            db.SaveChanges();
                        }
                    }
                    ViewBag.Message = postedFile.Count().ToString() + " files uploaded successfully.";
                    List<SelectListItem> lst1 = new List<SelectListItem>();
                    lst1.Add(new SelectListItem
                    {
                        Text = "Photo Gallery",
                        Value = "P"
                    });
                    lst1.Add(new SelectListItem
                    {
                        Text = "Video Gallery",
                        Value = "V"
                    });
                    model.photo_video_List = lst1;
                    return View(model);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Something went wrong"+ex.Message;
                    return View(model);
                }
            }

        }
        
        public ActionResult cp_gallery_activeDeactive(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    galary obj = null;
                    if (id != 0)
                    {
                        obj = db.galaries.SingleOrDefault(x => x.id == id);
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

        
        public ActionResult cp_gallery_getGalleries(string type)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    if (type == "P")
                    {
                        return Json(db.galaries.Where(x => x.photo_video == "P").Select(x => new
                        {
                            album = x.album_desc,
                            album_eng = x.album_desc_eng
                        }).Distinct().ToList(), JsonRequestBehavior.AllowGet);
                    }
                    else if (type == "V")
                    {
                        return Json(db.galaries.Where(x => x.photo_video == "V").Select(x => new
                        {
                            album = x.album_desc,
                            album_eng = x.album_desc_eng
                        }).Distinct().ToList(), JsonRequestBehavior.AllowGet);
                    }
                    else
                        return null;
                }
                catch (Exception)
                {
                    ViewBag.Message = "Something went wrong";
                    throw;
                }
            }

        }

        //चालू घडामोडी 
        
        public ActionResult cp_chalughadamodi()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                List<mst_single_image_pdfModel> model = new List<mst_single_image_pdfModel>();
                //Banner list
                List<mst_single_image_pdf> obj = new List<mst_single_image_pdf>();
                obj = db.mst_single_image_pdf.Where(x => x.content_type == "News").OrderByDescending(x => x.id).ToList();
                foreach (var item in obj)
                {
                    mst_single_image_pdfModel model1 = new mst_single_image_pdfModel();
                    model1.id = item.id;
                    model1.content_type = item.content_type;
                    model1.title1 = item.title;
                    model1.title_eng = item.title_eng;
                    model1.doc_name = item.doc_name;
                    model1.doc_location = item.doc_location;
                    model1.active_status = item.active_status;
                    model.Add(model1);
                }


                //mst_single_image_pdfModel model = new mst_single_image_pdfModel();

                //var lst_news = db.mst_single_image_pdf.Where(x => x.content_type == "News").OrderByDescending(x => x.id).ToList();
                //var lst_tblurlid = db.tbl_urlId.ToList();
                //var lst = from s in lst_news
                //          join u in lst_tblurlid on s.id equals u.id into table1
                //          from u in table1.ToList()
                //          select new ViewModel
                //          {
                //              mst_single_image_pdf = s,
                //              tbl_urlId = u
                //          };
                //model.lst_viewModel_banner = lst.ToList();

                return View(model);
            }
        }
        
        public ActionResult cp_chalughadamodi_add()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "CPAdmin");
            else
            {
                mst_single_image_pdfModel model = new Models.mst_single_image_pdfModel();
                //Year list
                ViewBag.YearListItems = new SelectList(db.mst_single_image_pdf.Where(x => x.active_status == "A" && x.content_type == "News").Select(m => m.title_eng).Distinct());
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult cp_chalughadamodi_add(mst_single_image_pdfModel model, HttpPostedFileBase[] postedFile)
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
                    foreach (HttpPostedFileBase file in postedFile)
                    {
                        //Checking file is available to save.  
                        if (file != null)
                        {
                            filename = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                            extension = System.IO.Path.GetExtension(file.FileName);
                            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                            
                            if (extension.ToString().ToUpper() != ".JPG" && extension.ToString().ToUpper() != ".JPEG")
                            {
                                ViewBag.fileExtensionMessage = "Please select jpeg/jpg file";
                                return View(model);
                            }
                        }
                    }
                    foreach (HttpPostedFileBase file in postedFile)
                    {
                        //Checking file is available to save.  
                        if (file != null)
                        {
                            filename = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                            extension = System.IO.Path.GetExtension(file.FileName);
                            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                            var InputFileName = Path.GetFileName(filename);
                            var ServerSavePath = Path.Combine(Server.MapPath("~/Content/images/ChaluGhadamodi/") + InputFileName);
                            //Save file to server folder  
                            file.SaveAs(ServerSavePath);

                            mst_single_image_pdf obj = new mst_single_image_pdf();
                            obj.created_by = Convert.ToInt32(Session["UserID"]);
                            obj.created_date = DateTime.Today;
                            obj.content_type = "News";
                            obj.title = model.title1;
                            obj.title_eng = model.title1;
                            obj.file_type = "i";
                            obj.doc_name = filename;
                            obj.doc_location = "~/Content/Images/ChaluGhadamodi/";
                            obj.active_status = "A";
                            db.mst_single_image_pdf.Add(obj);
                            db.SaveChanges();
                        }
                    }
                    ViewBag.Message = postedFile.Count().ToString() + " files uploaded successfully.";
                    //Year list
                    ViewBag.YearListItems = new SelectList(db.mst_single_image_pdf.Where(x => x.active_status == "A" && x.content_type == "News").Select(m => m.title_eng).Distinct());
                    return View(model);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Something went wrong" + ex.Message;
                    return View(model);
                }
            }

        }
        
        public ActionResult cp_chalughadamodi_activeDeactive(long? id)
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

        //Role

        public ActionResult cp_role()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                mst_roleModel model = new mst_roleModel();
                //Role list
                var lst = db.mst_role.ToList(); ;
                model.mst_roleList = lst.ToList();
                return View(model);
            }
        }
        public ActionResult cp_role_activeDeactive(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    mst_role obj = null;
                    if (id != 0)
                    {
                        obj = db.mst_role.SingleOrDefault(x => x.id == id);
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
        
        public ActionResult cp_role_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                mst_roleModel model = new Models.mst_roleModel();
                mst_role obj = new mst_role();
                if (id != 0 && id != null)
                {
                    obj = db.mst_role.SingleOrDefault(x => x.id == id);
                    if (obj != null)
                    {
                        model.id = obj.id;
                        model.role_name = obj.role_name;
                    }
                    else
                        ViewBag.error = "Something went wrong.";
                }
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult cp_role_addedit(mst_roleModel model)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    mst_role obj = null;
                    if (model.id != 0)
                    {
                        obj = db.mst_role.SingleOrDefault(x => x.id == model.id);
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
                        obj = new mst_role();
                        obj.created_by = Convert.ToInt32(Session["UserID"]);
                        obj.created_date = DateTime.Today;
                    }
                    obj.role_name = model.role_name;
                    obj.active_status = "A";
                    if (model.id == 0)
                        db.mst_role.Add(obj);
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

        //User

        public ActionResult cp_user()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                mst_userModel model = new mst_userModel();
                //User list
                var lst = db.mst_user.ToList(); ;
                model.mst_userList = lst.ToList();
                return View(model);
            }
        }
        public ActionResult cp_user_activeDeactive(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                try
                {
                    mst_user obj = null;
                    if (id != 0)
                    {
                        obj = db.mst_user.SingleOrDefault(x => x.user_id == id);
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
        
        public ActionResult cp_user_addedit(long? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            else
            {
                mst_userModel model = new Models.mst_userModel();
                mst_user obj = new mst_user();
                //Role list
                var lst = db.mst_role.Where(x=>x.active_status=="A").ToList(); 
                model.mst_roleList = lst.ToList();

                if (id != 0 && id != null)
                {
                    obj = db.mst_user.SingleOrDefault(x => x.user_id == id);
                    if (obj != null)
                    {
                        model.user_id = obj.user_id;
                        model.user_role_id = obj.user_role_id;
                        model.user_name = obj.user_name;
                        model.user_create_password = Decrypt( obj.user_password);
                        model.user_confirm_password = Decrypt( obj.user_password);
                        model.user_fname = obj.user_fname;
                        model.user_mname1 = obj.user_mname1;
                        model.user_lname2 = obj.user_lname2;
                    }
                    else
                        ViewBag.error = "Something went wrong.";
                }
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult cp_user_addedit(mst_userModel model)
        {
            if (ModelState.IsValid)
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "CPAdmin");
                }
                else
                {
                    //Role list
                    var lst = db.mst_role.Where(x => x.active_status == "A").ToList();
                    model.mst_roleList = lst.ToList();
                    try
                    {
                        mst_user obj = null;
                        if (model.user_id != 0)
                        {
                            obj = db.mst_user.SingleOrDefault(x => x.user_id == model.user_id);
                            if (obj == null)
                            {
                                return View();
                            }
                            else
                            {
                                //check user name 
                                if (db.mst_user.Where(x=> x.user_name ==model.user_name && x.user_id != model.user_id).ToList().Count  >=1)
                                {
                                    ViewBag.Message = "User Name already exists. Please choose another one!";
                                    return View(model);
                                }
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
                            //check user name 
                            if (db.mst_user.Where(x => x.user_name == model.user_name).ToList().Count >= 1)
                            {
                                ViewBag.Message = "User Name already exists. Please choose another one!";
                                return View(model);
                            }
                            obj = new mst_user();
                            obj.created_by = Convert.ToInt32(Session["UserID"]);
                            obj.created_date = DateTime.Today;
                        }
                        obj.user_role_id = model.user_role_id;
                        obj.user_name = model.user_name;
                        obj.user_password = Encrypt(model.user_create_password);
                        obj.user_fname = model.user_fname;
                        obj.user_mname1 = model.user_mname1;
                        obj.user_lname2 = model.user_lname2;
                        obj.active_status = "A";
                        if (model.user_id == 0)
                            db.mst_user.Add(obj);
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
            else
            {
                //Role list
                var lst = db.mst_role.Where(x => x.active_status == "A").ToList();
                model.mst_roleList = lst.ToList();
                return View(model);
                ViewBag.Message = "Wrong username or password. Try again.";
                return View(model);
            }
        }

        public ActionResult page(string url)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "CPAdmin");
            }
            return View();
        }
       
    }
}
