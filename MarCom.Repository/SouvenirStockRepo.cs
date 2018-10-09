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
                          select new SouvenirStockViewModel
                          {
                              Id = ts.Id,
                              Code = ts.Code,
                              R_Name = em.First_Name + " " + em.Last_Name,
                              Received_Date = ts.Received_Date,

                              Create_By = ts.Create_By,
                              Create_Date = DateTime.Now
                          }).ToList();
            }
            return result;
        }

        public static ResultResponse Update(SouvenirStockViewModel entity)
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
                        tsouv.Received_By = entity.Received_By;
                        tsouv.Received_Date = entity.Received_Date;
                        tsouv.Note = entity.Note;

                        tsouv.Create_By = entity.Create_By;
                        tsouv.Create_Date = DateTime.Now;

                        db.T_Souvenir.Add(tsouv);
                        db.SaveChanges();
                    }
                    else
                    {
                        T_Souvenir tsouv = db.T_Souvenir.Where(ts => ts.Id == entity.Id).FirstOrDefault();
                        if (tsouv != null)
                        {
                            tsouv.Code = entity.Code;
                            tsouv.Received_By = entity.Received_By;
                            tsouv.Received_Date = entity.Received_Date;
                            tsouv.Note = entity.Note;

                            tsouv.Update_By = entity.Update_By;
                            tsouv.Update_Date = DateTime.Now;
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
                              R_Name = em.First_Name+ " " + em.Last_Name,
                              Received_Date = ts.Received_Date,
                              Note = ts.Note
                          }).FirstOrDefault();
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
                              select new { reference = ts.Code})
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
    }
}
