using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CampX.Entities;

public partial class CampiXContext : DbContext
{
    public CampiXContext()
    {
    }

    public CampiXContext(DbContextOptions<CampiXContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Badge> Badges { get; set; }

    public virtual DbSet<Camper> Campers { get; set; }

    public virtual DbSet<CamperBadge> CamperBadges { get; set; }

    public virtual DbSet<Campsite> Campsites { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<EquipmentCamperTrip> EquipmentCamperTrips { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Night> Nights { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<TripCamper> TripCampers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Initial Catalog=CampiX;Integrated Security=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Badge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Badge__3214EC0756ABA16D");

            entity.ToTable("Badge");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Image).WithMany(p => p.Badges)
                .HasForeignKey(d => d.ImageId)
                .HasConstraintName("FK_Badge_Images");
        });

        modelBuilder.Entity<Camper>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Camper__3214EC07E754CC86");

            entity.ToTable("Camper");

            entity.HasIndex(e => e.Email, "UQ__Camper__A9D10534B2B064E4").IsUnique();

            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.IsBanned).HasColumnName("isBanned");
            entity.Property(e => e.LastName).HasMaxLength(100);

            entity.HasOne(d => d.Image).WithMany(p => p.Campers)
                .HasForeignKey(d => d.ImageId)
                .HasConstraintName("FK_Camper_Image");
        });

        modelBuilder.Entity<CamperBadge>(entity =>
        {
            entity.HasKey(e => new { e.BadgeId, e.CamperId }).HasName("PK__CamperBa__E67A48BE2EA5E98E");

            entity.ToTable("CamperBadge");

            entity.HasOne(d => d.Badge).WithMany(p => p.CamperBadges)
                .HasForeignKey(d => d.BadgeId)
                .HasConstraintName("FK_CamperBadge_Badge");

            entity.HasOne(d => d.Camper).WithMany(p => p.CamperBadges)
                .HasForeignKey(d => d.CamperId)
                .HasConstraintName("FK_CamperBadge_Camper");
        });

        modelBuilder.Entity<Campsite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Campsite__3214EC072EA2477B");

            entity.ToTable("Campsite");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Latitude).HasColumnType("decimal(12, 9)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(12, 9)");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Rating).HasColumnType("decimal(2, 1)");

            entity.HasOne(d => d.Camper).WithMany(p => p.Campsites)
                .HasForeignKey(d => d.CamperId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CampsiteCamper");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Equipmen__3214EC07C1D97D91");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<EquipmentCamperTrip>(entity =>
        {
            entity.HasKey(e => new { e.CamperId, e.EquipmentId, e.TripId }).HasName("PK__Equipmen__2E5B9ED191149D36");

            entity.ToTable("EquipmentCamperTrip");

            entity.HasOne(d => d.Camper).WithMany(p => p.EquipmentCamperTrips)
                .HasForeignKey(d => d.CamperId)
                .HasConstraintName("FK_EquipmentCamperTrip_Camper");

            entity.HasOne(d => d.Equipment).WithMany(p => p.EquipmentCamperTrips)
                .HasForeignKey(d => d.EquipmentId)
                .HasConstraintName("FK_EquipmentCampeTripr_Equipment");

            entity.HasOne(d => d.Trip).WithMany(p => p.EquipmentCamperTrips)
                .HasForeignKey(d => d.TripId)
                .HasConstraintName("FK_EquipmentCamperTrip_Trip");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Images__C8C63E4A5A89B8B5");

            entity.HasMany(d => d.Campsites).WithMany(p => p.Images)
                .UsingEntity<Dictionary<string, object>>(
                    "CampsiteImage",
                    r => r.HasOne<Campsite>().WithMany()
                        .HasForeignKey("CampsiteId")
                        .HasConstraintName("FK_CampsiteImages_Campsite"),
                    l => l.HasOne<Image>().WithMany()
                        .HasForeignKey("ImageId")
                        .HasConstraintName("FK_CampsiteImages_Image"),
                    j =>
                    {
                        j.HasKey("ImageId", "CampsiteId").HasName("PK__Campsite__65FFD46781223A53");
                        j.ToTable("CampsiteImages");
                    });
        });

        modelBuilder.Entity<Night>(entity =>
        {
            entity.HasKey(e => new { e.TripId, e.CampsiteId }).HasName("PK__Nights__70D161E418D9B9B9");

            entity.HasOne(d => d.Campsite).WithMany(p => p.Nights)
                .HasForeignKey(d => d.CampsiteId)
                .HasConstraintName("FK_Nights_Campsites");
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Note__3214EC07DDF55768");

            entity.ToTable("Note");

            entity.Property(e => e.Content).HasMaxLength(500);
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => new { e.TripId, e.CamperId }).HasName("PK__Request__B6F006DD86019C08");

            entity.ToTable("Request");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Description).HasMaxLength(500);

            entity.HasOne(d => d.Camper).WithMany(p => p.Requests)
                .HasForeignKey(d => d.CamperId)
                .HasConstraintName("FK_Request_Camper");

            entity.HasOne(d => d.Trip).WithMany(p => p.Requests)
                .HasForeignKey(d => d.TripId)
                .HasConstraintName("FK_Request_Trip");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Review__3214EC077B3F8F93");

            entity.ToTable("Review");

            entity.HasOne(d => d.Camper).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.CamperId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReviewCamper");

            entity.HasOne(d => d.Campsite).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.CampsiteId)
                .HasConstraintName("FK_Reviews_Campsites");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07CE019B45");

            entity.ToTable("Role");

            entity.HasIndex(e => e.Id, "UQ__Role__3214EC06FA4493C9").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasMany(d => d.Campers).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "CamperRole",
                    r => r.HasOne<Camper>().WithMany()
                        .HasForeignKey("CamperId")
                        .HasConstraintName("FK_CamperRole_Camper"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_CamperRole_Role"),
                    j =>
                    {
                        j.HasKey("RoleId", "CamperId").HasName("PK__CamperRo__9992045879EA5ED2");
                        j.ToTable("CamperRole");
                    });
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Trip__3214EC07A31F6526");

            entity.ToTable("Trip");

            entity.Property(e => e.Code)
                .HasMaxLength(6)
                .IsFixedLength();
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsFinished).HasColumnName("isFinished");
            entity.Property(e => e.IsPublic).HasColumnName("isPublic");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasMany(d => d.Campsites).WithMany(p => p.Trips)
                .UsingEntity<Dictionary<string, object>>(
                    "CampsiteTrip",
                    r => r.HasOne<Campsite>().WithMany()
                        .HasForeignKey("CampsiteId")
                        .HasConstraintName("FK_CampsiteTrip_Campsite"),
                    l => l.HasOne<Trip>().WithMany()
                        .HasForeignKey("TripId")
                        .HasConstraintName("FK_CampsiteTrip_Trip"),
                    j =>
                    {
                        j.HasKey("TripId", "CampsiteId").HasName("PK__Campsite__366D78FC9040F3D8");
                        j.ToTable("CampsiteTrip");
                    });
        });

        modelBuilder.Entity<TripCamper>(entity =>
        {
            entity.HasKey(e => new { e.TripId, e.CamperId }).HasName("PK__TripCamp__B6F006DDC85F7394");

            entity.ToTable("TripCamper");

            entity.Property(e => e.IsOrganizer).HasColumnName("isOrganizer");

            entity.HasOne(d => d.Camper).WithMany(p => p.TripCampers)
                .HasForeignKey(d => d.CamperId)
                .HasConstraintName("FK_CamperTrip_Camper");

            entity.HasOne(d => d.Trip).WithMany(p => p.TripCampers)
                .HasForeignKey(d => d.TripId)
                .HasConstraintName("FK_CamperTrip_Trip");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
