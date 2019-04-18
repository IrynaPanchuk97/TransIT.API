using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models.Extensions;

namespace TransIT.DAL.Models
{
    using Entities;
    
    public partial class TransITDBContext : DbContext
    {
        public TransITDBContext() {}

        public TransITDBContext(DbContextOptions<TransITDBContext> options)
            : base(options) {}

        public virtual DbSet<ActionType> ActionType { get; set; }
        public virtual DbSet<Bill> Bill { get; set; }
        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<Issue> Issue { get; set; }
        public virtual DbSet<IssueLog> IssueLog { get; set; }
        public virtual DbSet<Malfunction> Malfunction { get; set; }
        public virtual DbSet<MalfunctionGroup> MalfunctionGroup { get; set; }
        public virtual DbSet<MalfunctionSubgroup> MalfunctionSubgroup { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<Token> Token { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<VehicleType> VehicleType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            #region Seeding

            modelBuilder.SeedRoles();
            modelBuilder.SeedStates();            

            #endregion

            #region Configuration

            modelBuilder.Entity<ActionType>(entity =>
            {
                entity.ToTable("ACTION_TYPE");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__ACTION_T__D9C1FA00D8EDC403")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.ActionTypeCreate)
                    .HasForeignKey(d => d.CreateId)
                    .HasConstraintName("FK_CREATE_ACTION_TYPE_USER");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.ActionTypeMod)
                    .HasForeignKey(d => d.ModId)
                    .HasConstraintName("FK_MOD_ACTION_TYPE_USER");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("BILL");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.DocumentId).HasColumnName("DOCUMENT_ID");

                entity.Property(e => e.IssueId).HasColumnName("ISSUE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Sum)
                    .HasColumnName("SUM")
                    .HasColumnType("decimal(20, 2)");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.BillCreate)
                    .HasForeignKey(d => d.CreateId)
                    .HasConstraintName("FK_CREATE_BILL_USER");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.DocumentId)
                    .HasConstraintName("FK_BILL_DOCUMENT");

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.IssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BILL_ISSUE");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.BillMod)
                    .HasForeignKey(d => d.ModId)
                    .HasConstraintName("FK_MOD_BILL_USER");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("DOCUMENT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.Description).HasColumnName("DESCRIPTION");

                entity.Property(e => e.IssueLogId).HasColumnName("ISSUE_LOG_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.DocumentCreate)
                    .HasForeignKey(d => d.CreateId)
                    .HasConstraintName("FK_CREATE_DOCUMENT_USER");

                entity.HasOne(d => d.IssueLog)
                    .WithMany(p => p.Document)
                    .HasForeignKey(d => d.IssueLogId)
                    .HasConstraintName("FK_DOCUMENT_ISSUE_LOG");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.DocumentMod)
                    .HasForeignKey(d => d.ModId)
                    .HasConstraintName("FK_MOD_DOCUMENT_USER");
            });

            modelBuilder.Entity<Issue>(entity =>
            {
                entity.ToTable("ISSUE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AssignedTo).HasColumnName("ASSIGNED_TO");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.Deadline)
                    .HasColumnName("DEADLINE")
                    .HasColumnType("datetime");

                entity.Property(e => e.MalfunctionId).HasColumnName("MALFUNCTION_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.StateId)
                    .HasColumnName("STATE_ID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Summary)
                    .HasColumnName("SUMMARY")
                    .HasColumnType("text");

                entity.Property(e => e.VehicleId).HasColumnName("VEHICLE_ID");

                entity.Property(e => e.Warranty).HasColumnName("WARRANTY");

                entity.HasOne(d => d.AssignedToNavigation)
                    .WithMany(p => p.IssueAssignedToNavigation)
                    .HasForeignKey(d => d.AssignedTo);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.IssueCreate)
                    .HasForeignKey(d => d.CreateId)
                    .HasConstraintName("FK_CREATE_ISSUE_USER");

                entity.HasOne(d => d.Malfunction)
                    .WithMany(p => p.Issue)
                    .HasForeignKey(d => d.MalfunctionId)
                    .HasConstraintName("FK_ISSUE_MALFUNCTION");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.IssueMod)
                    .HasForeignKey(d => d.ModId)
                    .HasConstraintName("FK_MOD_ISSUE_USER");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Issue)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ISSUE_STATE");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Issue)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ISSUE_VEHICLE");
            });

            modelBuilder.Entity<IssueLog>(entity =>
            {
                entity.ToTable("ISSUE_LOG");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActionTypeId).HasColumnName("ACTION_TYPE_ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasColumnType("text");

                entity.Property(e => e.Expenses)
                    .HasColumnName("EXPENSES")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.IssueId).HasColumnName("ISSUE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.NewStateId).HasColumnName("NEW_STATE_ID");

                entity.Property(e => e.OldStateId).HasColumnName("OLD_STATE_ID");

                entity.Property(e => e.SupplierId).HasColumnName("SUPPLIER_ID");

                entity.HasOne(d => d.ActionType)
                    .WithMany(p => p.IssueLog)
                    .HasForeignKey(d => d.ActionTypeId)
                    .HasConstraintName("FK_ISSUE_LOG_ACTION_TYPE");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.IssueLogCreate)
                    .HasForeignKey(d => d.CreateId)
                    .HasConstraintName("FK_CREATE_ISSUE_LOG_USER");

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.IssueLog)
                    .HasForeignKey(d => d.IssueId)
                    .HasConstraintName("FK_ISSUE_LOG_ISSUE");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.IssueLogMod)
                    .HasForeignKey(d => d.ModId)
                    .HasConstraintName("FK_MOD_ISSUE_LOG_USER");

                entity.HasOne(d => d.NewState)
                    .WithMany(p => p.IssueLogNewState)
                    .HasForeignKey(d => d.NewStateId)
                    .HasConstraintName("FK_NEW_ISSUE_LOG_STATE");

                entity.HasOne(d => d.OldState)
                    .WithMany(p => p.IssueLogOldState)
                    .HasForeignKey(d => d.OldStateId)
                    .HasConstraintName("FK_OLD_ISSUE_LOG_STATE");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.IssueLog)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_ISSUE_LOG_SUPPLIER");
            });

            modelBuilder.Entity<Malfunction>(entity =>
            {
                entity.ToTable("MALFUNCTION");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.MalfunctionSubgroupId).HasColumnName("MALFUNCTION_SUBGROUP_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.MalfunctionCreate)
                    .HasForeignKey(d => d.CreateId)
                    .HasConstraintName("FK_CREATE_MALFUNCTION_ROLE");

                entity.HasOne(d => d.MalfunctionSubgroup)
                    .WithMany(p => p.Malfunction)
                    .HasForeignKey(d => d.MalfunctionSubgroupId)
                    .HasConstraintName("FK_MALFUNCTION_SUBGROUP_MALFUNCTION_SUBGROUP");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.MalfunctionMod)
                    .HasForeignKey(d => d.ModId)
                    .HasConstraintName("FK_MOD_MALFUNCTION_USER");
            });

            modelBuilder.Entity<MalfunctionGroup>(entity =>
            {
                entity.ToTable("MALFUNCTION_GROUP");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.MalfunctionGroupCreate)
                    .HasForeignKey(d => d.CreateId)
                    .HasConstraintName("FK__MALFUNCTI__CREAT__73BA3083");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.MalfunctionGroupMod)
                    .HasForeignKey(d => d.ModId)
                    .HasConstraintName("FK__MALFUNCTI__MOD_I__74AE54BC");
            });

            modelBuilder.Entity<MalfunctionSubgroup>(entity =>
            {
                entity.ToTable("MALFUNCTION_SUBGROUP");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.MalfunctionGroupId).HasColumnName("MALFUNCTION_GROUP_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.MalfunctionSubgroupCreate)
                    .HasForeignKey(d => d.CreateId)
                    .HasConstraintName("FK_CREATE_MALFUNCTION_SUBGROUP_USER");

                entity.HasOne(d => d.MalfunctionGroup)
                    .WithMany(p => p.MalfunctionSubgroup)
                    .HasForeignKey(d => d.MalfunctionGroupId)
                    .HasConstraintName("FK_MALFUNCTION_SUBGROUP_MALFUNCTION_GROUP");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.MalfunctionSubgroupMod)
                    .HasForeignKey(d => d.ModId)
                    .HasConstraintName("FK_MOD_MALFUNCTION_SUBGROUP_USER");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__ROLE__D9C1FA0001C36FF2")
                    .IsUnique();

                entity.HasIndex(e => e.TransName)
                    .HasName("UQ__ROLE__DF65CE2719872B43")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.TransName)
                    .IsRequired()
                    .HasColumnName("TRANS_NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.RoleCreate)
                    .HasForeignKey(d => d.CreateId)
                    .HasConstraintName("FK_CREATE_ROLE_USER");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.RoleMod)
                    .HasForeignKey(d => d.ModId)
                    .HasConstraintName("FK_MOD_ROLE_USER");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("STATE");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__STATE__D9C1FA004DDE9B3B")
                    .IsUnique();

                entity.HasIndex(e => e.TransName)
                    .HasName("UQ__STATE__DF65CE27E730D668")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.TransName)
                    .HasColumnName("TRANS_NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("SUPPLIER");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__SUPPLIER__D9C1FA0021944BFA")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.SupplierCreate)
                    .HasForeignKey(d => d.CreateId)
                    .HasConstraintName("FK_CREATE_SUPPLIER_USER");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.SupplierMod)
                    .HasForeignKey(d => d.ModId)
                    .HasConstraintName("FK_MOD_SUPPLIER_USER");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.ToTable("TOKEN");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.RefreshToken).HasColumnName("REFRESH_TOKEN");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.TokenCreate)
                    .HasForeignKey(d => d.CreateId)
                    .HasConstraintName("FK_CREATE_TOKEN_USER");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.TokenMod)
                    .HasForeignKey(d => d.ModId)
                    .HasConstraintName("FK_MOD_TOKEN_USER");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER");

                entity.HasIndex(e => e.Login)
                    .HasName("UQ__USER__E39E2665C934E6A0")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .HasColumnName("FIRST_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .HasColumnName("LAST_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("LOGIN")
                    .HasMaxLength(100);

                entity.Property(e => e.MiddleName)
                    .HasColumnName("MIDDLE_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(100);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("PHONE_NUMBER")
                    .HasMaxLength(15);

                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.InverseCreate)
                    .HasForeignKey(d => d.CreateId)
                    .HasConstraintName("FK_CREATE_USER_ROLE");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.InverseMod)
                    .HasForeignKey(d => d.ModId)
                    .HasConstraintName("FK_MOD_USER_ROLE");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_USER_ROLE");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("VEHICLE");

                entity.HasIndex(e => e.InventoryId)
                    .HasName("UQ__VEHICLE__65E4F1E6C39150D1")
                    .IsUnique();

                entity.HasIndex(e => e.RegNum)
                    .HasName("UQ__VEHICLE__0189DFA30AFC74AE")
                    .IsUnique();

                entity.HasIndex(e => e.Vincode)
                    .HasName("UQ__VEHICLE__5C84FBD285375A1F")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Brand)
                    .HasColumnName("BRAND")
                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.InventoryId)
                    .HasColumnName("INVENTORY_ID")
                    .HasMaxLength(40);

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Model)
                    .HasColumnName("MODEL")
                    .HasMaxLength(50);

                entity.Property(e => e.RegNum)
                    .HasColumnName("REG_NUM")
                    .HasMaxLength(8);

                entity.Property(e => e.VehicleTypeId).HasColumnName("VEHICLE_TYPE_ID");

                entity.Property(e => e.Vincode)
                    .HasColumnName("VINCODE")
                    .HasMaxLength(20);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.VehicleCreate)
                    .HasForeignKey(d => d.CreateId)
                    .HasConstraintName("FK_MOD_VEHICLE_USER");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.VehicleMod)
                    .HasForeignKey(d => d.ModId)
                    .HasConstraintName("FK_MOD_VEHICLE_ROLE");

                entity.HasOne(d => d.VehicleType)
                    .WithMany(p => p.Vehicle)
                    .HasForeignKey(d => d.VehicleTypeId)
                    .HasConstraintName("FK_VEHICLE_VEHICLE_TYPE");
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.ToTable("VEHICLE_TYPE");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__VEHICLE___D9C1FA0095358636")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.VehicleTypeCreate)
                    .HasForeignKey(d => d.CreateId)
                    .HasConstraintName("FK_MOD_VEHICLE_TYPE_USER");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.VehicleTypeMod)
                    .HasForeignKey(d => d.ModId)
                    .HasConstraintName("FK_MOD_VEHICLE_TYPE_ROLE");
            });
            
            #endregion
        }
    }
}
