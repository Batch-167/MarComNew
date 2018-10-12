﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class PromotionItemViewModel
    {
        public int Id { get; set; }

        public int T_Promotion_Id { get; set; }

        public int? T_Design_Item_Id { get; set; }

        public int M_Product_Id { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [DisplayName("Product Description")]
        public string ProductDescription { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [DisplayName("Request Pic")]
        public int Request_Pic { get; set; }

        [DisplayName("Start Date")]
        public DateTime? Start_Date { get; set; }

        [DisplayName("End Date")]
        public DateTime? End_Date { get; set; }

        [DisplayName("Due Date")]
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

        [StringLength(50)]
        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }
    }
}
