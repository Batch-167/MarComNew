using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Is_Delete = false;
        }
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Role Name")]
        public int M_Role_Id { get; set; }

        [Display(Name = "Role Name")]
        public string Role { get; set; }

        [Display(Name = "Employee Name")]
        public int M_Employee_Id { get; set; }

        [Display(Name ="Employee Name")]
        public string Fullname { get; set; }

        public string Company { get; set; }

        public bool Is_Delete { get; set; }

        [Display(Name = "Create By")]
        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        [Display(Name = "Create Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Create_Date { get; set; }
    }
}
