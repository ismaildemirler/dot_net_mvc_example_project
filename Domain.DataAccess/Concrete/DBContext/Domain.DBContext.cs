using Domain.Entity.Concrete;
using Domain.Infrastructure.DataAccess.EntityFramework.DbContextBase;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;

namespace Domain.DataAccess.Concrete.DBContext
{
    public class DomainDBContext : DbContextBase
    {
        public DomainDBContext() : base("name=DomainDBContext")
        {
            Database.SetInitializer<DomainDBContext>(null);
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 600; // seconds
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            return base.ValidateEntity(entityEntry, items);
        }

        public DbSet<Beneficiary> Beneficiary { get; set; }
        public DbSet<BlogContent> BlogContent { get; set; }
        public DbSet<BrandApplicationDetail> BrandApplicationDetail { get; set; }
        public DbSet<BrandClasses> BrandClasses { get; set; }
        public DbSet<BrandSubClasses> BrandSubClasses { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<CustomerApplication> CustomerApplication { get; set; }
        public DbSet<CustomerApplicationBrandClasses> CustomerApplicationBrandClasses { get; set; }
        public DbSet<CustomerDomain> CustomerDomain { get; set; }
        public DbSet<DomainPrice> DomainPrice { get; set; }
        public DbSet<Management> Management { get; set; }
        public DbSet<PrmFirmStatusType> PrmFirmStatusType { get; set; }
        public DbSet<PrmTLDType> PrmTLDType { get; set; }
        public DbSet<PrmBlogCategory> PrmBlogCategory { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<SaleDetail> SaleDetail { get; set; }
        public DbSet<SaleTransaction> SaleTransaction { get; set; }
        public DbSet<SystemUser> SystemUser { get; set; }
        public DbSet<Town> Town { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<SubMenu> SubMenu { get; set; }
        public DbSet<PrmBrandApplicationType> PrmBrandApplicationType { get; set; }
        public DbSet<BrandForWatching> BrandForWatching { get; set; }
        public DbSet<BrandWatchingApplicationDetail> BrandWatchingApplicationDetail { get; set; }
        public DbSet<PrmApplicationType> PrmApplicationType { get; set; }
        public DbSet<PatentApplicationDetail> PatentApplicationDetail { get; set; }
        public DbSet<Attachment> Attachment { get; set; }
        public DbSet<SliderContent> SliderContent { get; set; }
        public DbSet<SliderContentDetail> SliderContentDetail { get; set; }
        public DbSet<ReferenceFirm> ReferenceFirm { get; set; }
        public DbSet<RelatedEntity> RelatedEntity { get; set; }
        public DbSet<IndustrialDesignApplicationDetail> IndustrialDesignApplicationDetail { get; set; }
        public DbSet<Form> Form { get; set; }
        public DbSet<Services> Services { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
