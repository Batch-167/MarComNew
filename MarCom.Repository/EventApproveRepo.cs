using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class EventApproveRepo
    {
        public static ResultResponse Approve(EventApproveViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    T_Event ev = db.T_Event.Where(e => e.Id == entity.Id).FirstOrDefault();
                    if (ev != null)
                    {
                        ev.Reject_Reason = entity.Reject_Reason;
                        ev.Status = entity.Status;
                        ev.Assign_To = entity.Assign_To;

                        if (entity.Status == 2)
                        {
                            ev.Approved_Date = DateTime.Now;
                            ev.Approved_By = entity.Approved_By;
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
        
        public static EventApproveViewModel GetById(int id)
        {
            EventApproveViewModel result = new EventApproveViewModel();
            using (var db = new MarComContext())
            {
                result = (from e in db.T_Event
                          join em in db.M_Employee
                          on e.Request_By equals em.Id
                          where e.Id == id
                          select new EventApproveViewModel
                          {
                              Id = e.Id,
                              Code = e.Code,
                              Event_Name = e.Event_Name,
                              Place = e.Place,
                              Start_Date = e.Start_Date,
                              End_Date = e.End_Date,
                              Budget = e.Budget,
                              Request_By = e.Request_By,
                              NameRequest = em.First_Name + " " + em.Last_Name,
                              Request_Date = e.Request_Date,
                              Note = e.Note,
                              Status = e.Status
                          }).FirstOrDefault();
            }
            return result;
        }
        
        public static List<EmployeeViewModel> GetEmp()
        {
            List<EmployeeViewModel> result = new List<EmployeeViewModel>();
            using (var db = new MarComContext())
            {
                result = (from e in db.M_Employee
                          join u in db.M_User
                          on e.Id equals u.M_Employee_Id
                          join r in db.M_Role
                          on u.M_Role_Id equals r.Id
                          where r.Id == 2
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
