using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            Is_Delete = false;
        }
        public int Id { get; set; }

        [DisplayName("Product Code")]
        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [DisplayName("Product Name")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public bool Is_Delete { get; set; }

        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        public DateTime Create_Date { get; set; }
    }
}
