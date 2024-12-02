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
    
    private static List<List<int>> CalculateSequences(List<List<int>> levels)
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
        return nonSequenceLevels;
    }


    private static List<int> ReturnDuplicateList(List<int> levelsNumeric)
    {
        return levelsNumeric.GroupBy(x => x)
            .Where(g => g.Count() > 1)
            .Select(y => y.Key)
            .ToList();
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
                    // try removing duplicates
                    var query = ReturnDuplicateList(levelsNumeric);
                    if (query.Count == 1)
                    {
                        levelsNumeric.Remove(query[0]);
                        safe = IsSafeAsc(levelsNumeric);
                    }
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
                        // try removing duplicates
                        var query = ReturnDuplicateList(levelsNumeric);
                        if (query.Count == 1)
                        {
                            levelsNumeric.Remove(query[0]);
                            safe = IsSafeDesc(levelsNumeric);
                        }
                    }
                }
                
                // Other cases
                else
                {
                    // Remove cases with more than one duplicates
                    var query = ReturnDuplicateList(levelsNumeric);
                    if (query.Count > 1)
                    {
                        safe = false;
                        continue;
                    }

                    var AdditionList = new List<int>();
                    for (int i = 0; i < levelsNumeric.Count(); i++)
                    {
                        if (i + 1 == levelsNumeric.Count())
                            break;
                        
                        AdditionList.Add(levelsNumeric[i] - levelsNumeric[i+1]);
                    }

                    var largerThanZero = AdditionList.Where(i => i > 0);
                    var lowerThanZero = AdditionList.Where(i => i < 0);
                    var zeros = AdditionList.Where(i => i == 0);

                    if (largerThanZero.Count() == 2 || lowerThanZero.Count() == 2 || zeros.Count() >= 2 
                        || zeros.Count() == 1 && lowerThanZero.Count() == 1 || zeros.Count() == 1 && largerThanZero.Count() == 1
                        || largerThanZero.Count() >= 2 && lowerThanZero.Count() >= 2)
                    {
                        safe = false;
                    }
                    
                    /*
                    else
                    {

                        if (largerThanZero.Count() == 1)
                        {
                            var largerThanZeroNumber = AdditionList.Where(i => i > 0).First();
                            var indexInLevels = AdditionList.IndexOf(largerThanZeroNumber) + 1;
                            levelsNumeric.RemoveAt(indexInLevels);

                            safe = IsSafeAsc(levelsNumeric);
                        }
                        else if (lowerThanZero.Count() == 1)
                        {
                            var lowerThanZeroNumber = AdditionList.Where(i => i < 0).First();
                            var indexInLevels = AdditionList.IndexOf(lowerThanZeroNumber) + 1;
                            levelsNumeric.RemoveAt(indexInLevels);

                            safe = IsSafeDesc(levelsNumeric);
                        }
                        else
                        {
                            Console.WriteLine(string.Join(" ", AdditionList));
                            
                        }
                    }*/
                }
            }
            if (safe)
                safeReports += 1;
        }
        Console.WriteLine(safeReports);
        return new List<List<int>>();
    }
}