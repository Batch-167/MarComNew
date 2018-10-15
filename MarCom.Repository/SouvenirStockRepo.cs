using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class SouvenirStockRepo
    {
        public static IList<SouvenirStockViewModel> Get()
        {
            List<SouvenirStockViewModel> result = new List<SouvenirStockViewModel>();
            using (var db = new MarComContext())
            {
                result = (from ts in db.T_Souvenir
                          join em in db.M_Employee on
                          ts.Received_By equals em.Id
                          where ts.Status == null
                          select new SouvenirStockViewModel
                          {
                              Id = ts.Id,
                              Code = ts.Code,
                              Received_By = em.Id,
                              R_Name = em.First_Name + " " + em.Last_Name,
                              Received_Date = ts.Received_Date,

                              Create_By = ts.Create_By,
                              Create_Date = ts.Create_Date
                          }).ToList();
            }
            return result;
        }

        public static ResultResponse Update(SouvenirStockViewModel entity, List<SouvenirItemViewModel> entityitem)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        T_Souvenir tsouv = new T_Souvenir();
                        tsouv.Code = entity.Code;
                        tsouv.Type = "Additional";
                        tsouv.Received_By = entity.Received_By;
                        tsouv.Received_Date = entity.Received_Date;
                        tsouv.Note = entity.Note;

                        tsouv.Create_By = entity.Create_By;
                        tsouv.Create_Date = DateTime.Now;

                        db.T_Souvenir.Add(tsouv);

                        foreach (var item in entityitem)
                        {
                            T_Souvenir_Item tsouvItem = new T_Souvenir_Item();
                            tsouvItem.T_Souvenir_Id = entity.Id;
                            tsouvItem.M_Souvenir_Id = item.M_Souvenir_Id;
                            tsouvItem.Qty = item.Qty;
                            tsouvItem.Note = item.Note;
                            tsouvItem.Is_Delete = item.Is_Delete;

                            tsouvItem.Create_By = entity.Create_By;
                            tsouvItem.Create_Date = DateTime.Now;

                            db.T_Souvenir_Item.Add(tsouvItem);
                        }
                        db.SaveChanges();
                    }
                    else
                    {
                        T_Souvenir tsouv = db.T_Souvenir.Where(ts => ts.Id == entity.Id).FirstOrDefault();
                        if (tsouv != null)
                        {
                            tsouv.Code = entity.Code;
                            tsouv.Type = "Additional";
                            tsouv.Received_By = entity.Received_By;
                            tsouv.Received_Date = entity.Received_Date;
                            tsouv.Note = entity.Note;

                            tsouv.Update_By = entity.Update_By;
                            tsouv.Update_Date = DateTime.Now;

                            foreach (var item in entityitem)
                            {
                                if (item.Id == 0)
                                {
                                    T_Souvenir_Item tsouvItem = new T_Souvenir_Item();
                                    tsouvItem.T_Souvenir_Id = entity.Id;
                                    tsouvItem.M_Souvenir_Id = item.M_Souvenir_Id;
                                    tsouvItem.Qty = item.Qty;
                                    tsouvItem.Note = item.Note;
                                    tsouvItem.Is_Delete = item.Is_Delete;

                                    tsouvItem.Create_By = entity.Create_By;
                                    tsouvItem.Create_Date = DateTime.Now;

                                    db.T_Souvenir_Item.Add(tsouvItem);
                                }
                                else
                                {
                                    T_Souvenir_Item tsouvItem = db.T_Souvenir_Item.Where(ti => ti.Id == item.Id).FirstOrDefault();
                                    if (tsouvItem != null)
                                    {
                                        tsouvItem.T_Souvenir_Id = entity.Id;
                                        tsouvItem.M_Souvenir_Id = item.M_Souvenir_Id;
                                        tsouvItem.Qty = item.Qty;
                                        tsouvItem.Note = item.Note;
                                        tsouvItem.Is_Delete = item.Is_Delete;

                                        tsouvItem.Update_By = entity.Update_By;
                                        tsouvItem.Update_Date = DateTime.Now;

                                        db.T_Souvenir_Item.Add(tsouvItem);
                                    }
                                }
                            }
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

        public static SouvenirStockViewModel GetById(int id)
        {
            SouvenirStockViewModel result = new SouvenirStockViewModel();
            using (var db = new MarComContext())
            {
                result = (from ts in db.T_Souvenir
                          join em in db.M_Employee
                          on ts.Received_By equals em.Id
                          where ts.Id == id
                          select new SouvenirStockViewModel
                          {
                              Id = ts.Id,
                              Code = ts.Code,
                              Received_By = em.Id,
                              R_Name = em.First_Name + " " + em.Last_Name,
                              Received_Date = ts.Received_Date,
                              Note = ts.Note
                          }).FirstOrDefault();
            }
            return result;
        }

        public static List<SouvenirItemViewModel> GetItem(int id)
        {
            List<SouvenirItemViewModel> result = new List<SouvenirItemViewModel>();
            using (var db = new MarComContext())
            {
                result = (from i in db.T_Souvenir_Item
                          join s in db.M_Souvenir on
                          i.M_Souvenir_Id equals s.Id
                          where i.T_Souvenir_Id == id
                          select new SouvenirItemViewModel
                          {
                              Id = i.Id,
                              M_Souvenir_Id = s.Id,
                              Qty = i.Qty,
                              Note = i.Note
                          }).ToList();
            }
            return result;
        }

        public static string GetNewCode()
        {
            string date = DateTime.Now.ToString("dd") + DateTime.Now.Month.ToString("D2") + DateTime.Now.ToString("yy");
            string newRef = "TRSV" + date + "-";
            using (var db = new MarComContext())
            {
                var result = (from ts in db.T_Souvenir
                              where ts.Code.Contains(newRef)
                              select new { reference = ts.Code })
                              .OrderByDescending(ts => ts.reference).FirstOrDefault();
                if (result != null)
                {
                    string[] oldRef = result.reference.Split('-');
                    newRef += (int.Parse(oldRef[1]) + 1).ToString("D5");
                }
                else
                {
                    newRef += "00001";
                }
            }
            return newRef;
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
    }
}
