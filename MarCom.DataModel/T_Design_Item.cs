namespace MarCom.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Design_Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Design_Item()
        {
            T_Design_Item_File = new HashSet<T_Design_Item_File>();
            T_Promotion_Item = new HashSet<T_Promotion_Item>();
        }

        public int Id { get; set; }

        public int T_Design_Id { get; set; }

        public int M_Product_Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title_Item { get; set; }

        public int Request_Pic { get; set; }

        public DateTime? Start_Date { get; set; }

        public DateTime? End_Date { get; set; }

        public DateTime? Request_Due_Date { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        public bool? Is_Delete { get; set; }

        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        public DateTime Create_Date { get; set; }

        [StringLength(50)]
        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        public virtual M_Employee M_Employee { get; set; }

        public virtual M_Product M_Product { get; set; }

        public virtual T_Design T_Design { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Design_Item_File> T_Design_Item_File { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Promotion_Item> T_Promotion_Item { get; set; }
    }
}
