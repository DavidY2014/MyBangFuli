using BangBangFuli.H5.API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BangBangFuli.H5.API.EntityFrameworkCore
{
    public partial class CouponSystemDBContext : DbContext
    {

        public CouponSystemDBContext(DbContextOptions<CouponSystemDBContext> options)
                   : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Coupon> Coupons { get; set; }

        public DbSet<Banner> Banners { get; set; }

        public DbSet<ProductInformation> ProductInformations { get; set; }

        public DbSet<ProductDetail> ProductDetails { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<BannerDetail> BannerDetails { get; set; }

        public DbSet<BatchInformation> BatchInformations { get; set; }

        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<UserAccount> UserAccounts { get; set; }

        public DbSet<ModuleInfo> ModuleInfos { get; set; }

        public DbSet<UserRoleJurisdiction> UserRoleJurisdictions { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

    }
}
