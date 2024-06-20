using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using test.Models;

namespace test.Data;

public partial class ApbdContext : DbContext
{
    public ApbdContext()
    {
    }

    public ApbdContext(DbContextOptions<ApbdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BoatStandard> BoatStandards { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Sailboat> Sailboats { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BoatStandard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BoatStan__3214EC077446534A");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(255);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clients__3214EC07191DA612");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(15);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reservat__3214EC073E3E6433");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CancelReason).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdBoatStandardNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdBoatStandard)
                .HasConstraintName("FK__Reservati__IdBoa__3C69FB99");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK__Reservati__IdCli__3B75D760");
        });

        modelBuilder.Entity<Sailboat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sailboat__3214EC07D17FD250");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdBoatStandardNavigation).WithMany(p => p.Sailboats)
                .HasForeignKey(d => d.IdBoatStandard)
                .HasConstraintName("FK__Sailboats__IdBoa__3F466844");

            entity.HasMany(d => d.IdReservations).WithMany(p => p.IdSailboats)
                .UsingEntity<Dictionary<string, object>>(
                    "SailboatReservation",
                    r => r.HasOne<Reservation>().WithMany()
                        .HasForeignKey("IdReservation")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__SailboatR__IdRes__4316F928"),
                    l => l.HasOne<Sailboat>().WithMany()
                        .HasForeignKey("IdSailboat")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__SailboatR__IdSai__4222D4EF"),
                    j =>
                    {
                        j.HasKey("IdSailboat", "IdReservation").HasName("PK__Sailboat__80FF5A927722AA55");
                        j.ToTable("SailboatReservations");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
