using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Infrastructure;

namespace DemoAngular.Migrations
{
    [DbContext(typeof(BitlyDbContext))]
    partial class BitlyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Models.Link", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDateUTC");

                    b.Property<string>("OriginalLink")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 2000);

                    b.Property<string>("ShortenLinkCode")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 2000);

                    b.Property<int>("UsesNumber")
                        .IsConcurrencyToken();

                    b.HasKey("Id");

                    b.HasIndex("ShortenLinkCode");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("Domain.Models.UserLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LinkId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasAlternateKey("UserId", "LinkId");

                    b.HasIndex("LinkId");

                    b.HasIndex("UserId");

                    b.ToTable("UserLinks");
                });

            modelBuilder.Entity("Domain.Models.UserLink", b =>
                {
                    b.HasOne("Domain.Models.Link", "Link")
                        .WithMany()
                        .HasForeignKey("LinkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
