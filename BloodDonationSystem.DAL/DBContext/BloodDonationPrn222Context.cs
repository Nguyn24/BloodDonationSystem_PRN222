using BusinessObject.Entities;
using BusinessObject.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSystem.DAL.DBContext;

public partial class BloodDonationPrn222Context : DbContext
{
    public BloodDonationPrn222Context()
    {
    }

    public BloodDonationPrn222Context(DbContextOptions<BloodDonationPrn222Context> options)
        : base(options)
    {
    }

    public virtual DbSet<BloodStored> BloodStoreds { get; set; }

    public virtual DbSet<BloodType> BloodTypes { get; set; }

    public virtual DbSet<DonationRequest> DonationRequests { get; set; }

    public virtual DbSet<DonationsHistory> DonationsHistories { get; set; }

    public virtual DbSet<DonorInformation> DonorInformations { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<BloodType>(entity =>
        {
            entity.HasKey(e => e.BloodTypeId).HasName("PK__BloodTyp__B489BA630F3E6DE1");

            entity.HasIndex(e => e.Name, "UQ_BloodTypes_Name").IsUnique();

            entity.Property(e => e.BloodTypeId).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(10);
            
            entity.HasData(
                new BloodType
                {
                    BloodTypeId = Guid.Parse("2b0f96e4-9052-4d68-a937-9adfc9d231d1"),
                    Name = "A+",
                    Description = "A positive blood type"
                },
                new BloodType
                {
                    BloodTypeId = Guid.Parse("0f5f77fb-2bd4-4aeb-9bd4-bb56745c8845"),
                    Name = "A-",
                    Description = "A negative blood type"
                },
                new BloodType
                {
                    BloodTypeId = Guid.Parse("91baf3d9-759f-4bb8-82a4-3d9d645d91b7"),
                    Name = "B+",
                    Description = "B positive blood type"
                },
                new BloodType
                {
                    BloodTypeId = Guid.Parse("82f33bfb-7fa4-432e-8735-1c0e5c2f99f7"),
                    Name = "B-",
                    Description = "B negative blood type"
                },
                new BloodType
                {
                    BloodTypeId = Guid.Parse("edc95a1c-0c3f-4a61-a104-f949109e7c0f"),
                    Name = "AB+",
                    Description = "AB positive blood type (universal plasma donor)"
                },
                new BloodType
                {
                    BloodTypeId = Guid.Parse("1479d6c3-0c85-4cb7-a2c4-894c35e21eb1"),
                    Name = "AB-",
                    Description = "AB negative blood type"
                },
                new BloodType
                {
                    BloodTypeId = Guid.Parse("b160fa12-dfa5-44c7-a179-6ef0f3c7c28c"),
                    Name = "O+",
                    Description = "O positive blood type (most common)"
                },
                new BloodType
                {
                    BloodTypeId = Guid.Parse("62ef305e-755a-4651-9ed7-6fc4b4061e79"),
                    Name = "O-",
                    Description = "O negative blood type (universal donor)"
                }
            );
        });
         
        modelBuilder.Entity<BloodStored>(entity =>
        {
            entity.HasKey(e => e.StoredId).HasName("PK__BloodSto__A98AEA7D25309813");

            entity.ToTable("BloodStored");

            entity.Property(e => e.StoredId).ValueGeneratedNever();

            entity.HasOne(d => d.BloodType).WithMany(p => p.BloodStoreds)
                .HasForeignKey(d => d.BloodTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BloodStored_BloodTypes");
        });

        modelBuilder.Entity<DonationRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Donation__33A8517AA5458CF1");

            entity.Property(e => e.RequestId).ValueGeneratedNever();
            entity.Property(e => e.ContactName).HasMaxLength(100);
            entity.Property(e => e.ContactPhone).HasMaxLength(20);
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasConversion<string>();

            entity.HasOne(d => d.BloodType).WithMany(p => p.DonationRequests)
                .HasForeignKey(d => d.BloodTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DonationRequests_BloodTypes");

            entity.HasOne(d => d.User).WithMany(p => p.DonationRequests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DonationRequests_Users");
        });

        modelBuilder.Entity<DonationsHistory>(entity =>
        {
            entity.HasKey(e => e.DonationId).HasName("PK__Donation__C5082EFB21BC77A8");

            entity.ToTable("DonationsHistory");

            entity.Property(e => e.DonationId).ValueGeneratedNever();
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasConversion<string>();

            entity.HasOne(d => d.ConfirmedByNavigation).WithMany(p => p.DonationsHistoryConfirmedByNavigations)
                .HasForeignKey(d => d.ConfirmedBy)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Request).WithMany(p => p.DonationsHistories)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DonationsHistory_DonationRequests");

            entity.HasOne(d => d.User).WithMany(p => p.DonationsHistoryUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<DonorInformation>(entity =>
        {
            entity.HasKey(e => e.DonorInfoId).HasName("PK__DonorInf__DE4F057B17CD15BE");

            entity.ToTable("DonorInformation");

            entity.HasIndex(e => e.UserId, "UQ_DonorInformation_UserId").IsUnique();

            entity.Property(e => e.DonorInfoId).ValueGeneratedNever();
            entity.Property(e => e.Height).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MedicalStatus).HasMaxLength(500);
            entity.Property(e => e.Weight).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.User).WithOne(p => p.DonorInformation)
                .HasForeignKey<DonorInformation>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DonorInformation_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CA6BE1634");

            entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Gender)
                .HasConversion<string>()
                .HasMaxLength(50);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.IsDonor).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Role)
                .HasConversion<string>()
                .HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasConversion<string>();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
