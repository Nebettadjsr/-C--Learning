using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Voktrain
{
    public class VokabelTrainer
    {
        private  DataManager data = new DataManager();

        public void Start()
        {
            data.LoadAll();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            RunMainMenu();

            Console.WriteLine("Drücke ENTER zum beenden.");
            Console.ReadKey(true);

        }

        private void RunMainMenu()
        {
            string prompt = @"
                 _   _         _            _            _  _                 _                    
                | | | |       | |          | |          | || |               (_)                   
                | | | |  ___  | | __  __ _ | |__    ___ | || |_  _ __   __ _  _  _ __    ___  _ __ 
                | | | | / _ \ | |/ / / _` || '_ \  / _ \| || __|| '__| / _` || || '_ \  / _ \| '__|
                \ \_/ /| (_) ||   < | (_| || |_) ||  __/| || |_ | |   | (_| || || | | ||  __/| |   
                 \___/  \___/ |_|\_\ \__,_||_.__/  \___||_| \__||_|    \__,_||_||_| |_| \___||_|   

                Willkommen im Vokabeltrainer DEU - ENG! Was möchtest du tun?
                (Mit Pfeiltasten Optionen auswählen)
                ";

            string[] options = { "Vokabeln ansehen & bearbeiten", "Test konfigurieren & starten", "Statistiken einsehen", "Programm beenden" };
            Menu mainMenu = new Menu(prompt, options);
            int SelectIndex = mainMenu.RunV();

            switch(SelectIndex)
            {
                case 0:
                    VokabelnAnsehen();
                    break;
                case 1:
                    TestConfigStart();
                    break;
                case 2:
                    SeeStats();
                    break;
                case 3:
                    ExitProgram();
                    break;
            }
        }
        private void ExitProgram()
        {
            WriteLine("\n beliebige Taste drücken zum beenden.");
            ReadKey(true);
            Environment.Exit(0);
        }
        private void VokabelnAnsehen() 
        {
            var manager = new VokabelManager(data);
            manager.VokabelMenu();
        }
        
        private void TestConfigStart()
        {
            TestConfigStart testMenu = new TestConfigStart(data);
            testMenu.TestMenu();

        }
        private void SeeStats() 
        {
            var statMenu = new SeeStats(data);
            statMenu.StatMenu();
        }

    }
}
