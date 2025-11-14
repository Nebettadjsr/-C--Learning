using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Voktrain
{
    public class TestConfigStart
    {
        
        private readonly DataManager _data;
        public TestConfigStart(DataManager data) { _data = data; }

        // helper to get the current pool based on chosen difficulty
        public List<Vokabel> GetFragenPool()
        {
            var q = _data.WordList.AsEnumerable();
            if (TestSchwierigkeit != Difficulty.Alle)
                q = q.Where(v => v.Schwierigkeit == TestSchwierigkeit);
            return q.ToList();
        }
        public void TestMenu()
        {
            TestKonfigurieren();
        }
        public Difficulty TestSchwierigkeit = (Difficulty.Leicht);
        public int FragenAnzahl = 15;
        public bool DeutschNachEnglisch = true;
        private void TestKonfigurieren()
        {
            string prompt = $@"
                 _   _         _            _            _  _                 _                    
                | | | |       | |          | |          | || |               (_)                   
                | | | |  ___  | | __  __ _ | |__    ___ | || |_  _ __   __ _  _  _ __    ___  _ __ 
                | | | | / _ \ | |/ / / _` || '_ \  / _ \| || __|| '__| / _` || || '_ \  / _ \| '__|
                \ \_/ /| (_) ||   < | (_| || |_) ||  __/| || |_ | |   | (_| || || | | ||  __/| |   
                 \___/  \___/ |_|\_\ \__,_||_.__/  \___||_| \__||_|    \__,_||_||_| |_| \___||_|   

                TESTKONFIGURATOR
                
                Aktuelle Einstellungen:
                - Schwierigkeit: {TestSchwierigkeit}
                - Anzahl Fragen: {FragenAnzahl}
                - Richtung: {(DeutschNachEnglisch ? "Frage auf Deutsch -> Antworten auf Englisch" : "Frage auf Englisch -> Antworten auf Deutsch")}
                
                Was möchtest du tun?
                (Mit Pfeiltasten Optionen auswählen)
                ";

            string[] options = { "TEST STARTEN", "TEST anpassen", "Zurück ins Hauptmenü" };
            Menu mainMenu = new Menu(prompt, options);
            int SelectIndex = mainMenu.RunV();

            switch (SelectIndex)
            {
                case 0:
                    TestStarten();
                    break;
                case 1:
                    TestConfig();
                    break;
                case 2:
                    MainMenu();
                    break;

            }           
            
        }
        private void TestStarten()
        {
            TestStarten testStarten = new TestStarten(_data);
            testStarten.Run(this);
        }
        private void TestConfig()
        {
           TestConfig testConfig = new TestConfig();
           testConfig.TestAnpassen(this);

        }
        private void MainMenu()
        {
            VokabelTrainer trainer = new VokabelTrainer();
            trainer.Start();
        }
    }
}
