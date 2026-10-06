using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SRACMSApplication.Models;

namespace SRACMSApplication.Controllers
{
    public class GalleryController : Controller
    {
        //
        // GET: /Gallery/

        private sra_website_cmsEntities db = new sra_website_cmsEntities();

        public ActionResult Gallery(string desc)
        {
            if (Session["sraLogo"] == null)
                return RedirectToAction("Home", "Home");
            Session["Page"] = "Gallery";
            Session["Controller"] = "Gallery";
            galaryModel model = new galaryModel();

            //var lst1 = new SelectList(db.galaries.Where(x => x.photo_video == "P").Select(x => x.album_desc).Distinct().ToList(), "album_desc", "album_desc");
            var lst1 = new SelectList(db.galaries.Where(x => x.photo_video == "P" && x.active_status =="A").Select(x => x.album_desc).Distinct().ToList());
            var lst2 = new SelectList(db.galaries.Where(x => x.photo_video == "P" && x.active_status == "A").Select(x => x.album_desc_eng).Distinct().ToList());
            if (Session["Lang"].ToString() =="mr")
                model.DistinctPhotoAlbum = lst1.ToList();
            else
                model.DistinctPhotoAlbum = lst2.ToList();

            string firstAlbum ="";
             foreach (var item in model.DistinctPhotoAlbum)
            {
                firstAlbum = item.Text;
                Session["selectedAlbum"] = firstAlbum;
                break;
            }
            //var lst = db.galaries.Where(x => x.photo_video == "P").OrderByDescending(x => x.id).ToList();
            var lst = db.galaries.Where(x => x.photo_video == "P" && x.active_status=="A" && x.album_desc == firstAlbum).ToList();
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
      
    }
}
