using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class PromotionRepo
    {
        public static List<PromotionViewModel> Get()
        {
            List<PromotionViewModel> result = new List<PromotionViewModel>();
            using (var db = new MarComContext())
            {
                result = (from pr in db.T_Promotion
                          join e in db.M_Employee on pr.Request_By equals e.Id
                          select new PromotionViewModel
                          {
                              Id = pr.Id,
                              Code = pr.Code,
                              Request_By = pr.Request_By,
                              RequestBy = e.First_Name + " " + e.Last_Name,
                              Request_Date = pr.Request_Date,
                              Assign_To=pr.Assign_To,
                              //AssignName = e.First_Name+" "+e.Last_Name,
                              Flag_Design = pr.Flag_Design,
                              Status = pr.Status,

                              Create_By = pr.Create_By,
                              Create_Date = pr.Create_Date,
                              Is_Delete=pr.Is_Delete
                              
                          }).ToList();
            }
            return result;
        }

        public static PromotionViewModel GetById(int id)
        {
            PromotionViewModel result = new PromotionViewModel();
            using (var db = new MarComContext())
            {
                result = (from pr in db.T_Promotion
                          join ev in db.T_Event on pr.T_Event_Id equals ev.Id
                          join de in db.T_Design on pr.T_Design_Id equals de.Id
                          join e in db.M_Employee on pr.Request_By equals e.Id
                          where id == pr.Id
                          select new PromotionViewModel
                          {
                              Id = pr.Id,
                              Code = pr.Code,

                              T_Event_Id = pr.T_Event_Id,
                              EventCode = ev.Code,

                              T_Design_Id = pr.T_Design_Id,
                              DesignCode = de.Code,

                              Request_By = pr.Request_By,
                              RequestBy = e.First_Name + " " + e.Last_Name,

                              Status = pr.Status,
                              Request_Date = pr.Request_Date,
                              Note = pr.Note
                          }).FirstOrDefault();
            }
            return result;
        }

        //bikin list untuk view design request
        public static DesignRequestViewModel GetDesReq(int id)
        {
            DesignRequestViewModel result = new DesignRequestViewModel();
            using (var db = new MarComContext())
            {
                result = (from de in db.T_Design
                          join ev in db.T_Event on de.T_Event_Id equals ev.Id
                          join em in db.M_Employee on de.Request_By equals em.Id
                          where id == de.Id
                          select new DesignRequestViewModel
                          {
                              Id = de.Id,
                              T_Event_Id = de.T_Event_Id,
                              Code = de.Code,
                              Request_By = de.Request_By,
                              Title_Header = de.Title_Header,
                              NameRequest = em.First_Name + " " + em.Last_Name,
                              Request_Date = de.Request_Date,
                              Note = de.Note
                          }).FirstOrDefault();
            }
            return result;
        }

        // untuk view design request item
        public static List<PromotionItemViewModel> GetDesReqItem(int id)
        {
            List<PromotionItemViewModel> result = new List<PromotionItemViewModel>();
            using (var db = new MarComContext())
            {
                result = (from dei in db.T_Design_Item
                          join pr in db.M_Product on dei.M_Product_Id equals pr.Id
                          join des in db.T_Design on dei.T_Design_Id equals des.Id
                          where dei.T_Design_Id == id
                          select new PromotionItemViewModel
                          {
                              Id = dei.Id,
                              M_Product_Id = dei.M_Product_Id,
                              ProductName = pr.Name,
                              ProductDescription = pr.Description,
                              Title = dei.Title_Item
                          }).ToList();
            }
            return result;
        }

        public static ResultResponse Update(PromotionViewModel entity, List<PromotionItemViewModel> entityItem)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        T_Promotion promotion = new T_Promotion();
                        promotion.Code = entity.Code;
                        promotion.T_Event_Id = entity.T_Event_Id;
                        promotion.T_Design_Id = entity.T_Design_Id;
                        promotion.Is_Delete = entity.Is_Delete;
                        promotion.Flag_Design = "1";
                        promotion.Status = 1;
                        promotion.Title = entity.Title;
                        promotion.Note = entity.Note;
                        promotion.Create_By = entity.Create_By;
                        promotion.Create_Date = DateTime.Now;
                        promotion.Request_By = entity.Request_By;
                        promotion.Request_Date = DateTime.Now;

                        db.T_Promotion.Add(promotion);

                        foreach (var item in entityItem)
                        {
                            T_Promotion_Item promotionItem = new T_Promotion_Item();
                            promotionItem.M_Product_Id = item.M_Product_Id;
                            promotionItem.Title = item.Title;
                            promotionItem.Qty = item.Qty;
                            promotionItem.Todo = item.Todo;
                            promotionItem.Request_Due_Date = item.Request_Due_Date;
                            promotionItem.Note = item.Note;
                            promotionItem.T_Promotion_Id = entity.Id;
                            promotionItem.Request_Pic = 8;
                            promotionItem.Create_By = entity.Create_By;
                            promotionItem.Create_Date = DateTime.Now;

                            db.T_Promotion_Item.Add(promotionItem);
                        }
                        db.SaveChanges();
                    }
                    else
                    {
                        T_Promotion promotion = db.T_Promotion.Where(pr => pr.Id == entity.Id).FirstOrDefault();
                        if (promotion != null)
                        {
                            promotion.Code = entity.Code;
                            promotion.T_Event_Id = entity.T_Event_Id;
                            promotion.T_Design_Id = entity.T_Design_Id;
                            promotion.Flag_Design = "1";
                            promotion.Title = entity.Title;
                            promotion.Note = entity.Note;
                            promotion.Status = 1;

                            promotion.Update_By = entity.Update_By;
                            promotion.Update_Date = DateTime.Now;

                            foreach (var item in entityItem)
                            {
                                if (item.Id==0)
                                {
                                    T_Promotion_Item promotionItem = new T_Promotion_Item();
                                    promotionItem.M_Product_Id = item.M_Product_Id;
                                    promotionItem.Title = item.Title;
                                    promotionItem.Qty = item.Qty;
                                    promotionItem.Todo = item.Todo;
                                    promotionItem.Request_Due_Date = item.Request_Due_Date;
                                    promotionItem.Note = item.Note;
                                    promotionItem.T_Promotion_Id = entity.Id;
                                    promotionItem.Request_Pic = 8;

                                    promotionItem.Create_By = entity.Create_By;
                                    promotionItem.Create_Date = DateTime.Now;

                                    db.T_Promotion_Item.Add(promotionItem);
                                }
                                else
                                {
                                    T_Promotion_Item promotionItem = db.T_Promotion_Item.Where(pi => pi.Id == item.Id).FirstOrDefault();

                                    if (promotionItem != null)
                                    {
                                        promotionItem.M_Product_Id = item.M_Product_Id;
                                        promotionItem.Title = item.Title;
                                        promotionItem.Qty = item.Qty;
                                        promotionItem.Todo = item.Todo;
                                        promotionItem.Request_Due_Date = item.Request_Due_Date;
                                        promotionItem.Note = item.Note;
                                        promotionItem.T_Promotion_Id = entity.Id;
                                        promotionItem.Request_Pic = 8;

                                        promotionItem.Update_By = entity.Update_By;
                                        promotionItem.Update_Date = DateTime.Now;
                                    }
                                }
                            }
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

        public static ResultResponse UpdateFile(List<PromotionItemFileViewModel> entity, int id)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    foreach (var item in entity)
                    {
                        if (item.Id == 0)
                        {
                            T_Promotion_Item_File promotionFile = new T_Promotion_Item_File();
                            promotionFile.T_Promotion_id = id;
                            promotionFile.Filename = item.Filename;
                            promotionFile.Qty = item.Qty;
                            promotionFile.Todo = item.Todo;
                            promotionFile.Request_Due_Date = item.Request_Due_Date;
                            promotionFile.Note = item.Note;

                            promotionFile.Create_By = "Princess";
                            promotionFile.Create_Date = DateTime.Now;

                            db.T_Promotion_Item_File.Add(promotionFile);
                        }
                        else
                        {
                            T_Promotion_Item_File promotionFile = new T_Promotion_Item_File();
                            if (promotionFile != null)
                            {
                                promotionFile.T_Promotion_id = id;
                                promotionFile.Filename = item.Filename;
                                promotionFile.Qty = item.Qty;
                                promotionFile.Todo = item.Todo;
                                promotionFile.Request_Due_Date = item.Request_Due_Date;
                                promotionFile.Note = item.Note;

                                promotionFile.Update_By = "Princess";
                                promotionFile.Update_Date = DateTime.Now;
                            }
                        }
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                result.Success = true;
            }
            return result;
        }

        public static string GetTransactionCode()
        {
            string date = DateTime.Now.Day.ToString("D2");
            string month = DateTime.Now.Month.ToString("D2");
            string year = DateTime.Now.ToString("yy");
            string newRef = "TRWOMP" + date + month + year + "0";

            using (var db = new MarComContext())
            {
                var result = (from pr in db.T_Promotion
                              where pr.Code.Contains(newRef)
                              select new { code = pr.Code })
                              .OrderByDescending(o => o.code)
                              .FirstOrDefault();
                if (result != null)
                {
                    string[] oldRef = result.code.Split('0');
                    newRef += (int.Parse(oldRef[1]) + 1).ToString("D2");
                }
                else
                {
                    newRef += 0001;
                }
                return newRef;
            }
        }

        public static ResultResponse AddMenu1(PromotionViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                if (entity.Id == 0)
                {
                    T_Promotion promo = new T_Promotion();
                    promo.T_Event_Id = entity.T_Event_Id;
                    promo.T_Design_Id = entity.T_Design_Id;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static UserViewModel GetIdByName(string name)
        {
            UserViewModel result = new UserViewModel();
            using (var db = new MarComContext())
            {
                result = (from u in db.M_User
                          join e in db.M_Employee on u.M_Employee_Id equals e.Id
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
    }
}

