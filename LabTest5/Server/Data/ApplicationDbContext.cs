using IdentityServer4.EntityFramework.Options;

using LabTest5.Server.Data.Entities;
using LabTest5.Server.Models;

using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LabTest5.Server.Data
{
	public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
	{
		public ApplicationDbContext(
		    DbContextOptions options,
		    IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder
				.Entity<PostEntity>()
				.Property(e => e.Id)
				.HasDefaultValueSql("newid()");

			builder
				.Entity<CommentEntity>()
				.Property(e => e.Id)
				.HasDefaultValueSql("newid()");

			builder
				 .Entity<CommentEntity>()
				 .HasIndex(e => e.Number)
				 .IsUnique();

			builder
				.Entity<ReactionEntity>()
				.Property(e => e.Id)
				.HasDefaultValueSql("newid()");


		}
	}
}
