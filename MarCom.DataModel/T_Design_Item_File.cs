namespace MarCom.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Design_Item_File
    {
        public int Id { get; set; }

        public int T_Design_Item_Id { get; set; }

        [StringLength(100)]
        public string Filename { get; set; }

        [StringLength(11)]
        public string Size { get; set; }

        [StringLength(11)]
        public string Extention { get; set; }

        public bool? Is_Delete { get; set; }

        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        public DateTime Create_Date { get; set; }

        [StringLength(50)]
        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        public virtual T_Design_Item T_Design_Item { get; set; }
    }
}
