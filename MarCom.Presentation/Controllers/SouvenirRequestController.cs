using MarCom.DataModel;
using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

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

        public ActionResult Add()
        {
            UserViewModel result = SouvenirRequestRepo.GetIdByName(User.Identity.Name);
            SouvenirRequestViewModel model = new SouvenirRequestViewModel();
            ViewBag.Event = new SelectList(EventRepo.Get(), "Id", "Code");
            model.Request_By = result.M_Employee_Id;
            model.Name = result.Fullname;
            model.Code = SouvenirRequestRepo.GetNewCode();
            model.Request_Date = DateTime.Now;
            return PartialView("_Add", model);
        }

        [HttpPost]
        public ActionResult Add(SouvenirRequestViewModel model, List<SouvenirItemViewModel> item)
        {
            model.Create_By = User.Identity.Name;
            ResultResponse result = SouvenirRequestRepo.Update(model,item);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddItem ()
        {
            ViewBag.Souvenir = new SelectList(SouvenirRepo.Get(), "Id", "Name");
            SouvenirItemViewModel model = new SouvenirItemViewModel();
            return PartialView("_AddItem", model);
        }

        public ActionResult Edit(int id)
        {
            SouvenirRequestViewModel model = SouvenirRequestRepo.GetById(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(SouvenirRequestViewModel model, List<SouvenirItemViewModel> item)
        {
            model.Update_By = User.Identity.Name;
            SouvenirRequestRepo.DeleteItem();
            ResultResponse result = SouvenirRequestRepo.Update(model, item);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListItem(int id)
        {
            ViewBag.Souvenir = new SelectList(SouvenirRepo.Get(), "Id", "Name");
            List<SouvenirItemViewModel> model = SouvenirRequestRepo.GetItem(id);
            return PartialView("_ListItem", model);
        }

        public ActionResult Received(int id)
        {
            SouvenirRequestViewModel model = SouvenirRequestRepo.GetById(id);
            return PartialView("_Received", model);
        }
    }
}