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
        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Display(Name="Unit Name")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public bool Is_Delete { get; set; }

        [Display(Name = "Create By")]
        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        [Display(Name = "Create Date")]
        public DateTime Create_Date { get; set; }


    }
}
