namespace AdventOfCode._2024;

public class FileManager
{
    public static IEnumerable<string> ReturnListOfStrings(string filePath)
    {
        var lines = File.ReadLines(filePath);
        return lines;
    }
}