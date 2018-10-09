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
                          select new PromotionViewModel
                          {
                              Id=pr.Id,
                              Code=pr.Code,
                              Request_By=pr.Request_By,
                              Request_Date=pr.Request_Date,
                              Assign_To=pr.Assign_To,
                              Status=pr.Status,
                              Create_By=pr.Create_By,
                              Create_Date=pr.Create_Date
                          }).ToList();
            }
            return result;
        }

        public static List<PromotionViewModel> GetCreate()
        {
            List<PromotionViewModel> result = new List<PromotionViewModel>();
            using (var db = new MarComContext())
            {
                result = (from pr in db.T_Promotion
                              //join ev in db_
                          select new PromotionViewModel
                          {

                          }).ToList();

            }
            return result;
        }
        //public static ResultResponse Update(PromotionViewModel entity)
        //{
        //    ResultResponse result = new ResultResponse();
        //    try
        //    {
        //        using (var db=new MarComContext())
        //        {
        //            if (entity.Id==0)
        //            {
        //                using (var db = new T_Event())
        //                {

        //                }
        //                T_Promotion promotion = new T_Promotion();
                        
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return result;
        //}

        public static string GetTransactionCode()
        {
            string date = DateTime.Now.Day.ToString("D2");
            string month = DateTime.Now.Month.ToString("D2");
            string year = DateTime.Now.ToString("yy");
            string newRef = "TRWOMP" + date + month + year+" ";

            using (var db = new MarComContext())
            {
                var result = (from pr in db.T_Promotion
                              where pr.Code.Contains(newRef)
                              select new { code = pr.Code })
                              .OrderByDescending(o => o.code)
                              .FirstOrDefault();
                if (result != null)
                {
                    string[] oldRef = result.code.Split(' ');
                    newRef += (int.Parse(oldRef[1]) + 1).ToString("D2");
                }
                else
                {
                    newRef += 0001;
                }
                return newRef;
            }
        }
    }
}
