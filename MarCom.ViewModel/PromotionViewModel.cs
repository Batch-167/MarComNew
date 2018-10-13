using System;
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
        public PromotionViewModel()
        {
            Is_Delete = false;
            Request_Date = DateTime.Now;
            Flag_Design = "0";
           // AssignName = "-";
            Assign_To = 1;
        }

       // public string AssignName { get; set; }

        public string FlagDesign
        {
            get
            {
                if (Flag_Design=="1")
                {
                    return "Yes";
                }
                else if (Flag_Design=="0")
                {
                    return "No";
                }
                else
                {
                    return "N/A";
                }
            }
        }

        public string StatusCondt
        {
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
                else if (Status==2)
                {
                    return "In Progress";
                }
                else if (Status==3)
                {
                    return "Done";
                }
                else
                {
                    return "Rejected";
                }
            }
        }
        public string RequestBy { get; set; }
        public string DesignCode { get; set; }

        [DisplayName("Event Code")]
        public string EventCode { get; set; }

        public int Id { get; set; }

        [DisplayName("Transaction Code")]
        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [DisplayName("Select From Design")]
        [Required]
        [StringLength(1)]
        public string Flag_Design { get; set; }


        [DisplayName("Title Header")]
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

        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        //prop untuk List Design Request
        [DisplayName("Design Code")]
        public string CodeDesign { get; set; }
        [DisplayName("Title Header")]
        public string TitleHeader { get; set; }
        [DisplayName("Request By")]
        public int? ReqBy { get; set; }
        [DisplayName("Request Date")]
        public DateTime? ReqDate { get; set; }
        [DisplayName("Note")]
        public string Notess { get; set; }
    }
}
