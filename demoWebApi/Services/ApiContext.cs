using demoWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace demoWebApi.Services
{
    /// <summary>
    ///     This class implements a service to manage the database for the API.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class ApiContext : DbContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ApiContext" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
            var countTask = Definitions.CountAsync();
            if (countTask.Result == 0)
            {
                AddTestData();
            }
        }

        /// <summary>
        ///     Gets or sets the definitions.
        /// </summary>
        /// <value>This is the collection of definitions.</value>
        public DbSet<Definition> Definitions { get; set; }

        private void AddTestData()
        {
            Definitions.Add(new Definition() { DefinitionId = 1, Name = "Definition 1" });
            Definitions.Add(new Definition() { DefinitionId = 2, Name = "Definition 2" });
            SaveChanges();
        }
    }
}
