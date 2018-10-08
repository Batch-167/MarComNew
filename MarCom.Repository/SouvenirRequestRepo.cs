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
                              Status = sr.Status,

                              Is_Delete = sr.Is_Delete,

                              Create_Date = DateTime.Now,
                              Create_By = "Administrator"
                          })//.Where(p => p.Is_Delete == all ? p.Is_Delete : true)
                            .ToList();
            }
            return result;
        }

        public static string GetNewCode()
        {
            string yearMonth = DateTime.Now.ToString("yy") +
                DateTime.Now.Month.ToString("D2") +
                DateTime.Now.Day.ToString("D2");
            string newRef = "TRSV" + yearMonth + "0";
            using (var db = new MarComContext())
            {
                var result = (from sr in db.T_Souvenir
                              where sr.Code.Contains(newRef)
                              select new { code = sr.Code })
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
