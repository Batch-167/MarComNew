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
                          select new MenuAccessViewModel
                          {
                              Id = ma.Id,
                              M_Role_Id = ma.M_Role_Id,
                              RoleName = r.Name,
                              Is_Delete = ma.Is_Delete,

                              Create_By = ma.Create_By,
                              Create_Date = ma.Create_Date


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
                        M_Menu_Access menuaccess = new M_Menu_Access();
                        menuaccess.M_Role_Id = entity.M_Role_Id;
                        menuaccess.M_Menu_Id = entity.M_Menu_Id;
                        menuaccess.Is_Delete = entity.Is_Delete;
                        menuaccess.Create_By = entity.Create_By;
                        menuaccess.Create_Date = DateTime.Now;

                        db.M_Menu_Access.Add(menuaccess);
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
    }
}
