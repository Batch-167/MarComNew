using MarCom.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarCom.Presentation.Controllers
{
    public class PromotionController : Controller
    {
        // GET: Promotion
        public ActionResult Index()
        {
            return View();
        }

        //GET: List
        public ActionResult List()
        {
            return PartialView("_List", PromotionRepo.Get());
        }

        //public ActionResult Create()
        //{
        //    return PartialView("_Create", PromotionRepo.Update(model));
        //}
    }
}