using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class ProductRepo
    {
        public static List<ProductViewModel> Get()
        {
            return Get(true);

        }
        public static List<ProductViewModel> Get(bool all)
        {
            List<ProductViewModel> result = new List<ProductViewModel>();
            using (var db = new MarComContext())
            {
                result = (from p in db.M_Product
                          select new ProductViewModel
                          {
                              Id = p.Id,
                              Code = p.Code,
                              Name = p.Name,
                              Description = p.Description,
                              Is_Delete = p.Is_Delete,

                              Create_Date = DateTime.Now,
                              Create_By = "Administrator"
                          }).Where(p => p.Is_Delete == all ? p.Is_Delete : true)
                            .ToList();
            }
            return result;
        }

        public static ResultResponse Update(ProductViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        M_Product product = new M_Product();
                        product.Code = entity.Code;
                        product.Name = entity.Name;
                        product.Description = entity.Description;
                        product.Is_Delete = entity.Is_Delete;
                        product.Create_Date = DateTime.Now;
                        product.Create_By = "Administrator";

                        db.M_Product.Add(product);
                        db.SaveChanges();

                    }
                    else
                    {
                        M_Product product = db.M_Product.Where(p => p.Id == entity.Id).FirstOrDefault();
                        if (product != null)
                        {
                            product.Code = entity.Code;
                            product.Name = entity.Name;
                            product.Description = entity.Description;

                            product.Update_Date = DateTime.Now;
                            product.Update_By = "Administrator";

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


        public static ProductViewModel GetById(int id)
        {
            ProductViewModel result = new ProductViewModel();
            using (var db = new MarComContext())
            {
                result = (from p in db.M_Product
                          where p.Id == id
                          select new ProductViewModel
                          {
                              Id = p.Id,
                              Code = p.Code,
                              Name = p.Name,
                              Description = p.Description,
                              Is_Delete = p.Is_Delete,
                              Create_Date = p.Create_Date,
                              Create_By = p.Create_By
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
                    M_Product product = db.M_Product.Where(p => p.Id == id).FirstOrDefault();

                    if (product != null)
                    {
                        product.Is_Delete = true;
                        db.SaveChanges();
                    }
                }
            }

            catch (Exception)
            {
                ProductRepo.GetNewCode();
                return false;
            }
            return result;
        }

        public static string GetNewCode()
        {
            string newRef = "PR";
            using (var db = new MarComContext())
            {
                var result = (from p in db.M_Product
                              where p.Code.Contains(newRef)
                              select new { code = p.Code })
                              .OrderByDescending(o => o.code)
                              .FirstOrDefault();
                if (result != null)
                {
                    string[] oldRef = result.code.Split('R');
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
