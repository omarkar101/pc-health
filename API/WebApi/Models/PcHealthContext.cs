using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApi.Models
{
    public partial class PcHealthContext : DbContext
    {
        //public PcHealthContext()
        //{
        //}

        public PcHealthContext(DbContextOptions<PcHealthContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<Pc> Pcs { get; set; }
        public virtual DbSet<Service> Services { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("Server=localhost;port=5555;Database=PcHealth;username=root;password=omar123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.AdminCredentialsUsername)
                    .HasName("PRIMARY");

                entity.ToTable("admin");

                entity.Property(e => e.AdminCredentialsUsername).HasMaxLength(45);

                entity.Property(e => e.AdminFirstName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.AdminLastName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.HasOne(d => d.AdminCredentialsUsernameNavigation)
                    .WithOne(p => p.Admin)
                    .HasForeignKey<Admin>(d => d.AdminCredentialsUsername)
                    .HasConstraintName("fk_Admin_Credentials1");
            });

            modelBuilder.Entity<Credential>(entity =>
            {
                entity.HasKey(e => e.CredentialsUsername)
                    .HasName("PRIMARY");

                entity.ToTable("credentials");

                entity.HasIndex(e => e.CredentialsUsername, "CredentialsUsername_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.CredentialsUsername).HasMaxLength(45);

                entity.Property(e => e.CredentialsPassword)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CredentialsSalt)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Pc>(entity =>
            {
                entity.ToTable("pc");

                entity.HasIndex(e => e.AdminCredentialsUsername, "AdminCredentialsUsername_idx");

                entity.HasIndex(e => e.PcId, "idPc_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.AdminCredentialsUsername)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.PcOs)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("PcOS");

                entity.Property(e => e.PcUsername)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.HasOne(d => d.AdminCredentialsUsernameNavigation)
                    .WithMany(p => p.Pcs)
                    .HasForeignKey(d => d.AdminCredentialsUsername)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AdminCredentialsUsername");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => new { e.ServiceName, e.PcId })
                    .HasName("PRIMARY");

                entity.ToTable("service");

                entity.HasIndex(e => e.PcId, "PcId_idx");

                entity.Property(e => e.ServiceName).HasMaxLength(45);

                entity.HasOne(d => d.Pc)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.PcId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PcId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
