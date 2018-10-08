using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class MenuRepo
    {
        public static List<MenuViewModel> Get()
        {
            return Get(true);
        }

        public static List<MenuViewModel> GetMenu()
        {
            List<MenuViewModel> result = new List<MenuViewModel>();
            using (var db = new MarComContext())
            {
                result = (from m in db.M_Menu
                          where m.Parent_Id == null
                          select new MenuViewModel
                          {
                              Id = m.Id,
                              Code = m.Code,
                              Name = m.Name,
                              Controller = m.Controller,
                              Is_Delete = m.Is_Delete,

                              Create_By = m.Create_By,
                              Create_Date = m.Create_Date,
                          }).ToList();
            }
            return result;
        }

        public static List<MenuViewModel> Get(bool all)
        {
            List<MenuViewModel> result = new List<MenuViewModel>();
            using (var db = new MarComContext())
            {
                result = (from m in db.M_Menu
                          where m.Parent_Id != null                                              
                          select new MenuViewModel
                          {
                              Id = m.Id,
                              Code = m.Code,
                              Name = m.Name,
                              Controller = m.Controller,
                              Parent_Id = m.Parent_Id,
                              Is_Delete = m.Is_Delete,
                              
                              Create_By = m.Create_By,
                              Create_Date = m.Create_Date,
                          })
                          .Where(m => m.Is_Delete == all ? m.Is_Delete : true)
                          .ToList();
            }
            return result;
        }

        public static ResultResponse Update(MenuViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        M_Menu menu = new M_Menu();
                        menu.Code = entity.Code;
                        menu.Name = entity.Name;
                        menu.Controller = entity.Controller;
                        menu.Parent_Id = entity.Parent_Id;
                        menu.Is_Delete = entity.Is_Delete;

                        menu.Create_By = entity.Create_By;
                        menu.Create_Date = DateTime.Now;

                        db.M_Menu.Add(menu);
                        db.SaveChanges();
                    }
                    else
                    {
                        M_Menu menu = db.M_Menu.Where(m => m.Id == entity.Id).FirstOrDefault();
                        if (menu != null)
                        {
                            menu.Code = entity.Code;
                            menu.Name = entity.Name;
                            menu.Controller = entity.Controller;
                            menu.Parent_Id = entity.Parent_Id;

                            menu.Update_By = entity.Update_By;
                            menu.Update_Date = DateTime.Now;

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

        public static MenuViewModel GetById(int id)
        {
            MenuViewModel result = new MenuViewModel();
            using (var db = new MarComContext())
            {
                result = (from m in db.M_Menu
                          where m.Id == id
                          select new MenuViewModel
                          {
                              Id = m.Id,
                              Code = m.Code,
                              Name = m.Name,
                              Controller = m.Controller,
                              Parent_Id = m.Parent_Id,
                              Is_Delete = m.Is_Delete,

                              Create_By = m.Create_By,
                              Create_Date = m.Create_Date,
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
                    M_Menu menu = db.M_Menu.Where(m => m.Id == id).FirstOrDefault();
                    if (menu != null)
                    {
                        menu.Is_Delete = true;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return result;
        }

        public static string GetNewCode()
        {
            string newRef = "ME";
            using (var db = new MarComContext())
            {
                var result = (from m in db.M_Menu
                              where m.Code.Contains(newRef)
                              select new { code = m.Code })
                             .OrderByDescending(o => o.code)
                             .FirstOrDefault();
                if (result != null)
                {
                    string[] oldRef = result.code.Split('E');
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
