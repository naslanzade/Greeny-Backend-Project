using Greeny.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opttion) : base(opttion)
        {

        }
        public DbSet<Setting> Settings { get; set;}
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderInfo> SliderInfos { get; set; }
        public DbSet<Text> Texts { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<AboutImage> AboutImages { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<BgImage> BgImages { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Disocunt> Disocunts { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Blog> Blogs { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AboutImage>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Text>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Team>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Testimonial>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<BgImage>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Branch>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Slider>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Brand>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<SubCategory>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Advert>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Tag>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<SubCategory>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<ProductImage>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Blog>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Author>().HasQueryFilter(m => !m.SoftDeleted);



            modelBuilder.Entity<SubCategory>()
                        .HasMany(e => e.Product)
                        .WithOne(e => e.SubCategory)
                        .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
