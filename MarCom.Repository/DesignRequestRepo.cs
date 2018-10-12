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
        public static List<DesignRequestViewModel> Get()
        {
            List<DesignRequestViewModel> result = new List<DesignRequestViewModel>();
            try
            {
                using (var db = new MarComContext())
                {
                    result = (from dr in db.T_Design
                              join e in db.T_Event on
                              dr.T_Event_Id equals e.Id
                              select new DesignRequestViewModel
                              {
                                  Id = dr.Id,
                                  Code = dr.Code,
                                  T_Event_Id = dr.T_Event_Id,
                                  EventCode = e.Code,
                                  Request_By = dr.Request_By,
                                  Request_Date = dr.Request_Date,
                                  Assign_To = dr.Assign_To,
                                  Status = dr.Status,

                                  Is_Delete = dr.Is_Delete,

                                  Create_Date = dr.Create_Date,
                                  Create_By = "Administrator"
                              }).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }

        public static ResultResponse Update(DesignRequestViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        T_Design design = new T_Design();
                        design.Code = entity.Code;
                        design.Title_Header = entity.Title_Header;
                        design.T_Event_Id = entity.T_Event_Id;
                        design.Request_By = entity.Request_By;
                        design.Request_Date = DateTime.Now;
                        design.Note = entity.Note;

                        design.Create_Date = DateTime.Now;
                        design.Create_By = "Administrator";

                        db.T_Design.Add(design);
                        db.SaveChanges();

                    }
                    else
                    {
                        T_Design design = db.T_Design.Where(dr => dr.Id == entity.Id).FirstOrDefault();
                        if (design != null)
                        {
                            design.Code = entity.Code;
                            design.Title_Header = entity.Title_Header;
                            design.T_Event_Id = entity.T_Event_Id;
                            design.Request_By = entity.Request_By;
                            design.Request_Date = DateTime.Now;
                            design.Note = entity.Note;

                            design.Update_Date = DateTime.Now;
                            design.Update_By = "Administrator";

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
        public static string GetNewCode()
        {
            string yearMonth = DateTime.Now.ToString("dd") +
                DateTime.Now.Month.ToString("D2") +
                DateTime.Now.ToString("yy");
            string newRef = "TRWODS" + yearMonth + "0";
            using (var db = new MarComContext())
            {
                var result = (from dr in db.T_Design
                              where dr.Code.Contains(newRef)
                              select new { code = dr.Code })
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
        public static List<DesignRequestViewModel> Get()
        {
            List<DesignRequestViewModel> result = new List<DesignRequestViewModel>();
            using (var db = new MarComContext())
            {
                result = (from dr in db.T_Design
                          join e in db.T_Event on
                          dr.T_Event_Id equals e.Id
                          select new DesignRequestViewModel
                          {
                              Id = dr.Id,
                              Code = dr.Code,
                              T_Event_Id = dr.T_Event_Id,
                              EventCode = e.Code,
                              Request_By = dr.Request_By,
                              Request_Date = dr.Request_Date,
                              Assign_To = dr.Assign_To,
                              Status = dr.Status,

                              Is_Delete = dr.Is_Delete,

                              Create_Date = dr.Create_Date,
                              Create_By = dr.Create_By
                          })//.Where(p => p.Is_Delete == all ? p.Is_Delete : true)
                            .ToList();
            }
            return result;
        }

        public static ResultResponse Update(DesignRequestViewModel entity, List<DesignItemViewModel> entityitem)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        T_Design design = new T_Design();
                        design.Code = GetNewCode();
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
                            designItem.Request_Pic = 1;
                            designItem.Start_Date = item.Start_Date;
                            designItem.End_Date = item.End_Date;
                            designItem.Note = item.Note;
                            designItem.Is_Delete = item.Is_Delete;

                            designItem.Create_By = entity.Create_By;
                            designItem.Create_Date = DateTime.Now;

                            db.T_Design_Item.Add(designItem);

                        }
                        db.SaveChanges();

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
        public static string GetNewCode()
        {
            string yearMonth = DateTime.Now.ToString("dd") +
                DateTime.Now.Month.ToString("D2") +
                DateTime.Now.ToString("yy");
            string newRef = "TRWODS" + yearMonth + "0";
            using (var db = new MarComContext())
            {
                var result = (from dr in db.T_Design
                              where dr.Code.Contains(newRef)
                              select new { code = dr.Code })
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
                              Status =d.Status,
                              Request_By = d.Request_By,
                              NameRequest = em.First_Name + " " + em.Last_Name,
                              Request_Date = d.Request_Date,
                              Note = d.Note

                          }).FirstOrDefault();
                          
            }
            return result;
        }

        //public static List<UserViewModel> GetRole()
        //{
        //    List<UserViewModel> result = new List<UserViewModel>();
        //    using (var db = new MarComContext())
        //    {
        //        result = (from u in db.M_User
        //                  where name == "Requester"
        //                  select new UserViewModel
        //                  {
        //                      Id = di.Id,
        //                      T_Event_Id = dr.T_Event_Id,
        //                      EventCode = e.Code,
        //                      Request_By = dr.Request_By,
        //                      Request_Date = dr.Request_Date,
        //                      Assign_To = dr.Assign_To,
        //                      Status = dr.Status,

        //                      Is_Delete = dr.Is_Delete,

        //                      Create_Date = dr.Create_Date,
        //                      Create_By = "Administrator"
        //                  })//.Where(p => p.Is_Delete == all ? p.Is_Delete : true)
        //                    .ToList();
        //    }
        //    return result;
        //}
    }
}
