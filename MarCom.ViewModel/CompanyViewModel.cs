﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class CompanyViewModel
    {
        public CompanyViewModel()
        {
            Is_Delete = false;
        }

        [DisplayName("No")]
        public int Row { get; set; }
        public int Id { get; set; }

        [StringLength(50)]
        [DisplayName("Company Code")]
        public string Code { get; set; }

        [DisplayName("Company Name")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(255)]
        [Required]
        public string Address { get; set; }

        [StringLength(50)]
        [Required]
        public string Phone { get; set; }

        [StringLength(50)]
        [Required]
        public string Email { get; set; }

        public bool Is_Delete { get; set; }

        [DisplayName("Create By")]
        public string Create_By { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
        [DisplayName("Create Date")]
        public DateTime Create_Date { get; set; }
        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

    }
}
