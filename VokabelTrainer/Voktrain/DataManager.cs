using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Voktrain
{
    public class DataManager
    {
        public List<Vokabel> WordList { get; } = new List<Vokabel>();
        public Statistik Stats { get; set; } = new Statistik();

       
        public void LoadVokabeln(string filePath, Difficulty diff)
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                var vokabeln = JsonSerializer.Deserialize<List<Vokabel>>(jsonString,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                /* 
                 JsonSerializer.Deserialize converts a JSON string into a C# object
                 From a string: var myObject = JsonSerializer.Deserialize<MyClass>(jsonString);

                new JsonSerializerOptions { PropertyNameCaseInsensitive = true } 
                configures the .NET System.Text.Json serializer to ignore the case of property names 
                when deserializing JSON into an object
                A JSON object like {"userName": "John Doe"} can be deserialized into an object with 
                a property named >>> UserName <<< by setting PropertyNameCaseInsensitive = true
                 */

                if (vokabeln != null)
                {
                    foreach (var vokabel in vokabeln)
                    {
                        vokabel.Schwierigkeit = diff;
                        WordList.Add(vokabel);
                    }
                }
                else
                {
                    Console.WriteLine($"Fehler beim Laden der Vokabeln aus {filePath}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Lesen/Deserialisieren von {filePath}: {ex.Message}");
            }
        }

        public void LoadAll()
        {
            LoadVokabeln(@"data\leicht.json", Difficulty.Leicht);
            LoadVokabeln(@"data\mittel.json", Difficulty.Mittel);
            LoadVokabeln(@"data\schwer.json", Difficulty.Schwer);
            LoadWordStats();
            
        }


        public void AddVokabel(string deutsch, string englisch, Difficulty diff)
        {
            // Check for duplicate
            string Key(string de, string en)
                => $"{de.Trim().ToLowerInvariant()}|{en.Trim().ToLowerInvariant()}";

            bool exists = WordList.Any(v => Key(v.Deutsch, v.Englisch) == Key(deutsch, englisch));
            if (exists)
                throw new InvalidOperationException("Vokabel existiert bereits.");

            // Add
            var v = new Vokabel(deutsch, englisch) { Schwierigkeit = diff };
            WordList.Add(v);

            // Persist
            SaveVokabelnNachDifficulty();
            SaveWordStats();
        }
        public void SaveVokabelnNachDifficulty()
        {
            var opt = new JsonSerializerOptions { WriteIndented = true };
            try
            {
                void save(string file, Difficulty d)
                {
                    var path = Path.Combine(AppContext.BaseDirectory, "data", file);                                       
                    var pairsOnly = WordList
                        .Where(v => v.Schwierigkeit == d)
                        .Select(v => new { v.Deutsch, v.Englisch })   
                        .ToList();

                    var json = JsonSerializer.Serialize(pairsOnly, opt);
                    File.WriteAllText(path, json);
                }

                save("leicht.json", Difficulty.Leicht);
                save("mittel.json", Difficulty.Mittel);
                save("schwer.json", Difficulty.Schwer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Speichern der Vokabeln: {ex.Message}");
            }
        }



        public void RemoveVokabel(Vokabel vokabel)
        {
            WordList.Remove(vokabel);
        }

        public List<Vokabel> GetAllVokabeln()
        {
            return WordList;
        }
        public List<TestResult> GettAllTests()
        {             var path = DataPath("stats_tests.json");
            var tests = new List<TestResult>();
            if (File.Exists(path))
            {
                try
                {
                    string jsonString = File.ReadAllText(path);
                    tests = JsonSerializer.Deserialize<List<TestResult>>(jsonString) ?? new List<TestResult>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fehler beim Lesen oder Deserialisieren: {ex.Message}");
                }
            }
            return tests;
        }

        const string TestsFile = "stats_tests.json";
        const string WordsFile = "stats_words.json";
        private string DataPath(string fileName)
        {
            string absoluterPfad = Path.Combine(AppContext.BaseDirectory, "data", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(absoluterPfad)!);
            return (absoluterPfad);
        }

        public void AppendTestResult(TestResult t)
        {
            var path = DataPath("stats_tests.json");
            var options = new JsonSerializerOptions { WriteIndented = true };
            List<TestResult> tests = new List<TestResult>();
            if (File.Exists(path))
            {
                try
                {
                    string jsonString = File.ReadAllText(path);
                    tests = JsonSerializer.Deserialize<List<TestResult>>(jsonString) ?? new List<TestResult>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fehler beim Lesen oder Deserialisieren: {ex.Message}");
                }
            }
            if (string.IsNullOrWhiteSpace(t.Name))
            {
                t.Name = $"Test {tests.Count + 1}";
            }
            tests.Add(t);
            string newJson = JsonSerializer.Serialize(tests, options);
            File.WriteAllText(path, newJson);

        }

        public void SaveWordStats()
        {
            var path = DataPath("stats_words.json");
            var options = new JsonSerializerOptions { WriteIndented = true };

            try
            {
                string json = JsonSerializer.Serialize(WordList, options);
                File.WriteAllText(path, json);                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Speichern der Wortstatistik: {ex.Message}");
            }
        }
        public void LoadWordStats()
        {
            var path = DataPath("stats_words.json");

            if (!File.Exists(path))
            {
                Console.WriteLine("Noch keine Wortstatistik vorhanden.");
                return;
            }

            try
            {
                string json = File.ReadAllText(path);
                var loaded = JsonSerializer.Deserialize<List<Vokabel>>(json);
                if (loaded != null)
                {
                    WordList.Clear();
                    WordList.AddRange(loaded);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Laden der Wortstatistik: {ex.Message}");
            }
        }
        public void RecordCorrect(Vokabel v)
        {
            v.GeuebtGesamt++;
            v.RichtigGesamt++;
            Stats.GefragtGesamt++;
            Stats.GefragtRichtig++;
        }

        public void RecordWrong(Vokabel v)
        {
            v.GeuebtGesamt++;
            Stats.GefragtGesamt++;
        }


    }
}
