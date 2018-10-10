using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class PromotionRepo
    {
        public static List<PromotionViewModel> Get()
        {
            List<PromotionViewModel> result = new List<PromotionViewModel>();
            using (var db=new MarComContext())
            {
                result = (from pr in db.T_Promotion
                          join ev in db.T_Event on pr.T_Event_Id equals ev.Id
                          join de in db.T_Design on pr.T_Design_Id equals de.Id
                          select new PromotionViewModel
                          {
                              Id=pr.Id,
                              Code=pr.Code,
                              Request_By=pr.Request_By,
                              Request_Date=pr.Request_Date,
                              Assign_To=pr.Assign_To,
                              Status=pr.Status,

                              Title=pr.Title,
                              T_Event_Id=pr.T_Event_Id,
                              EventCode=ev.Code,
                              T_Design_Id=pr.T_Design_Id,
                              DesignCode=de.Code,

                              Create_By=pr.Create_By,
                              Create_Date =pr.Create_Date
                          }).ToList();
            }
            return result;
        }

        public static ResultResponse Update(PromotionViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        T_Promotion promotion = new T_Promotion();
                        promotion.Code = entity.Code;
                        promotion.T_Event_Id = entity.T_Event_Id;
                        promotion.T_Design_Id = entity.T_Design_Id;
                        promotion.Title = entity.Title;
                        promotion.Note = entity.Note;
                        promotion.Request_By = entity.Request_By;
                        promotion.Request_Date = DateTime.Now;

                        db.T_Promotion.Add(promotion);
                        db.SaveChanges();
                    }
                    else
                    {
                        T_Promotion promotion = db.T_Promotion.Where(pr => pr.Id == entity.Id).FirstOrDefault();
                        if (promotion != null) //tambahin kondisi &&
                        {
                            promotion.Code = entity.Code;
                            promotion.T_Event_Id = entity.T_Event_Id;
                            promotion.T_Design_Id = entity.T_Design_Id;
                            promotion.Title = entity.Title;
                            promotion.Note = entity.Note;

                            promotion.Request_By = entity.Request_By;
                            promotion.Request_Date = DateTime.Now;

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

        public static string GetTransactionCode()
        {
            string date = DateTime.Now.Day.ToString("D2");
            string month = DateTime.Now.Month.ToString("D2");
            string year = DateTime.Now.ToString("yy");
            string newRef = "TRWOMP" + date + month + year+"0";

            using (var db = new MarComContext())
            {
                var result = (from pr in db.T_Promotion
                              where pr.Code.Contains(newRef)
                              select new { code = pr.Code })
                              .OrderByDescending(o => o.code)
                              .FirstOrDefault();
                if (result != null)
                {
                    string[] oldRef = result.code.Split('0');
                    newRef += (int.Parse(oldRef[1]) + 1).ToString("D2");
                }
                else
                {
                    newRef += 0001;
                }
                return newRef;
            }
        }

        public static ResultResponse AddMenu1(PromotionViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                if (entity.Id == 0)
                {
                    T_Promotion promo = new T_Promotion();
                    promo.T_Event_Id = entity.T_Event_Id;
                    promo.T_Design_Id = entity.T_Design_Id;
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
