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
            return Get(true);
        }
        public static List<EmployeeViewModel> Get(bool yes)
        {
            List<EmployeeViewModel> result = new List<EmployeeViewModel>();
            using (var db = new MarComContext())
            {
                result = (from e in db.M_Employee
                          join c in db.M_Company on e.M_Company_Id equals c.Id
                          select new EmployeeViewModel
                          {
                              Id = e.Id,
                              Employee_Number = e.Employee_Number,
                              First_Name = e.First_Name,
                              Last_Name = e.Last_Name,
                              M_Company_Id = e.M_Company_Id,
                              CompanyName = c.Name,
                              Email = e.Email,
                              Is_Delete = e.Is_Delete,
                              Create_By = e.Create_By,
                              Create_Date = DateTime.Now
                          })
                          .Where(e => e.Is_Delete == yes ? e.Is_Delete : true)
                          .ToList();
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
                        employee.Create_By = entity.Create_By;
                        employee.Create_Date = DateTime.Now;

                        db.M_Employee.Add(employee);
                        db.SaveChanges();
                        result.Message = "Data Employee" + entity.FullName + "has been saved";
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

                            employee.Update_By = entity.Update_By;
                            employee.Update_Date = DateTime.Now;

                            db.SaveChanges();
                            result.Message = "Data Employee" + entity.FullName + "has been edited";

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
                          join c in db.M_Company on e.M_Company_Id equals c.Id
                          where e.Id == id
                          select new EmployeeViewModel
                          {
                              Id = e.Id,
                              Employee_Number = e.Employee_Number,
                              First_Name = e.First_Name,
                              Last_Name = e.Last_Name,
                              M_Company_Id = e.M_Company_Id,
                              CompanyName = c.Name,
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

        public static string GetNewCode()
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.Month.ToString("D2");
            string date = DateTime.Now.Day.ToString("D2");
            string newRef = year + "." + month + "." + date + ".";

            using (var db = new MarComContext())
            {
                var result = (from e in db.M_Employee
                              where e.Employee_Number.Contains(newRef)
                              select new { employee_number = e.Employee_Number })
                              .OrderByDescending(o => o.employee_number)
                              .FirstOrDefault();
                if (result != null)
                {
                    string[] oldRef = result.employee_number.Split('.');
                    newRef += (int.Parse(oldRef[3]) + 1).ToString("D2");
                }
                else
                {
                    newRef += "01";
                }
                return newRef;
            }
        }

        public static List<EmployeeViewModel> Filter(EmployeeViewModel entity)
        {
            string date = entity.Create_Date.ToString();
            string[] olddate = date.Split(' ');
            date = olddate[0];

            List<EmployeeViewModel> result = new List<EmployeeViewModel>();
            using (var db = new MarComContext())
            {
                result = (from e in db.M_Employee
                          join c in db.M_Company on e.M_Company_Id equals c.Id
                          where e.Employee_Number.Contains(entity.Employee_Number) || e.First_Name.Contains(entity.FullName) && e.Last_Name.Contains(entity.FullName) || e.M_Company_Id == entity.M_Company_Id || e.Create_By.Contains(entity.Create_By) || c.Create_Date.ToString().Contains(entity.Create_Date.ToString())

                          select new EmployeeViewModel
                          {
                              Id = e.Id,
                              Employee_Number = e.Employee_Number,
                              First_Name = e.First_Name,
                              Last_Name = e.Last_Name,
                              M_Company_Id = e.M_Company_Id,
                              CompanyName = c.Name,
                              Create_By = e.Create_By,
                              Create_Date = e.Create_Date

                          }).ToList();
            }
            return result;
        }

        public static UserViewModel GetIdByName(string name)
        {
            UserViewModel result = new UserViewModel();
            using (var db = new MarComContext())
            {
                result = (from u in db.M_User
                          join e in db.M_Employee
                          on u.M_Employee_Id equals e.Id
                          join r in db.M_Role on u.M_Role_Id equals r.Id
                          where name == u.UserName
                          select new UserViewModel
                          {
                              Id = u.Id,
                              Password = u.PasswordHash,
                              M_Employee_Id = u.M_Employee_Id,
                              Fullname = e.First_Name + " " + e.Last_Name,
                              Role = r.Name
                          }).FirstOrDefault();
            }
            return result;
        }
    }
}
