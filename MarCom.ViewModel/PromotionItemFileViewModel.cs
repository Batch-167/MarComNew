using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MarCom.ViewModel
{
    public class PromotionItemFileViewModel
    {
        public PromotionItemFileViewModel()
        {
            Is_Delete = false;
            Todo = 1;
            Qty = 1;
        }

        public HttpPostedFileBase ImageFile { get; set; }
        public int Id { get; set; }

        public int T_Promotion_id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:MM-dd-yyyy}")]
        [DisplayName("Start Date")]
        public DateTime? Start_Date { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
        [DisplayName("End Date")]
        public DateTime? End_Date { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
        [DisplayName("Request Due Date")]
        public DateTime? Request_Due_Date { get; set; }

        public int? Qty { get; set; }

        public int Todo { get; set; }

        [DisplayName("Todo")]
        public string TodoName
        {
            get
            {
                if (Todo == 1)
                {
                    return "Print/Cetak";
                }
                else if (Todo == 2)
                {
                    return "Post to Social Media";
                }
                else if (Todo == 3)
                {
                    return "Post to Company Profile Website";
                }
                else if (Todo == 4)
                {
                    return "Post to Xsis Academy Website";
                }
                else if (Todo == 5)
                {
                    return "Other";
                }
                else
                {
                    return "Nothing to do";
                }

            }
        }

        [StringLength(255)]
        public string Note { get; set; }

        public bool? Is_Delete { get; set; }

        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        public DateTime Create_Date { get; set; }

        //untuk UPLOAD IMAGE
        public string ImagePath { get; set; }

        [DataType(DataType.Upload)]
        [StringLength(100)]
        [DisplayName("File Name")]
        [Required(ErrorMessage ="Please choose file to upload.")]
        public string Filename { get; set; }

        [StringLength(11)]
        public string Size { get; set; }

        [StringLength(11)]
        public string Extention { get; set; }

        //public HttpPostedFileBase MyProperty { get; set; }

    }
}
