// <copyright file="ApplicationContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Infrastructure.DATA;

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public partial class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    private readonly string? connectionString;

    public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration configuration)
        : base(options)
    {
        this.connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public virtual DbSet<FriendList> FriendLists { get; set; }

    public virtual DbSet<MailComprehension> MailComprehensions { get; set; }

    public virtual DbSet<Pozdrik> Pozdriks { get; set; }

    public virtual DbSet<TgComprehension> TgComprehensions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(this.connectionString);
        }

        // #if DEBUG
        //         if (!optionsBuilder.IsConfigured)
        //         {
        //             var config = new ConfigurationBuilder()
        //                 .SetBasePath(Directory.GetCurrentDirectory())
        //                 .AddJsonFile("appsettings.json")
        //                 .Build();
        //
        //             var connectionString = config.GetConnectionString("DefaultConnection");
        //
        //             if (string.IsNullOrEmpty(connectionString))
        //             {
        //                 throw new InvalidOperationException("Connection string 'DefaultConnection' is not set.");
        //             }
        //
        //             optionsBuilder.UseNpgsql(connectionString);
        //         }
        // #endif
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FriendList>(entity =>
        {
            entity.HasKey(e => new { e.Appid, e.FriendUsername }).HasName("friend_list_pkey");

            entity.ToTable("friend_list");

            entity.HasIndex(e => e.IdPozdr, "IX_friend_list_id_pozdr");

            entity.Property(e => e.Appid).HasColumnName("appid");
            entity.Property(e => e.FriendUsername)
                .HasDefaultValueSql("''::text")
                .HasColumnName("friend_username");
            entity.Property(e => e.DateBirth).HasColumnName("date_birth");
            entity.Property(e => e.FriendName).HasColumnName("friend_name");
            entity.Property(e => e.IdPozdr).HasColumnName("id_pozdr");

            entity.HasOne(d => d.App).WithMany(p => p.FriendLists)
                .HasForeignKey(d => d.Appid)
                .HasConstraintName("friend_list_appid_fkey");

            entity.HasOne(d => d.IdPozdrNavigation).WithMany(p => p.FriendLists)
                .HasForeignKey(d => d.IdPozdr)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("friend_list_id_pozdr_fkey");
        });

        modelBuilder.Entity<MailComprehension>(entity =>
        {
            entity.HasKey(e => new { e.Appid, e.Mail }).HasName("mail_comprehensions_pkey");

            entity.ToTable("mail_comprehensions");

            entity.HasIndex(e => e.Mail, "mail_comprehensions_mail_key").IsUnique();

            entity.Property(e => e.Appid).HasColumnName("appid");
            entity.Property(e => e.Mail).HasColumnName("mail");

            entity.HasOne(d => d.App).WithMany(p => p.MailComprehensions)
                .HasForeignKey(d => d.Appid)
                .HasConstraintName("mail_comprehensions_appid_fkey");
        });

        modelBuilder.Entity<Pozdrik>(entity =>
        {
            entity.HasKey(e => e.IdPozdr).HasName("pozdrik_pkey");

            entity.ToTable("pozdrik");

            entity.Property(e => e.IdPozdr).HasColumnName("id_pozdr");
            entity.Property(e => e.Interest).HasColumnName("interest");
            entity.Property(e => e.Pozhelanie).HasColumnName("pozhelanie");
            entity.Property(e => e.Textpozdr).HasColumnName("textpozdr");
        });

        modelBuilder.Entity<TgComprehension>(entity =>
        {
            entity.HasKey(e => new { e.Appid, e.TgId }).HasName("tg_comprehensions_pkey");

            entity.ToTable("tg_comprehensions");

            entity.HasIndex(e => e.TgId, "tg_comprehensions_tg_id_key").IsUnique();

            entity.Property(e => e.Appid).HasColumnName("appid");
            entity.Property(e => e.TgId)
                .HasDefaultValue(0L)
                .HasColumnName("tg_id");

            entity.HasOne(d => d.App).WithMany(p => p.TgComprehensions)
                .HasForeignKey(d => d.Appid)
                .HasConstraintName("tg_comprehensions_appid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Appid).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Login, "users_login_key").IsUnique();

            entity.Property(e => e.Appid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("appid");
            entity.Property(e => e.Login).HasColumnName("login");
            entity.Property(e => e.Password).HasColumnName("password");
        });

        this.OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
