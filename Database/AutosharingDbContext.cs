using Autosharing.Models;
using System.Data.Entity;

namespace Autosharing.Database
{
	public class AutosharingDbContext : DbContext
	{
		static AutosharingDbContext()
		{
			var initializer = new DropCreateDatabaseIfModelChanges<AutosharingDbContext>();
			System.Data.Entity.Database.SetInitializer(initializer);
		}

		public AutosharingDbContext() : base(Startup.ConnectionString)
		{
		}

		public DbSet<Group> Groups { get; set; }

		public DbSet<Membership> Memberships { get; set; }

		public DbSet<Entity> Entities { get; set; }

		public DbSet<EntityProperty> EntityProperties { get; set; }

		public DbSet<SharingFilter> SharingFilters { get; set; }

		public DbSet<SharingRule> SharingRules { get; set; }

		public DbSet<User> Users { get; set; }
	}
}
