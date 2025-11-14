using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Voktrain
{
    public class VokabelManager
    {
        private readonly DataManager _data;

        public VokabelManager(DataManager data)
        {
            _data = data;
        }

        public void VokabelMenu()
        {
            MainVokabMenu();
        }
        private void MainVokabMenu()
        {
            string prompt = @"
                 _   _         _            _            _  _                 _                    
                | | | |       | |          | |          | || |               (_)                   
                | | | |  ___  | | __  __ _ | |__    ___ | || |_  _ __   __ _  _  _ __    ___  _ __ 
                | | | | / _ \ | |/ / / _` || '_ \  / _ \| || __|| '__| / _` || || '_ \  / _ \| '__|
                \ \_/ /| (_) ||   < | (_| || |_) ||  __/| || |_ | |   | (_| || || | | ||  __/| |   
                 \___/  \___/ |_|\_\ \__,_||_.__/  \___||_| \__||_|    \__,_||_||_| |_| \___||_|   

                VOKABELMANAGER
                Was möchtest du tun?
                (Mit Pfeiltasten Optionen auswählen)
                ";

            string[] options = { "Vokabeln ansehen & bearbeiten", "neue Vokabeln hinzufügen", "Zurück ins Hauptmenü" };
            Menu mainMenu = new Menu(prompt, options);
            int SelectIndex = mainMenu.RunV();

            switch (SelectIndex)
            {
                case 0:
                    VokabelnAnsehenBearbeiten();
                    break;
                case 1:
                    VokabelnHinzu();
                    break;
                case 2:
                    MainMenu();
                    break;
                
            }
           
        }
        private void VokabelnAnsehenBearbeiten()
        {
            var view = new VokabelnAnsehenBearbeiten(_data);
            view.Run();
            MainVokabMenu();
        }
        private void VokabelnHinzu()
        {
            var add = new VokabelnHinzu(_data);
            add.Run();
            MainVokabMenu();

        }
        private void MainMenu()
        {
            VokabelTrainer trainer = new VokabelTrainer();
            trainer.Start();
        }

    }
}
