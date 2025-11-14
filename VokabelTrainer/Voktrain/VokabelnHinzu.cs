using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voktrain
{
    public class VokabelnHinzu
    {
        private readonly DataManager _data;
        public VokabelnHinzu(DataManager data) { _data = data; }
        public void Run()
        {
            Console.Clear();
            Console.WriteLine(@"
                 _   _         _            _            _  _                 _                    
                | | | |       | |          | |          | || |               (_)                   
                | | | |  ___  | | __  __ _ | |__    ___ | || |_  _ __   __ _  _  _ __    ___  _ __ 
                | | | | / _ \ | |/ / / _` || '_ \  / _ \| || __|| '__| / _` || || '_ \  / _ \| '__|
                \ \_/ /| (_) ||   < | (_| || |_) ||  __/| || |_ | |   | (_| || || | | ||  __/| |   
                 \___/  \___/ |_|\_\ \__,_||_.__/  \___||_| \__||_|    \__,_||_||_| |_| \___||_|   

                VOKABELMANAGER - VOKABEL HINZUFÜGEN
                =================
            ");
            string deutsch = "";
            string englisch = "";
            bool inputValid = false;
            while (!inputValid)
            {
                Console.Write("Deutsches Wort: ");
                deutsch = (Console.ReadLine() ?? "").Trim();
                if (!string.IsNullOrWhiteSpace(deutsch))
                {
                    inputValid = true;
                }
                else
                {
                    Console.WriteLine("Bitte geben Sie ein gültiges deutsches Wort ein.");
                }
            }
            inputValid = false;
            while (!inputValid)
            {
                Console.Write("Englisches Wort: ");
                englisch = (Console.ReadLine() ?? "").Trim();
                if (!string.IsNullOrWhiteSpace(englisch))
                {
                    inputValid = true;
                }
                else
                {
                    Console.WriteLine("Bitte geben Sie ein gültiges englisches Wort ein.");
                }
            }

            string prompt = $@"
                 _   _         _            _            _  _                 _                    
                | | | |       | |          | |          | || |               (_)                   
                | | | |  ___  | | __  __ _ | |__    ___ | || |_  _ __   __ _  _  _ __    ___  _ __ 
                | | | | / _ \ | |/ / / _` || '_ \  / _ \| || __|| '__| / _` || || '_ \  / _ \| '__|
                \ \_/ /| (_) ||   < | (_| || |_) ||  __/| || |_ | |   | (_| || || | | ||  __/| |   
                 \___/  \___/ |_|\_\ \__,_||_.__/  \___||_| \__||_|    \__,_||_||_| |_| \___||_|   

                VOKABELMANAGER - VOKABEL HINZUFÜGEN
                =================
                Deutsches Wort = {deutsch}
                Englisches Wort = {englisch}
                Bitte wähle den Schwierigkeitsgrad:";
            string[] options = { "Leicht", "Mittel", "Schwer", "Abbrechen" };

            Menu diffMenu = new Menu(prompt, options);
            int selected = diffMenu.RunV();

            Difficulty chosenDiff;

            switch (selected)
            {
                case 0:
                    chosenDiff = Difficulty.Leicht;
                    break;
                case 1:
                    chosenDiff = Difficulty.Mittel;
                    break;
                case 2:
                    chosenDiff = Difficulty.Schwer;
                    break;
                default:
                    Console.WriteLine("Vorgang abgebrochen.");
                    return; // Abbrechen
            }

            // Key-Funktion (normiert)
            string Key(string de, string en)
                => $"{de.Trim().ToLowerInvariant()}|{en.Trim().ToLowerInvariant()}";

            // Existenz prüfen – vergleiche gegen aktuelle Eingaben
            bool exists = _data.WordList.Any(v => Key(v.Deutsch, v.Englisch) == Key(deutsch, englisch));
            if (exists)
            {
                Console.WriteLine("Diese Vokabel gibt es schon.");
                Console.WriteLine("Taste drücken, um zurückzukehren...");
                Console.ReadKey(true);
                return;   
            }
            else
            {
                Console.Clear();
                prompt = $@"
                 _   _         _            _            _  _                 _                    
                | | | |       | |          | |          | || |               (_)                   
                | | | |  ___  | | __  __ _ | |__    ___ | || |_  _ __   __ _  _  _ __    ___  _ __ 
                | | | | / _ \ | |/ / / _` || '_ \  / _ \| || __|| '__| / _` || || '_ \  / _ \| '__|
                \ \_/ /| (_) ||   < | (_| || |_) ||  __/| || |_ | |   | (_| || || | | ||  __/| |   
                 \___/  \___/ |_|\_\ \__,_||_.__/  \___||_| \__||_|    \__,_||_||_| |_| \___||_|   

                VOKABELMANAGER - VOKABEL HINZUFÜGEN
                =================
                Deutsches Wort = {deutsch}
                Englisches Wort = {englisch}
                Schwierigkeit = {chosenDiff}
                
                Sollen diese Eingaben so gespeichert werden?

                ";
                options = new[] { "Ja", "Nein / Abbrechen" };
                Menu saveMenu = new Menu(prompt, options);
                int save = saveMenu.RunV();
                switch (save) { 
                    case 0:
                        try
                        {
                            _data.AddVokabel(deutsch, englisch, chosenDiff);
                            Console.WriteLine($"Vokabel '{deutsch} - {englisch}' wurde gespeichert.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Fehler: {ex.Message}");
                        }
                        Console.WriteLine("Taste drücken, um zurückzukehren...");
                        Console.ReadKey(true);
                        return;
                    case 1:
                        return;
                }
            }
            ;

            Console.WriteLine($"Vokabel '{deutsch} - {englisch}' wurde hinzugefügt.");
            Console.WriteLine("Taste drücken, um zurückzukehren...");
            Console.ReadKey(true);
            return;

        }
    }
}
