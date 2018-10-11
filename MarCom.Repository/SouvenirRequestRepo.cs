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
                          join e in db.M_Employee on sr.Request_By equals e.Id
                          select new SouvenirRequestViewModel
                          {
                              Id = sr.Id,
                              Code = sr.Code,
                              Request_By = sr.Request_By,
                              Request_Date = sr.Request_Date,
                              Name = e.First_Name+ " "+e.Last_Name,
                              Request_Due_Date = sr.Request_Due_Date,
                              Status = sr.Status,
                              Is_Delete = sr.Is_Delete,

                              Create_Date = sr.Create_Date,
                              Create_By = "Administrator"
                          }).ToList();
            }
            return result;
        }

        public static ResultResponse Update(SouvenirRequestViewModel entity, List<SouvenirItemViewModel> entityitem)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        T_Souvenir t_Souv = new T_Souvenir();
                        t_Souv.Code = GetNewCode();
                        t_Souv.Type = "Additional";
                        t_Souv.T_Event_Id = entity.T_Event_Id;
                        t_Souv.Request_By = entity.Request_By;
                        t_Souv.Request_Date = DateTime.Now;
                        t_Souv.Request_Due_Date = entity.Request_Due_Date;
                        t_Souv.Note = entity.Note;
                        t_Souv.Status = 1;

                        t_Souv.Create_By = entity.Create_By;
                        t_Souv.Create_Date = DateTime.Now;

                        db.T_Souvenir.Add(t_Souv);

                        foreach (var item in entityitem)
                        {
                            T_Souvenir_Item t_SouvItem = new T_Souvenir_Item();
                            t_SouvItem.T_Souvenir_Id = entity.Id;
                            t_SouvItem.M_Souvenir_Id = item.M_Souvenir_Id;
                            t_SouvItem.Qty = item.Qty;
                            t_SouvItem.Note = item.Note;
                            t_SouvItem.Is_Delete = item.Is_Delete;

                            t_SouvItem.Create_By = entity.Create_By;
                            t_SouvItem.Create_Date = DateTime.Now;

                            db.T_Souvenir_Item.Add(t_SouvItem);
                        }
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
            string yearMonth = DateTime.Now.Day.ToString("D2") + DateTime.Now.Month.ToString("D2") + DateTime.Now.ToString("yy");
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
