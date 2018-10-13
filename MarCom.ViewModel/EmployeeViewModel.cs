using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            Is_Delete = false;
        }
        public int Id { get; set; }

        [DisplayName("Employee ID Number")]
        [Required]
        [StringLength(50)]
        public string Employee_Number { get; set; }

        [DisplayName("First Name")]
        [Required]
        [StringLength(50)]
        public string First_Name { get; set; }

        [DisplayName("Last Name")]
        [Required]
        [StringLength(50)]
        public string Last_Name { get; set; }

        public string Full_Name { get; set; }

        [DisplayName("Employee Name")]
        public string FullName
        {
            get
            {
                return First_Name + (String.IsNullOrEmpty(Last_Name) ? " " : " " + Last_Name);
            }
        }

        [DisplayName("Company Name")]
        public int M_Company_Id { get; set; }

        [DisplayName("Company Name")]
        public string CompanyName {get; set;}

        [StringLength(150)]
        public string Email { get; set; }

        public bool Is_Delete { get; set; }

        [DisplayName("Created By")]
        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        [DisplayName("Create Date")]
        public DateTime Create_Date { get; set; }
    }
}
