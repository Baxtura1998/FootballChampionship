using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace FootballChampionship
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new FootballChampionship())
            {
                //--------------------------------Safexburto Gundebis Sheqmna Da Bazashi Chawera---------------------------------------

                Console.WriteLine("Shemoitanet Chempionatshi Monawile Gundebis Raodenoba!");
                int z = Convert.ToInt32(Console.ReadLine()); //Shemogvaq Gundebis Raodenoba
                if (z < 5)
                    throw new Exception("Shecdoma!Gundebis Raodenoba Ar Unda Iyos 5-ze Naklebi!"); // Agdebs Shecdomas Im Shemtxvevashi, Tu Gundebis Raodenoba Naklebia 5-ze
                Console.WriteLine("Shemoitanet Gundebis Saxelebi");
                for (int i = 0; i < z; i++)
                {
                    db.FootballTeam.Add(new FootballTeam() { FootballTeamName = Console.ReadLine() }); //Shemogvaq Gundebis Saxelebi Da Vwert Bazashi, FootballTeam Cxrilshi!
                }
                db.SaveChanges(); //Vamaxsovrebt Cvlilebebs!
                Console.WriteLine("Gundebis Saxelebi Chaiwera Bazashi!");

                //---------------------------------Matchebis Sheqmna Da Shesabamisad Shedegebis Bazashi Chawera-------------------

                Console.WriteLine("Shemoitanet Chatarebuli Matchebis Angarishebi");
                for (int i = 0; i < z - 1; i++)//Shemogvaq Matchebis Angarishebi
                {
                    for (int j = i + 1; j < z; j++)
                    {
                        Console.WriteLine("Shemoitanet Shemdegi Matchis Angarishi:" + db.FootballTeam.ToArray()[i].FootballTeamName +
                            " - " + db.FootballTeam.ToArray()[j].FootballTeamName);
                        db.ChampionshipFixtureResults.Add(new ChampionshipFixtureResults()
                        {
                            FootballTeam1 = db.FootballTeam.ToArray()[i].FootballTeamName,
                            GoalsScoredByFirstTeam = Convert.ToInt32(Console.ReadLine()),
                            FootballTeam2 = db.FootballTeam.ToArray()[j].FootballTeamName,
                            GoalsScoredBySecondTeam = Convert.ToInt32(Console.ReadLine())
                        });
                        //Angarishebs Vwert Bazashi, ChampionshipFixtureResults Cxrilshi
                    }
                }
                db.SaveChanges();//Vamaxsovrebt Cvlilebebs
                Console.WriteLine("Shexvedrebis Angarishebi Chaiwera Bazashi!");

                //---------------------------------------------Gundebis Mier Dagrovili Qulebis Gamotvla------------------

                int q = db.ChampionshipFixtureResults.ToArray().Length;
                int dict;// Shemogvaq Es Integeri, Qvemot Moyvanili ChampRank-is Damxmared          
                Championship[] ChampRank = new Championship[z];//ChampRank-i Gvchirdeba Qulebisa,Gundebisa Da Adgilebis Championship Cxrilshi Sworad Chasawerad
                for (int i = 0; i < z; i++)//Am Kodit Xdeba Qulebis Gamotvla
                {
                    dict = 0;
                    for (int j = 0; j < q; j++)
                    {
                        if (db.ChampionshipFixtureResults.ToArray()[j].FootballTeam1 == db.FootballTeam.ToArray()[i].FootballTeamName)
                        {
                            if (db.ChampionshipFixtureResults.ToArray()[j].GoalsScoredByFirstTeam < db.ChampionshipFixtureResults.ToArray()[j].GoalsScoredBySecondTeam)
                            {
                                dict = dict - 1;
                            }
                            if (db.ChampionshipFixtureResults.ToArray()[j].GoalsScoredByFirstTeam > db.ChampionshipFixtureResults.ToArray()[j].GoalsScoredBySecondTeam)
                                dict += 3;
                        }
                        else if (db.FootballTeam.ToArray()[i].FootballTeamName == db.ChampionshipFixtureResults.ToArray()[j].FootballTeam2)
                        {
                            if (db.ChampionshipFixtureResults.ToArray()[j].GoalsScoredBySecondTeam < db.ChampionshipFixtureResults.ToArray()[j].GoalsScoredByFirstTeam)
                            {
                                dict -= 1;
                            }
                            if (db.ChampionshipFixtureResults.ToArray()[j].GoalsScoredBySecondTeam > db.ChampionshipFixtureResults.ToArray()[j].GoalsScoredByFirstTeam)
                                dict += 3;
                        }
                    }
                    ChampRank[i] = new Championship() { FootballTeamName = db.FootballTeam.ToArray()[i].FootballTeamName, ChampionshipPoints = dict }; //ChampRank-
                }

                //-------Dakavebuli Adgilebis Sworad Gansazgvra(Optimizacia) Shesabamisi Qulebisa Da Pirobis Mixedvit-------                

                ChampRank = ChampRank.OrderByDescending(x => x.ChampionshipPoints).ToArray();//Aq Xdeba Qulebis Klebadobit Monacemebis Dalageba
                List<int> arr = new List<int>();//Damxmare Listis Shemotana
                arr.Add(ChampRank[0].ChampionshipPoints);
                int c = arr[0];
                for (int i = 1; i < ChampRank.Length; i++)//Mteli Am Moqmedebis Mizani Aris Is,Rom List-shi Shevinaxot Is Qulebi
                                                          //(Tito-Tito,Ori Ertnairi Qula Ar Sheinaxeba.Sityvaze Tu 3-Ma Gundma Daagrove Erti Da Igive Qula, Listshi Iwereba Mxolod Erti Matgani), 
                                                          //Romlebic Daagroves Gundebma
                {
                    if (c == ChampRank[i].ChampionshipPoints)
                        continue;
                    else
                    {
                        c = ChampRank[i].ChampionshipPoints;
                        arr.Add(c);
                    }
                }
                List<Championship> Championships = new List<Championship>();//Shemogvaq Kidev Erti Damxmare Listi
                for (int i = 0; i < arr.Count; i++)//Am Moqmedebis Mizani Aris Championships Listshi Monacemebis Qulebis 
                                                   //Klebadobis Mixedvis Chawera, Oghond Ise, Rom Tu Ramdenime Gunds Eqneba Ertnairi Qula, Programa Adgilebs
                                                   //Gansazgvravs Anbanis Mixedvit!
                {
                    Championship[] MyChampionship = ChampRank.Where(x => x.ChampionshipPoints == arr[i]).OrderBy(s => s.FootballTeamName).ToArray();
                    for (int j = 0; j < MyChampionship.Length; j++)
                        Championships.Add(MyChampionship[j]);
                }
                //--------Optimizaciis Shedegad Qulebis, Gundebisa Da Mat Mier Dakavebuli Adgilebis Bazashi Chawera----------
                for (int i = 0; i < Championships.Count; i++)//Am Moqmedebis Shedegad Ki Xdeba, Gundebis,Mat Mier Dakavebuli
                                                             //Adgilebisa Da Qulebis Bazashi, Championship Cxrilshi, Chawera Sworad Da Dalagebulad!
                {
                    db.Championship.Add(new Championship()
                    {
                        ChampionshipPoints = Championships[i].ChampionshipPoints,
                        FootballTeamName = Championships[i].FootballTeamName,
                        Place = i + 1
                    });
                }
                db.SaveChanges();//Vamaxsovrebt Cvlilebebs               
                Console.WriteLine("Bazashi Chaiwera Titoeuli Gundis Mier Mopovebuli Qula Da Turnirze Dakavebuli Adgili!");
            }
        }
    }
    public class FootballChampionship : DbContext
    {
        public DbSet<FootballTeam> FootballTeam { get; set; }
        public DbSet<ChampionshipFixtureResults> ChampionshipFixtureResults { get; set; }
        public DbSet<Championship> Championship { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-UNEAH8K;database=FootballChampionship;integrated security=true;");
        }
    }
    public class FootballTeam
    {
        [Key]
        public int FootballTeamId { get; set; }
        public string FootballTeamName { get; set; }
    }
    public class ChampionshipFixtureResults
    {
        [Key]
        public int ChampionshipFixtureResultId { get; set; }
        public string FootballTeam1 { get; set; }
        public int GoalsScoredByFirstTeam { get; set; }
        public string FootballTeam2 { get; set; }
        public int GoalsScoredBySecondTeam { get; set; }
    }
    public class Championship
    {
        [Key]
        public int ChampionshipRankId { get; set; }
        public string FootballTeamName { get; set; }
        public int Place { get; set; }
        public int ChampionshipPoints { get; set; }
    }
}
