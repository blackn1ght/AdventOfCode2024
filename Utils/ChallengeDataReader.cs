namespace AdventOfCode2024.Utils;

public static class ChallengeDataReader
{
    public static string[] GetDataForDay(int day, InputTypes inputType)
        => inputType switch 
        {
            InputTypes.Input => ReadFile(day, "input.txt"),
            InputTypes.Example => ReadFile(day, "example.txt"),
            _ => throw new ArgumentException($"Invalid inputType {Enum.GetName(inputType)}")
        };
    

    private static string[] ReadFile(int day, string filename)
    {
        var file = "";
        var filepath = $"Day{day.ToString().PadLeft(2, '0')}/{filename}";
        using (var reader = new StreamReader(filepath))
        {
            file = reader.ReadToEnd();
        }

        return file.Split(Environment.NewLine);
    }
}