using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Voktrain
{
    public class Menu
    {
        private int SelectIndex;
        private string[] Options;   
        private string Prompt;

        public Menu(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectIndex = 0;
        }
        public ConsoleKey LastKey { get; private set; }

        // VMenu = VERTICAL Menu
        private void VMenu()
        {
            Console.WriteLine(Prompt);
            int width = Console.WindowWidth;           

            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                bool selected = (i == SelectIndex);

                if (selected)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                var line = $"{(selected ? "*" : " ")} <<{currentOption}>>";
                // ➜ überschreibe den Rest der Zeile:
                if (line.Length < width) line = line.PadRight(width - 1);

                Console.WriteLine(line);
                Console.ResetColor();                   // wichtig: pro Zeile zurücksetzen
            }
            Console.ResetColor();
        }


        // HMenu = HORIZONTAL Menu
        private void HMenu()
        {
            Console.WriteLine(Prompt);
            for (int i = 0; i < Options.Length; i++)
            {
                Console.CursorVisible = false;
                string currentOption = Options[i];
                string prefix;

                if (i == SelectIndex)
                {
                    prefix = "*";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;

                }
                else
                {
                    prefix = " ";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;

                }

                Console.Write($"| {prefix} <<{currentOption}>> |");
            }
            ResetColor();
        }
        public int RunV()
        {
            ConsoleKey keyPressed;
            do
            {
                Clear();
                VMenu();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                LastKey = keyPressed;

                switch(keyPressed){
                    case ConsoleKey.UpArrow:
                        SelectIndex = (SelectIndex - 1 + Options.Length) % Options.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        SelectIndex = (SelectIndex + 1) % Options.Length;
                        break;
                    case ConsoleKey.LeftArrow :
                        return (int)LastKey;
                    case ConsoleKey.RightArrow:
                        return (int)LastKey;
                    case ConsoleKey.Enter: return SelectIndex;

                }

            } while (keyPressed != ConsoleKey.Enter);

            return (SelectIndex);
        }
        public int RunH()
        {
            ConsoleKey keyPressed;
            
            do
            {
                Clear();
                HMenu();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                LastKey = keyPressed;


                // update selection here
                if (keyPressed == ConsoleKey.LeftArrow)
                {
                    SelectIndex--;
                    if (SelectIndex == -1)
                    {
                        SelectIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.RightArrow)
                {
                    SelectIndex++;
                    if (SelectIndex >= Options.Length)
                    {
                        SelectIndex = 0;
                    }
                }
                

            } while (keyPressed != ConsoleKey.Enter);
            Console.CursorVisible = true;

            return SelectIndex;           
        }
    }

}
