using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MarCom.Presentation.Models
{

    public class M_Menu
    {   
        [Key]  
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        public string Controller { get; set; }

        public int? Parent_Id { get; set; }

        public bool Is_Delete { get; set; }

        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        public DateTime Create_Date { get; set; }

        [StringLength(50)]
        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }
        
        public virtual ICollection<M_Menu> ListMenu { get; set; }

        [ForeignKey("Parent_Id")]
        public virtual M_Menu ParentMenu { get; set; }
    }

    public class M_Menu_Access
    {
        [Key]
        public int Id { get; set; }

        public int M_Role_Id { get; set; }

        public int M_Menu_Id { get; set; }

        public bool Is_Delete { get; set; }

        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        public DateTime Create_Date { get; set; }

        [StringLength(50)]
        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }
        [ForeignKey("M_Menu_Id")]
        public M_Menu Menu { get; set; }

        [ForeignKey("M_Role_Id")]
        public MRole Role { get; set; }
    }
}