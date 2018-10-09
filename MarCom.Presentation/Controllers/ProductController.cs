using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarCom.Presentation.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", ProductRepo.Get());
        }

        //GET : New Product
        public ActionResult Create()
        {
            ViewBag.Product = new SelectList(ProductRepo.Get(), "Id", "Name");
            return PartialView("_Create", new ProductViewModel());
        }

        //POS
        //[Authorize]
        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
            //model.Create_By = User.Identity.Name;
            ResultResponse result = ProductRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        //VIEW
        public ActionResult View(int id)
        {
            ProductViewModel model = ProductRepo.GetById(id);
            return PartialView("_View", model);
        }

        //EDIT
        public ActionResult Edit(int id)
        {
            ViewBag.Product = new SelectList(ProductRepo.Get(), "Id", "Name");
            ProductViewModel model = ProductRepo.GetById(id);
            return PartialView("_Edit", model);
        }

        //POST
        [HttpPost]
        public ActionResult Edit(ProductViewModel model)
        {
            ResultResponse result = ProductRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }


        //DELETE
        public ActionResult Delete(int id)
        {

            ProductViewModel model = ProductRepo.GetById(id);
            return PartialView("_Delete", model);
        }

        //POST
        [HttpPost]
        public ActionResult Delete(ProductViewModel model)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            bool result = ProductRepo.Delete(id);
            
            if (result)
            {
                return Json(new
                {
                    success = result,
                    entity = "",
                    message = "delete success"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = result,
                    entity = "",
                    message = "delete failed"
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}