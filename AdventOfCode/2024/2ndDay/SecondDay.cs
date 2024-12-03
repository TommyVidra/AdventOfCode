namespace AdventOfCode._2024._2ndDay;

public class SecondDay
{
    public static void FirstPart()
    {
        CalculateSequences(ParseFile());
    }

    public static void SecondPart()
    {
        var revisedList = TryRemovingOneElement(ParseFile());
    }
    
    private static List<List<int>> ParseFile()
    {
        var list = FileManager.ReturnListOfStrings(
            "input.txt");

        var levelsList = new List<List<int>>();
        
        foreach (var rowLevel in list)
        {
            var levels = rowLevel.Split(" ");
            var levelsNumeric = levels.Select(l => Int32.Parse(l)).ToList();
            levelsList.Add(levelsNumeric);
        }

        return levelsList;
    }

    private static bool IsSafeAsc(List<int> levels)
    {
        var safe = true;
        while (true)
        {
            try
            {
                var min = levels.Min();
                levels.Remove(min);

                var secondMin = levels.Min();

                if (min == secondMin || secondMin > min + 3)
                {
                    safe = false;
                }
            }
            catch (InvalidOperationException)
            {
                break;
            }
        }
        return safe;
    }
    
    private static bool IsSafeDesc(List<int> levels)
    {
        var safe = true;
        while (true)
        {
            try
            {
                var max = levels.Max();
                levels.Remove(max);

                var secondMax = levels.Max();

                if (max == secondMax || max > secondMax + 3)
                {
                    safe = false;
                }
            }
            catch (InvalidOperationException)
            {
                break;
            }
        }
        return safe;
    }
    
    private static int CalculateSequences(List<List<int>> levels)
    {
        int safeReports = 0;

        var nonSequenceLevels = new List<List<int>>();

        foreach (var levelsNumeric in levels)
        {
            var safe = true;
            
            var levelsNumericSorted = levelsNumeric.OrderBy(l => l).ToList();
            if (Enumerable.SequenceEqual(levelsNumericSorted, levelsNumeric))
            {
                safe = IsSafeAsc(levelsNumeric);
            }
            else
            {
                levelsNumericSorted = levelsNumericSorted.OrderByDescending(l => l).ToList();
                
                if (!Enumerable.SequenceEqual(levelsNumericSorted, levelsNumeric))
                    continue;

                safe = IsSafeDesc(levelsNumeric);
            }
            if (safe)
                safeReports += 1;

        }

        Console.WriteLine(safeReports);
        return safeReports;
    }
    
    public static List<List<int>> TryRemovingOneElement(List<List<int>> levels)
    {

        int safeReports = 0;
        
        foreach (var levelsNumeric in levels)
        {
            var safe = true;
            
            var levelsNumericSorted = levelsNumeric.OrderBy(l => l).ToList();
            var levelsNumericCopy = levelsNumeric.GetRange(0, levelsNumeric.Count);
            
            // Asc sort worked (possible duplicates)
            if (Enumerable.SequenceEqual(levelsNumericSorted, levelsNumeric))
            {
                safe = IsSafeAsc(levelsNumericCopy);
                
                if (!safe)
                {
                    var possibleLevels = new List<List<int>>();
                    for (int i = 0; i < levelsNumericSorted.Count; i++)
                    {
                        var possibleList = levelsNumeric.GetRange(0, levelsNumeric.Count);
                        possibleList.RemoveAt(i);
                        possibleLevels.Add(possibleList);
                    }

                    var safeCount = CalculateSequences(possibleLevels);
                    if (safeCount > 0)
                        safe = true;
                }
            }
            else
            {
                levelsNumericSorted = levelsNumericSorted.OrderByDescending(l => l).ToList();
                
                // Desc sort worked (possible duplicates)
                if (Enumerable.SequenceEqual(levelsNumericSorted, levelsNumeric))
                {
                    safe = IsSafeDesc(levelsNumericCopy);

                    if (!safe)
                    {
                        var possibleLevels = new List<List<int>>();
                        for (int i = 0; i < levelsNumericSorted.Count; i++)
                        {
                            var possibleList = levelsNumeric.GetRange(0, levelsNumeric.Count);
                            possibleList.RemoveAt(i);
                            possibleLevels.Add(possibleList);
                        }

                        var safeCount = CalculateSequences(possibleLevels);
                        if (safeCount > 0)
                            safe = true;
                    }
                }
                
                // Other cases
                else
                {
                    var possibleLevels = new List<List<int>>();
                    for (int i = 0; i < levelsNumericSorted.Count; i++)
                    {
                        var possibleList = levelsNumeric.GetRange(0, levelsNumeric.Count);
                        possibleList.RemoveAt(i);
                        possibleLevels.Add(possibleList);
                    }

                    var safeCount = CalculateSequences(possibleLevels);
                    if (safeCount > 0)
                        safe = true;
                    else
                    {
                        safe = false; 
                    }
                }
            }
            if (safe)
                safeReports += 1;
        }
        Console.WriteLine(safeReports);
        return new List<List<int>>();
    }
}