using SRACMSApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRACMSApplication.Controllers
{
    public class ShasanNirnayController : Controller
    {

        private sra_website_cmsEntities db = new sra_website_cmsEntities();
        //
        // GET: /ShasanNirnay/

        public ActionResult ShasanNirnay()
        {
            if (Session["sraLogo"] == null)
                return RedirectToAction("Home", "Home");
            Session["Page"] = "ShasanNirnay";
            Session["Controller"] = "ShasanNirnay";
            govResolutionsModel model = new govResolutionsModel();
            //PMC Slum list
            //var lst_pmc = db.GovtResolutions.Where(x => x.active_status == "A").OrderByDescending(x=>x.id);
            var lst_pmc = db.GovtResolutions.Where(x => x.active_status == "A").OrderByDescending(x => x.resolution_date).ToList();
            model.GovtResolutionList = lst_pmc.ToList();
            return View(model);
        }
       

    }
}
