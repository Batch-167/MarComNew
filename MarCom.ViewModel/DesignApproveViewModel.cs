using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class DesignApproveViewModel
    {
        public DesignApproveViewModel()
        {
            Status = 1;
        }
        public int Id { get; set; }

        [DisplayName("Transaction Code")]
        [StringLength(50), Required]
        public string Code { get; set; }

        public int T_Event_Id { get; set; }

        [DisplayName("Event Code")]
        public string EventCode { get; set; }

        [DisplayName("Design Title")]
        [StringLength(255), Required]
        public string Title_Header { get; set; }

        public int Request_By { get; set; }

        [DisplayName("Request By")]
        public string NameRequest { get; set; }

        [DisplayName("Request Date")]
        public DateTime Request_Date { get; set; }

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

        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        public DateTime Create_Date { get; set; }

        [StringLength(50)]
        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }
    }
}
