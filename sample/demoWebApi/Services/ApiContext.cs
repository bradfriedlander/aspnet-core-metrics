using System.Threading.Tasks;
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

        /// <summary>
        ///     Does the definition exist.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns><c>true</c> if there is a <see cref="Definition" /> with the matching <paramref name="id" />; otherwise, <c>false</c>.</returns>
        public async Task<bool> DoesDefinitionExist(int id)
        {
            var match = await Definitions.IgnoreQueryFilters().FirstOrDefaultAsync(d => id == d.DefinitionId);
            return match != null;
        }

        /// <summary>
        ///     Override this method to further configure the model that was discovered by convention from the entity types exposed in <see
        ///     cref="Microsoft.EntityFrameworkCore.DbSet{T}" /> properties on your derived context. The resulting model may be cached and re-used for
        ///     subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">
        ///     The builder being used to construct the model for this context. Databases (and other extensions) typically define extension methods on
        ///     this object that allow you to configure aspects of the model that are specific to a given database.
        /// </param>
        /// <remarks>
        ///     If a model is explicitly set on the options for this context (via <see
        ///     cref="DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />) then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.Entity<Definition>().HasQueryFilter(p => !p.IsDeleted);

        private void AddTestData()
        {
            Definitions.Add(new Definition() { DefinitionId = 1, Name = "Definition 1" });
            Definitions.Add(new Definition() { DefinitionId = 2, Name = "Definition 2" });
            SaveChanges();
        }
    }
}
