using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class SouvenirStockViewModel
    {
        public SouvenirStockViewModel()
        {
            Is_Delete = false; 
        }

        public int Id { get; set; }

        [Display(Name ="Transaction Code")]
        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Display(Name = "Received By")]
        public int? Received_By { get; set; }

        [Display(Name = "Received By")]
        public string R_Name { get; set; }

        [Display(Name = "Received Date")]
        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Received_Date { get; set; }

        [Display(Name = "Create By")]
        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        [Display(Name = "Create Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Create_Date { get; set; }

        public bool Is_Delete { get; set; }

        public string Note { get; set; }

        [StringLength(50)]
        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }
    }
}
