﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class SouvenirItemViewModel
    {
        public SouvenirItemViewModel()
        {
            Is_Delete = false;
        }

        public int Id { get; set; }

        public int T_Souvenir_Id { get; set; }

        [Required]
        public int M_Souvenir_Id { get; set; }

        [DisplayName("Souvenir Name")]
        public string SouvenirName { get; set; }

        [Required]
        public int? Qty { get; set; }

        [DisplayName("Qty Actual")]
        public int? Qty_Settlement { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        public bool Is_Delete { get; set; }

        [StringLength(50)]
        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }
    }
}
