using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarCom.Presentation.Controllers
{
    public class DesignRequestController : Controller
    {
        // GET: DesignRequest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Approve(int id)
        {
            ViewBag.Employee = new SelectList(EmployeeRepo.Get(), "Id", "First_Name");
            DesignApproveViewModel model = DesignApproveRepo.GetById(id);
            return PartialView("_Approve", model);
        }
    }
}