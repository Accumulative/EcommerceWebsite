using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace MichellesWebsite.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        [Display(Name = "FullName", ResourceType = typeof(ViewRes.SharedStrings))]
        public string FullName { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<MichellesWebsite.Models.ProductModel> ProductModels { get; set; }

        public System.Data.Entity.DbSet<MichellesWebsite.Models.ProductPrice> ProductPrices { get; set; }

        public System.Data.Entity.DbSet<MichellesWebsite.Models.SaleModel> SaleModels { get; set; }

        public System.Data.Entity.DbSet<MichellesWebsite.Models.PayPalTransaction> PayPalTransactions { get; set; }

        public System.Data.Entity.DbSet<MichellesWebsite.Models.SaleProductModel> SaleProductModels { get; set; }

        public System.Data.Entity.DbSet<MichellesWebsite.Models.Contact> Contacts { get; set; }

        public System.Data.Entity.DbSet<MichellesWebsite.Models.News> News { get; set; }

        public System.Data.Entity.DbSet<MichellesWebsite.Models.Address> Addresses { get; set; }

        public System.Data.Entity.DbSet<MichellesWebsite.Models.Enquiry> Enquiries { get; set; }

        public System.Data.Entity.DbSet<MichellesWebsite.Models.StockTransaction> StockTransactions { get; set; }

        public System.Data.Entity.DbSet<MichellesWebsite.Models.CultureCountry> CultureCountries { get; set; }
    }
}