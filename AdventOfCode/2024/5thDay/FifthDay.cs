namespace AdventOfCode._2024._5thDay;

public class FifthDay
{
    private static List<string> lines = ParseFile();
    private static Dictionary<int, List<int>> numberMappings = new Dictionary<int, List<int>>();

    
    private static List<string> ParseFile()
    {
        return FileManager.ReturnListOfStrings(
            "input.txt").ToList();
    }

    private static Dictionary<int, List<int>> ParseInput()
    {
        foreach (var line in lines)
        {
            if (line.Contains("|"))
            {
                var numbers = line.Split("|");
                var lesser = Int32.Parse(numbers[0]);
                var bigger = Int32.Parse(numbers[1]);
                if (!numberMappings.ContainsKey(lesser))
                    numberMappings[lesser] = new List<int>();

                numberMappings[lesser].Add(bigger);
            }
        }

        return numberMappings;
    }

    private static bool CheckIfLineInOrder(string line)
    {
        var inOrder = true;
        var numbers = line.Split(",").Where(s => !string.IsNullOrEmpty(s)).Select(s => Int32.Parse(s)).ToList();
        for (int i = 1; i < numbers.Count; i++)
        {
            if (numberMappings.ContainsKey(numbers[i]))
            {
                // Fetch numbers that need to be after current one
                var biggerNumbers = numberMappings[numbers[i]];
                for (int j = i - 1; j >= 0; j--)
                {
                    if (biggerNumbers.Contains(numbers[j]))
                        inOrder = false;
                }
            }
        }

        return inOrder;
    }
    
    public static void FirstPart()
    {
        ParseInput();   
        var sum = 0;
        
        foreach (var line in lines)
        {
            if(line.Contains(","))
            {
                var inOrder = true;
                var numbers = line.Split(",").Where(s => !string.IsNullOrEmpty(s)).Select(s => Int32.Parse(s)).ToList();
                for (int i = 1; i < numbers.Count; i++)
                {
                    if (numberMappings.ContainsKey(numbers[i]))
                    {
                        // Fetch numbers that need to be after current one
                        var biggerNumbers = numberMappings[numbers[i]];
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (biggerNumbers.Contains(numbers[j]))
                                inOrder = false;
                        }
                    }
                }

                if (inOrder && !string.IsNullOrEmpty(line))
                {
                    var middle = numbers.Count() / 2;
                    sum += numbers[middle];
                }
            } 
        }
        Console.WriteLine(sum);
    }

    public static void SecondPart()
    {
        ParseInput();   
        var sum = 0;
        
        foreach (var line in lines)
        {
            if(line.Contains(","))
            {
                var inOrder = false;
                var notInOrder = false;
                var numbers = line.Split(",").Where(s => !string.IsNullOrEmpty(s)).Select(s => Int32.Parse(s)).ToList();
                
                while (!inOrder)
                {
                    var counter = 0;
                    for (int i = 1; i < numbers.Count; i++)
                    {
                        if (numberMappings.ContainsKey(numbers[i]))
                        {
                            // Fetch numbers that need to be after current one
                            var biggerNumbers = numberMappings[numbers[i]];
                            for (int j = i - 1; j >= 0; j--)
                            {
                                if (biggerNumbers.Contains(numbers[j]))
                                {
                                    notInOrder = true;
                                    counter++;
                                    var copy = numbers[i];
                                    numbers[i] = numbers[j];
                                    numbers[j] = copy;
                                }
                            }
                        }
                    }

                    if (counter == 0)
                        inOrder = true;
                }
                
                if (notInOrder && inOrder && !string.IsNullOrEmpty(line))
                {
                    var middle = numbers.Count() / 2;
                    sum += numbers[middle];
                }
            } 
        }
        Console.WriteLine(sum);
    }
}
