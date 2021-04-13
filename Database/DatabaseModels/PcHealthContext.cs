using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Database.DatabaseModels
{
    public partial class PcHealthContext : DbContext
    {
        public PcHealthContext(DbContextOptions<PcHealthContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<AdminHasPc> AdminHasPcs { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<LastMinute> LastMinutes { get; set; }
        public virtual DbSet<Pc> Pcs { get; set; }
        public virtual DbSet<Service> Services { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=bsvhzy1r6yrrhqucx9o9-mysql.services.clever-cloud.com;port=3306;database=bsvhzy1r6yrrhqucx9o9;username=uclrhckmdjsm76tx;password=pUd7Fb8karg1EjPc6hVd");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.AdminCredentialsUsername)
                    .HasName("PRIMARY");

                entity.ToTable("Admin");

                entity.Property(e => e.AdminCredentialsUsername)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AdminFirstName)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AdminLastName)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.AdminCredentialsUsernameNavigation)
                    .WithOne(p => p.Admin)
                    .HasForeignKey<Admin>(d => d.AdminCredentialsUsername)
                    .HasConstraintName("fk_Admin_Credentials1");
            });

            modelBuilder.Entity<AdminHasPc>(entity =>
            {
                entity.HasKey(e => new { e.AdminCredentialsUsername, e.PcId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("Admin_has_Pc");

                entity.HasIndex(e => e.AdminCredentialsUsername, "fk_Admin_has_Pc_Admin1_idx");

                entity.HasIndex(e => e.PcId, "fk_Admin_has_Pc_Pc1_idx");

                entity.Property(e => e.AdminCredentialsUsername)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PcId)
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.AdminCredentialsUsernameNavigation)
                    .WithMany(p => p.AdminHasPcs)
                    .HasForeignKey(d => d.AdminCredentialsUsername)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Admin_has_Pc_Admin1");

                entity.HasOne(d => d.Pc)
                    .WithMany(p => p.AdminHasPcs)
                    .HasForeignKey(d => d.PcId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Admin_has_Pc_Pc1");
            });

            modelBuilder.Entity<Credential>(entity =>
            {
                entity.HasKey(e => e.CredentialsUsername)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.CredentialsUsername, "CredentialsUsername_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.CredentialsUsername)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CredentialChangePasswordId)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CredentialsPassword)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CredentialsSalt)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PcCredentialPassword)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<LastMinute>(entity =>
            {
                entity.HasKey(e => new { e.PcId, e.Second })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("Last_Minute");

                entity.Property(e => e.PcId)
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TimeChanged).HasColumnType("datetime");
            });

            modelBuilder.Entity<Pc>(entity =>
            {
                entity.ToTable("Pc");

                entity.HasIndex(e => e.PcId, "idPc_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.PcId)
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PcFirewallStatus)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PcOs)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("PcOS")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PcUsername)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => new { e.ServiceName, e.PcId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("Service");

                entity.HasIndex(e => e.PcId, "PcId_idx");

                entity.Property(e => e.ServiceName)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PcId)
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

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
