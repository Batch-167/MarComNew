using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class CompanyRepo
    {
        public static List<CompanyViewModel> Get()
        {
            List<CompanyViewModel> result = new List<CompanyViewModel>();
            using (var db = new MarComContext())
            {
                result = (from c in db.M_Company
                          where c.Is_Delete == false
                          select new CompanyViewModel
                          {
                              Id = c.Id,
                              Code = c.Code,
                              Name = c.Name,
                              Create_Date = c.Create_Date,
                              Create_By = c.Create_By
                          }).ToList();
            }
            return result;
        }

        public static CompanyViewModel GetById(int id)
        {
            CompanyViewModel result = new CompanyViewModel();
            using (var db = new MarComContext())
            {
                result = (from c in db.M_Company
                          where c.Id == id
                          select new CompanyViewModel
                          {
                              Id = c.Id,
                              Code = c.Code,
                              Name = c.Name,
                              Address = c.Address,
                              Email = c.Email,
                              Phone = c.Phone,
                              Is_Delete = c.Is_Delete
                          }).FirstOrDefault();
            }
            return result;
        }


        public static ResultResponse Update(CompanyViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {

                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        M_Company comp = new M_Company();
                        comp.Code = entity.Code;
                        comp.Name = entity.Name;
                        comp.Address = entity.Address;
                        comp.Email = entity.Email;
                        comp.Phone = entity.Phone;
                        comp.Is_Delete = entity.Is_Delete;

                        comp.Create_By = entity.Create_By;
                        comp.Create_Date = DateTime.Now;


                        db.M_Company.Add(comp);
                        db.SaveChanges();

                    }
                    else
                    {
                        M_Company comp = db.M_Company.Where(c => c.Id == entity.Id).FirstOrDefault();
                        if (comp != null)
                        {
                            comp.Code = entity.Code;
                            comp.Name = entity.Name;
                            comp.Address = entity.Address;
                            comp.Email = entity.Email;
                            comp.Phone = entity.Phone;

                            comp.Update_By = entity.Update_By;
                            comp.Update_Date = DateTime.Now;

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


        public static bool Delete(int id)
        {
            bool result = true;
            try
            {
                using (var db = new MarComContext())
                {
                    M_Company comp = db.M_Company.Where(c => c.Id == id).FirstOrDefault();
                    if (comp != null)
                    {
                        comp.Is_Delete = true;
                        db.SaveChanges();
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public static string GetCode()
        {
            string newRef = "CP";
            using (var db = new MarComContext())
            {
                var result = (from c in db.M_Company
                              where c.Code.Contains(newRef)
                              select new { code = c.Code })
                              .OrderByDescending(c => c.code)
                              .FirstOrDefault();
                if (result != null)
                {
                    string[] oldRef = result.code.Split('P');
                    newRef += (int.Parse(oldRef[1]) + 1).ToString("D4");
                }
                else
                {
                    newRef += "0001";
                }
            }
            return newRef;
        }

        public static List<CompanyViewModel> Filter(CompanyViewModel entity)
        {
            List<CompanyViewModel> result = new List<CompanyViewModel>();
            using (var db = new MarComContext())
            {
                result = (from c in db.M_Company
                          where c.Code == entity.Code || c.Name == entity.Name || c.Create_By.Contains(entity.Create_By) || c.Create_Date == entity.Create_Date
                          select new CompanyViewModel
                          {
                              Code = c.Code,
                              Name = c.Name,
                              Create_By =c.Create_By,
                              Create_Date=c.Create_Date
                            
                          }
                          ).ToList();
            }
            return result;
                  
        }

    }
}
