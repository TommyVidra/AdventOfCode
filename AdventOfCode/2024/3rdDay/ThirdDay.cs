using System.Text.RegularExpressions;

namespace AdventOfCode._2024._3rdDay;

public class ThirdDay
{
    private static List<string> ParseFile()
    {
        return FileManager.ReturnListOfStrings(
            "input.txt").ToList();
    }
    
    public static void FirstPart()
    {
        var inputStringList = ParseFile();
        var regexPattern =
            @"(mul\(\d{3},\d{3}\))|(mul\(\d{2},\d{2}\))|(mul\(\d{1},\d{1}\))|(mul\(\d{3},\d{1}\))|(mul\(\d{3},\d{2}\))|(mul\(\d{1},\d{3}\))|(mul\(\d{1},\d{2}\))|(mul\(\d{2},\d{3}\))|(mul\(\d{2},\d{1}\))";

        Regex reg = new Regex(regexPattern, RegexOptions.None);
        var sum = 0;
        foreach (var textLine in inputStringList)
        {
            Match m = reg.Match(textLine);
            while (m.Success)
            {
                var numbers = m.Value.Split(",");
                var first = Int32.Parse(numbers[0].Split("(")[1]);
                var second = Int32.Parse(numbers[1].Split(")")[0]);

                sum += first * second;
                m = m.NextMatch();
            }
        }
        Console.WriteLine(sum);
    }

    public static void SecondPart()
    {
        var inputStringList = ParseFile();
        var regexPattern =
            @"(do\(\))|(don't\(\))|(mul\(\d{3},\d{3}\))|(mul\(\d{2},\d{2}\))|(mul\(\d{1},\d{1}\))|(mul\(\d{3},\d{1}\))|(mul\(\d{3},\d{2}\))|(mul\(\d{1},\d{3}\))|(mul\(\d{1},\d{2}\))|(mul\(\d{2},\d{3}\))|(mul\(\d{2},\d{1}\))";

        Regex reg = new Regex(regexPattern, RegexOptions.None);
        var sum = 0;
        var firstDo = false;
        var collectMul = true;
        
        foreach (var textLine in inputStringList)
        {
            Match m = reg.Match(textLine);
            while (m.Success)
            {
                if (m.Value.Contains("do()"))
                {
                    collectMul = true;
                    firstDo = true;
                }

                if (m.Value.Contains("don't()"))
                {
                    collectMul = false;
                }
                var isMul = m.Value.Contains("mul");
                
                if (isMul && (firstDo && collectMul || collectMul))
                {
                    var numbers = m.Value.Split(",");
                    var first = Int32.Parse(numbers[0].Split("(")[1]);
                    var second = Int32.Parse(numbers[1].Split(")")[0]);

                    sum += first * second;    
                }
                m = m.NextMatch();
            }
        }
        Console.WriteLine(sum);
    }
}