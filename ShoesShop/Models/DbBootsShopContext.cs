using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ShoesShop.Models;

public partial class DbBootsShopContext : DbContext
{
    public DbBootsShopContext()
    {
    }

    public DbBootsShopContext(DbContextOptions<DbBootsShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Manufactur> Manufacturs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderComposition> OrderCompositions { get; set; }

    public virtual DbSet<PickUpPoint> PickUpPoints { get; set; }

    public virtual DbSet<Supplyer> Supplyers { get; set; }

    public virtual DbSet<Tovar> Tovars { get; set; }

    public virtual DbSet<TovarCategory> TovarCategories { get; set; }

    public virtual DbSet<TovarType> TovarTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=Qwerty1234;database=db_boots_shop", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Manufactur>(entity =>
        {
            entity.HasKey(e => e.ManufacturId).HasName("PRIMARY");

            entity.ToTable("manufacturs");

            entity.Property(e => e.ManufacturId).HasColumnName("manufactur_id");
            entity.Property(e => e.ManufacturName)
                .HasMaxLength(80)
                .HasColumnName("manufactur_name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("orders");

            entity.HasIndex(e => e.OrderClientId, "fk_order_client_id_idx");

            entity.HasIndex(e => e.OrderPickupPoint, "fk_order_pickuppoint_idx");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("order_id");
            entity.Property(e => e.OrderClientId).HasColumnName("order_client_id");
            entity.Property(e => e.OrderCode)
                .HasMaxLength(6)
                .HasColumnName("order_code");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.OrderDeliveryDate).HasColumnName("order_delivery_date");
            entity.Property(e => e.OrderPickupPoint).HasColumnName("order_pickup_point");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(20)
                .HasColumnName("order_status");

            entity.HasOne(d => d.OrderClient).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderClientId)
                .HasConstraintName("fk_order_client_id");

            entity.HasOne(d => d.OrderPickupPointNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderPickupPoint)
                .HasConstraintName("fk_order_pickuppoint");
        });

        modelBuilder.Entity<OrderComposition>(entity =>
        {
            entity.HasKey(e => e.OrderCompositionId).HasName("PRIMARY");

            entity.ToTable("order_composition");

            entity.HasIndex(e => e.OcOrderId, "fk_oc_order_id_idx");

            entity.HasIndex(e => e.OcTovarId, "fk_oc_tovar_id_idx");

            entity.Property(e => e.OrderCompositionId).HasColumnName("order_composition_id");
            entity.Property(e => e.OcOrderId).HasColumnName("oc_order_id");
            entity.Property(e => e.OcTovarAmount).HasColumnName("oc_tovar_amount");
            entity.Property(e => e.OcTovarId)
                .HasMaxLength(7)
                .HasColumnName("oc_tovar_id");

            entity.HasOne(d => d.OcOrder).WithMany(p => p.OrderCompositions)
                .HasForeignKey(d => d.OcOrderId)
                .HasConstraintName("fk_oc_order_id");

            entity.HasOne(d => d.OcTovar).WithMany(p => p.OrderCompositions)
                .HasForeignKey(d => d.OcTovarId)
                .HasConstraintName("fk_oc_tovar_id");
        });

        modelBuilder.Entity<PickUpPoint>(entity =>
        {
            entity.HasKey(e => e.PickUpPointId).HasName("PRIMARY");

            entity.ToTable("pick_up_points");

            entity.Property(e => e.PickUpPointId)
                .ValueGeneratedNever()
                .HasColumnName("pick_up_point_id");
            entity.Property(e => e.PickUpPointCity)
                .HasMaxLength(35)
                .HasColumnName("pick_up_point_city");
            entity.Property(e => e.PickUpPointHouse)
                .HasMaxLength(5)
                .HasColumnName("pick_up_point_house");
            entity.Property(e => e.PickUpPointIndex)
                .HasMaxLength(7)
                .HasColumnName("pick_up_point_index");
            entity.Property(e => e.PickUpPointStreet)
                .HasMaxLength(30)
                .HasColumnName("pick_up_point_street");
        });

        modelBuilder.Entity<Supplyer>(entity =>
        {
            entity.HasKey(e => e.SupplyerId).HasName("PRIMARY");

            entity.ToTable("supplyers");

            entity.Property(e => e.SupplyerId).HasColumnName("supplyer_id");
            entity.Property(e => e.SupplyerName)
                .HasMaxLength(100)
                .HasColumnName("supplyer_name");
        });

        modelBuilder.Entity<Tovar>(entity =>
        {
            entity.HasKey(e => e.TovarId).HasName("PRIMARY");

            entity.ToTable("tovars");

            entity.HasIndex(e => e.TovarCategory, "fk_tovar_category_id_idx");

            entity.HasIndex(e => e.TovarManufactur, "fk_tovar_manufactur_id_idx");

            entity.HasIndex(e => e.TovarSupplyer, "fk_tovar_suplyer_id_idx");

            entity.HasIndex(e => e.TovarType, "fk_tovar_type_id_idx");

            entity.Property(e => e.TovarId)
                .HasMaxLength(7)
                .HasColumnName("tovar_id");
            entity.Property(e => e.TovarCategory).HasColumnName("tovar_category");
            entity.Property(e => e.TovarCurrentDiscount).HasColumnName("tovar_current_discount");
            entity.Property(e => e.TovarDescription)
                .HasColumnType("text")
                .HasColumnName("tovar_description");
            entity.Property(e => e.TovarImage)
                .HasMaxLength(80)
                .HasColumnName("tovar_image");
            entity.Property(e => e.TovarManufactur).HasColumnName("tovar_manufactur");
            entity.Property(e => e.TovarPrice)
                .HasPrecision(7, 2)
                .HasColumnName("tovar_price");
            entity.Property(e => e.TovarStatus)
                .HasDefaultValueSql("'active'")
                .HasColumnType("enum('active','deleted')")
                .HasColumnName("tovar_status");
            entity.Property(e => e.TovarStorageAmount).HasColumnName("tovar_storage_amount");
            entity.Property(e => e.TovarSupplyer).HasColumnName("tovar_supplyer");
            entity.Property(e => e.TovarType).HasColumnName("tovar_type");
            entity.Property(e => e.TovarUnit)
                .HasMaxLength(6)
                .HasColumnName("tovar_unit");

            entity.HasOne(d => d.TovarCategoryNavigation).WithMany(p => p.Tovars)
                .HasForeignKey(d => d.TovarCategory)
                .HasConstraintName("fk_tovar_category_id");

            entity.HasOne(d => d.TovarManufacturNavigation).WithMany(p => p.Tovars)
                .HasForeignKey(d => d.TovarManufactur)
                .HasConstraintName("fk_tovar_manufactur_id");

            entity.HasOne(d => d.TovarSupplyerNavigation).WithMany(p => p.Tovars)
                .HasForeignKey(d => d.TovarSupplyer)
                .HasConstraintName("fk_tovar_suplyer_id");

            entity.HasOne(d => d.TovarTypeNavigation).WithMany(p => p.Tovars)
                .HasForeignKey(d => d.TovarType)
                .HasConstraintName("fk_tovar_type_id");
        });

        modelBuilder.Entity<TovarCategory>(entity =>
        {
            entity.HasKey(e => e.TovarCategoryId).HasName("PRIMARY");

            entity.ToTable("tovar_categories");

            entity.Property(e => e.TovarCategoryId)
                .ValueGeneratedNever()
                .HasColumnName("tovar_category_id");
            entity.Property(e => e.TovarCategoryName)
                .HasMaxLength(45)
                .HasColumnName("tovar_category_name");
        });

        modelBuilder.Entity<TovarType>(entity =>
        {
            entity.HasKey(e => e.TovarTypeId).HasName("PRIMARY");

            entity.ToTable("tovar_types");

            entity.Property(e => e.TovarTypeId).HasColumnName("tovar_type_id");
            entity.Property(e => e.TovarTypeName)
                .HasMaxLength(20)
                .HasColumnName("tovar_type_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.UserRole, "fk_user_role_id_idx");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.UserLogin)
                .HasMaxLength(60)
                .HasColumnName("user_login");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .HasColumnName("user_name");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(255)
                .HasColumnName("user_password");
            entity.Property(e => e.UserPatronymic)
                .HasMaxLength(30)
                .HasColumnName("user_patronymic");
            entity.Property(e => e.UserRole).HasColumnName("user_role");
            entity.Property(e => e.UserSurname)
                .HasMaxLength(30)
                .HasColumnName("user_surname");

            entity.HasOne(d => d.UserRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRole)
                .HasConstraintName("fk_user_role_id");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PRIMARY");

            entity.ToTable("user_roles");

            entity.Property(e => e.UserRoleId)
                .ValueGeneratedNever()
                .HasColumnName("user_role_id");
            entity.Property(e => e.UserRoleName)
                .HasMaxLength(45)
                .HasColumnName("user_role_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
