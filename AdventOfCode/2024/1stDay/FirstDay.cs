namespace AdventOfCode._2024._1stDay;

public class FirstDay
{
    private static List<int> listL = new List<int>();
    private static List<int> listR = new List<int>();
    
    public static void FirstPart()
    {
        var totalDistance = 0;
        
        FillLists();
        listL.Sort();
        listR.Sort();

        while (true)
        {
            try
            {
                var minL = listL.Min();
                listL.Remove(minL);

                var minR = listR.Min();
                listR.Remove(minR);

                totalDistance += Math.Abs(minL - minR);
            }
            catch (InvalidOperationException)
            {
                break;
            }
        }
        Console.WriteLine(totalDistance);
    }

    public static void SecondPart()
    {
        var totalSimilarity = 0;
        
        FillLists();

        foreach (var number in listL)
        {
            var similarity = listR.FindAll(x => x == number).Count();
            totalSimilarity += number*similarity;
        }
        
        Console.WriteLine(totalSimilarity);
    }

    private static void FillLists()
    {
        var lists = FileManager.ReturnListOfStrings("input.txt");
        foreach (var line in lists)
        {
            var lines = line.Split(" ");
            var leftList = lines[0];
            var rightList = lines[1];

            listL.Add(Int32.Parse(leftList));
            listR.Add(Int32.Parse(rightList));
        }
    }
}