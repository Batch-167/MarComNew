using MarCom.DataModel;
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
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    T_Design design = db.T_Design.Where(d => d.Id == entity.Id).FirstOrDefault();
                    if (design != null)
                    {
                        design.Reject_Reason = entity.Reject_Reason;
                        design.Status = entity.Status;
                        design.Assign_To = entity.Assign_To;

                        if (entity.Status == 2)
                        {
                            design.Approved_Date = DateTime.Now;
                            design.Approved_By = 1;
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
    }
}
