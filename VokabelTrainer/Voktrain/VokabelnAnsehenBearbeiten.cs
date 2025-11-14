using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voktrain
{
    public class VokabelnAnsehenBearbeiten
    {
        private readonly DataManager _data;
        public VokabelnAnsehenBearbeiten(DataManager data)
        {
            _data = data;
        }
        public void Run()
        {
            while (true)                               
            {
                Console.Clear();
                string prompt1 = $@"
                     _   _         _            _            _  _                 _                    
                    | | | |       | |          | |          | || |               (_)                   
                    | | | |  ___  | | __  __ _ | |__    ___ | || |_  _ __   __ _  _  _ __    ___  _ __ 
                    | | | | / _ \ | |/ / / _` || '_ \  / _ \| || __|| '__| / _` || || '_ \  / _ \| '__|
                    \ \_/ /| (_) ||   < | (_| || |_) ||  __/| || |_ | |   | (_| || || | | ||  __/| |   
                     \___/  \___/ |_|\_\ \__,_||_.__/  \___||_| \__||_|    \__,_||_||_| |_| \___||_|   

                    VOKABELN ANSEHEN & BEARBEITEN
                    =============================
                    Nutze die Pfeiltasten ←/→ & Enter zum Auswählen. 
            
                ";
                String[] options1 = { "Leicht", "Mittel", "Schwer", "Suchen", "Zurück zum Hauptmenü" };
                Menu menu1 = new Menu(prompt1, options1);
                int selectedIndex1 = menu1.RunH();
                Difficulty diff;
                switch (selectedIndex1)
                {
                    case 0: ShowListFor(Difficulty.Leicht); break;  
                    case 1: ShowListFor(Difficulty.Mittel); break;
                    case 2: ShowListFor(Difficulty.Schwer); break;
                    case 3: Suchen(); break;  
                    case 4: return;                                   
                    default: return;
                }                
            }
                
        }
        private void ShowListFor(Difficulty diff)
        {
            var filtered = _data.WordList
                .Where(v => v.Schwierigkeit == diff)
                .OrderBy(v => v.Deutsch)
                .ToList();

            if (filtered.Count == 0)
            {
                Console.WriteLine("Keine Vokabeln in dieser Kategorie.");
                Console.WriteLine("Taste drücken…");
                Console.ReadKey(true);
                return;                            
            }

            const int PAGE_SIZE = 20;
            int page = 0;

            while (true)
            {
                var pageItems = filtered
                    .Skip(page * PAGE_SIZE)
                    .Take(PAGE_SIZE)
                    .Select(v => $" {v.Deutsch,18} — {v.Englisch,18}  ")
                    .ToArray();

                var options2 = pageItems.Concat(new[]{
            page > 0 ? " ← Vorherige Seite " : " - ",
            (page+1)*PAGE_SIZE < filtered.Count ? " Nächste Seite → " : " - ",
            " Schwierigkeit wechseln ",   
            " Zurück "
        }).ToArray();
                string prompt2 = $@"
                Bearbeiten: {diff}   |   Seite {page + 1}/{Math.Max(1, (int)Math.Ceiling(filtered.Count / (double)PAGE_SIZE))}
                (Nutze die Pfeiltasten ←/→ und ↑/↓ zum Navigieren. Enter zum Auswählen.)
                ";

                var menu = new Menu(prompt2, options2);
                int sel = menu.RunV();
                var keyInfo = menu.LastKey;

                if (sel < pageItems.Length)
                {
                    var vok = filtered[page * PAGE_SIZE + sel];
                    OpenWordActions(vok);

                    
                    filtered = _data.WordList
                        .Where(v => v.Schwierigkeit == diff)
                        .OrderBy(v => v.Deutsch)
                        .ToList();
                    if (page * PAGE_SIZE >= filtered.Count && page > 0) page--;
                    continue;
                }
                if (menu.LastKey == ConsoleKey.RightArrow)
                {
                    if ((page + 1) * PAGE_SIZE < filtered.Count) page++;
                    continue;
                }
                if (menu.LastKey == ConsoleKey.LeftArrow)
                {
                    if (page > 0) page--;
                    continue;
                }

                if (sel == pageItems.Length) { if (page > 0) page--; continue; }          
                if (sel == pageItems.Length + 1) { if ((page + 1) * PAGE_SIZE < filtered.Count) page++; continue; } 
                if (sel == pageItems.Length + 2) return;                                      
                /* sel == pageItems.Length + 3 */
                return;                                      
            }
        }

        private void OpenWordActions(Vokabel v)
        {
            while (true)
            {
                Console.Clear();
                string prompt = $@"
                 _   _         _            _            _  _                 _                    
                | | | |       | |          | |          | || |               (_)                   
                | | | |  ___  | | __  __ _ | |__    ___ | || |_  _ __   __ _  _  _ __    ___  _ __ 
                | | | | / _ \ | |/ / / _` || '_ \  / _ \| || __|| '__| / _` || || '_ \  / _ \| '__|
                \ \_/ /| (_) ||   < | (_| || |_) ||  __/| || |_ | |   | (_| || || | | ||  __/| |   
                 \___/  \___/ |_|\_\ \__,_||_.__/  \___||_| \__||_|    \__,_||_||_| |_| \___||_|   

                VOKABELN ANSEHEN & BEARBEITEN
                =============================
                
                Wortdetails:
                Deutsch:   {v.Deutsch}
                Englisch:  {v.Englisch}
                Level:     {v.Schwierigkeit}
                Geübt:     {v.GeuebtGesamt}   Richtig: {v.RichtigGesamt}   ({v.ProzentRichtig:F1}%)
                ------------------------------

                Was möchtest du tun?
                (Wähle mit ↑/↓ und ENTER)
                ";
                                string[] options =
                                {
                            "Bearbeiten",
                            "Schwierigkeit ändern",
                            "Statistik zurücksetzen",
                            "Löschen",
                            "Zurück"
                        };

                var menu = new Menu(prompt, options);
                int sel = menu.RunV();

                switch (sel)
                {
                    case 0: EditWord(v); break;
                    case 1: ChangeDifficulty(v); break;
                    case 2: ResetStats(v); break;
                    case 3: DeleteWord(v); return; 
                    case 4: return; 
                }
            }
        }
        private void EditWord(Vokabel v)
        {
            Console.Clear();
            Console.WriteLine("Vokabel bearbeiten:");
            Console.WriteLine($"Aktuell: Deutsch = '{v.Deutsch}', Englisch = '{v.Englisch}'");
            Console.Write("Neues deutsches Wort (leer lassen zum Beibehalten): ");
            string newDe = Console.ReadLine();
            Console.Write("Neues englisches Wort (leer lassen zum Beibehalten): ");
            string newEn = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDe)) v.Deutsch = newDe;
            if (!string.IsNullOrWhiteSpace(newEn)) v.Englisch = newEn;
            _data.SaveVokabelnNachDifficulty();
            Console.WriteLine("Änderungen gespeichert. Taste drücken...");
            Console.ReadKey(true);
        }
        private void ChangeDifficulty(Vokabel v)
        {
            string prompt = "Neuen Schwierigkeitsgrad wählen:";
            string[] opts = { "Leicht", "Mittel", "Schwer", "Abbrechen" };
            var menu = new Menu(prompt, opts);
            int sel = menu.RunV();

            if (sel < 3)
            {
                v.Schwierigkeit = (Difficulty)sel;
                _data.SaveVokabelnNachDifficulty();
                Console.WriteLine("Schwierigkeit geändert. Taste drücken...");
                Console.ReadKey(true);
            }
        }
        private void ResetStats(Vokabel v)
        {
            Console.Clear();
            Console.WriteLine("Statistik zurücksetzen:");
            Console.WriteLine($"Aktuell: Geübt = {v.GeuebtGesamt}, Richtig = {v.RichtigGesamt}, Prozent Richtig = {v.ProzentRichtig:F1}%");
            Console.Write("Bist du sicher, dass du die Statistik zurücksetzen möchtest? (j/n): ");
            string input = Console.ReadLine();
            if (input.Equals("j", StringComparison.InvariantCultureIgnoreCase))
            {
                v.GeuebtGesamt = 0;
                v.RichtigGesamt = 0;
                _data.SaveWordStats();               
            }
            else
            {
                Console.WriteLine("Abgebrochen. Statistik nicht zurückgesetzt.");
            }
            Console.WriteLine("Drücke eine Taste, um fortzufahren...");
            Console.ReadKey(true);
        }
        private void DeleteWord(Vokabel v)
        {
            string prompt = $"Wirklich löschen: {v.Deutsch} – {v.Englisch}?";
            string[] opts = { "Ja", "Nein" };
            var menu = new Menu(prompt, opts);
            int sel = menu.RunV();
            if (sel == 0)
            {
                _data.WordList.Remove(v);
                _data.SaveVokabelnNachDifficulty();
                _data.SaveWordStats();
                Console.WriteLine("Vokabel gelöscht. Taste drücken...");
                Console.ReadKey(true);
            }
        }
        private void Suchen()
        {
            Console.Clear();
            Console.Write(@"
                 _   _         _            _            _  _                 _                    
                | | | |       | |          | |          | || |               (_)                   
                | | | |  ___  | | __  __ _ | |__    ___ | || |_  _ __   __ _  _  _ __    ___  _ __ 
                | | | | / _ \ | |/ / / _` || '_ \  / _ \| || __|| '__| / _` || || '_ \  / _ \| '__|
                \ \_/ /| (_) ||   < | (_| || |_) ||  __/| || |_ | |   | (_| || || | | ||  __/| |   
                 \___/  \___/ |_|\_\ \__,_||_.__/  \___||_| \__||_|    \__,_||_||_| |_| \___||_|   

                VOKABELN ANSEHEN & BEARBEITEN
                =============================
                Suchbegriff eingeben (Deutsch oder Englisch): ");
            string term = Console.ReadLine().Trim();
            var results = _data.WordList
                .Where(v => v.Deutsch.Contains(term, StringComparison.InvariantCultureIgnoreCase) ||
                            v.Englisch.Contains(term, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(v => v.Deutsch)
                .ToList();
            if (results.Count == 0)
            {
                Console.WriteLine("Keine Vokabeln gefunden. Taste drücken...");
                Console.ReadKey(true);
                return;
            }
            foreach (var vok in results)
            {
                OpenWordActions(vok);
            }
        }

    }
}
