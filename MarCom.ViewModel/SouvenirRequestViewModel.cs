using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class SouvenirRequestViewModel
    {
        public SouvenirRequestViewModel()
        {
            Is_Delete = false;
        }
        public int Id { get; set; }

        [DisplayName("Transaction Code")]
        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(11)]
        public string Type { get; set; }

        public int? T_Event_Id { get; set; }

        public int? Request_By { get; set; }

        public DateTime? Request_Date { get; set; }

        public int? Approved_By { get; set; }

        [DisplayName("Received By")]
        public int? Received_By { get; set; }

        [DisplayName("Received Date")]
        public DateTime? Received_Date { get; set; }

        public int? Settlement_By { get; set; }

        public DateTime? Settlement_Date { get; set; }

        public int? Settlement_Approved_By { get; set; }

        public DateTime? Settlement_Approved_Date { get; set; }

        public int? Status { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

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
