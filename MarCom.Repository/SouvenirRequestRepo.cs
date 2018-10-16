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
                              Name = e.First_Name + " " + e.Last_Name,
                              Request_Due_Date = sr.Request_Due_Date,
                              Status = sr.Status,
                              Is_Delete = sr.Is_Delete,

                              Create_Date = sr.Create_Date,
                              Create_By = "Administrator"
                          }).ToList();
            }
            return result;
        }

        public static List<SouvenirItemViewModel> GetItem(int id)
        {
            List<SouvenirItemViewModel> result = new List<SouvenirItemViewModel>();
            using (var db = new MarComContext())
            {
                result = (from si in db.T_Souvenir_Item
                          join ts in db.T_Souvenir on si.T_Souvenir_Id equals ts.Id
                          join s in db.M_Souvenir on si.M_Souvenir_Id equals s.Id
                          where si.T_Souvenir_Id == id
                          select new SouvenirItemViewModel
                          {
                              Id = si.Id,
                              T_Souvenir_Id = si.T_Souvenir_Id,
                              M_Souvenir_Id = si.M_Souvenir_Id,
                              SouvenirName = s.Name,
                              Qty = si.Qty,
                              Note = si.Note,
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

                        foreach (var item in entityitem)
                        {
                            T_Souvenir_Item t_SouvItem = new T_Souvenir_Item();
                            t_SouvItem.T_Souvenir_Id = t_Souv.Id;
                            t_SouvItem.M_Souvenir_Id = item.M_Souvenir_Id;
                            t_SouvItem.Qty = item.Qty;
                            t_SouvItem.Note = item.Note;
                            t_SouvItem.Is_Delete = item.Is_Delete;

                            t_SouvItem.Create_By = entity.Create_By;
                            t_SouvItem.Create_Date = DateTime.Now;

                            db.T_Souvenir_Item.Add(t_SouvItem);
                            db.SaveChanges();
                        }
                        result.Message = "Data Saved ! Transaction Souvenir Request has been add with code " + entity.Code;
                    }
                    else
                    {
                        T_Souvenir t_Souv = db.T_Souvenir.Where(ts => ts.Id == entity.Id).FirstOrDefault();
                        if (t_Souv != null)
                        {
                            t_Souv.Code = entity.Code;
                            t_Souv.T_Event_Id = entity.T_Event_Id;
                            t_Souv.Request_By = entity.Request_By;
                            t_Souv.Request_Date = entity.Request_Date;
                            t_Souv.Request_Due_Date = entity.Request_Due_Date;
                            t_Souv.Note = entity.Note;
                            t_Souv.Status = 1;

                            t_Souv.Update_By = entity.Update_By;
                            t_Souv.Update_Date = DateTime.Now;

                            foreach (var item in entityitem)
                            {
                                T_Souvenir_Item t_SouvItem = new T_Souvenir_Item();
                                t_SouvItem.T_Souvenir_Id = entity.Id;
                                t_SouvItem.M_Souvenir_Id = item.M_Souvenir_Id;
                                t_SouvItem.Qty = item.Qty;
                                t_SouvItem.Note = item.Note;
                                t_SouvItem.Is_Delete = item.Is_Delete;

                                if (item.Id == 0)
                                {
                                    t_SouvItem.Create_By = entity.Update_By;
                                    t_SouvItem.Create_Date = DateTime.Now;
                                }
                                else
                                {
                                    t_SouvItem.Update_By = entity.Update_By;
                                    t_SouvItem.Update_Date = DateTime.Now;
                                }
                                db.T_Souvenir_Item.Add(t_SouvItem);
                            }
                            db.SaveChanges();
                        }
                        result.Message = "Data Update ! Transaction Souvenir Request with code " + entity.Code + " has been update";
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static ResultResponse UpdateQtyActual(SouvenirItemViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    T_Souvenir_Item si = db.T_Souvenir_Item.Where(s => s.Id == entity.Id).FirstOrDefault();
                    if (si !=null)
                    {
                        si.Qty_Settlement = entity.Qty_Settlement;
                    }
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
                    newRef += (int.Parse(oldRef[1]) + 1).ToString("D5");
                }
                else
                {
                    newRef += "00001";
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

        public static SouvenirRequestViewModel GetById(int id)
        {
            SouvenirRequestViewModel result = new SouvenirRequestViewModel();
            using (var db = new MarComContext())
            {
                result = (from s in db.T_Souvenir
                          join e in db.T_Event on s.T_Event_Id equals e.Id
                          join em in db.M_Employee on s.Request_By equals em.Id
                          where id == s.Id
                          select new SouvenirRequestViewModel
                          {
                              Id = s.Id,
                              Code = s.Code,
                              T_Event_Id = s.T_Event_Id,
                              CodeEvent = e.Code,
                              Status = s.Status,
                              Request_By = s.Request_By,
                              Name = em.First_Name + " " + em.Last_Name,
                              Request_Date = s.Request_Date,
                              Request_Due_Date = s.Request_Due_Date,
                              Note = s.Note,
                          }).FirstOrDefault();
            }
            return result;
        }

        public static void DeleteItem(int id)
        {
            using (var db = new MarComContext())
            {
                foreach (var item in db.T_Souvenir_Item)
                {
                    if (item.T_Souvenir_Id == id)
                    {
                        db.T_Souvenir_Item.Remove(item);
                    }
                }
                db.SaveChanges();
            }
        }


        public static ResultResponse Approve(SouvenirRequestViewModel entity)
        {
            int? stat = entity.Status;
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    T_Souvenir souv = db.T_Souvenir.Where(s => s.Id == entity.Id).FirstOrDefault();
                    if (souv != null)
                    {
                        souv.Reject_Reason = entity.Reject_Reason;
                        souv.Status = stat;

                        if (entity.Status == 2)
                        {
                            souv.Approve_Date = DateTime.Now;
                            souv.Approved_By = entity.Approved_By;
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

        public static ResultResponse Received(SouvenirRequestViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    T_Souvenir t_Souv = db.T_Souvenir.Where(ts => ts.Id == entity.Id).FirstOrDefault();
                    if (t_Souv != null)
                    {
                        t_Souv.Status = 3;

                        t_Souv.Received_By = entity.Received_By;
                        t_Souv.Received_Date = DateTime.Now;
                    }
                    db.SaveChanges();
                    result.Message = "Data Updated !! Transaction Souvenir Request with code" + entity.Code + "has been Received By Requester";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        //Ambil Id untuk Ubah status saat Approval
        public static ResultResponse Approved(SouvenirRequestViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    T_Souvenir so = db.T_Souvenir.Where(s => s.Id == entity.Id).FirstOrDefault();
                    if (so != null)
                    {
                        so.Reject_Reason = entity.Reject_Reason;
                        so.Status = entity.Status;

                        if (entity.Status == 2)
                        {
                            so.Settlement_Approved_By = entity.Settlement_Approved_By;
                            so.Settlement_Approved_Date = DateTime.Now;
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

    }
}
