// <auto-generated />
using System;
using BlazorBugOne.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BlazorBugOne.Server.Migrations
{
    [DbContext(typeof(PGContext))]
    [Migration("20210619185452_TryFix1")]
    partial class TryFix1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("BlazorBugOne.Shared.Bug", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<int?>("personid")
                        .HasColumnType("integer");

                    b.Property<string>("progressreport")
                        .HasColumnType("text");

                    b.Property<int?>("projectid")
                        .HasColumnType("integer");

                    b.Property<DateTime>("resolutiondate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("resolutionsummary")
                        .HasColumnType("text");

                    b.Property<string>("summary")
                        .HasColumnType("text");

                    b.Property<DateTime>("targetdate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("id");

                    b.HasIndex("personid");

                    b.HasIndex("projectid");

                    b.ToTable("Bugs");
                });

            modelBuilder.Entity("BlazorBugOne.Shared.Person", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("firstname")
                        .HasColumnType("text");

                    b.Property<string>("lastname")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("BlazorBugOne.Shared.Project", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("BlazorBugOne.Shared.Bug", b =>
                {
                    b.HasOne("BlazorBugOne.Shared.Person", "person")
                        .WithMany()
                        .HasForeignKey("personid");

                    b.HasOne("BlazorBugOne.Shared.Project", "project")
                        .WithMany()
                        .HasForeignKey("projectid");

                    b.Navigation("person");

                    b.Navigation("project");
                });
#pragma warning restore 612, 618
        }
    }
}
