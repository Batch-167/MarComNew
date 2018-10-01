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
            Is_Delete = true;
        }
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Employee_Number { get; set; }

        [Required]
        [StringLength(50)]
        public string First_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Last_Name { get; set; }

        [DisplayName ("Employee Name")]
        public string FullName
        {
            get
            {
                return First_Name + (String.IsNullOrEmpty(Last_Name) ? "" : "" + Last_Name);
            }
        }

        public int M_Company_Id { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        public bool Is_Delete { get; set; }

        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        public DateTime Create_Date { get; set; }
    }
}
