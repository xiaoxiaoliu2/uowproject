using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace uowpublic.Data
{
	public class AppDbContext : DbContext
	{
		protected readonly IConfiguration Configuration;

		public AppDbContext(IConfiguration configuration)
		{
			Configuration = configuration;
		}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
			options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

		public DbSet<Properties> Properties { get; set; }
    }
}

