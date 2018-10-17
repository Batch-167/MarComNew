using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class DesignRequestRepo
    {

        //LIST
        public static List<DesignRequestViewModel> Get()
        {
            List<DesignRequestViewModel> result = new List<DesignRequestViewModel>();
            using (var db = new MarComContext())
            {
                result = (from dr in db.T_Design
                          join e in db.T_Event on
                          dr.T_Event_Id equals e.Id
                          join em in db.M_Employee on
                          dr.Request_By equals em.Id
                          select new DesignRequestViewModel
                          {
                              Id = dr.Id,
                              Code = dr.Code,
                              T_Event_Id = dr.T_Event_Id,
                              EventCode = e.Code,
                              Request_By = dr.Request_By,
                              NameRequest = em.First_Name + " " + em.Last_Name,
                              Request_Date = dr.Request_Date,
                              Assign_To = dr.Assign_To,
                              Status = dr.Status,

                              Is_Delete = dr.Is_Delete,

                              Create_Date = dr.Create_Date,
                              Create_By = dr.Create_By
                          }).ToList();
            }
            return result;
        }


        //CREATE and EDIT
        public static ResultResponse Update(DesignRequestViewModel entity, List<DesignItemViewModel> entityitem)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        string newCode = GetNewCode();
                        T_Design design = new T_Design();
                        design.Code = newCode;
                        design.Title_Header = entity.Title_Header;
                        design.T_Event_Id = entity.T_Event_Id;
                        design.Request_By = entity.Request_By;
                        design.Request_Date = DateTime.Now;
                        design.Note = entity.Note;
                        design.Status = 1;

                        design.Create_Date = DateTime.Now;
                        design.Create_By = entity.Create_By;

                        db.T_Design.Add(design);

                        foreach (var item in entityitem)
                        {
                            T_Design_Item designItem = new T_Design_Item();
                            designItem.T_Design_Id = entity.Id;
                            designItem.M_Product_Id = item.M_Product_Id;
                            designItem.Title_Item = item.Title_Item;
                            designItem.Request_Pic = item.Request_Pic;
                            designItem.Request_Due_Date = item.Request_Due_Date;
                            designItem.Note = item.Note;
                            designItem.Is_Delete = item.Is_Delete;

                            designItem.Create_By = entity.Create_By;
                            designItem.Create_Date = DateTime.Now;

                            db.T_Design_Item.Add(designItem);

                        }
                        db.SaveChanges();
                        result.Message = "Data Saved ! Transaction Design Request has been add with code " + entity.Code;
                    }
                    else
                    {
                        T_Design design = db.T_Design.Where(td => td.Id == entity.Id).FirstOrDefault();
                        if (design != null)
                        {
                            design.Code = entity.Code;
                            design.T_Event_Id = entity.T_Event_Id;
                            design.Title_Header = entity.Title_Header;
                            design.Request_By = entity.Request_By;
                            design.Request_Date = DateTime.Now;
                            design.Note = entity.Note;
                            design.Status = 1;

                            design.Update_Date = DateTime.Now;
                            design.Update_By = entity.Update_By;

                            foreach (var item in entityitem)
                            {
                                T_Design_Item designItem = new T_Design_Item();
                                designItem.T_Design_Id = entity.Id;
                                designItem.M_Product_Id = item.M_Product_Id;
                                designItem.Title_Item = item.Title_Item;
                                designItem.Request_Pic = item.Request_Pic;
                                designItem.Request_Due_Date = item.Request_Due_Date;
                                designItem.Note = item.Note;
                                designItem.Is_Delete = item.Is_Delete;

                                if (item.Id == 0)
                                {
                                    designItem.Create_By = entity.Update_By;
                                    designItem.Create_Date = DateTime.Now;
                                }
                                else
                                {
                                    designItem.Update_By = entity.Update_By;
                                    designItem.Update_Date = DateTime.Now;
                                }
                                db.T_Design_Item.Add(designItem);
                            }
                            db.SaveChanges();
                        }
                        result.Message = "Data Update ! Transaction Design Request with code " + entity.Code + " has been update";
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

        //CODE
        public static string GetNewCode()
        {
            string yearMonth = DateTime.Now.ToString("dd") +
                DateTime.Now.Month.ToString("D2") +
                DateTime.Now.ToString("yy");
            string newRef = "TRWODS" + yearMonth + "-";
            using (var db = new MarComContext())
            {
                var result = (from dr in db.T_Design
                              where dr.Code.Contains(newRef)
                              select new { code = dr.Code })
                              .OrderByDescending(o => o.code)
                              .FirstOrDefault();
                if (result != null)
                {
                    string[] oldRef = result.code.Split('-');
                    newRef += (int.Parse(oldRef[1]) + 1).ToString("D5");
                }
                else
                {
                    newRef += "00001";
                }
            }
            return newRef;
        }

        //REQUEST_BY
        public static UserViewModel GetIdByName(string name)
        {
            UserViewModel result = new UserViewModel();
            using (var db = new MarComContext())
            {
                result = (from u in db.M_User
                          join e in db.M_Employee
                          on u.M_Employee_Id equals e.Id
                          where name == u.UserName
                          select new UserViewModel
                          {
                              Id = u.Id,
                              Password = u.PasswordHash,
                              M_Employee_Id = u.M_Employee_Id,
                              Fullname = e.First_Name + " " + e.Last_Name
                          }).FirstOrDefault();
            }
            return result;
        }

        public static List<EmployeeViewModel> GetPic()
        {
            List<EmployeeViewModel> result = new List<EmployeeViewModel>();
            using (var db = new MarComContext())
            {
                result = (from e in db.M_Employee
                          join u in db.M_User
                          on e.Id equals u.M_Employee_Id
                          join r in db.M_Role
                          on u.M_Role_Id equals r.Id
                          where r.Name == "Requester"
                          select new EmployeeViewModel
                          {
                              Id = e.Id,
                              Full_Name = e.First_Name + " " + e.Last_Name
                          }).ToList();
            }
            return result;
        }

        public static DesignRequestViewModel GetById(int id)
        {
            DesignRequestViewModel result = new DesignRequestViewModel();
            using (var db = new MarComContext())
            {
                result = (from d in db.T_Design
                          join e in db.T_Event
                          on d.T_Event_Id equals e.Id
                          join em in db.M_Employee
                          on d.Request_By equals em.Id
                          where id == d.Id
                          select new DesignRequestViewModel
                          {
                              Id = d.Id,
                              Code = d.Code,
                              T_Event_Id = d.T_Event_Id,
                              EventCode = e.Code,
                              Title_Header = d.Title_Header,
                              Status = d.Status,
                              Request_By = d.Request_By,
                              NameRequest = em.First_Name + " " + em.Last_Name,
                              Request_Date = d.Request_Date,
                              Note = d.Note

                          }).FirstOrDefault();

            }
            return result;
        }

        public static List<DesignItemViewModel> GetItem(int id)
        {
            List<DesignItemViewModel> result = new List<DesignItemViewModel>();
            using (var db = new MarComContext())
            {
                result = (from di in db.T_Design_Item
                          join td in db.T_Design on di.T_Design_Id equals td.Id
                          join p in db.M_Product on di.M_Product_Id equals p.Id
                          where di.T_Design_Id == id
                          select new DesignItemViewModel
                          {
                              Id = di.Id,
                              T_Design_Id = di.T_Design_Id,
                              M_Product_Id = di.M_Product_Id,
                              ProductName = p.Name,
                              Description = p.Description,
                              Title_Item = di.Title_Item,
                              Request_Pic = di.Request_Pic,
                              Request_Due_Date = di.Request_Due_Date,
                              Start_Date = di.Start_Date,
                              End_Date = di.End_Date,
                              Note = di.Note
                          }).ToList();
            }
            return result;
        }

        public static List<DesignItemViewModel> GetCloseItem(int id)
        {
            List<DesignItemViewModel> result = new List<DesignItemViewModel>();
            using (var db = new MarComContext())
            {
                result = (from di in db.T_Design_Item
                          join td in db.T_Design on di.T_Design_Id equals td.Id
                          join p in db.M_Product on di.M_Product_Id equals p.Id
                          join df in db.T_Design_Item_File on di.Id equals df.T_Design_Item_Id
                          where di.T_Design_Id == id
                          select new DesignItemViewModel
                          {
                              Id = di.Id,
                              T_Design_Id = di.T_Design_Id,
                              M_Product_Id = di.M_Product_Id,
                              ProductName = p.Name,
                              Description = p.Description,
                              Title_Item = di.Title_Item,
                              Request_Pic = di.Request_Pic,
                              Request_Due_Date = di.Request_Due_Date,
                              Start_Date = di.Start_Date,
                              End_Date = di.End_Date,
                              Note = di.Note
                          }).ToList();
            }
            return result;
        }

        public static void DeleteItem(int id)
        {
            using (var db = new MarComContext())
            {
                foreach (var item in db.T_Design_Item)
                {
                    if (item.T_Design_Id == id)
                    {
                        db.T_Design_Item.Remove(item);
                    }
                }
                db.SaveChanges();
            }
        }

        public static List<DesignRequestViewModel> Filter(DesignRequestViewModel entity)
        {
            List<DesignRequestViewModel> result = new List<DesignRequestViewModel>();
            using (var db = new MarComContext())
            {
                result = (from d in db.T_Design
                          where d.Code.Contains(entity.Code) || d.Request_By == entity.Request_By || d.Request_Date == entity.Request_Date || d.Assign_To == entity.Assign_To || d.Status == entity.Status || d.Create_Date == entity.Create_Date || d.Create_By.Contains(entity.Create_By)
                          select new DesignRequestViewModel
                          {
                              Code = d.Code,
                              Request_By = d.Request_By,
                              Request_Date = d.Request_Date,
                              Assign_To = d.Assign_To,
                              Status = d.Status,
                              Create_Date = d.Create_Date,
                              Create_By = d.Create_By
                          }).ToList();

            }
            return result;
        }
    }
}
