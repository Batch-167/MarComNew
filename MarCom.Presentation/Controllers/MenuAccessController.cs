using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarCom.Presentation.Controllers
{
    public class MenuAccessController : Controller
    {
        // GET: MenuAccess
        public ActionResult Index()
        {
            return View();
        }

        //GET: List
        public ActionResult List()
        {
            return PartialView("_List", MenuAccessRepo.Get());
        }

        //GET: Create
        public ActionResult Create()
        {
            //List<MenuAccessViewModel> model = MenuRepo.GetList();
            //return PartialView("_Create", new MenuAccessViewModel(), model);
            return PartialView("_Create", new MenuAccessViewModel());
        }
    }
}