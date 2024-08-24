namespace CAssessment.InputOutput;

public class ConsoleReader
{
    public static string GetInput(string caption, int attempts = 3, bool required = true, string defaultValue = "")
    {
        string value;
        caption = required ? "*" + caption : caption;

        do
        {
            Console.Write($"{caption}: ");
            value = Console.ReadLine()!;
            attempts--;
        }
        while (attempts > 0 && value == string.Empty && required);

        // After we've exhausted all the attempts, but haven't gotten
        // any value from the user
        if (value.Trim() == string.Empty)
        {
            if (required)
                //,throw an error if this input is required
                throw new ConsoleReaderAborted("Input operation aborted");
            return defaultValue;
        }

        return value.Trim();
    }

    public static int GetInt(string caption, int attempts = 3, bool required = true, int defaultValue = 0)
    {
        var value = GetInput(caption, attempts, required);
        if (value == string.Empty) return defaultValue;

        var isInt = int.TryParse(value, out int intValue);
        if (!isInt)
            throw new ConsoleReaderInvalidInput($"Invalid input({value}). Expected an integer value");

        return intValue;
    }

    public static DateTime GetDate(string caption, int attempts = 3, bool required = true, DateTime defaultValue = default)
    {
        caption += " (yyyy-mm-dd)";
        var dateString = GetInput(caption, attempts, required);


        if (dateString == string.Empty) return defaultValue;

        var OK = DateTime.TryParse(dateString, out DateTime value);
        if (!OK)
            throw new ConsoleReaderInvalidInput("Invalid date format");

        return value;
    }

    public static float GetFloat(string caption, int attempts = 3, bool required = true, float defaultValue = 0)
    {
        var value = GetInput(caption, attempts, required);
        if (value == string.Empty) return defaultValue;

        var isFloat = float.TryParse(value, out float fvalue);
        if (!isFloat)
            throw new ConsoleReaderInvalidInput($"Invalid input({value}). Expected a decimal number");

        return fvalue;
    }

    public static bool GetBool(string caption)
    {
        caption += " (y/n)";

        var value = GetInput(caption, required: false);
        if (value == null || value.Trim().Length == 0)
            return false;

        return value.StartsWith("y", StringComparison.CurrentCultureIgnoreCase);
    }
}


public class ConsoleReaderAborted(string msg) : Exception(msg) { }

public class ConsoleReaderInvalidInput(string msg) : Exception(msg) { }
