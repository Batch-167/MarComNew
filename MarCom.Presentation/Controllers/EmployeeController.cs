using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarCom.Presentation.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        // GET: List
        public ActionResult List()
        {
            return PartialView("_List", EmployeeRepo.Get());
        }

        // GET : Create
        public ActionResult Create()
        {
            return PartialView("_Create", new EmployeeViewModel());
        }


    }
}