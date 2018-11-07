using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace demoWebReact.Services
{
    /// <summary>
    ///     This is the identify service database context.
    /// </summary>
    /// <seealso cref="IdentityDbContext" />
    public class ApplicationDbContext : IdentityDbContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ApplicationDbContext" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
