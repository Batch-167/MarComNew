using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class EmployeeRepo
    {
        public static List<EmployeeViewModel> Get()
        {
            List<EmployeeViewModel> result = new List<EmployeeViewModel>();
            using (var db = new MarComContext())
            {
                result = (from e in db.M_Employee
                          select new EmployeeViewModel
                          {
                              Id = e.Id,
                              Employee_Number = e.Employee_Number,
                              First_Name = e.First_Name,
                              Last_Name = e.Last_Name,
                              M_Company_Id = e.M_Company_Id,
                              Email = e.Email,
                              Is_Delete = e.Is_Delete,
                              Create_By = e.Create_By,
                              Create_Date = e.Create_Date
                          }).ToList();
            }
            return result;
        }

        public static ResultResponse Update(EmployeeViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        M_Employee employee = new M_Employee();
                        employee.Employee_Number = entity.Employee_Number;
                        employee.First_Name = entity.First_Name;
                        employee.Last_Name = entity.Last_Name;
                        employee.M_Company_Id = entity.M_Company_Id;
                        employee.Email = entity.Email;
                        employee.Is_Delete = entity.Is_Delete;
                        employee.Create_By = "Princess";
                        employee.Create_Date = DateTime.Now;

                        db.M_Employee.Add(employee);
                        db.SaveChanges();
                    }
                    else
                    {
                        M_Employee employee = db.M_Employee.Where(c => c.Id == entity.Id).FirstOrDefault();
                        if (employee != null)
                        {
                            employee.First_Name = entity.First_Name;
                            employee.Last_Name = entity.Last_Name;
                            employee.M_Company_Id = entity.M_Company_Id;
                            employee.Email = entity.Email;

                            employee.Update_By = "Princess";
                            employee.Update_Date = DateTime.Now;

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

        public static EmployeeViewModel GetById(int id)
        {
            EmployeeViewModel result = new EmployeeViewModel();
            using (var db = new MarComContext())
            {
                result = (from e in db.M_Employee
                          where e.Id == id
                          select new EmployeeViewModel
                          {
                              Id = e.Id,
                              First_Name = e.First_Name,
                              Last_Name = e.Last_Name,
                              M_Company_Id = e.M_Company_Id,
                              Email = e.Email
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
                    M_Employee employee = db.M_Employee.Where(e => e.Id == id).FirstOrDefault();
                    if (employee != null)
                    {
                        employee.Is_Delete = true;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}
