using System;

public class Program
{
    public static float GetNumber(string prompt)
        {
            Console.WriteLine(prompt);
            
            string input = Console.ReadLine();
            bool isValid = float.TryParse(input, out float number);
            
            while (!isValid)
                {
                    Console.WriteLine("Invalid number! Please enter a valid number:");
                    input = Console.ReadLine();
                    isValid = float.TryParse(input, out number);
                }
            
            return number;
        }
        
    public static void Main()
    {
        Console.WriteLine("Welcome to my little Calculator!");
        string cont = "y";
        while (cont == "y")
        {
        
        Console.WriteLine("Choose operation: \n 1 - Add \n 2 - Subtract \n 3 - Multiply \n 4 - Divide");
        
        string choiceInput = Console.ReadLine();
        bool isChoiceValid = int.TryParse(choiceInput, out int choice);

        if (!isChoiceValid || choice < 1 || choice > 4)
        {
            Console.WriteLine("Invalid choice! Please only enter numbers from 1 to 4!");
            continue;
        }
         
        float first = GetNumber("Enter first number:");
        float sec = GetNumber("Enter second number:");

        switch(choice)
        {
            case 1:
                float a = first + sec;
                Console.WriteLine($"Result\n {first} + {sec} = {a}");
                break;
            case 2:
                float s = first - sec;
                Console.WriteLine($"Result\n {first} - {sec} = {s}");
                break;
            case 3:
                float m = first * sec;
                Console.WriteLine($"Result\n {first} * {sec} = {m}");
                break;
            case 4:
                if (sec == 0)
                    {
                        Console.WriteLine("Invalid calculation! Cannot divide by zero.");
                        continue;
                    }
                float d = first / sec;
                Console.WriteLine($"Result\n {first} / {sec} = {d}");
                break;
            
        }
        Console.WriteLine("Do you want to calculate more? Y/N");
        string answer = Console.ReadLine();
        if (answer != "y" && answer != "n" )
        {
            Console.WriteLine("Invalid answer! Please only type y or n!");
            continue;
        }
        if (answer == "y" )
        {
            continue;
        }
        if (answer == "n" )
        {
            Console.WriteLine("Thank you for using my little Calculator");
            break;
        }
        }
    }
}
