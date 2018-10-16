namespace MarCom.DataModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MarComContext : DbContext
    {
        public MarComContext()
            : base("name=MarComContext")
        {
        }

        public virtual DbSet<M_Company> M_Company { get; set; }
        public virtual DbSet<M_Employee> M_Employee { get; set; }
        public virtual DbSet<M_Menu> M_Menu { get; set; }
        public virtual DbSet<M_Menu_Access> M_Menu_Access { get; set; }
        public virtual DbSet<M_Product> M_Product { get; set; }
        public virtual DbSet<M_Role> M_Role { get; set; }
        public virtual DbSet<M_Souvenir> M_Souvenir { get; set; }
        public virtual DbSet<M_Unit> M_Unit { get; set; }
        public virtual DbSet<M_User> M_User { get; set; }
        public virtual DbSet<M_User_Claim> M_User_Claim { get; set; }
        public virtual DbSet<M_User_Login> M_User_Login { get; set; }
        public virtual DbSet<T_Design> T_Design { get; set; }
        public virtual DbSet<T_Design_Item> T_Design_Item { get; set; }
        public virtual DbSet<T_Design_Item_File> T_Design_Item_File { get; set; }
        public virtual DbSet<T_Event> T_Event { get; set; }
        public virtual DbSet<T_Promotion> T_Promotion { get; set; }
        public virtual DbSet<T_Promotion_Item> T_Promotion_Item { get; set; }
        public virtual DbSet<T_Promotion_Item_File> T_Promotion_Item_File { get; set; }
        public virtual DbSet<T_Souvenir> T_Souvenir { get; set; }
        public virtual DbSet<T_Souvenir_Item> T_Souvenir_Item { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<M_Company>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<M_Company>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<M_Company>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<M_Company>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<M_Company>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<M_Company>()
                .Property(e => e.Create_By)
                .IsUnicode(false);

            modelBuilder.Entity<M_Company>()
                .Property(e => e.Update_By)
                .IsUnicode(false);

            modelBuilder.Entity<M_Company>()
                .HasMany(e => e.M_Employee)
                .WithRequired(e => e.M_Company)
                .HasForeignKey(e => e.M_Company_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<M_Employee>()
                .Property(e => e.Employee_Number)
                .IsUnicode(false);

            modelBuilder.Entity<M_Employee>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<M_Employee>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<M_Employee>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<M_Employee>()
                .Property(e => e.Create_By)
                .IsUnicode(false);

            modelBuilder.Entity<M_Employee>()
                .Property(e => e.Update_By)
                .IsUnicode(false);

            modelBuilder.Entity<M_Employee>()
                .HasMany(e => e.M_User)
                .WithRequired(e => e.M_Employee)
                .HasForeignKey(e => e.M_Employee_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<M_Employee>()
                .HasMany(e => e.T_Design_Item)
                .WithRequired(e => e.M_Employee)
                .HasForeignKey(e => e.Request_Pic)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<M_Employee>()
                .HasMany(e => e.T_Design)
                .WithRequired(e => e.M_Employee)
                .HasForeignKey(e => e.Request_By)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<M_Employee>()
                .HasMany(e => e.T_Design1)
                .WithOptional(e => e.M_Employee1)
                .HasForeignKey(e => e.Approved_By);

            modelBuilder.Entity<M_Employee>()
                .HasMany(e => e.T_Design2)
                .WithOptional(e => e.M_Employee2)
                .HasForeignKey(e => e.Assign_To);

            modelBuilder.Entity<M_Employee>()
                .HasMany(e => e.T_Event)
                .WithRequired(e => e.M_Employee)
                .HasForeignKey(e => e.Request_By)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<M_Employee>()
                .HasMany(e => e.T_Event1)
                .WithOptional(e => e.M_Employee1)
                .HasForeignKey(e => e.Approved_By);

            modelBuilder.Entity<M_Employee>()
                .HasMany(e => e.T_Event2)
                .WithOptional(e => e.M_Employee2)
                .HasForeignKey(e => e.Assign_To);

            modelBuilder.Entity<M_Employee>()
                .HasMany(e => e.T_Promotion_Item)
                .WithRequired(e => e.M_Employee)
                .HasForeignKey(e => e.Request_Pic)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<M_Employee>()
                .HasMany(e => e.T_Promotion)
                .WithOptional(e => e.M_Employee)
                .HasForeignKey(e => e.Request_By);

            modelBuilder.Entity<M_Employee>()
                .HasMany(e => e.T_Promotion1)
                .WithOptional(e => e.M_Employee1)
                .HasForeignKey(e => e.Approved_By);

            modelBuilder.Entity<M_Employee>()
                .HasMany(e => e.T_Promotion2)
                .WithOptional(e => e.M_Employee2)
                .HasForeignKey(e => e.Assign_To);

            modelBuilder.Entity<M_Employee>()
                .HasMany(e => e.T_Souvenir)
                .WithOptional(e => e.M_Employee)
                .HasForeignKey(e => e.Received_By);

            modelBuilder.Entity<M_Employee>()
                .HasMany(e => e.T_Souvenir1)
                .WithOptional(e => e.M_Employee1)
                .HasForeignKey(e => e.Approved_By);

            modelBuilder.Entity<M_Employee>()
                .HasMany(e => e.T_Souvenir2)
                .WithOptional(e => e.M_Employee2)
                .HasForeignKey(e => e.Received_By);

            modelBuilder.Entity<M_Employee>()
                .HasMany(e => e.T_Souvenir3)
                .WithOptional(e => e.M_Employee3)
                .HasForeignKey(e => e.Settlement_By);

            modelBuilder.Entity<M_Employee>()
                .HasMany(e => e.T_Souvenir4)
                .WithOptional(e => e.M_Employee4)
                .HasForeignKey(e => e.Settlement_Approved_By);

            modelBuilder.Entity<M_Menu>()
                .HasMany(e => e.M_Menu_Access)
                .WithRequired(e => e.M_Menu)
                .HasForeignKey(e => e.M_Menu_Id);

            modelBuilder.Entity<M_Menu>()
                .HasMany(e => e.M_Menu1)
                .WithOptional(e => e.M_Menu2)
                .HasForeignKey(e => e.Parent_Id);

            modelBuilder.Entity<M_Product>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<M_Product>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<M_Product>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<M_Product>()
                .Property(e => e.Create_By)
                .IsUnicode(false);

            modelBuilder.Entity<M_Product>()
                .Property(e => e.Update_By)
                .IsUnicode(false);

            modelBuilder.Entity<M_Product>()
                .HasMany(e => e.T_Design_Item)
                .WithRequired(e => e.M_Product)
                .HasForeignKey(e => e.M_Product_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<M_Product>()
                .HasMany(e => e.T_Promotion_Item)
                .WithRequired(e => e.M_Product)
                .HasForeignKey(e => e.M_Product_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<M_Role>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<M_Role>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<M_Role>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<M_Role>()
                .Property(e => e.Create_By)
                .IsUnicode(false);

            modelBuilder.Entity<M_Role>()
                .Property(e => e.Update_By)
                .IsUnicode(false);

            modelBuilder.Entity<M_Role>()
                .HasMany(e => e.M_Menu_Access)
                .WithRequired(e => e.M_Role)
                .HasForeignKey(e => e.M_Role_Id);

            modelBuilder.Entity<M_Role>()
                .HasMany(e => e.M_User)
                .WithOptional(e => e.M_Role)
                .HasForeignKey(e => e.M_Role_Id);

            modelBuilder.Entity<M_Role>()
                .HasMany(e => e.M_User1)
                .WithMany(e => e.M_Role1)
                .Map(m => m.ToTable("M_User_Role").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<M_Souvenir>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<M_Souvenir>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<M_Souvenir>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<M_Souvenir>()
                .Property(e => e.Create_By)
                .IsUnicode(false);

            modelBuilder.Entity<M_Souvenir>()
                .Property(e => e.Update_By)
                .IsUnicode(false);

            modelBuilder.Entity<M_Souvenir>()
                .HasMany(e => e.T_Souvenir_Item)
                .WithRequired(e => e.M_Souvenir)
                .HasForeignKey(e => e.M_Souvenir_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<M_Unit>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<M_Unit>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<M_Unit>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<M_Unit>()
                .Property(e => e.Create_By)
                .IsUnicode(false);

            modelBuilder.Entity<M_Unit>()
                .Property(e => e.Update_By)
                .IsUnicode(false);

            modelBuilder.Entity<M_Unit>()
                .HasMany(e => e.M_Souvenir)
                .WithRequired(e => e.M_Unit)
                .HasForeignKey(e => e.M_Unit_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<M_User>()
                .Property(e => e.Create_By)
                .IsUnicode(false);

            modelBuilder.Entity<M_User>()
                .Property(e => e.Update_By)
                .IsUnicode(false);

            modelBuilder.Entity<M_User>()
                .HasMany(e => e.M_User_Claim)
                .WithRequired(e => e.M_User)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<M_User>()
                .HasMany(e => e.M_User_Login)
                .WithRequired(e => e.M_User)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<T_Design>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<T_Design>()
                .Property(e => e.Title_Header)
                .IsUnicode(false);

            modelBuilder.Entity<T_Design>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<T_Design>()
                .Property(e => e.Reject_Reason)
                .IsUnicode(false);

            modelBuilder.Entity<T_Design>()
                .Property(e => e.Create_By)
                .IsUnicode(false);

            modelBuilder.Entity<T_Design>()
                .Property(e => e.Update_By)
                .IsUnicode(false);

            modelBuilder.Entity<T_Design>()
                .HasMany(e => e.T_Design_Item)
                .WithRequired(e => e.T_Design)
                .HasForeignKey(e => e.T_Design_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_Design>()
                .HasMany(e => e.T_Promotion)
                .WithOptional(e => e.T_Design)
                .HasForeignKey(e => e.T_Design_Id);

            modelBuilder.Entity<T_Design_Item>()
                .Property(e => e.Title_Item)
                .IsUnicode(false);

            modelBuilder.Entity<T_Design_Item>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<T_Design_Item>()
                .Property(e => e.Create_By)
                .IsUnicode(false);

            modelBuilder.Entity<T_Design_Item>()
                .Property(e => e.Update_By)
                .IsUnicode(false);

            modelBuilder.Entity<T_Design_Item>()
                .HasMany(e => e.T_Design_Item_File)
                .WithRequired(e => e.T_Design_Item)
                .HasForeignKey(e => e.T_Design_Item_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_Design_Item>()
                .HasMany(e => e.T_Promotion_Item)
                .WithOptional(e => e.T_Design_Item)
                .HasForeignKey(e => e.T_Design_Item_Id);

            modelBuilder.Entity<T_Design_Item_File>()
                .Property(e => e.Filename)
                .IsUnicode(false);

            modelBuilder.Entity<T_Design_Item_File>()
                .Property(e => e.Size)
                .IsUnicode(false);

            modelBuilder.Entity<T_Design_Item_File>()
                .Property(e => e.Extention)
                .IsUnicode(false);

            modelBuilder.Entity<T_Design_Item_File>()
                .Property(e => e.Create_By)
                .IsUnicode(false);

            modelBuilder.Entity<T_Design_Item_File>()
                .Property(e => e.Update_By)
                .IsUnicode(false);

            modelBuilder.Entity<T_Event>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<T_Event>()
                .Property(e => e.Event_Name)
                .IsUnicode(false);

            modelBuilder.Entity<T_Event>()
                .Property(e => e.Place)
                .IsUnicode(false);

            modelBuilder.Entity<T_Event>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<T_Event>()
                .Property(e => e.Reject_Reason)
                .IsUnicode(false);

            modelBuilder.Entity<T_Event>()
                .Property(e => e.Create_By)
                .IsUnicode(false);

            modelBuilder.Entity<T_Event>()
                .Property(e => e.Update_By)
                .IsUnicode(false);

            modelBuilder.Entity<T_Event>()
                .HasMany(e => e.T_Design)
                .WithRequired(e => e.T_Event)
                .HasForeignKey(e => e.T_Event_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_Event>()
                .HasMany(e => e.T_Promotion)
                .WithRequired(e => e.T_Event)
                .HasForeignKey(e => e.T_Event_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_Promotion>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<T_Promotion>()
                .Property(e => e.Flag_Design)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<T_Promotion>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<T_Promotion>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<T_Promotion>()
                .Property(e => e.Reject_Reason)
                .IsUnicode(false);

            modelBuilder.Entity<T_Promotion>()
                .Property(e => e.Create_By)
                .IsUnicode(false);

            modelBuilder.Entity<T_Promotion>()
                .Property(e => e.Update_By)
                .IsUnicode(false);

            //modelBuilder.Entity<T_Promotion>()
            //    .HasMany(e => e.T_Promotion_Item_File)
            //    .WithRequired(e => e.T_Promotion)
            //    .HasForeignKey(e => e.T_Promotion_id)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_Promotion>()
                .HasMany(e => e.T_Promotion_Item)
                .WithRequired(e => e.T_Promotion)
                .HasForeignKey(e => e.T_Promotion_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_Promotion_Item>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<T_Promotion_Item>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<T_Promotion_Item>()
                .Property(e => e.Create_By)
                .IsUnicode(false);

            modelBuilder.Entity<T_Promotion_Item>()
                .Property(e => e.Update_By)
                .IsUnicode(false);

            modelBuilder.Entity<T_Promotion_Item_File>()
                .Property(e => e.Filename)
                .IsUnicode(false);

            modelBuilder.Entity<T_Promotion_Item_File>()
                .Property(e => e.Size)
                .IsUnicode(false);

            modelBuilder.Entity<T_Promotion_Item_File>()
                .Property(e => e.Extention)
                .IsUnicode(false);

            modelBuilder.Entity<T_Promotion_Item_File>()
                .Property(e => e.ImagePath)
                .IsUnicode(false);

            modelBuilder.Entity<T_Promotion_Item_File>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<T_Promotion_Item_File>()
                .Property(e => e.Create_By)
                .IsUnicode(false);

            modelBuilder.Entity<T_Promotion_Item_File>()
                .Property(e => e.Update_By)
                .IsUnicode(false);

            modelBuilder.Entity<T_Souvenir>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<T_Souvenir>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<T_Souvenir>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<T_Souvenir>()
                .Property(e => e.Reject_Reason)
                .IsUnicode(false);

            modelBuilder.Entity<T_Souvenir>()
                .Property(e => e.Create_By)
                .IsUnicode(false);

            modelBuilder.Entity<T_Souvenir>()
                .Property(e => e.Update_By)
                .IsUnicode(false);

            modelBuilder.Entity<T_Souvenir>()
                .HasMany(e => e.T_Souvenir_Item)
                .WithRequired(e => e.T_Souvenir)
                .HasForeignKey(e => e.T_Souvenir_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_Souvenir_Item>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<T_Souvenir_Item>()
                .Property(e => e.Create_By)
                .IsUnicode(false);

            modelBuilder.Entity<T_Souvenir_Item>()
                .Property(e => e.Update_By)
                .IsUnicode(false);
        }
    }
}
