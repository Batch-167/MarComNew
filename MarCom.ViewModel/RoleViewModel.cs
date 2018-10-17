using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
            Is_Delete = false;
        }
        public int Id { get; set; }

        [DisplayName("Role Code")]
        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [DisplayName("Role Name")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        public bool Is_Delete { get; set; }

        [DisplayName("Create By")]
        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        [DisplayName("Create Date")]
        public DateTime Create_Date { get; set; }

        [StringLength(50)]
        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

    }
}
