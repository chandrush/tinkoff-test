using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class BitlyDbContext: DbContext, IBitlyDbContextSets
    {
		public DbSet<Link> Links { get; set; }

		public DbSet<UserLink> UserLinks { get; set; }

		public BitlyDbContext()
		{

		}

		public BitlyDbContext(DbContextOptions<BitlyDbContext> options)
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			
			modelBuilder.Entity<Link>(tb => 
			{
				tb.Property(t => t.Id)
					.ValueGeneratedOnAdd();
				tb.HasKey(t => t.Id);

				tb.Property(t => t.UsesNumber)
					.IsConcurrencyToken();

				tb.Property(t => t.CreationDateUTC)
					.IsRequired();

				tb.Property(t => t.OriginalLink)
					.HasMaxLength(2000)
					.IsRequired();

				//! если код регистрозависимый, то нужно убедиться, 
				//что колонка в базе будет иметь соотвествующий collation, либо
				//TODO: кастомизировать sql-мигратор, аннотация для создания collation
				tb.Property(t => t.ShortenLinkCode)
					.HasMaxLength(2000)
					.IsRequired();

				tb.HasIndex(t => t.ShortenLinkCode);
			});

			modelBuilder.Entity<UserLink>(tb => 
			{
				tb.Property(t => t.Id)
					   .ValueGeneratedOnAdd();
				tb.HasKey(t => t.Id);

				tb.Property(t => t.UserId)
					.IsRequired();

				tb.Property(t => t.LinkId)
					.IsRequired();

				tb.HasAlternateKey(t => new { t.UserId, t.LinkId });

				tb.HasIndex(t => t.UserId);
			});
		}
	}
}
