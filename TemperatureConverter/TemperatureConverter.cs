using System;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Type 1 for Celsius to Fahrenheit, 2 for Fahrenheit to Celsius:");
        
        string choiceInput = Console.ReadLine();
        bool isChoiceValid = int.TryParse(choiceInput, out int choice);

        if (!isChoiceValid)
        {
            Console.WriteLine("Invalid choice! Please enter a number (1 or 2).");
            return;
        }

        Console.WriteLine("What temperature value do you want to convert?");
        
        string tempInput = Console.ReadLine();
        bool isTempValid = float.TryParse(tempInput, out float temp);

        if (!isTempValid)
        {
            Console.WriteLine("Invalid temperature! Please enter a valid number.");
            return;
        }

        if (choice == 1)
        {
            float f = (temp * 9 / 5) + 32;
            Console.WriteLine($"The converted temperature is: {f:F2} °F");
        }
        else if (choice == 2)
        {
            float c = (temp - 32) * 5 / 9;
            Console.WriteLine($"The converted temperature is: {c:F2} °C");
        }
        else
        {
            Console.WriteLine("Only numbers 1 or 2 are accepted answers");
        }
    }
}
