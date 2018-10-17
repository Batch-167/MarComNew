using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class UnitViewModel
    {
        public UnitViewModel()
        {
            Is_Delete = false;
        }

        public int Id { get; set; }

        [Display(Name ="Unit Code")]
        [StringLength(50)]
        public string Code { get; set; }

        [Display(Name="Unit Name")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        [Required]
        public string Description { get; set; }

        public bool Is_Delete { get; set; }

        [Display(Name = "Create By")]
        [StringLength(50)]
        public string Create_By { get; set; }

        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString ="{0:MM/dd/yyyy}")]
        [Display(Name = "Create Date")]
        public DateTime Create_Date { get; set; }

        [Display(Name = "Update By")]
        [StringLength(50)]
        public string Update_By { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Update Date")]
        public DateTime? Update_Date { get; set; }
    }
}
