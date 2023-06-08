using System.Data.Entity;
using InvoiceManager.Models.Domains;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InvoiceManager.Models.ViewModels
{

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
    }
}