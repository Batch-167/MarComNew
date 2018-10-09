using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class UnitRepo
    {
        public static List<UnitViewModel> Get()
        {
            List<UnitViewModel> result = new List<UnitViewModel>();
            using (var db = new MarComContext())
            {
                result = (from u in db.M_Unit
                          //where u.Is_Delete == false
                          select new UnitViewModel
                          {
                              Id = u.Id,
                              Code = u.Code,
                              Name = u.Name,

                              Create_By = u.Create_By,
                              Create_Date = DateTime.Now
                          }).ToList();
            }
            return result;
        }

        public static ResultResponse Update(UnitViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        M_Unit unit = new M_Unit();
                        unit.Code = entity.Code;
                        unit.Name = entity.Name;
                        unit.Description = entity.Description;
                        unit.Is_Delete = entity.Is_Delete;
                        unit.Create_By = entity.Create_By;
                        
                        unit.Create_Date = DateTime.Now;

                        db.M_Unit.Add(unit);
                        db.SaveChanges();

                    }
                    else
                    {
                        M_Unit unit = db.M_Unit.Where(u => u.Id == entity.Id).FirstOrDefault();
                        if (unit != null)
                        {
                            unit.Code = entity.Code;
                            unit.Name = entity.Name;
                            unit.Description = entity.Description;

                            unit.Update_By = entity.Update_By;
                            unit.Update_Date = DateTime.Now;
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

        public static UnitViewModel GetById(int id)
        {
            UnitViewModel result = new UnitViewModel();
            using (var db = new MarComContext())
            {
                result = (from u in db.M_Unit
                          where u.Id == id
                          select new UnitViewModel
                          {
                              Id = u.Id,
                              Code = u.Code,
                              Name = u.Name,
                              Description = u.Description,
                              Is_Delete = u.Is_Delete
                          }).FirstOrDefault();
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
                    M_Unit unit = db.M_Unit.Where(u => u.Id == id).FirstOrDefault();
                    if (unit != null)
                    {
                        unit.Is_Delete = true;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        
        public static string GetNewCode()
        {
            string newRef = "UN";
            using (var db =  new MarComContext())
            {
                var result = (from u in db.M_Unit
                              where u.Code.Contains(newRef)
                              select new { code = u.Code }
                              ).OrderByDescending(u => u.code)
                              .FirstOrDefault();
                if (result != null)
                {
                    string[] oldRef = result.code.Split('N');
                    newRef += (int.Parse(oldRef[1]) + 1).ToString("D4");
                }
                else
                {
                    newRef += "0001";
                }
            }
            return newRef;
        }

        public static List<UnitViewModel> Filter(UnitViewModel entity)
        {
            List<UnitViewModel> result = new List<UnitViewModel>();
            using (var db = new MarComContext())
            {
                result = (from u in db.M_Unit
                          where u.Code == entity.Code || u.Name == entity.Name ||(u.Create_Date.ToString()).Contains(entity.Create_Date.ToString()) || u.Create_By.Contains(entity.Create_By) 
                          select new UnitViewModel
                          {
                              Code = u.Code,
                              Name = u.Name,
                              Create_By = u.Create_By,
                              Create_Date = u.Create_Date
                          }).ToList();
            }
            return result;
        }
    }
}
