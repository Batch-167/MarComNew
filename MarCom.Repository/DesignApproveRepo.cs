﻿using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class DesignApproveRepo
    {
        public static ResultResponse Approve(DesignApproveViewModel entity)
        {
            int? stat = entity.Status;
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    T_Design design = db.T_Design.Where(d => d.Id == entity.Id).FirstOrDefault();
                    if (design != null)
                    {
                        design.Reject_Reason = entity.Reject_Reason;
                        design.Assign_To = entity.Assign_To;
                        design.Status = stat;

                        if (entity.Status == 2)
                        {
                            design.Approved_Date = DateTime.Now;
                            design.Approved_By = entity.Approved_By;
                        }
                        db.SaveChanges();
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

        public static DesignApproveViewModel GetById(int id)
        {
            DesignApproveViewModel result = new DesignApproveViewModel();
            using (var db = new MarComContext())
            {
                result = (from d in db.T_Design
                          join e in db.T_Event on d.T_Event_Id equals e.Id
                          join em in db.M_Employee on d.Request_By equals em.Id
                          where d.Id == id
                          select new DesignApproveViewModel
                          {
                              Id = d.Id,
                              Code = d.Code,
                              EventCode = e.Code, 
                              Title_Header = d.Title_Header,
                              NameRequest = em.First_Name,
                              Request_Date = d.Request_Date,
                              Status = d.Status,
                              Note = d.Note
                          }).FirstOrDefault();
            }
            return result;
        }

        public static List<DesignItemViewModel> Get(int id)
        {
            List<DesignItemViewModel> result = new List<DesignItemViewModel>();
            using (var db = new MarComContext())
            {
                result = (from di in db.T_Design_Item
                          join d in db.T_Design on di.T_Design_Id equals d.Id
                          join p in db.M_Product on di.M_Product_Id equals p.Id
                          where di.T_Design_Id == id
                          select new DesignItemViewModel
                          {
                              Id = di.Id,
                              T_Design_Id = di.T_Design_Id,
                              ProductName = p.Name,
                              Description = p.Description,
                              Title_Item = di.Title_Item,
                              Request_Pic = di.Request_Pic,
                              Request_Due_Date = di.Request_Due_Date,
                              Start_Date = di.Start_Date,
                              End_Date = di.End_Date,
                              Note = di.Note,
                          }).ToList();
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
                          where name == u.UserName
                          select new UserViewModel
                          {
                              Id = u.Id,
                              Password = u.PasswordHash,
                              M_Employee_Id = u.M_Employee_Id,
                              Fullname = e.First_Name + " " + e.Last_Name
                          }).FirstOrDefault();
            }
            return result;
        }
    }
}
