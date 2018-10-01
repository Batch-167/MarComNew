namespace MarCom.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Design
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Design()
        {
            T_Design_Item = new HashSet<T_Design_Item>();
            T_Promotion = new HashSet<T_Promotion>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        public int T_Event_Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title_Header { get; set; }

        public int Request_By { get; set; }

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

        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        public DateTime Create_Date { get; set; }

        [StringLength(50)]
        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        public virtual M_Employee M_Employee { get; set; }

        public virtual M_Employee M_Employee1 { get; set; }

        public virtual M_Employee M_Employee2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Design_Item> T_Design_Item { get; set; }

        public virtual T_Event T_Event { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Promotion> T_Promotion { get; set; }
    }
}
