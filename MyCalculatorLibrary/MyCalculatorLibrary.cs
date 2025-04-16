using Newtonsoft.Json;

namespace MyCalculatorLibrary;

public class MyCalculator
{
    private readonly JsonWriter _writer;
    public MyCalculator()
    {
        StreamWriter logfile = File.CreateText("mycalculatorlog.json");
        logfile.AutoFlush = true;
        _writer = new JsonTextWriter(logfile);
        _writer.Formatting = Formatting.Indented;
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operations");
        _writer.WriteStartArray();
    }
    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; //Default value is "Not-a-number" if an operation, as division, could result in an error.
        
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operand1");
        _writer.WriteValue(num1);
        _writer.WritePropertyName("Operand2");
        _writer.WriteValue(num2);
        _writer.WritePropertyName("Operation");
        // Switch statement to do the math.
        switch (op)
        {
            case "+":
                result = num1 + num2;
                _writer.WriteValue("Add");
                break;
            case "-":
                result = num1 - num2;
                _writer.WriteValue("Subtract");
                break;
            case "*":
                result = num1 * num2;
                _writer.WriteValue("Multiply");
                break;
            case "/":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                    _writer.WriteValue("Divide");
                }
                break;
        }
        _writer.WritePropertyName("Result");
        _writer.WriteValue(result);
        _writer.WriteEndObject();
        
        return result;
    }

    public void ShowHistory(List<string> historico)
    {
        for (int i = 0; i + 3 <= historico.Count ; i += 4)
        {
            Console.WriteLine($"{double.Parse(historico[i]):0.##} {historico[i + 2]} " +
                              $"{double.Parse(historico[i + 1]):0.##} = {double.Parse(historico[i + 3]):0.##}");        }
    }
    
    public void Finish()
    {
        _writer.WriteEndArray();
        _writer.WriteEndObject();
        _writer.Close();
    }
}