using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballChampionship
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new FootballChampionship8())
            {
                //--------------------------------Safexburto Gundebis Sheqmna Da Bazashi Chawera---------------------------------------

                Console.WriteLine("Shemoitanet Chempionatshi Monawile Gundebis Raodenoba!");
                int z = Convert.ToInt32(Console.ReadLine()); //Shemogvaq Gundebis Raodenoba
                if (z < 5)
                    throw new Exception("Shecdoma!Gundebis Raodenoba Ar Unda Iyos 5-ze Naklebi!"); // Agdebs Shecdomas Im Shemtxvevashi, Tu Gundebis Raodenoba Naklebia 5-ze
                Console.WriteLine("Shemoitanet Gundebis Saxelebi");
                for (int i = 0; i < z; i++)
                {
                    db.FootballTeams.Add(new FootballTeams() { FootballTeamName = Console.ReadLine() }); //Shemogvaq Gundebis Saxelebi Da Vwert Bazashi, FootballTeam Cxrilshi!
                }
                db.SaveChanges(); //Vamaxsovrebt Cvlilebebs!
                Console.WriteLine("Gundebis Saxelebi Chaiwera Bazashi!");
                
                //---------------------------------Matchebis Sheqmna Da Shesabamisad Shedegebis Bazashi Chawera-------------------

                Console.WriteLine("Shemoitanet Chatarebuli Matchebis Angarishebi");
                for (int i = 0; i < z - 1; i++)//Shemogvaq Matchebis Angarishebi
                {
                    for (int j = i + 1; j < z; j++)
                    {
                        Console.WriteLine("Shemoitanet Shemdegi Matchis Angarishi:" + db.FootballTeams.ToArray()[i].FootballTeamName +
                            " - " + db.FootballTeams.ToArray()[j].FootballTeamName);
                        db.Matches.Add(new Matches()
                        {

                            FirstTeam = db.FootballTeams.ToArray()[i],
                            GoalsScoredByFirstTeam = Convert.ToInt32(Console.ReadLine()),
                            SecondTeam = db.FootballTeams.ToArray()[j],
                            GoalsScoredBySecondTeam = Convert.ToInt32(Console.ReadLine())
                        });
                        //Angarishebs Vwert Bazashi, ChampionshipFixtureResults Cxrilshi
                    }
                }
                db.SaveChanges();//Vamaxsovrebt Cvlilebebs
                Console.WriteLine("Shexvedrebis Angarishebi Chaiwera Bazashi!");

                //---------------------------------------------Gundebis Mier Dagrovili Qulebis Gamotvla------------------
                
                int q = db.Matches.ToArray().Length;
                int dict;// Shemogvaq Es Integeri, Qvemot Moyvanili ChampRank-is Damxmared          
                Ranks[] ChampRank = new Ranks[z];//ChampRank-i Gvchirdeba Qulebisa,Gundebisa Da Adgilebis Ranks Cxrilshi Sworad Chasawerad
                for (int i = 0; i < z; i++)//Am Kodit Xdeba Qulebis Gamotvla
                {
                    dict = 0;
                    for (int j = 0; j < q; j++)
                    {
                        if (db.Matches.ToArray()[j].FirstTeam.FootballTeamName == db.FootballTeams.ToArray()[i].FootballTeamName)
                        {
                            if (db.Matches.ToArray()[j].GoalsScoredByFirstTeam < db.Matches.ToArray()[j].GoalsScoredBySecondTeam)
                            {
                                dict = dict - 1;
                            }
                            if (db.Matches.ToArray()[j].GoalsScoredByFirstTeam > db.Matches.ToArray()[j].GoalsScoredBySecondTeam)
                                dict += 3;
                        }
                        else if (db.FootballTeams.ToArray()[i].FootballTeamName == db.Matches.ToArray()[j].SecondTeam.FootballTeamName)
                        {
                            if (db.Matches.ToArray()[j].GoalsScoredBySecondTeam < db.Matches.ToArray()[j].GoalsScoredByFirstTeam)
                            {
                                dict -= 1;
                            }
                            if (db.Matches.ToArray()[j].GoalsScoredBySecondTeam > db.Matches.ToArray()[j].GoalsScoredByFirstTeam)
                                dict += 3;
                        }
                    }
                    ChampRank[i] = new Ranks() { Team=db.FootballTeams.ToArray()[i],ChampionshipPoints = dict }; //ChampRank-is shevseba
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
                List<Ranks> Championships = new List<Ranks>();//Shemogvaq Kidev Erti Damxmare Listi
                for (int i = 0; i < arr.Count; i++)//Am Moqmedebis Mizani Aris Championships Listshi Monacemebis Qulebis 
                                                   //Klebadobis Mixedvis Chawera, Oghond Ise, Rom Tu Ramdenime Gunds Eqneba Ertnairi Qula, Programa Adgilebs
                                                   //Gansazgvravs Anbanis Mixedvit!
                {
                    Ranks[] MyChampionship = ChampRank.Where(x => x.ChampionshipPoints == arr[i]).OrderBy(s => s.Team.FootballTeamName).ToArray();
                    for (int j = 0; j < MyChampionship.Length; j++)
                    {
                        Championships.Add(MyChampionship[j]);
                        Console.WriteLine(MyChampionship[j].Team.FootballTeamName);
                    }
                }
                //--------Optimizaciis Shedegad Qulebis, Gundebisa Da Mat Mier Dakavebuli Adgilebis Bazashi Chawera----------
                for (int i = 0; i <Championships.Count; i++)
                {
                    db.Ranks.Add(new Ranks()
                    {
                        Team = Championships[i].Team,
                        Place = i + 1,
                        ChampionshipPoints = Championships[i].ChampionshipPoints
                    }) ;
                    db.SaveChanges();//Bazashi Damaxsovreba
                }                             
                Console.WriteLine("Bazashi Chaiwera Titoeuli Gundis Mier Mopovebuli Qula Da Turnirze Dakavebuli Adgili!");          
            }
        }
        public class FootballChampionship8 : DbContext
        {
            public DbSet<FootballTeams> FootballTeams { get; set; }
            public DbSet<Matches> Matches { get; set; }
            public DbSet<Ranks> Ranks { get; set; }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("server=DESKTOP-UNEAH8K;database=FootballChampionship8;integrated security=true;");
            }
        }
        public class FootballTeams
        {
            public int FootballTeamsId { get; set; }
            public string FootballTeamName { get; set; }
        }
        public class Matches
        {
            public int MatchesId { get; set; }
            public virtual FootballTeams FirstTeam { get; set; }
            public int GoalsScoredByFirstTeam { get; set; }
            public virtual FootballTeams SecondTeam { get; set; }
            public int GoalsScoredBySecondTeam { get; set; }
        }
        public class Ranks
        {
            public int RanksId { get; set; }
            public virtual FootballTeams Team { get; set; }
            public int Place { get; set; }
            public int ChampionshipPoints { get; set; }
        }
    }
}
