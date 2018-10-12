
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class EventViewModel
    {
        public EventViewModel()
        {
            Is_Delete = false;
            Request_Date = DateTime.Now;
            Status = 1;
        }
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Transaction Code")]
        public string Code { get; set; }

        [Required]
        [StringLength(255)]
        public string Event_Name { get; set; }

        [DisplayName("Start Date")]
        [Required]
       // [DataType(DataType.Date)]
        public DateTime? Start_Date { get; set; }

        [DisplayName("End Date")]
        [Required]
        public DateTime? End_Date { get; set; }

        [StringLength(255)]
        [Required]
        public string Place { get; set; }

        [Required]
        public decimal? Budget { get; set; }

        public int Request_By { get; set; }

        public string NameRequest { get; set; }

        public string StatusName {
            get
            {
                if (Status==0)
                {
                    return "Rejected";
                }
                else if (Status==1)
                {
                    return "Submitted";
                }
                else if (Status == 2)
                {
                    return "In Progress";
                }
                else if (Status == 3)
                {
                    return "Done";
                }
                else
                {
                    return "Nothing Happen";
                }
            }
                
                }

        [DisplayName("Request Date")]
        public DateTime Request_Date { get; set; }

        public int? Approved_By { get; set; }

        public DateTime? Approved_Date { get; set; }

        public int? Assign_To { get; set; }

        public DateTime? Closed_Date { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        public int? Status { get; set; }

        [StringLength(255)]
        public string Reject_Reason { get; set; }

        public bool? Is_Delete { get; set; }

        [StringLength(50)]
        public string Create_By { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? Create_Date { get; set; }

        [StringLength(50)]
        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }
    }
}
