namespace MarCom.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Souvenir_Item
    {
        public int Id { get; set; }

        public int T_Souvenir_Id { get; set; }

        public int M_Souvenir_Id { get; set; }

        public int? Qty { get; set; }

        public int? Qty_Settlement { get; set; }

        [Required]
        [StringLength(255)]
        public string Note { get; set; }

        public bool Is_Delete { get; set; }

        [StringLength(50)]
        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        [StringLength(50)]
        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        public virtual M_Souvenir M_Souvenir { get; set; }

        public virtual T_Souvenir T_Souvenir { get; set; }
    }
}
