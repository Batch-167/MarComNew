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
            List<RoleViewModel> entities = new List<RoleViewModel>();
            using (var db = new MarComContext())
            {
                entities = (from r in db.M_Role
                            select new RoleViewModel
                            {
                                Id = r.Id,
                                Code = r.Code,
                                Name = r.Name,
                                Description = r.Description,
                                Is_Delete = r.Is_Delete,
                                Create_Date = r.Create_Date,
                                Create_By = r.Create_By,

                            }).ToList();
            }
            return entities;
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
                        role.Code = entity.Code;
                        role.Name = entity.Name;
                        role.Description = entity.Description;

                        role.Create_Date = DateTime.Now;
                        role.Create_By = "Administrator";

                        db.M_Role.Add(role);
                        db.SaveChanges();

                    }
                    else
                    {
                        M_Role role = db.M_Role.Where(r => r.Id == entity.Id).FirstOrDefault();
                        if (role != null)
                        {
                            role.Code = entity.Code;
                            role.Name = entity.Name;
                            role.Description = entity.Description;

                            role.Update_Date = DateTime.Now;
                            role.Update_By = "Administrator";

                            db.SaveChanges();

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
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
    }
}
