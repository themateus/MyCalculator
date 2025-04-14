namespace MyCalculatorLibrary;

public static class MyCalculator
{
    public static double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; //Default value is "Not-a-number" if an operation, as division, could result in an error.
        
        // Switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                break;
            case "s":
                result = num1 - num2;
                break;
            case "m":
                result = num1 * num2;
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                break;
            // Return text for an incorrect option entry.
        }
        return result;
    }
}