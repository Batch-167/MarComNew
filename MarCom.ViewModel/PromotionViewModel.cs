﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class PromotionViewModel
    {
        public string EventCode { get; set; }

        public int Id { get; set; }

        [DisplayName("Transaction Code")]
        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(1)]
        public string Flag_Design { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public int T_Event_Id { get; set; }

        public int? T_Design_Id { get; set; }

        [DisplayName("Request By")]
        public int? Request_By { get; set; }

        [DisplayName("Request Date")]
        public DateTime? Request_Date { get; set; }

        public int? Approved_By { get; set; }

        public DateTime? Approved_Date { get; set; }

        [DisplayName("Assign To")]
        public int? Assign_To { get; set; }

        public DateTime? Closed_Date { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        public int? Status { get; set; }

        [StringLength(255)]
        public string Reject_Reason { get; set; }

        public bool? Is_Delete { get; set; }

        [DisplayName("Create By")]
        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        [DisplayName("Create Date")]
        public DateTime Create_Date { get; set; }
    }
}
