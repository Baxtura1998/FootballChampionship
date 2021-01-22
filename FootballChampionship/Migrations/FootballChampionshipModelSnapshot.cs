﻿// <auto-generated />
using FootballChampionship;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FootballChampionship.Migrations
{
    [DbContext(typeof(FootballChampionship))]
    partial class FootballChampionshipModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("FootballChampionship.Championship", b =>
                {
                    b.Property<int>("ChampionshipRankId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ChampionshipPoints")
                        .HasColumnType("int");

                    b.Property<string>("FootballTeamName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Place")
                        .HasColumnType("int");

                    b.HasKey("ChampionshipRankId");

                    b.ToTable("Championship");
                });

            modelBuilder.Entity("FootballChampionship.ChampionshipFixtureResults", b =>
                {
                    b.Property<int>("ChampionshipFixtureResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FootballTeam1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FootballTeam2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GoalsScoredByFirstTeam")
                        .HasColumnType("int");

                    b.Property<int>("GoalsScoredBySecondTeam")
                        .HasColumnType("int");

                    b.HasKey("ChampionshipFixtureResultId");

                    b.ToTable("ChampionshipFixtureResults");
                });

            modelBuilder.Entity("FootballChampionship.FootballTeam", b =>
                {
                    b.Property<int>("FootballTeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FootballTeamName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FootballTeamId");

                    b.ToTable("FootballTeam");
                });
#pragma warning restore 612, 618
        }
    }
}
