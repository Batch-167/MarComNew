using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class DesignRequestViewModel
    {
        public DesignRequestViewModel()
        {
            Is_Delete = false;
            Request_By = 1;
            Request_Date = DateTime.Now;
            Create_Date = DateTime.Now;
        }
        public int Id { get; set; }

        [DisplayName("Transaction Code")]
        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [DisplayName("Event Code")]
        public int T_Event_Id { get; set; }

        public string EventCode { get; set; }

        [DisplayName("Design Title")]
        [Required]
        [StringLength(255)]
        public string Title_Header { get; set; }

        [DisplayName("Request By")]
        public int Request_By { get; set; }

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

        [DisplayName("Create By")]
        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        [DisplayName("Created Date")]
        public DateTime Create_Date { get; set; }

        //buat view di promotion
        [DisplayName("Design Code")]
        public string DesignCode { get; set; }
    }
}
