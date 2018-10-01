namespace MarCom.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Promotion_Item
    {
        public int Id { get; set; }

        public int T_Promotion_Id { get; set; }

        public int? T_Design_Item_Id { get; set; }

        public int M_Product_Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public int Request_Pic { get; set; }

        public DateTime? Start_Date { get; set; }

        public DateTime? End_Date { get; set; }

        public DateTime? Request_Due_Date { get; set; }

        public int? Qty { get; set; }

        public int Todo { get; set; }

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

        public virtual T_Design_Item T_Design_Item { get; set; }

        public virtual T_Promotion T_Promotion { get; set; }
    }
}
