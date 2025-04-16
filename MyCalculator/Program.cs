using System.Globalization;
using System.Text.RegularExpressions;
using MyCalculatorLibrary;

namespace MyCalculatorProgram;

static class Program
{
    static void Main()
    {
        int numCalcs = 0;
        List<string> historico = new List<string>();
        double result = 0;

        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\r");

        MyCalculator myCalculator = new MyCalculator();
        
        while (!endApp)
        {
            // Declare variables and set to empty
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            string? numInput1;
            string? numInput2;
            
            // Ask the user to type the first number.
            Console.WriteLine("Type a number, or choose a result (first result = #1...) and then press Enter: ");
            numInput1 = Console.ReadLine();

            for (int i = 1; i <= numCalcs; i++)
            {
                if (numInput1 == $"#{i}") numInput1 = historico[(i*4)-1];
            }
            
            double cleanNum1;
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                
                Console.WriteLine("This is not valid input. Please enter a numeric value: ");
                numInput1 = Console.ReadLine();
            }
            
            // Ask the user to type the second number.
            Console.WriteLine("Type another number, or choose a result (first result = #1...) and then press Enter: ");
            numInput2 = Console.ReadLine();

            double cleanNum2;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.WriteLine("This is not valid input. Please enter a numeric value: ");
                numInput2 = Console.ReadLine();
            }
            
            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\t+ - Add");
            Console.WriteLine("\t- - Subtract");
            Console.WriteLine("\t* - Multiply");
            Console.WriteLine("\t/ - Divide");
            Console.WriteLine("Your option? ");

            string? op = Console.ReadLine();
            
            // Validate input is not null, and matches the pattern
            if (op == null || !Regex.IsMatch(op, "[+|-|*|/]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                try
                {
                    result = myCalculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }
            Console.WriteLine("--------------------------\n");
            
            numCalcs++; // Add 1 for each time the calculator was used.
            if (op != null) historico.AddRange(new List<string> { numInput1, numInput2, op, result.ToString(CultureInfo.InvariantCulture) });
            myCalculator.ShowHistory(historico);
            
            // Wait for the user to respond before closing.
            Console.WriteLine("\nPress 'n' and Enter to close the app\n" +
                              "Press 'd' and Enter to delete the history\n" +
                              "Press only Enter to continue the app");
            
            string? closeOrNot = Console.ReadLine();
            if (closeOrNot == "n") endApp = true;
            else if (closeOrNot == "d") historico.Clear();
            
        }
        // Add call to close the JSON _writer before return
        myCalculator.Finish();
    }
}