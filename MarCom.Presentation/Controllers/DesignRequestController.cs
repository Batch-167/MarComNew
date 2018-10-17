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
using System.Web.Routing;

namespace MarCom.Presentation.Controllers
{
    [Authorize]
    public class DesignRequestController : Controller
    {
        // GET: DesignRequest
        public ActionResult Index()
        {
            ViewBag.Assign = new SelectList(DesignRequestRepo.Get(), "Assign_To", "Assign_To");
            return View();
        }

        public ActionResult List()
        {
            UserViewModel access = DesignApproveRepo.GetIdByName(User.Identity.Name);
            if (access.Role == "Requester" || access.Role == "Admin" || access.Role == "Staff")
            {
                return PartialView("_List", DesignRequestRepo.Get());
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        //FILTER
        [HttpPost]
        public ActionResult Filter(DesignRequestViewModel model)
        {
            return PartialView("_List", DesignRequestRepo.Filter(model));
        }

        [AllowAnonymous]
        public ActionResult Approve(int id)
        {
            UserViewModel result = UserRepo.GetIdByName(User.Identity.Name);
            ViewBag.Panel = "Approval Design Request";
            ViewBag.Employee = new SelectList(DesignApproveRepo.GetAssign(), "Id", "Full_Name");
            DesignApproveViewModel model = DesignApproveRepo.GetById(id);
            if (result.Role == "Admin")
            {
                return PartialView("_Approve", model);
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        [HttpPost]
        public ActionResult Approve(DesignApproveViewModel model)
        {
            UserViewModel model2 = DesignApproveRepo.GetIdByName(User.Identity.Name);
            model.Approved_By = model2.M_Employee_Id;
            ResultResponse result = DesignApproveRepo.Approve(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }


        //GET : New Product
        public ActionResult Create()
        {

            UserViewModel result = DesignRequestRepo.GetIdByName(User.Identity.Name);
            DesignRequestViewModel model = new DesignRequestViewModel();
            model.Request_By = result.M_Employee_Id;
            model.NameRequest = result.Fullname;
            model.Code = DesignRequestRepo.GetNewCode();
            ViewBag.DesignRequest = new SelectList(EventRepo.Get(), "Id", "Code");
            UserViewModel access = DesignApproveRepo.GetIdByName(User.Identity.Name);
            if (access.Role == "Requester" || access.Role == "Admin")
            {
                return PartialView("_Create", model);
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }

        }

        [HttpPost]
        public ActionResult Create(DesignRequestViewModel model, List<DesignItemViewModel> item)
        {
            model.Create_By = User.Identity.Name;
            ResultResponse result = DesignRequestRepo.Update(model, item);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AddItem()
        {
            ViewBag.Product = new SelectList(ProductRepo.Get(), "Id", "Name");
            ViewBag.Description = new SelectList(ProductRepo.Get(), "Id", "Description");
            ViewBag.Employee = new SelectList(DesignRequestRepo.GetPic(), "Id", "Full_Name");
            DesignItemViewModel model = new DesignItemViewModel();
            return PartialView("_AddItem", model);
        }



        //EDIT
        public ActionResult Edit(int id)
        {
            DesignRequestViewModel model = DesignRequestRepo.GetById(id);
            ViewBag.DesignRequest = new SelectList(EventRepo.Get(), "Id", "Code");
            UserViewModel access = DesignApproveRepo.GetIdByName(User.Identity.Name);
            if (access.Role == "Requester" || access.Role == "Admin")
            {
                return PartialView("_Edit", model);
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        //EDIT POST
        [HttpPost]
        public ActionResult Edit(DesignRequestViewModel model, List<DesignItemViewModel> item)
        {
            model.Create_By = User.Identity.Name;
            model.Update_By = User.Identity.Name;
            DesignRequestRepo.DeleteItem(model.Id);
            ResultResponse result = DesignRequestRepo.Update(model, item);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        //CLOSE DESIGN REQUEST POST
        public ActionResult Close(int id)
        {
            ViewBag.Employee = new SelectList(EmployeeRepo.Get(), "Id", "First_Name");
            DesignApproveViewModel model = DesignApproveRepo.GetById(id);
            UserViewModel access = DesignApproveRepo.GetIdByName(User.Identity.Name);
            if (access.Role == "Staff" || access.Role == "Admin")
            {
                return PartialView("_Close", model);
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        //Add Item EDIT
        public ActionResult EditList(int id)
        {
            ViewBag.Product = new SelectList(ProductRepo.Get(), "Id", "Name");
            ViewBag.Description = new SelectList(ProductRepo.Get(), "Id", "Description");
            List<DesignItemViewModel> model = DesignRequestRepo.GetItem(id);
            return PartialView("_EditList", DesignApproveRepo.Get(id));
        }

        //Add Item APPROVE
        public ActionResult ProductList(int id)
        {
            return PartialView("_ProductList", DesignApproveRepo.Get(id));
        }

        //Add Item CLOSE
        public ActionResult CloseList(int id)
        {
            ViewBag.Product = new SelectList(ProductRepo.Get(), "Id", "Name");
            ViewBag.Description = new SelectList(ProductRepo.Get(), "Id", "Description");
            List<DesignItemViewModel> model = DesignRequestRepo.GetCloseItem(id);
            return PartialView("_CloseList", DesignApproveRepo.Get(id));

        }

    }
}
