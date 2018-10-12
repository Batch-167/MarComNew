using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class DesignRequestRepo
    {
        public static List<DesignRequestViewModel> Get()
        {
            List<DesignRequestViewModel> result = new List<DesignRequestViewModel>();
            try
            {
                using (var db = new MarComContext())
                {
                    result = (from dr in db.T_Design
                              join e in db.T_Event on
                              dr.T_Event_Id equals e.Id
                              select new DesignRequestViewModel
                              {
                                  Id = dr.Id,
                                  Code = dr.Code,
                                  T_Event_Id = dr.T_Event_Id,
                                  EventCode = e.Code,
                                  Request_By = dr.Request_By,
                                  Request_Date = dr.Request_Date,
                                  Assign_To = dr.Assign_To,
                                  Status = dr.Status,

                                  Is_Delete = dr.Is_Delete,

                                  Create_Date = dr.Create_Date,
                                  Create_By = "Administrator"
                              }).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }

        public static ResultResponse Update(DesignRequestViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        T_Design design = new T_Design();
                        design.Code = entity.Code;
                        design.Title_Header = entity.Title_Header;
                        design.T_Event_Id = entity.T_Event_Id;
                        design.Request_By = entity.Request_By;
                        design.Request_Date = DateTime.Now;
                        design.Note = entity.Note;

                        design.Create_Date = DateTime.Now;
                        design.Create_By = "Administrator";

                        db.T_Design.Add(design);
                        db.SaveChanges();

                    }
                    else
                    {
                        T_Design design = db.T_Design.Where(dr => dr.Id == entity.Id).FirstOrDefault();
                        if (design != null)
                        {
                            design.Code = entity.Code;
                            design.Title_Header = entity.Title_Header;
                            design.T_Event_Id = entity.T_Event_Id;
                            design.Request_By = entity.Request_By;
                            design.Request_Date = DateTime.Now;
                            design.Note = entity.Note;

                            design.Update_Date = DateTime.Now;
                            design.Update_By = "Administrator";

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
        public static string GetNewCode()
        {
            string yearMonth = DateTime.Now.ToString("dd") +
                DateTime.Now.Month.ToString("D2") +
                DateTime.Now.ToString("yy");
            string newRef = "TRWODS" + yearMonth + "0";
            using (var db = new MarComContext())
            {
                var result = (from dr in db.T_Design
                              where dr.Code.Contains(newRef)
                              select new { code = dr.Code })
                              .OrderByDescending(o => o.code)
                              .FirstOrDefault();
                if (result != null)
                {
                    string[] oldRef = result.code.Split('0');
                    newRef += (int.Parse(oldRef[1]) + 1).ToString("D4");
                }
                else
                {
                    newRef += "0001";
                }
            }
            return newRef;
        }
    }
}
