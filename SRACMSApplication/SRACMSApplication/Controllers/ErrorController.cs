using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRACMSApplication.Controllers
{
    public class ErrorController : Controller
    {
       public ActionResult NotFound()//HTTP 404
        {
            return View();
        }

    }
}
