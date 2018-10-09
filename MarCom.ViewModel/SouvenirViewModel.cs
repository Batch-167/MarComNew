using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class SouvenirViewModel
    {
        public SouvenirViewModel()
        {
            Is_Delete = false;
            Quantity = 0;
        }
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public int? Quantity { get; set; }

        [DisplayName("Unit Name")]
        public int M_Unit_Id { get; set; }
        public string Unit { get; set; }
        public bool Is_Delete { get; set; }
        [DisplayName("Created")]
        public string Create_By { get; set; }
        [DisplayName("Create Date")]
        public DateTime Create_Date { get; set; }

        [DisplayName("Update By")]
        public string Update_By { get; set; }
    }
}
