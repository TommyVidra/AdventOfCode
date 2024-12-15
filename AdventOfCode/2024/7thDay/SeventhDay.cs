using System.Runtime.InteropServices.JavaScript;

namespace AdventOfCode._2024._7thDay;

public class SeventhDay
{
    private static List<string> ParseFile()
    {
        return FileManager.ReturnListOfStrings(
            "input.txt").ToList();
    }
    
    public static void FirstPart()
    {
        var equations = ParseFile();
        long sum = 0;
        
        foreach (var equation in equations)
        {
            var equationParts = equation.Split(":");
            var equationResult = Int64.Parse(equationParts[0]);
            var numbers = equationParts[1].Split(" ");
            
            
            var numbersParsed = numbers.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => Int64.Parse(s)).ToList();
                
            for (int i = 0; i < Math.Pow(2, numbersParsed.Count -1); i++)
            {
                // Convert to binary so that all of the cases are passed through
                // 1 -> multiply
                // 0 -> sum
                var binaryCase = Int64.Parse(Convert.ToString(i, 2));
                var binaryCaseCharList = binaryCase.ToString($"D{numbersParsed.Count - 1}").ToCharArray();
                var caseSum = numbersParsed[0];
                for (int j = 1; j <= binaryCaseCharList.Length; j++)
                {
                    if (binaryCaseCharList[j-1] == '0')
                        caseSum += numbersParsed[j];
                    else
                        caseSum *= numbersParsed[j];
                }

                if (caseSum == equationResult)
                {
                    sum += equationResult;
                    break;
                }
            }
        }
        Console.WriteLine(sum);
    }
    
    
    public static void SecondPart()
    {
        var equations = ParseFile();
        long sum = 0;
        
        foreach (var equation in equations)
        {
            var equationParts = equation.Split(":");
            var equationResult = Int64.Parse(equationParts[0]);
            var numbers = equationParts[1].Split(" ");
            
            var numbersParsed = numbers.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => Int64.Parse(s)).ToList();
                
            for (int i = 0; i < Math.Pow(3, numbersParsed.Count -1); i++)
            {
                // Convert to binary so that all of the cases are passed through
                // 2 -> concatenate
                // 1 -> multiply
                // 0 -> sum
                var binaryCase = Int64.Parse(ConvertToBaseThree(i));
                var binaryCaseCharList = binaryCase.ToString($"D{numbersParsed.Count - 1}").ToCharArray();
                var caseSum = numbersParsed[0];
                for (int j = 1; j <= binaryCaseCharList.Length; j++)
                {
                    if (binaryCaseCharList[j-1] == '0')
                        caseSum += numbersParsed[j];
                    else if (binaryCaseCharList[j - 1] == '2')
                    {
                        var newCaseSum = $"{caseSum}{numbersParsed[j]}";
                        caseSum = Int64.Parse(newCaseSum);
                    }
                    else
                        caseSum *= numbersParsed[j];
                }

                if (caseSum == equationResult)
                {
                    sum += equationResult;
                    break;
                }
            }
        }
        Console.WriteLine(sum);
    }

    private static string ConvertToBaseThree(int number)
    {
        if (number == 0)
        {
            return "0";
        }
     
        var result = "";
        var rest = number;
        while (rest > 0)
        {
            var restResult = rest % 3;
            result = $"{restResult}{result}";

            rest = rest / 3;
        }

        return result;
    }
}