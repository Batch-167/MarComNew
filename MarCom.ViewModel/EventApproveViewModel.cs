using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class EventApproveViewModel
    {
        public EventApproveViewModel()
        {
            Status = 1;
        }
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Transaction Code")]
        public string Code { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Event Name")]
        public string Event_Name { get; set; }

        [DisplayName("Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Start_Date { get; set; }

        [DisplayName("End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? End_Date { get; set; }

        [StringLength(255)]
        public string Place { get; set; }

        public decimal? Budget { get; set; }

        [DisplayName("Request By")]
        public int Request_By { get; set; }

        public string NameRequest { get; set; }

        [DisplayName("Request Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Request_Date { get; set; }

        public int? Approved_By { get; set; }

        public DateTime? Approved_Date { get; set; }

        [DisplayName("Asign To")]
        public int? Assign_To { get; set; }

        public DateTime? Closed_Date { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        public int? Status { get; set; }

        [StringLength(255)]
        public string Reject_Reason { get; set; }
    }
}
