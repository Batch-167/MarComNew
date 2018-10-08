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
    [Authorize]
    public class SouvenirController : Controller
    {
        // GET: Souvenir
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List",SouvenirRepo.Get());
        }

        public ActionResult Create()
        {
            ViewBag.Souvenir = new SelectList(UnitRepo.Get(), "Id", "Name");            
            return PartialView("_Create", new SouvenirViewModel());
        }

        //POS        
        [HttpPost]
        public ActionResult Create(SouvenirViewModel model)
        {
            model.Create_By = User.Identity.Name;
            ResultResponse result = SouvenirRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("index");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Unit = new SelectList(UnitRepo.Get(), "Id", "Name");
            SouvenirViewModel model = SouvenirRepo.GetById(id);
            return PartialView("_Edit", model);
        }

        //POST        
        [HttpPost]
        public ActionResult Edit(SouvenirViewModel model)
        {
            model.Update_By = User.Identity.Name;
            ResultResponse result = SouvenirRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Show(int id)
        {
            SouvenirViewModel model = SouvenirRepo.GetById(id);
            return PartialView("_Show", model);
        }
        

        public ActionResult Delete(int id)
        {
            
            //ViewBag.Unit = new SelectList(SouvenirRepo.Get(), "Id", "Name");
            SouvenirViewModel model = SouvenirRepo.GetById(id);
            return PartialView("_Delete", model);
        }

        //POST
        [HttpPost]
        public ActionResult Delete(SouvenirViewModel model)
        {

            return RedirectToAction("index");
        }
   
        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            if (SouvenirRepo.Delete(id))
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}