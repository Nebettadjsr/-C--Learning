using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voktrain
{
    public class TestStarten
    {
        private readonly DataManager _data;
        public TestStarten(DataManager data) { _data = data; }
        private readonly Random _rnd = new Random();
        private void Shuffle<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = _rnd.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        public void Run(TestConfigStart owner)
        {
            Console.Clear();

            // 1) build the question pool based on current difficulty
            var pool = owner.GetFragenPool();

            // 2) guard: empty pool?
            if (pool.Count == 0)
            {
                Console.WriteLine("Keine Wörter für diesen Schwierigkeitsgrad vorhanden.");
                Console.WriteLine("Taste drücken, um zurückzukehren...");
                Console.ReadKey(true);
                return;
            }

            // 3) choose how many questions
            int n = Math.Min(owner.FragenAnzahl, pool.Count);

            // TODO(next): shuffle pool and take n; then ask questions
            Console.WriteLine($"Starte Test: {owner.TestSchwierigkeit}, Fragen: {n}, Richtung: {(owner.DeutschNachEnglisch ? "DE→EN" : "EN→DE")}");
            Console.WriteLine("Taste zum Start...");
            Console.ReadKey(true);

            int asked = 0;
            int correct = 0;
            Shuffle(pool); // optional but nice
            var questions = pool.Take(n).ToList();


            foreach (var (v, idx) in questions.Select((v, i) => (v, i)))
            {
                asked++;
                bool ok = AskOneMC(v, pool, owner.DeutschNachEnglisch, idx + 1, n);  // one question per call
                if (ok)
                {
                    correct++;
                    _data.RecordCorrect(v);
                }
                else
                {
                    _data.RecordWrong(v);
                }
            }
            Console.Clear();
            _data.SaveWordStats();
            var result = new TestResult
            {
                Difficulty = owner.TestSchwierigkeit,
                AskedQuestions = asked,
                CorrectAnswers = correct
            };

            _data.AppendTestResult(result);
            Console.WriteLine($@"
                 _   _         _            _            _  _                 _                    
                | | | |       | |          | |          | || |               (_)                   
                | | | |  ___  | | __  __ _ | |__    ___ | || |_  _ __   __ _  _  _ __    ___  _ __ 
                | | | | / _ \ | |/ / / _` || '_ \  / _ \| || __|| '__| / _` || || '_ \  / _ \| '__|
                \ \_/ /| (_) ||   < | (_| || |_) ||  __/| || |_ | |   | (_| || || | | ||  __/| |   
                 \___/  \___/ |_|\_\ \__,_||_.__/  \___||_| \__||_|    \__,_||_||_| |_| \___||_|   
                TEST 
                ==================
                
                Test beendet! Richtig beantwortet: {correct}/{asked} ({(double)correct / asked * 100:F1}%)");
            Console.WriteLine("Drücke eine Taste, um fortzufahren...");
            Console.ReadKey(true);
            owner.TestMenu();
        }

        private (string[] options, int correctIdx) BuildOptions(
            Vokabel v, List<Vokabel> pool, bool deToEn)
        {
            // 1) the correct answer text
            string correct = deToEn ? v.Englisch : v.Deutsch;

            // 2) collect candidates for wrong answers
            //    (use the *same side* as the correct answer, exclude this word)
            var candidates = pool
                .Where(x => !ReferenceEquals(x, v))
                .Select(x => deToEn ? x.Englisch : x.Deutsch)
                .Distinct(StringComparer.InvariantCultureIgnoreCase)
                .Where(s => !string.Equals(s, correct, StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            // 3) pick up to 3 random wrong answers
            //    (shuffle candidates, then take 3)
            for (int i = candidates.Count - 1; i > 0; i--) { int j = _rnd.Next(i + 1); (candidates[i], candidates[j]) = (candidates[j], candidates[i]); }
            var wrongs = candidates.Take(3).ToList();

            // 4) combine and shuffle
            var all = new List<string>(wrongs) { correct };
            for (int i = all.Count - 1; i > 0; i--) { int j = _rnd.Next(i + 1); (all[i], all[j]) = (all[j], all[i]); }

            int correctIdx = all.FindIndex(s => string.Equals(s, correct, StringComparison.InvariantCultureIgnoreCase));
            return (all.ToArray(), correctIdx);
        }
        private bool AskOneMC(Vokabel v, List<Vokabel> pool, bool deToEn, int frageNummer, int totalQuestions)
        {
            string question = deToEn ? v.Deutsch : v.Englisch;
            var (options, correctIdx) = BuildOptions(v, pool, deToEn);

                string prompt =$@"
                 _   _         _            _            _  _                 _                    
                | | | |       | |          | |          | || |               (_)                   
                | | | |  ___  | | __  __ _ | |__    ___ | || |_  _ __   __ _  _  _ __    ___  _ __ 
                | | | | / _ \ | |/ / / _` || '_ \  / _ \| || __|| '__| / _` || || '_ \  / _ \| '__|
                \ \_/ /| (_) ||   < | (_| || |_) ||  __/| || |_ | |   | (_| || || | | ||  __/| |   
                 \___/  \___/ |_|\_\ \__,_||_.__/  \___||_| \__||_|    \__,_||_||_| |_| \___||_|   
                TEST 
                ==================
                Frage {frageNummer} von {totalQuestions}

                Was ist die korrekte Übersetzung von 

>>>' {question} '<<< ? 

                (Wähle mit ↑/↓ und ENTER)""
                ";
                
                Menu mainMenu = new Menu(prompt, options);
                int selected = mainMenu.RunV();
                bool ok = (selected == correctIdx);
            Console.WriteLine(ok ? "\n Richtig!" : $"\n Falsch. Richtige Antwort: {options[correctIdx]}");
            Console.WriteLine("Weiter mit Taste…");
            Console.ReadKey(true);
            return ok;

        }
    }
}
