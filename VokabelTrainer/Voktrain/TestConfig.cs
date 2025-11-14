using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voktrain
{
    internal class TestConfig
    {
        public void TestAnpassen(TestConfigStart owner)
        {
            TestEinstellungen1(owner);
        }
        private void TestEinstellungen1(TestConfigStart owner)
        {
            string prompt = @"
                 _   _         _            _            _  _                 _                    
                | | | |       | |          | |          | || |               (_)                   
                | | | |  ___  | | __  __ _ | |__    ___ | || |_  _ __   __ _  _  _ __    ___  _ __ 
                | | | | / _ \ | |/ / / _` || '_ \  / _ \| || __|| '__| / _` || || '_ \  / _ \| '__|
                \ \_/ /| (_) ||   < | (_| || |_) ||  __/| || |_ | |   | (_| || || | | ||  __/| |   
                 \___/  \___/ |_|\_\ \__,_||_.__/  \___||_| \__||_|    \__,_||_||_| |_| \___||_|   
                TEST ANPASSEN
                ==================
                1. Richtung des Tests festlegen.
                 Frage auf Deutsch -> Antworten auf Englisch
                 Frage auf Englisch -> Antworten auf Deutsch
                 Zurück zum Hauptmenü
                Wähle die Richtung:
                ";
            string[] options = { "Deutsch -> Englisch", "Englisch -> Deutsch", "Zurück ins Hauptmenü" };
            Menu mainMenu = new Menu(prompt, options);
            int SelectIndex = mainMenu.RunV();
            switch (SelectIndex)
            {
                case 0:
                    owner.DeutschNachEnglisch = true;                    
                    TestEinstellungen2(owner);
                    TestEinstellungen3(owner);
                    break;
                case 1:
                    owner.DeutschNachEnglisch = false;
                    TestEinstellungen2(owner);
                    TestEinstellungen3(owner);
                    break;
                case 2:
                    return; // back
            }
        }
        private void TestEinstellungen2(TestConfigStart owner)
        {
           string prompt = @"
                 _   _         _            _            _  _                 _                    
                | | | |       | |          | |          | || |               (_)                   
                | | | |  ___  | | __  __ _ | |__    ___ | || |_  _ __   __ _  _  _ __    ___  _ __ 
                | | | | / _ \ | |/ / / _` || '_ \  / _ \| || __|| '__| / _` || || '_ \  / _ \| '__|
                \ \_/ /| (_) ||   < | (_| || |_) ||  __/| || |_ | |   | (_| || || | | ||  __/| |   
                 \___/  \___/ |_|\_\ \__,_||_.__/  \___||_| \__||_|    \__,_||_||_| |_| \___||_|   

                TEST ANPASSEN
                ==================
               2. Schwierigkeitsgrad festlegen für den Test.
                 Leicht   - Niveau A1
                 Mittel   - Niveau B1
                 Schwer   - Niveau C1
                 Alle     - Alle Gemischt.
                 Zurück zum Hauptmenü
                Wähle Schwierigkeitsgrad:
                ";

            string[] options = { "Leicht", "Mittel", "Schwer", "Alle", "Zurück ins Hauptmenü" };
            Menu mainMenu = new Menu(prompt, options);
            int SelectIndex = mainMenu.RunV();

            switch (SelectIndex)
            {
                case 0:
                    owner.TestSchwierigkeit = Difficulty.Leicht;                    
                    break;
                case 1:
                    owner.TestSchwierigkeit = Difficulty.Mittel;
                    break;
                case 2:
                    owner.TestSchwierigkeit = Difficulty.Schwer;
                    break;
                case 3:
                    owner.TestSchwierigkeit = Difficulty.Alle;
                    break;
                case 4:
                    return; // back
            }

        }
        private void TestEinstellungen3(TestConfigStart owner)
        {

            Console.Clear();
            int MaxAnzahl = owner.GetFragenPool().Count;

            Console.WriteLine( $@"
                 _   _         _            _            _  _                 _                    
                | | | |       | |          | |          | || |               (_)                   
                | | | |  ___  | | __  __ _ | |__    ___ | || |_  _ __   __ _  _  _ __    ___  _ __ 
                | | | | / _ \ | |/ / / _` || '_ \  / _ \| || __|| '__| / _` || || '_ \  / _ \| '__|
                \ \_/ /| (_) ||   < | (_| || |_) ||  __/| || |_ | |   | (_| || || | | ||  __/| |   
                 \___/  \___/ |_|\_\ \__,_||_.__/  \___||_| \__||_|    \__,_||_||_| |_| \___||_|   

                TEST ANPASSEN
                ==================
               3. Wieviele Fragen soll der Test enthalten?
                   Maximal {MaxAnzahl} Fragen. 
                ");
            Console.Clear();
            Console.WriteLine($"[Debug] Gewählte Schwierigkeit: {owner.TestSchwierigkeit}");
            Console.WriteLine($"[Debug] Gesamtanzahl Wörter im DataManager: {owner.GetFragenPool().Count} (nach Filter)");

            bool input = false;
            while (input == false)
            {
                Console.Write("Gib die Anzahl der Fragen ein: ");
                int UserInput = Convert.ToInt32(Console.ReadLine());
                if (UserInput > 0 && UserInput <= MaxAnzahl)
                {
                    owner.FragenAnzahl = UserInput;
                    input = true;
                }
                else
                {
                    Console.WriteLine($"Ungültige Eingabe. Bitte eine Zahl zwischen 1 und {owner.FragenAnzahl} eingeben.");
                }
            }
            owner.TestMenu();

        }
    }
}
