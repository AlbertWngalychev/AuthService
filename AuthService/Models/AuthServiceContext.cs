using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AuthService.Models
{
    public partial class AuthServiceContext : DbContext
    {
        public AuthServiceContext()
        {
        }

        public AuthServiceContext(DbContextOptions<AuthServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Auth> Auths { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auth>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Auth");

                entity.Property(e => e.Guid)
                    .HasColumnName("GUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.HashedPass)
                    .HasMaxLength(255)
                    .HasColumnName("hashed_pass")
                    .IsFixedLength();

                entity.Property(e => e.Salt)
                    .HasMaxLength(1024)
                    .HasColumnName("salt")
                    .IsFixedLength();

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
