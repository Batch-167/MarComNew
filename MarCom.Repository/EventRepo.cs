using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class EventRepo
    {
        public static List<EventViewModel> Get()
        {
            List<EventViewModel> result = new List<EventViewModel>();
            using (var db = new MarComContext())
            {
                result = (from e in db.T_Event
                          join em in db.M_Employee
                          on e.Request_By equals em.Id
                          select new EventViewModel
                          {
                              Id = e.Id,
                              Id=e.Id,
                              Code = e.Code,
                              Event_Name = e.Event_Name,
                              Request_By = e.Request_By,
                              Request_Date = e.Request_Date,
                              NameRequest = em.First_Name +" "+ em.Last_Name,
                              Status = e.Status,
                              Create_Date = e.Create_Date,
                              Create_By = e.Create_By
                          }
                          ).ToList();

            }
            return result;
        }

        public static EventViewModel GetById(int id)
        {
            EventViewModel result = new EventViewModel();
            using (var db = new MarComContext())
            {
                result = (from e in db.T_Event
                          join em in db.M_Employee
                          on e.Request_By equals em.Id
                          where e.Id == id
                          select new EventViewModel
                          {
                              Id = e.Id,
                              Code = e.Code,
                              Event_Name = e.Event_Name,
                              Place = e.Place,
                              Start_Date = e.Start_Date,
                              End_Date = e.End_Date,
                              NameRequest = em.First_Name + " " + em.Last_Name,
                              Budget = e.Budget,
                              Request_By = e.Request_By,
                              Request_Date = e.Request_Date,
                              Note = e.Note,
                              Status = e.Status

                          }
                          ).FirstOrDefault();
            }
            return result;
        }

        public static string GetCode()
        {
            string tgl = DateTime.Now.ToString("dd") + DateTime.Now.Month.ToString("D2") + DateTime.Now.ToString("yy");
            string inisial = "TRWOEV" + tgl + "-";
            using (var db = new MarComContext())
            {
                var result = (from c in db.T_Event
                              where c.Code.Contains(inisial)
                              select new { code = c.Code }
                             ).OrderByDescending(c => c.code)
                             .FirstOrDefault();
                if (result != null)
                {
                    string[] oldInis = result.code.Split('-');
                    inisial += (int.Parse(oldInis[1]) + 1).ToString("D4");
                }
                else
                {
                    inisial += "0001";
                }
            }
            return inisial;
        }

        public static ResultResponse Update(EventViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        T_Event ev = new T_Event();
                        ev.Code = entity.Code;
                        ev.Event_Name = entity.Event_Name;
                        ev.Start_Date = entity.Start_Date;
                        ev.End_Date = entity.End_Date;
                        ev.Place = entity.Place;
                        ev.Budget = entity.Budget;
                        ev.Request_By = entity.Request_By;
                        ev.Request_Date = entity.Request_Date;
                        ev.Note = entity.Note;
                        ev.Is_Delete = entity.Is_Delete;
                        ev.Status = entity.Status;

                        ev.Create_By = entity.Create_By;
                        ev.Create_Date = DateTime.Now;

                        db.T_Event.Add(ev);
                        db.SaveChanges();

                    }
                    else
                    {
                        T_Event ev = db.T_Event.Where(e => e.Id == entity.Id).FirstOrDefault();
                        if (ev != null)
                        {
                            ev.Id = entity.Id;
                            ev.Code = entity.Code;
                            ev.Event_Name = entity.Event_Name;
                            ev.Start_Date = entity.Start_Date;
                            ev.End_Date = entity.End_Date;
                            ev.Place = entity.Place;
                            ev.Budget = entity.Budget;
                            ev.Request_By = entity.Request_By;
                            ev.Request_Date = entity.Request_Date;
                            ev.Note = entity.Note;
                            
                            ev.Update_By = entity.Update_By;
                            ev.Update_Date = DateTime.Now;

                            db.SaveChanges();
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
    }
}
