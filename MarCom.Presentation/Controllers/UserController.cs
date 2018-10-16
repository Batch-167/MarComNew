using MarCom.Presentation.Models;
using MarCom.Repository;
using MarCom.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MarCom.Presentation.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationUserManager _userManager;
        // GET: User
        public ActionResult Index()
        {
            ViewBag.Employee = new SelectList(EmployeeRepo.Get(), "Id", "First_Name");
            ViewBag.Role = new SelectList(RoleRepo.Get(), "Id", "Name");
            ViewBag.Company = new SelectList(CompanyRepo.Get(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Filter(UserViewModel model)
        {
            return PartialView("_List", UserRepo.Filter(model));
        }

        public ActionResult List()
        {
            return PartialView("_List", UserRepo.Get());
        }

        [AllowAnonymous]
        public ActionResult Add()
        {
            UserViewModel result = UserRepo.GetIdByName(User.Identity.Name);
            ViewBag.Employee = new SelectList(EmployeeRepo.Get(), "Id", "First_Name");
            ViewBag.Role = new SelectList(RoleRepo.Get(), "Id", "Name");
            if (result.Role == "Admin")
            {
            return PartialView("_Add", new RegisterViewModel());
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(RegisterViewModel model)
            {

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, M_Employee_Id = model.M_Employee_Id, Is_Delete = model.Is_Delete, Create_By = model.Create_By, Create_Date = model.Create_Date, M_Role_Id = model.RoleId };
                var result = await UserManager.CreateAsync(user, model.Password);
                //if (result.Succeeded)
                //{
                //   // await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                //    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //    // Send an email with this link
                //    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                //    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                //    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                //    //return RedirectToAction("Index", "Home");
                //}
                AddErrors(result);
                Update(user.Id, model.RoleId);
            }
            return RedirectToAction("Index");
            // If we got this far, something failed, redisplay form
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Role = new SelectList(RoleRepo.Get(), "Id", "Name");
            ViewBag.Employee = new SelectList(EmployeeRepo.Get(), "Id", "Fullname");
            UserViewModel model = UserRepo.GetById(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            ResultResponse result = UserRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            UserViewModel model = UserRepo.GetById(id);
            return PartialView("_Delete", model);
        }


        [HttpPost]
        public ActionResult Delete(UserViewModel model)
        {
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            if (UserRepo.Delete(id))
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Details(int id)
        {
            UserViewModel model = UserRepo.GetById(id);
            return PartialView("_Details", model);
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        //UserRepo
        public static ResultResponse Update(int id, int roleId)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new ApplicationDbContext())
                {

                    MUserRole userRole = new MUserRole();
                    userRole.UserId = id;
                    userRole.RoleId = roleId;

                    db.M_User_Role.Add(userRole);
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}