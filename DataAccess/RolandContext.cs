using System;
using System.Collections.Generic;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Domain.Models
{
    public partial class RolandContext : DbContext
    {
        public RolandContext()
        {
        }

        public RolandContext(DbContextOptions<RolandContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Airport> Airports { get; set; } = null!;
        public virtual DbSet<Baggage> Baggages { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<ChangeHistory> ChangeHistories { get; set; } = null!;
        public virtual DbSet<Charter> Charters { get; set; } = null!;
        public virtual DbSet<Flight> Flights { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Passenger> Passengers { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<SpecialService> SpecialServices { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAB116-24\\MSSQLSERVER01;Database=Roland;User Id=UIT\\user;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airport>(entity =>
            {
                entity.HasIndex(e => e.AirportCode, "UQ__Airports__4B677353CE793A21")
                    .IsUnique();

                entity.Property(e => e.AirportId).HasColumnName("AirportID");

                entity.Property(e => e.AirportCode).HasMaxLength(10);

                entity.Property(e => e.AirportName).HasMaxLength(100);

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.Country).HasMaxLength(100);
            });

            modelBuilder.Entity<Baggage>(entity =>
            {
                entity.ToTable("Baggage");

                entity.Property(e => e.BaggageId).HasColumnName("BaggageID");

                entity.Property(e => e.PassengerId).HasColumnName("PassengerID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.Passenger)
                    .WithMany(p => p.Baggages)
                    .HasForeignKey(d => d.PassengerId)
                    .HasConstraintName("FK__Baggage__Passeng__534D60F1");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.BookingId).HasColumnName("BookingID");

                entity.Property(e => e.BookingDate).HasColumnType("datetime");

                entity.Property(e => e.FlightId).HasColumnName("FlightID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.FlightId)
                    .HasConstraintName("FK__Bookings__Flight__46E78A0C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Bookings__UserID__45F365D3");

                entity.HasMany(d => d.Passengers)
                    .WithMany(p => p.Bookings)
                    .UsingEntity<Dictionary<string, object>>(
                        "BookingPassenger",
                        l => l.HasOne<Passenger>().WithMany().HasForeignKey("PassengerId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__BookingPa__Passe__5070F446"),
                        r => r.HasOne<Booking>().WithMany().HasForeignKey("BookingId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__BookingPa__Booki__4F7CD00D"),
                        j =>
                        {
                            j.HasKey("BookingId", "PassengerId").HasName("PK__BookingP__6B1C0F34CBC4C50F");

                            j.ToTable("BookingPassenger");

                            j.IndexerProperty<int>("BookingId").HasColumnName("BookingID");

                            j.IndexerProperty<int>("PassengerId").HasColumnName("PassengerID");
                        });

                entity.HasMany(d => d.Services)
                    .WithMany(p => p.Bookings)
                    .UsingEntity<Dictionary<string, object>>(
                        "BookingService",
                        l => l.HasOne<SpecialService>().WithMany().HasForeignKey("ServiceId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__BookingSe__Servi__5BE2A6F2"),
                        r => r.HasOne<Booking>().WithMany().HasForeignKey("BookingId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__BookingSe__Booki__5AEE82B9"),
                        j =>
                        {
                            j.HasKey("BookingId", "ServiceId").HasName("PK__BookingS__CFC4A1C3F7ED2104");

                            j.ToTable("BookingService");

                            j.IndexerProperty<int>("BookingId").HasColumnName("BookingID");

                            j.IndexerProperty<int>("ServiceId").HasColumnName("ServiceID");
                        });
            });

            modelBuilder.Entity<ChangeHistory>(entity =>
            {
                entity.HasKey(e => e.ChangeId)
                    .HasName("PK__ChangeHi__0E05C5B70BB6A67F");

                entity.ToTable("ChangeHistory");

                entity.Property(e => e.ChangeId).HasColumnName("ChangeID");

                entity.Property(e => e.ChangeDate).HasColumnType("datetime");

                entity.Property(e => e.ChangeType).HasMaxLength(50);

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.TableName).HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ChangeHistories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__ChangeHis__UserI__66603565");
            });

            modelBuilder.Entity<Charter>(entity =>
            {
                entity.Property(e => e.CharterId).HasColumnName("CharterID");

                entity.Property(e => e.CharterCompany).HasMaxLength(100);

                entity.Property(e => e.FlightId).HasColumnName("FlightID");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.Charters)
                    .HasForeignKey(d => d.FlightId)
                    .HasConstraintName("FK__Charters__Flight__5629CD9C");
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasIndex(e => e.FlightNumber, "UQ__Flights__2EAE6F50C4D5B712")
                    .IsUnique();

                entity.Property(e => e.FlightId).HasColumnName("FlightID");

                entity.Property(e => e.ArrivalAirportId).HasColumnName("ArrivalAirportID");

                entity.Property(e => e.ArrivalTime).HasColumnType("datetime");

                entity.Property(e => e.DepartureAirportId).HasColumnName("DepartureAirportID");

                entity.Property(e => e.DepartureTime).HasColumnType("datetime");

                entity.Property(e => e.FlightNumber).HasMaxLength(10);

                entity.HasOne(d => d.ArrivalAirport)
                    .WithMany(p => p.FlightArrivalAirports)
                    .HasForeignKey(d => d.ArrivalAirportId)
                    .HasConstraintName("FK__Flights__Arrival__4316F928");

                entity.HasOne(d => d.DepartureAirport)
                    .WithMany(p => p.FlightDepartureAirports)
                    .HasForeignKey(d => d.DepartureAirportId)
                    .HasConstraintName("FK__Flights__Departu__4222D4EF");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.NotificationId).HasColumnName("NotificationID");

                entity.Property(e => e.IsRead).HasDefaultValueSql("((0))");

                entity.Property(e => e.NotificationDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Notificat__UserI__6383C8BA");
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.HasIndex(e => e.PassportNumber, "UQ__Passenge__45809E710D19B492")
                    .IsUnique();

                entity.Property(e => e.PassengerId).HasColumnName("PassengerID");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.PassportNumber).HasMaxLength(50);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.ReviewId).HasColumnName("ReviewID");

                entity.Property(e => e.FlightId).HasColumnName("FlightID");

                entity.Property(e => e.ReviewDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.FlightId)
                    .HasConstraintName("FK__Reviews__FlightI__5FB337D6");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Reviews__UserID__5EBF139D");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160B95222E4")
                    .IsUnique();

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<SpecialService>(entity =>
            {
                entity.HasKey(e => e.ServiceId)
                    .HasName("PK__SpecialS__C51BB0EA257E70C1");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.ServiceName).HasMaxLength(100);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.TicketId).HasColumnName("TicketID");

                entity.Property(e => e.BookingId).HasColumnName("BookingID");

                entity.Property(e => e.Class).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.SeatNumber).HasMaxLength(10);

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK__Tickets__Booking__49C3F6B7");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Users__A9D105348A958CAE")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.PasswordHash).HasMaxLength(255);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__RoleID__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
