using MarCom.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarCom.Presentation.Controllers
{
    public class SouvenirRequestController : Controller
    {
        // GET: SouvenirRequest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", SouvenirRequestRepo.Get());
        }
    }
}