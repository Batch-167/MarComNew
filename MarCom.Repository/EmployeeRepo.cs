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
    }
}
