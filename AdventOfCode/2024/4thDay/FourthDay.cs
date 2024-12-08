namespace AdventOfCode._2024._4thDay;

public class FourthDay
{
    private static List<string> ParseFile()
    {
        return FileManager.ReturnListOfStrings(
            "input.txt").ToList();
    }
    
    public static void FirstPart()
    {
        var inputLines = ParseFile();
        var lineLength = inputLines[0].Length;

        var inlineCounter = 0;
        var verticalCounter = 0;
        var regexVerticalCounter = 0;
        var diagonalCounter = 0;
        foreach (string line in inputLines)
        {
            for (int i = 0; i + 3 < lineLength; i++)
            {
                var horisontalLine = $"{line[i]}{line[i + 1]}{line[i + 2]}{line[i + 3]}";
                if (horisontalLine == "XMAS" || horisontalLine == "SAMX")
                    inlineCounter++;
            }
        }

        // check four by four lines
        for (int i = 0; i + 3 < inputLines.Count(); i++)
        {
            
            // check for vertical xmas
            for(int j = 0; j < lineLength; j++)
            {
                var verticalLine = $"{inputLines[i][j]}{inputLines[i+1][j]}{inputLines[i+2][j]}{inputLines[i+3][j]}";
                if (verticalLine == "XMAS" || verticalLine == "SAMX")
                    verticalCounter++;
            }
            // check for diagonal xmas
            for (int j = 0; j + 3 < lineLength; j++)
            {
                // from up \ to down
                var diagonalLine = $"{inputLines[i][j]}{inputLines[i+1][j+1]}{inputLines[i+2][j+2]}{inputLines[i+3][j+3]}";
                if (diagonalLine == "XMAS" || diagonalLine == "SAMX")
                    diagonalCounter++;
                
                // from down / to up
                diagonalLine = $"{inputLines[i][j+3]}{inputLines[i+1][j+2]}{inputLines[i+2][j+1]}{inputLines[i+3][j]}";
                if (diagonalLine == "XMAS" || diagonalLine == "SAMX")
                    diagonalCounter++;
            }
        }
        Console.WriteLine(diagonalCounter+inlineCounter+verticalCounter);
    }

    public static void SecondPart()
    {
        
        var inputLines = ParseFile();
        
        var lineLength = inputLines[0].Length;

        var diagonalCounter = 0;
        // check four by four lines
        for (int i = 0; i + 2 < inputLines.Count(); i++)
        {
            // check for diagonal xmas
            for (int j = 0; j + 2 < lineLength; j++)
            {
                var firstTrue = false;
                // from up \ to down
                var diagonalLine = $"{inputLines[i][j]}{inputLines[i+1][j+1]}{inputLines[i+2][j+2]}";
                if (diagonalLine == "MAS" || diagonalLine == "SAM")
                    firstTrue = true;
                
                // from down / to up
                diagonalLine = $"{inputLines[i][j+2]}{inputLines[i+1][j+1]}{inputLines[i+2][j]}";
                if (firstTrue && (diagonalLine == "MAS" || diagonalLine == "SAM" ))
                    diagonalCounter++;
            }
        }
        Console.WriteLine(diagonalCounter);
    }
}