using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MarCom.Presentation.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Filter(ProductViewModel model)
        {
            return PartialView("_List", ProductRepo.Filter(model));
        }

        public ActionResult List()
        {
            UserViewModel access = DesignApproveRepo.GetIdByName(User.Identity.Name);
            if (access.Role == "Admin")
            {
                return PartialView("_List", ProductRepo.Get());
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        //GET : New Product
        public ActionResult Create()
        {
            ViewBag.Product = new SelectList(ProductRepo.Get(), "Id", "Name");
            UserViewModel access = DesignApproveRepo.GetIdByName(User.Identity.Name);
            if (access.Role == "Admin")
            {
                return PartialView("_Create", new ProductViewModel());
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        //POS
        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
            model.Create_By = User.Identity.Name;
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
            UserViewModel access = DesignApproveRepo.GetIdByName(User.Identity.Name);
            if (access.Role == "Admin")
            {
                return PartialView("_View", model);
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        //EDIT
        public ActionResult Edit(int id)
        {
            ViewBag.Product = new SelectList(ProductRepo.Get(), "Id", "Name");
            ProductViewModel model = ProductRepo.GetById(id);
            UserViewModel access = DesignApproveRepo.GetIdByName(User.Identity.Name);
            if (access.Role == "Admin")
            {
                return PartialView("_Edit", model);
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        //POST
        [HttpPost]
        public ActionResult Edit(ProductViewModel model)
        {
            model.Create_By = User.Identity.Name;
            model.Update_By = User.Identity.Name;
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
            UserViewModel access = DesignApproveRepo.GetIdByName(User.Identity.Name);
            if (access.Role == "Admin")
            {
                return PartialView("_Delete", model);
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
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
                    message = "Data Deleted! Data Product has been deleted!"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = result,
                    entity = "",
                    message = "Delete Failed!!!"
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}