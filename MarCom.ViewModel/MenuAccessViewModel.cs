using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class MenuAccessViewModel
    {

        [DisplayName("Menu Name")]
        public int M_Menu_Id { get; set; }

        public MenuAccessViewModel()
        {
            Is_Delete = false;
        }
        public int Id { get; set; }

        [DisplayName("Role Code")]
        public string RoleCode { get; set; }

        [DisplayName("Role Name")]
        public int M_Role_Id { get; set; }

        [DisplayName("Role Name")]
        public string RoleName { get; set; }

        [DisplayName("Menu Name")]
        public string MenuName { get; set; }


        public bool Is_Delete { get; set; }

        [DisplayName("Created")]
        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        [DisplayName("Create Date")]
        public DateTime Create_Date { get; set; }

        [StringLength(50)]
        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        public List<MenuAccessViewModel> Menu { get; set; }

        
    }
}
