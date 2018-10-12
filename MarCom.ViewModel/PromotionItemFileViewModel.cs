using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class PromotionItemFileViewModel
    {
        public PromotionItemFileViewModel()
        {
            Is_Delete = false;
        }
        public int Id { get; set; }

        public int T_Promotion_id { get; set; }

        [StringLength(100)]
        public string Filename { get; set; }

        [StringLength(11)]
        public string Size { get; set; }

        [StringLength(11)]
        public string Extention { get; set; }

        public DateTime? Start_Date { get; set; }

        public DateTime? End_Date { get; set; }

        public DateTime? Request_Due_Date { get; set; }

        public int? Qty { get; set; }

        public int Todo { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        public bool? Is_Delete { get; set; }

        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        public DateTime Create_Date { get; set; }
    }
}
