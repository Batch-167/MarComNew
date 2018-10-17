using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class RoleRepo
    {
        public static List<RoleViewModel> Get()
        {
            return Get(true);
        }

        public static List<RoleViewModel> Get(bool all)
        {
            List<RoleViewModel> result = new List<RoleViewModel>();
            using (var db = new MarComContext())
            {
                result = (from r in db.M_Role
                          select new RoleViewModel
                          {
                              Id = r.Id,
                              Code = r.Code,
                              Name = r.Name,
                              Description = r.Description,
                              Is_Delete = r.Is_Delete,

                              Create_Date = DateTime.Now,
                              Create_By = r.Create_By,

                          }).Where(r => r.Is_Delete == all ? r.Is_Delete : true)
                            .ToList();
            }
            return result;
        }


        public static ResultResponse Update(RoleViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        M_Role role = new M_Role();
                        bool nameExists = db.M_Role.Any(nm => nm.Name.Equals(entity.Name));

                        if (nameExists)
                        {
                            result.Message = "Role with name " + entity.Name + "already exist!";
                        }
                        else
                        {
                            role.Code = entity.Code;
                            role.Name = entity.Name;
                            role.Description = entity.Description;

                            role.Create_Date = DateTime.Now;
                            role.Create_By = entity.Create_By;

                            db.M_Role.Add(role);
                            db.SaveChanges();
                            result.Message = "Data Saved ! New Role has been add with code " + entity.Code + "!";
                        }

                    }
                    else
                    {
                        M_Role role = db.M_Role.Where(r => r.Id == entity.Id).FirstOrDefault();
                        if (role != null)
                        {
                            bool nameExists = db.M_Role.Any(nm => nm.Name.Equals(entity.Name) && nm.Code != entity.Code);

                            if (nameExists)
                            {
                                result.Message = "Role with name " + entity.Name + "already exist!";
                            }
                            else
                            {
                                role.Code = entity.Code;
                                role.Name = entity.Name;
                                role.Description = entity.Description;

                                role.Update_Date = DateTime.Now;
                                role.Update_By = entity.Update_By;

                                db.SaveChanges();
                                result.Message = "Data Updated ! Data Role has been update!";
                            }
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

        //EDIT
        public static RoleViewModel GetById(int id)
        {
            RoleViewModel result = new RoleViewModel();
            using (var db = new MarComContext())
            {
                result = (from r in db.M_Role
                          where r.Id == id
                          select new RoleViewModel
                          {
                              Id = r.Id,
                              Code = r.Code,
                              Name = r.Name,
                              Description = r.Description,
                              Is_Delete = r.Is_Delete,
                              Create_Date = r.Create_Date,
                              Create_By = r.Create_By,
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
                    M_Role role = db.M_Role.Where(p => p.Id == id).FirstOrDefault();
                    if (role != null)
                    {
                        role.Is_Delete = true;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public static string GetNewCode()
        {
            string newRef = "RO";
            using (var db = new MarComContext())
            {
                var result = (from r in db.M_Role
                              where r.Code.Contains(newRef)
                              select new { code = r.Code })
                              .OrderByDescending(o => o.code)
                              .FirstOrDefault();
                if (result != null)
                {
                    string[] oldRef = result.code.Split('O');
                    newRef += (int.Parse(oldRef[1]) + 1).ToString("D4");
                }
                else
                {
                    newRef += "0001";
                }
            }
            return newRef;
        }

        public static List<RoleViewModel> Filter(RoleViewModel entity)
        {
            List<RoleViewModel> result = new List<RoleViewModel>();
            using (var db = new MarComContext())
            {
                result = (from r in db.M_Role
                          where r.Code == entity.Code || r.Name == entity.Name || r.Create_Date == entity.Create_Date || r.Create_By.Contains(entity.Create_By)
                          select new RoleViewModel
                          {
                              Code = r.Code,
                              Name = r.Name,
                              Create_Date = r.Create_Date,
                              Create_By = r.Create_By
                          }).ToList();
            }
            return result;
        }
    }
}
