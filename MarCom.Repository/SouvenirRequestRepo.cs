using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class SouvenirRequestRepo
    {
        public static List<SouvenirRequestViewModel> Get()
        {
            List<SouvenirRequestViewModel> result = new List<SouvenirRequestViewModel>();
            using (var db = new MarComContext())
            {
                result = (from sr in db.T_Souvenir
                          select new SouvenirRequestViewModel
                          {
                              Id = sr.Id,
                              Code = sr.Code,
                              Request_By = sr.Request_By,
                              Request_Date = sr.Request_Date,
                              Request_Due_Date = sr.Request_Due_Date,
                              Status = sr.Status,
                              Is_Delete = sr.Is_Delete,

                              Create_Date = sr.Create_Date,
                              Create_By = "Administrator"
                          }).ToList();
            }
            return result;
        }

        public static ResultResponse Update (SouvenirRequestViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        T_Souvenir t_Souv = new T_Souvenir();
                        t_Souv.Code = entity.Code;
                        t_Souv.Type = "Additional";
                        t_Souv.T_Event_Id = entity.T_Event_Id;
                        t_Souv.Request_By = entity.Request_By;
                        t_Souv.Request_Date = entity.Request_Date;
                        t_Souv.Request_Due_Date = entity.Request_Due_Date;
                        t_Souv.Note = entity.Note;
                        t_Souv.Status = 1;

                        t_Souv.Create_By = entity.Create_By;
                        t_Souv.Create_Date = DateTime.Now;

                        db.T_Souvenir.Add(t_Souv);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }

        public static string GetNewCode()
        {
            string yearMonth = DateTime.Now.Day.ToString("D2")+DateTime.Now.Month.ToString("D2")+DateTime.Now.ToString("yy");
            string newRef = "TRSV" + yearMonth + "-";
            using (var db = new MarComContext())
            {
                var result = (from sr in db.T_Souvenir
                              where sr.Code.Contains(newRef)
                              select new { code = sr.Code })
                              .OrderByDescending(o => o.code)
                              .FirstOrDefault();
                if (result != null)
                {
                    string[] oldRef = result.code.Split('-');
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
