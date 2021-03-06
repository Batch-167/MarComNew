﻿using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class UserRepo
    {
        public static List<UserViewModel> Get()
        {
            List<UserViewModel> result = new List<UserViewModel>();
            using (var db = new MarComContext())
            {
                result = (from us in db.M_User
                          join em in db.M_Employee on
                          us.M_Employee_Id equals em.Id
                          join c in db.M_Company on
                          em.M_Company_Id equals c.Id
                          join ro in db.M_Role on
                          us.M_Role_Id equals ro.Id
                          select new UserViewModel
                          {
                              Id = us.Id,
                              M_Employee_Id = us.M_Employee_Id,
                              Fullname = em.First_Name + " " + em.Last_Name,
                              Company = c.Name,
                              Username = us.UserName,
                              Password = us.PasswordHash,
                              Is_Delete = us.Is_Delete,
                              Role = ro.Name,

                              Create_By = us.Create_By,
                              Create_Date = DateTime.Now
                          }).ToList();
            }
            return result;
        }

        public static ResultResponse Update(UserViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        M_User user = new M_User();
                        user.UserName = entity.Username;
                        user.PasswordHash = entity.Password;
                        user.M_Employee_Id = entity.M_Employee_Id;
                        user.Is_Delete = entity.Is_Delete;

                        user.Create_By = "Admin";
                        user.Create_Date = DateTime.Now;

                        db.M_User.Add(user);
                        db.SaveChanges();

                        result.Message = "Data Saved";
                    }
                    else
                    {
                        M_User user = db.M_User.Where(us => us.Id == entity.Id).FirstOrDefault();
                        if (user != null)
                        {
                            bool nameExists = db.M_User.Any(nm => nm.UserName.Equals(entity.Username));
                            if (nameExists)
                            {
                                result.Message = "Username " + entity.Username + " already exists";
                            }
                            else
                            {
                                user.UserName = entity.Username;
                                if (!string.IsNullOrEmpty(entity.Password))
                                {
                                    user.PasswordHash = entity.Password;
                                }
                                user.M_Employee_Id = entity.M_Employee_Id;
                                user.M_Role_Id = entity.M_Role_Id;
                                user.Is_Delete = entity.Is_Delete;

                                user.Update_By = "Admin";
                                user.Update_Date = DateTime.Now;
                                db.SaveChanges();

                                result.Message = "Data Update";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static UserViewModel GetById(int id)
        {
            UserViewModel result = new UserViewModel();
            using (var db = new MarComContext())
            {
                result = (from us in db.M_User
                          join em in db.M_Employee on
                          us.M_Employee_Id equals em.Id
                          join ro in db.M_Role on
                          us.M_Role_Id equals ro.Id
                          where us.Id == id
                          select new UserViewModel
                          {
                              Id = us.Id,
                              Username = us.UserName,
                              Password = us.PasswordHash,
                              ConfirmPassword = us.PasswordHash,
                              M_Role_Id = ro.Id,
                              Role = ro.Name,
                              M_Employee_Id = em.Id,
                              Fullname = em.First_Name + " " + em.Last_Name,

                              Is_Delete = us.Is_Delete
                          }).FirstOrDefault();
            }
            return result;
        }

        public static UserViewModel GetIdByName(string name)
        {
            UserViewModel result = new UserViewModel();
            using (var db = new MarComContext())
            {
                result = (from u in db.M_User
                          join e in db.M_Employee
                          on u.M_Employee_Id equals e.Id
                          join r in db.M_Role on u.M_Role_Id equals r.Id
                          where name == u.UserName
                          select new UserViewModel
                          {
                              Id = u.Id,
                              Password = u.PasswordHash,
                              M_Employee_Id = u.M_Employee_Id,
                              Fullname = e.First_Name + " " + e.Last_Name,
                              Role = r.Name
                          }).FirstOrDefault();
            }
            return result;
        }

        public static bool Delete(int id)
        {
            bool result = true;
            try
            {
                using (var db = new MarComContext())
                {
                    M_User user = db.M_User.Where(us => us.Id == id).FirstOrDefault();
                    if (user != null)
                    {
                        user.Is_Delete = true;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public static List<UserViewModel> Filter(UserViewModel entity)
        {
            List<UserViewModel> result = new List<UserViewModel>();
            using (var db = new MarComContext())
            {
                result = (from us in db.M_User
                          join em in db.M_Employee on
                          us.M_Employee_Id equals em.Id
                          join c in db.M_Company on
                          em.M_Company_Id equals c.Id
                          join ro in db.M_Role on
                          us.M_Role_Id equals ro.Id
                          where em.Id == entity.Id || ro.Id == entity.Id || c.Id.ToString() == entity.Company || us.UserName.Contains(entity.Username) || (us.Create_Date.ToString()).Contains(entity.Create_Date.ToString()) || us.Create_By.Contains(entity.Create_By)
                          select new UserViewModel
                          {
                              M_Employee_Id = us.M_Employee_Id,
                              M_Role_Id = ro.Id,
                              Company = c.Name,
                              Username = us.UserName,

                              Create_By = us.Create_By,
                              Create_Date = us.Create_Date
                          }).ToList();
            }
            return result;
        }

        public static List<EmployeeViewModel> GetEmp()
        {
            List<EmployeeViewModel> result = new List<EmployeeViewModel>();
            using (var db = new MarComContext())
            {
                result = (from e in db.M_Employee
                          where e.Em_Role == 0
                          select new EmployeeViewModel
                          {
                              Id = e.Id,
                              Full_Name = e.First_Name + " " + e.Last_Name
                          }
                          ).ToList();
            }
            return result;
        }

      
    }
}
