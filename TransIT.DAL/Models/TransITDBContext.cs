﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TransIT.DAL.Models
{
    public partial class TransITDBContext : DbContext
    {
        public TransITDBContext()
        {
        }

        public TransITDBContext(DbContextOptions<TransITDBContext> options)
            : base(options)
        {
        }

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
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<VehicleType> VehicleType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TransITDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<ActionType>(entity =>
            {
                entity.ToTable("ACTION_TYPE");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__ACTION_T__D9C1FA00A91243E9")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.ActionTypeCreate)
                    .HasForeignKey(d => d.CreateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CREATE_ACTION_TYPE_USER");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.ActionTypeMod)
                    .HasForeignKey(d => d.ModId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOD_ACTION_TYPE_USER");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("BILL");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.DocumentId).HasColumnName("DOCUMENT_ID");

                entity.Property(e => e.IssueId).HasColumnName("ISSUE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Sum)
                    .HasColumnName("SUM")
                    .HasColumnType("decimal(20, 2)");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.BillCreate)
                    .HasForeignKey(d => d.CreateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CREATE_BILL_USER");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BILL_DOCUMENT");

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.IssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BILL_ISSUE");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.BillMod)
                    .HasForeignKey(d => d.ModId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOD_BILL_USER");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("DOCUMENT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.Description).HasColumnName("DESCRIPTION");

                entity.Property(e => e.IssueLogId).HasColumnName("ISSUE_LOG_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.DocumentCreate)
                    .HasForeignKey(d => d.CreateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CREATE_DOCUMENT_USER");

                entity.HasOne(d => d.IssueLog)
                    .WithMany(p => p.Document)
                    .HasForeignKey(d => d.IssueLogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DOCUMENT_ISSUE_LOG");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.DocumentMod)
                    .HasForeignKey(d => d.ModId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOD_DOCUMENT_USER");
            });

            modelBuilder.Entity<Issue>(entity =>
            {
                entity.ToTable("ISSUE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AssignedTo).HasColumnName("ASSIGNED_TO");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.Deadline)
                    .HasColumnName("DEADLINE")
                    .HasColumnType("date");

                entity.Property(e => e.MalfunctionId).HasColumnName("MALFUNCTION_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.StateId).HasColumnName("STATE_ID");

                entity.Property(e => e.Summary)
                    .IsRequired()
                    .HasColumnName("SUMMARY")
                    .HasColumnType("text");

                entity.Property(e => e.VehicleId).HasColumnName("VEHICLE_ID");

                entity.Property(e => e.Warranty).HasColumnName("WARRANTY");

                entity.HasOne(d => d.AssignedToNavigation)
                    .WithMany(p => p.IssueAssignedToNavigation)
                    .HasForeignKey(d => d.AssignedTo)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.IssueCreate)
                    .HasForeignKey(d => d.CreateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CREATE_ISSUE_USER");

                entity.HasOne(d => d.Malfunction)
                    .WithMany(p => p.Issue)
                    .HasForeignKey(d => d.MalfunctionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ISSUE_MALFUNCTION");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.IssueMod)
                    .HasForeignKey(d => d.ModId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
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
                    .HasColumnType("date");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("DESCRIPTION")
                    .HasColumnType("text");

                entity.Property(e => e.Expenses)
                    .HasColumnName("EXPENSES")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.IssueId).HasColumnName("ISSUE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.NewStateId).HasColumnName("NEW_STATE_ID");

                entity.Property(e => e.OldStateId).HasColumnName("OLD_STATE_ID");

                entity.Property(e => e.SupplierId).HasColumnName("SUPPLIER_ID");

                entity.HasOne(d => d.ActionType)
                    .WithMany(p => p.IssueLog)
                    .HasForeignKey(d => d.ActionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ISSUE_LOG_ACTION_TYPE");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.IssueLogCreate)
                    .HasForeignKey(d => d.CreateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CREATE_ISSUE_LOG_USER");

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.IssueLog)
                    .HasForeignKey(d => d.IssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ISSUE_LOG_ISSUE");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.IssueLogMod)
                    .HasForeignKey(d => d.ModId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOD_ISSUE_LOG_USER");

                entity.HasOne(d => d.NewState)
                    .WithMany(p => p.IssueLogNewState)
                    .HasForeignKey(d => d.NewStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NEW_ISSUE_LOG_STATE");

                entity.HasOne(d => d.OldState)
                    .WithMany(p => p.IssueLogOldState)
                    .HasForeignKey(d => d.OldStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLD_ISSUE_LOG_STATE");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.IssueLog)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ISSUE_LOG_SUPPLIER");
            });

            modelBuilder.Entity<Malfunction>(entity =>
            {
                entity.ToTable("MALFUNCTION");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.MalfunctionSubgroupId).HasColumnName("MALFUNCTION_SUBGROUP_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.MalfunctionCreate)
                    .HasForeignKey(d => d.CreateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CREATE_MALFUNCTION_ROLE");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.MalfunctionMod)
                    .HasForeignKey(d => d.ModId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOD_MALFUNCTION_USER");
            });

            modelBuilder.Entity<MalfunctionGroup>(entity =>
            {
                entity.ToTable("MALFUNCTION_GROUP");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<MalfunctionSubgroup>(entity =>
            {
                entity.ToTable("MALFUNCTION_SUBGROUP");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.MalfunctionGroupId).HasColumnName("MALFUNCTION_GROUP_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.MalfunctionSubgroupCreate)
                    .HasForeignKey(d => d.CreateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CREATE_MALFUNCTION_SUBGROUP_USER");

                entity.HasOne(d => d.MalfunctionGroup)
                    .WithMany(p => p.MalfunctionSubgroup)
                    .HasForeignKey(d => d.MalfunctionGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MALFUNCTION_SUBGROUP_MALFUNCTION_GROUP");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.MalfunctionSubgroupMod)
                    .HasForeignKey(d => d.ModId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOD_MALFUNCTION_SUBGROUP_USER");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__ROLE__D9C1FA005C4BDA82")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.RoleCreate)
                    .HasForeignKey(d => d.CreateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CREATE_ROLE_USER");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.RoleMod)
                    .HasForeignKey(d => d.ModId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOD_ROLE_USER");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("STATE");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__STATE__D9C1FA003F189068")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("SUPPLIER");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__SUPPLIER__D9C1FA00BB2E795D")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.SupplierCreate)
                    .HasForeignKey(d => d.CreateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CREATE_SUPPLIER_USER");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.SupplierMod)
                    .HasForeignKey(d => d.ModId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOD_SUPPLIER_USER");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("LOGIN")
                    .HasMaxLength(100);

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(100);

                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.InverseCreate)
                    .HasForeignKey(d => d.CreateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CREATE_USER_ROLE");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.InverseMod)
                    .HasForeignKey(d => d.ModId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOD_USER_ROLE");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.InverseRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_ROLE");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("VEHICLE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Brand)
                    .HasColumnName("BRAND")
                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.InventoryId)
                    .HasColumnName("INVENTORY_ID")
                    .HasMaxLength(40);

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("date");

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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOD_VEHICLE_USER");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.VehicleMod)
                    .HasForeignKey(d => d.ModId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOD_VEHICLE_ROLE");

                entity.HasOne(d => d.VehicleType)
                    .WithMany(p => p.Vehicle)
                    .HasForeignKey(d => d.VehicleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VEHICLE_VEHICLE_TYPE");
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.ToTable("VEHICLE_TYPE");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__VEHICLE___D9C1FA007B66BE06")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.CreateId).HasColumnName("CREATE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.ModId).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.VehicleTypeCreate)
                    .HasForeignKey(d => d.CreateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOD_VEHICLE_TYPE_USER");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.VehicleTypeMod)
                    .HasForeignKey(d => d.ModId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOD_VEHICLE_TYPE_ROLE");
            });
        }
    }
}
