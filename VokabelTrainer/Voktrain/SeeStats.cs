using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.Net.Mime.MediaTypeNames;

namespace Voktrain
{
    public class SeeStats
    {
        private readonly DataManager _data;

        public SeeStats(DataManager data)
        {
            _data = data;
        }
        public void StatMenu()
        {
            MainStatMenu();
        }
        private void MainStatMenu()
        {
            string prompt = @"
                 _   _         _            _            _  _                 _                    
                | | | |       | |          | |          | || |               (_)                   
                | | | |  ___  | | __  __ _ | |__    ___ | || |_  _ __   __ _  _  _ __    ___  _ __ 
                | | | | / _ \ | |/ / / _` || '_ \  / _ \| || __|| '__| / _` || || '_ \  / _ \| '__|
                \ \_/ /| (_) ||   < | (_| || |_) ||  __/| || |_ | |   | (_| || || | | ||  __/| |   
                 \___/  \___/ |_|\_\ \__,_||_.__/  \___||_| \__||_|    \__,_||_||_| |_| \___||_|   

                STATISTIKEN
                Nutze <- und -> zum Wählen zwischen Statistik pro Vokabel oder pro Test
                Nutze Pfeiltasten Auf und Ab zum Blättern der Seiten
                Mit ENTER Zurück ins Hauptmenü
                ";

            string[] options = { "Vokabeln", "Tests", "Zurück ins Hauptmenü" };
            Menu mainMenu = new Menu(prompt, options);
            int SelectIndex = mainMenu.RunH();

            switch (SelectIndex)
            {
                case 0:
                    VokabelStatistik();
                    break;
                case 1:
                    TestStatistik();
                    break;
                case 2:
                    MainMenu();
                    return;
            }


        }
        private void MainMenu()
        {
            VokabelTrainer trainer = new VokabelTrainer();
            trainer.Start();
        }
        private void VokabelStatistik()
        {
            var ListStats = _data.GetAllVokabeln();

            const int PAGE_SIZE = 20;
            int page = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Vokabel Statistik (Seite {page+1} von {ListStats.Count/20+1})");
                Console.WriteLine("------------------------------------");

                var seitenInhalt = ListStats
              .Skip(page * PAGE_SIZE)
              .Take(PAGE_SIZE);
                WriteLine($@" Vokabelpaar                            | Geübt | Richtig | Richtig% | Schwierigkeit");
                foreach (var Stat in seitenInhalt)
                {
                    WriteLine($@"{Stat.Deutsch,18} - {Stat.Englisch,18} | {Stat.GeuebtGesamt,5} | {Stat.RichtigGesamt,7} | {Stat.ProzentRichtig,7:F2}% | {Stat.Schwierigkeit,2}");
                }
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Verwende Pfeiltasten <- und -> zum Blättern, ENTER zum Zurückkehren.");
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.RightArrow)
                {
                    if ((page + 1) * PAGE_SIZE < ListStats.Count)
                    {
                        page++;
                    }
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    if (page > 0)
                    {
                        page--;
                    }
                }
                else if (key == ConsoleKey.Enter)
                {
                    MainStatMenu();
                    break; // Exit the loop on ENTER
                }


            }
        }
        private void TestStatistik()
        {
            var ListTests = _data.GettAllTests();

            const int PAGE_SIZE = 20;
            int page = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Vokabel Statistik (Seite {page+1} von {ListTests.Count/20+1})");
                Console.WriteLine("------------------------------------");

                var seitenInhalt = ListTests
              .Skip(page * PAGE_SIZE)
              .Take(PAGE_SIZE);
                WriteLine($@" Test Nr. | Anzahl Fragen | Richtig | Richtig% | Schwierigkeit");
                foreach (var Test in seitenInhalt)
                {
                    WriteLine($@"{Test.Name,9} | {Test.AskedQuestions,13} | {Test.CorrectAnswers,7} | {Test.PercentageCorrect,7 :F2}% | {Test.Difficulty,2}");
                }
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Verwende Pfeiltasten <- und -> zum Blättern, ENTER zum Zurückkehren.");
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.RightArrow)
                {
                    if ((page + 1) * PAGE_SIZE < ListTests.Count)
                    {
                        page++;
                    }
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    if (page > 0)
                    {
                        page--;
                    }
                }
                else if (key == ConsoleKey.Enter)
                {
                    MainStatMenu();
                    break; // Exit the loop on ENTER
                }


            }

            
            

        }
    }
}
