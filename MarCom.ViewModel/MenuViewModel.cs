using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class MenuViewModel
    {
        public MenuViewModel()
        {
            Is_Delete = false;            
        }
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        
        [StringLength(50), Required, DisplayName("Menu Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        public string Controller { get; set; }

        [DisplayName("Parent")]
        public int? Parent_Id { get; set; }

        public bool Is_Delete { get; set; }

        [DisplayName("Created")]
        [StringLength(50)]
        public string Create_By { get; set; }

        [DisplayName("Create Date")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Create_Date { get; set; }

        [DisplayName("Update By")]
        public string Update_By { get; set; }
    }
}
