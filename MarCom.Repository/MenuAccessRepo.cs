using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class MenuAccessRepo
    {
        public static List<MenuAccessViewModel> Get()
        {
            List<MenuAccessViewModel> result = new List<MenuAccessViewModel>();
            using (var db = new MarComContext())
            {
                result = (from ma in db.M_Menu_Access
                          join r in db.M_Role on ma.M_Role_Id equals r.Id
                          join m in db.M_Menu on ma.M_Menu_Id equals m.Id
                          select new MenuAccessViewModel
                          {
                              Id = ma.Id,
                              M_Role_Id = ma.M_Role_Id,
                              RoleName = r.Name,
                              RoleCode = r.Code,
                              M_Menu_Id = ma.M_Menu_Id,
                              MenuName = m.Name,

                              Is_Delete = ma.Is_Delete,

                              Create_By = "Princess",
                              Create_Date = DateTime.Now
                          }).ToList();
            }
            return result;
        }

        public static ResultResponse Update(MenuAccessViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        foreach (var item in entity.Menu)
                        {
                            M_Menu_Access menuaccess = new M_Menu_Access();
                            menuaccess.M_Role_Id = entity.M_Role_Id;
                            menuaccess.M_Menu_Id = item.M_Menu_Id;
                            menuaccess.Is_Delete = entity.Is_Delete;

                            menuaccess.Create_By = "Princess";
                            menuaccess.Create_Date = DateTime.Now;

                            db.M_Menu_Access.Add(menuaccess);
                        }
                        db.SaveChanges();
                    }
                    else
                    {
                        M_Menu_Access menuaccess = db.M_Menu_Access.Where(c => c.Id == entity.Id).FirstOrDefault();
                        if (menuaccess != null)
                        {
                            menuaccess.M_Role_Id = entity.M_Role_Id;
                            menuaccess.M_Menu_Id = entity.M_Menu_Id;

                            menuaccess.Update_By = "Princess";
                            menuaccess.Update_Date = DateTime.Now;

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

        //public static List<MenuViewModel> GetMenu1()
        //{
        //    List<MenuViewModel> result = new List<MenuViewModel>();
        //    using (var db = new MarComContext())
        //    {
        //        result = (from m in db.M_Menu
        //                  where m.Parent_Id != null
        //                  select new MenuViewModel
        //                  {
        //                      Id = m.Id,
        //                      Code = m.Code,
        //                      Name = m.Name,
        //                      Controller = m.Controller,
        //                      Parent_Id = m.Parent_Id,
        //                      Is_Delete = m.Is_Delete,

        //                      Create_By = "Admin",
        //                      Create_Date = m.Create_Date,
        //                  })
        //                  .ToList();
        //    }
        //    return result;
        //}

        public static MenuAccessViewModel GetById(int id)
        {
            MenuAccessViewModel result = new MenuAccessViewModel();
            using (var db = new MarComContext())
            {
                result = (from ma in db.M_Menu_Access
                          join r in db.M_Role on ma.M_Role_Id equals r.Id
                          join m in db.M_Menu on ma.M_Menu_Id equals m.Id
                          where ma.Id == id
                          select new MenuAccessViewModel
                          {
                              Id = ma.Id,
                              M_Menu_Id = ma.M_Menu_Id,
                              M_Role_Id = ma.M_Role_Id,
                              RoleCode = r.Code,
                              MenuName = m.Name,
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
                    M_Menu_Access menuaccess = db.M_Menu_Access.Where(e => e.Id == id).FirstOrDefault();
                    if (menuaccess != null)
                    {
                        menuaccess.Is_Delete = true;
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
    }
}
