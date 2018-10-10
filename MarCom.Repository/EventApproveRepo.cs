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
                        ev.Assign_To = entity.Assign_To;

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
                              Request_Date = e.Request_Date,
                              Note = e.Note,
                              Status = e.Status
                          }).FirstOrDefault();
            }
            return result;
        }      
    }
}
