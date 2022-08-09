using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CustomerRelationshipManagement.UI.Models.DBContext
{
    public partial class CustomerRelationshipManagementDBContext : DbContext
    {
        public CustomerRelationshipManagementDBContext()
        {
        }

        public CustomerRelationshipManagementDBContext(DbContextOptions<CustomerRelationshipManagementDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerCall> CustomerCalls { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<CallByName> CallByNames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-6B3L7RE;Database=CustomerRelationshipManagementDB;Trusted_Connection=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerNo);

                entity.ToTable("Customer");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerSurname)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.PostCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomerCall>(entity =>
            {
                entity.ToTable("CustomerCall");
                entity.HasKey(e => e.CustomerCallId);
                entity.Property(e => e.CustomerCallId)
                    .ValueGeneratedNever()
                    .HasColumnName("CustomerCall_id");

                entity.Property(e => e.DateOfCall).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Subject)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TimeOfCall).HasColumnType("datetime");

                entity.HasOne(d => d.CustomerNoNavigation)
                    .WithMany(p => p.CustomerCalls)
                    .HasForeignKey(d => d.CustomerNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerCall_CustomerNo");
            });

            modelBuilder.Entity<CallByName>(entity =>
            {
                entity.HasKey(e => e.CustomerCall_id);
                entity.Property(e => e.CustomerName)
                    .ValueGeneratedNever()
                    .HasColumnName("CustomerName");

                entity.Property(e => e.DateOfCall).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Subject)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TimeOfCall).HasColumnType("datetime");

            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.ToTable("Registration");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Rolename)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
