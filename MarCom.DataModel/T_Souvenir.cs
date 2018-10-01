namespace MarCom.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Souvenir
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Souvenir()
        {
            T_Souvenir_Item = new HashSet<T_Souvenir_Item>();
        }

        public int Id { get; set; }

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

        public int? Received_By { get; set; }

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

        public virtual M_Employee M_Employee3 { get; set; }

        public virtual M_Employee M_Employee4 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Souvenir_Item> T_Souvenir_Item { get; set; }
    }
}
