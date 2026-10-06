using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRACMSApplication.Models
{
    public class galaryModel
    {
        public long id { get; set; }
        public string photo_video { get; set; }
        public string album_desc { get; set; }
        public string album_desc_eng { get; set; }
        public string doc_name { get; set; }
        public string doc_location { get; set; }
        public string active_status { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<long> updated_count { get; set; }
        [NotMapped]
        public List<galary> PhotoGalleryList { get; set; }
        [NotMapped]
        public List<galary> VideoGalleryList { get; set; }
        public List<SelectListItem> photo_video_List { get; set; }
        public List<SelectListItem> VideoAlbum { get; set; }
        [NotMapped]
        public List<galary> PhotoAlbum { get; set; }
        public List<SelectListItem> DistinctPhotoAlbum { get; set; }
        public List<ViewModel> lst_viewModel_video { get; set; }
        public List<ViewModel> lst_viewModel_photo { get; set; }

        //Added by Umesh on 22/07/2026
        [NotMapped]
        public List<galary> NewsGalleryList { get; set; }
        

    }
}