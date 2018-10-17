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
                              Assign_To = pr.Assign_To,
                              //AssignName = e.First_Name+" "+e.Last_Name,
                              Flag_Design = pr.Flag_Design,
                              Status = pr.Status,

                              Create_By = pr.Create_By,
                              Create_Date = pr.Create_Date,
                              Is_Delete = pr.Is_Delete

                          }).ToList();
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
                          //where id == de.Id
                          where de.Id == id
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
                              M_Product_Id = dei.M_Product_Id,
                              Id = dei.Id,
                              T_Design_Item_Id = dei.Id,
                              ProductName = pr.Name,
                              ProductDescription = pr.Description,
                              Title = dei.Title_Item
                          }).ToList();
            }
            return result;
        }

        public static ResultResponse Update(PromotionViewModel entity, List<PromotionItemViewModel> entityItem, List<PromotionItemFileViewModel> entityFile)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        string newCode = GetTransactionCode();
                        T_Promotion promotion = new T_Promotion();
                        promotion.Code = newCode;
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
                            promotionItem.T_Design_Item_Id = item.T_Design_Item_Id;
                            promotionItem.Qty = item.Qty;
                            promotionItem.Todo = item.Todo;
                            promotionItem.Request_Due_Date = item.Request_Due_Date;
                            promotionItem.Note = item.Note;
                            promotionItem.T_Promotion_Id = entity.Id;
                            promotionItem.Request_Pic = 8;
                            promotionItem.Create_By = promotion.Create_By;
                            promotionItem.Create_Date = DateTime.Now;

                            db.T_Promotion_Item.Add(promotionItem);
                        }

                        foreach (var item in entityFile)
                        {
                            T_Promotion_Item_File promotionFile = new T_Promotion_Item_File();
                            promotionFile.T_Promotion_id = promotion.Id;
                            promotionFile.Filename = item.Filename;
                            //promotionFile.ImagePath = item.ImagePath;
                            promotionFile.Extention = item.Extention;
                            promotionFile.Qty = item.Qty;
                            promotionFile.Todo = item.Todo;
                            promotionFile.Request_Due_Date = item.Request_Due_Date;
                            promotionFile.Note = item.Note;

                            promotionFile.Create_By = promotion.Create_By;
                            promotionFile.Create_Date = DateTime.Now;

                            db.T_Promotion_Item_File.Add(promotionFile);
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
                                if (item.Id == 0) //ini buat add item edit misal mau nambah file
                                {
                                    T_Promotion_Item promotionItem = new T_Promotion_Item();
                                    promotionItem.T_Promotion_Id = promotion.Id;
                                    promotionItem.M_Product_Id = item.M_Product_Id;
                                    promotionItem.Title = item.Title;
                                    promotionItem.Qty = item.Qty;
                                    promotionItem.Todo = item.Todo;
                                    promotionItem.Request_Due_Date = item.Request_Due_Date;
                                    promotionItem.Note = item.Note;
                                    promotionItem.Request_Pic = 8;

                                    promotionItem.Create_By = promotion.Create_By;
                                    promotionItem.Create_Date = DateTime.Now;

                                    db.T_Promotion_Item.Add(promotionItem);
                                }
                                else
                                {
                                    T_Promotion_Item promotionItem = db.T_Promotion_Item.Where(pi => pi.Id == item.Id).FirstOrDefault();

                                    if (promotionItem != null) //nah ini misal mau edit si add item.
                                    {

                                        promotionItem.T_Promotion_Id = promotion.Id;
                                        promotionItem.T_Design_Item_Id = item.T_Design_Item_Id;
                                        promotionItem.M_Product_Id = item.M_Product_Id;
                                        promotionItem.Title = item.Title;
                                        promotionItem.Qty = item.Qty;
                                        promotionItem.Todo = item.Todo;
                                        promotionItem.Request_Due_Date = item.Request_Due_Date;
                                        promotionItem.Note = item.Note;
                                        promotionItem.Request_Pic = 8;

                                        promotionItem.Update_By = promotion.Update_By;
                                        promotionItem.Update_Date = DateTime.Now;
                                    }
                                }
                            }
                            foreach (var item in entityFile)
                            {
                                if (item.Id == 0)
                                {
                                    T_Promotion_Item_File promotionFile = new T_Promotion_Item_File();
                                    promotionFile.T_Promotion_id = promotion.Id;
                                    promotionFile.Filename = item.Filename;
                                    //promotionFile.ImagePath = item.ImagePath;
                                    promotionFile.Extention = item.Extention;
                                    promotionFile.Qty = item.Qty;
                                    promotionFile.Todo = item.Todo;
                                    promotionFile.Request_Due_Date = item.Request_Due_Date;
                                    promotionFile.Note = item.Note;

                                    promotionFile.Create_By = promotion.Create_By;
                                    promotionFile.Create_Date = DateTime.Now;

                                    db.T_Promotion_Item_File.Add(promotionFile);
                                }
                                else
                                {
                                    T_Promotion_Item_File promotionFile = new T_Promotion_Item_File();
                                    if (promotionFile != null)
                                    {
                                        promotionFile.T_Promotion_id = promotion.Id;
                                        promotionFile.Filename = item.Filename;
                                        //promotionFile.ImagePath = item.ImagePath;
                                        promotionFile.Extention = item.Extention;
                                        promotionFile.Qty = item.Qty;
                                        promotionFile.Todo = item.Todo;
                                        promotionFile.Request_Due_Date = item.Request_Due_Date;
                                        promotionFile.Note = item.Note;

                                        promotionFile.Update_By = promotion.Update_By;
                                        promotionFile.Update_Date = DateTime.Now;
                                    }
                                }
                                db.SaveChanges();
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


        //untuk update Create 3

        public static ResultResponse UpdateCreate3(PromotionViewModel entity, List<PromotionItemFileViewModel> entityFile, int id)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        string newCode = GetTransactionCode();
                        T_Promotion promotion = new T_Promotion();
                        promotion.Code = newCode;
                        promotion.T_Event_Id = entity.T_Event_Id;
                        promotion.Is_Delete = entity.Is_Delete;
                        promotion.Flag_Design = "0";
                        promotion.Status = 1;
                        promotion.Title = entity.Title;
                        promotion.Note = entity.Note;
                        promotion.Create_By = entity.Create_By;
                        promotion.Create_Date = DateTime.Now;
                        promotion.Request_By = entity.Request_By;
                        promotion.Request_Date = DateTime.Now;

                        db.T_Promotion.Add(promotion);

                        foreach (var item in entityFile)
                        {
                            T_Promotion_Item_File promotionFile = new T_Promotion_Item_File();
                            promotionFile.T_Promotion_id = promotion.Id;
                            promotionFile.Filename = item.Filename;
                            promotionFile.Qty = item.Qty;
                            promotionFile.Todo = item.Todo;
                            promotionFile.Request_Due_Date = item.Request_Due_Date;
                            promotionFile.Note = item.Note;

                            promotionFile.Create_By = promotion.Create_By;
                            promotionFile.Create_Date = DateTime.Now;

                            db.T_Promotion_Item_File.Add(promotionFile);
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

                            foreach (var item in entityFile)
                            {
                                if (item.Id == 0)
                                {
                                    T_Promotion_Item_File promotionFile = new T_Promotion_Item_File();
                                    promotionFile.T_Promotion_id = promotion.Id;
                                    promotionFile.Filename = item.Filename;
                                    //promotionFile.ImagePath = item.ImagePath;
                                    promotionFile.Extention = item.Extention;
                                    promotionFile.Qty = item.Qty;
                                    promotionFile.Todo = item.Todo;
                                    promotionFile.Request_Due_Date = item.Request_Due_Date;
                                    promotionFile.Note = item.Note;

                                    promotionFile.Create_By = promotion.Create_By;
                                    promotionFile.Create_Date = DateTime.Now;

                                    db.T_Promotion_Item_File.Add(promotionFile);
                                }
                                else
                                {
                                    T_Promotion_Item_File promotionFile = new T_Promotion_Item_File();
                                    if (promotionFile != null)
                                    {
                                        promotionFile.T_Promotion_id = promotion.Id;
                                        promotionFile.Filename = item.Filename;
                                        //promotionFile.ImagePath = item.ImagePath;
                                        promotionFile.Extention = item.Extention;
                                        promotionFile.Qty = item.Qty;
                                        promotionFile.Todo = item.Todo;
                                        promotionFile.Request_Due_Date = item.Request_Due_Date;
                                        promotionFile.Note = item.Note;

                                        promotionFile.Update_By = promotion.Update_By;
                                        promotionFile.Update_Date = DateTime.Now;
                                    }
                                }
                            }
                        }
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static string GetTransactionCode()
        {
            string date = DateTime.Now.Day.ToString("D2") + DateTime.Now.Month.ToString("D2") + DateTime.Now.ToString("yy");
            string newRef = "TRWOMP" + date + "-";

            using (var db = new MarComContext())
            {
                var result = (from pr in db.T_Promotion
                              where pr.Code.Contains(newRef)
                              select new { code = pr.Code })
                              .OrderByDescending(o => o.code)
                              .FirstOrDefault();
                if (result != null)
                {
                    string[] oldRef = result.code.Split('-');
                    newRef += (int.Parse(oldRef[1]) + 1).ToString("D4");
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

        public static PromotionViewModel GetById(int id)
        {
            PromotionViewModel result = new PromotionViewModel();
            using (var db = new MarComContext())
            {
                result = (from pr in db.T_Promotion
                          join ev in db.T_Event on pr.T_Event_Id equals ev.Id
                         // join de in db.T_Design on pr.T_Design_Id equals de.Id
                          //join de in db.T_Design on pr.T_Design_Id equals de.Id
                          join e in db.M_Employee on pr.Request_By equals e.Id
                          where pr.Id == id
                          select new PromotionViewModel
                          {
                              Id = pr.Id,
                              Code = pr.Code,
                              Flag_Design = pr.Flag_Design,
                              Title = pr.Title,
                              T_Event_Id = pr.T_Event_Id,
                              EventCode = ev.Code,
                              T_Design_Id = pr.T_Design_Id,
                         //     DesignCode = de.Code,
                              //DesignCode = de.Code,
                              Request_By = pr.Request_By,
                              RequestBy = e.First_Name + " " + e.Last_Name,

                              Status = pr.Status,
                              Request_Date = pr.Request_Date,
                              Note = pr.Note

                          }).FirstOrDefault();
            }
            return result;
        }

        public static DesignRequestViewModel GetId(int id)
        {
            DesignRequestViewModel result = new DesignRequestViewModel();
            using (var db = new MarComContext())
            {
                result = (from dr in db.T_Design
                          join pr in db.T_Promotion
                          on dr.Id equals pr.T_Design_Id
                          join em in db.M_Employee
                          on dr.Request_By equals em.Id
                          join di in db.T_Design_Item
                          on dr.Id equals di.T_Design_Id
                          where pr.Id == id
                          select new DesignRequestViewModel
                          {
                              Id = dr.Id,
                              Code = dr.Code,
                              Title_Header = dr.Title_Header,
                              Request_By = dr.Request_By,
                              NameRequest = em.First_Name + "" + em.Last_Name,
                              Request_Date = dr.Request_Date,
                              Note = dr.Note

                          }).FirstOrDefault();

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
                          join r in db.M_Role on u.M_Role_Id equals r.Id
                          where name == u.UserName
                          select new UserViewModel
                          {
                              Id = u.Id,
                              Password = u.PasswordHash,
                              Role=r.Name,
                              M_Employee_Id = u.M_Employee_Id,
                              Fullname = e.First_Name + " " + e.Last_Name
                          }).FirstOrDefault();
            }
            return result;
        }

        public static List<PromotionItemViewModel> GetItemId(int id)
        {
            List<PromotionItemViewModel> result = new List<PromotionItemViewModel>();
            using (var db = new MarComContext())
            {
                result = (from pi in db.T_Promotion_Item
                          join p in db.T_Promotion
                          on pi.T_Promotion_Id equals p.Id
                          join di in db.T_Design_Item
                          on pi.T_Design_Item_Id equals di.Id
                          join pr in db.M_Product
                          on pi.M_Product_Id equals pr.Id
                          where id == p.Id
                          select new PromotionItemViewModel
                          {
                              Id = pi.Id,
                              Title = pi.Title,
                              M_Product_Id=pi.M_Product_Id,
                              T_Design_Item_Id=pi.T_Design_Item_Id,
                              ProductName = pr.Name,
                              ProductDescription = pr.Description,
                              Create_By=p.Create_By,
                              Create_Date=p.Create_Date,

                              Qty = pi.Qty,
                              Todo = pi.Todo,
                              Request_Due_Date = pi.Request_Due_Date,
                              Start_Date = pi.Start_Date,
                              End_Date = pi.End_Date,
                              Note = pi.Note
                          }).ToList();
            }
            return result;
        }

        //Untuk Mengambil Data berdasarkan Id untuk PromotionItemFile
        public static List<PromotionItemFileViewModel> GetIdFile(int id)
        {
            List<PromotionItemFileViewModel> result = new List<PromotionItemFileViewModel>();

            using (var db = new MarComContext())
            {

                result = (from it in db.T_Promotion_Item_File
                          join p in db.T_Promotion
                          on it.T_Promotion_id equals p.Id
                          where id == p.Id
                          select new PromotionItemFileViewModel
                          {
                              Id = it.Id,
                              Filename = it.Filename,
                              Qty = it.Qty,
                              Todo = it.Todo,
                              Request_Due_Date = it.Request_Due_Date,
                              Start_Date = it.Start_Date,
                              End_Date = it.End_Date,
                              Note = it.Note
                          }
                          ).ToList();
            }
            return result;

        }

        //Ambil Id untuk Ubah status saat Approval
        public static ResultResponse Approve(PromotionViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    T_Promotion pr = db.T_Promotion.Where(p => p.Id == entity.Id).FirstOrDefault();
                    if (pr != null)
                    {
                        pr.Reject_Reason = entity.Reject_Reason;
                        pr.Status = entity.Status;
                        pr.Assign_To = entity.Assign_To;
                        if (entity.Status == 2)
                        {
                            pr.Approved_By = entity.Approved_By;
                            pr.Approved_Date = DateTime.Now;
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

        public static ResultResponse UploadItem(PromotionItemFileViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id==0)
                    {
                        T_Promotion_Item_File it = new T_Promotion_Item_File();
                        it.Id = it.Id;
                        it.Filename = it.Filename;
                        it.Qty = it.Qty;
                        it.Todo = it.Todo;
                        it.Request_Due_Date = it.Request_Due_Date;
                        it.Note = it.Note;

                        it.Create_By = it.Create_By;
                        it.Create_Date = DateTime.Now;

                        db.T_Promotion_Item_File.Add(it);
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

        public static List<EmployeeViewModel> GetStaff()
        {
            List<EmployeeViewModel> result = new List<EmployeeViewModel>();

            using (var db = new MarComContext())
            {
                result = (from e in db.M_Employee
                          join u in db.M_User
                          on e.Id equals u.M_Employee_Id
                          join r in db.M_Role
                          on u.M_Role_Id equals r.Id
                          where r.Name == "Staff"
                          select new EmployeeViewModel
                          {
                              Id = e.Id,
                              Full_Name = e.First_Name + " " + e.Last_Name
                          }
                           ).ToList();

            }

            return result;
        }

    }

}

