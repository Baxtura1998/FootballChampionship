using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;


namespace FootballChampionship
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class BLoggingDb: DbContext
    {
        public DbSet<FootballTeam> FootballTeam { get; set; }
        public DbSet<ChampionshipFixtureResults> ChampionshipFixtureResults { get; set; }
        public DbSet<ChampionshipRanks> ChampionshipRanks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-UNEAH8K;database=BLoggingDb;integrated security=true;");
        }
       /* protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FootballTeam>().HasKey(x=> new { x.FootballTeamId });
            modelBuilder.Entity<ChampionshipFixtureResults>().HasKey(x=> new { x.ChampionshipFixtureResultId });
            modelBuilder.Entity<ChampionshipRanks>().HasKey(x=> new { x.ChampionshipRankId });
        }*/
             
    }
    public class FootballTeam
    {
        public int FootballTeamId { get; set; }
        public string FootballTeamName { get; set; }        
    }

    public class ChampionshipFixtureResults
    {
        public int ChampionshipFixtureResultId { get; set; }
        public string FootballTeam1 { get; set; }
        public int GoalsScoredByFirstTeam { get; set; }
        public string FootballTeam2 { get; set; }
        public int GoalsScoredBySecondTeam { get; set; }
    }

    public class ChampionshipRanks
    {
        public int ChampionshipRankId { get; set; }
        public string FootballTeamName { get; set; }
        public int ChampionshipPoints { get; set; }
    }
}
