using System.Runtime.InteropServices;
using System.Text;

namespace AdventOfCode._2024._6thDay;

public class SixthDay
{

    private static List<string> ParseFile()
    {
        return FileManager.ReturnListOfStrings(
            "input.txt").ToList();
    }

    public static Dictionary<int, HashSet<int>> FirstPart()
    {
        var lines = ParseFile();
        var lineWithGuard = lines.First(s => s.Contains("^"));
        var tileWithGuard = lineWithGuard.First(c => c == '^');

        var listOfIndexes = new Dictionary<int, HashSet<int>>();

        var yIndex = lines.IndexOf(lineWithGuard);
        var xIndex = lineWithGuard.IndexOf(tileWithGuard);
        listOfIndexes.Add(xIndex, new HashSet<int>(){yIndex});
        
        var isVertical = true;
        var isUp = true;
        var isLeft = false;
        while (true)
        {
            var hitObsticle = false;
            if (isVertical)
            {
                var verticalLines = lines.Select(s => s[xIndex]).ToList();
                // We are finding if # exists on the vertical path
                if (isUp)
                {
                    for (int index = yIndex; index >= 0; index--) // this is horisontal index (key)
                    {
                        if (verticalLines[index] != '#')
                        {
                            if (!listOfIndexes.ContainsKey(xIndex))
                                listOfIndexes[xIndex] = new HashSet<int>();
                            
                            if (!listOfIndexes[xIndex].Contains(index))
                            {
                                listOfIndexes[xIndex].Add(index);
                            }
                        }
                        else
                        {
                            hitObsticle = true;
                            isVertical = false;
                            isUp = false;
                            break;
                        }
                        yIndex = index;
                    }    
                }
                
                else
                {
                    for (int index = yIndex; index < lines.Count(); index++) // this is horisontal index (key)
                    {
                        if (verticalLines[index] != '#')
                        {
                            if (!listOfIndexes.ContainsKey(xIndex))
                                listOfIndexes[xIndex] = new HashSet<int>();

                            if (!listOfIndexes[xIndex].Contains(index))
                            {
                                listOfIndexes[xIndex].Add(index);
                            }
                        }
                        else
                        {
                            hitObsticle = true;
                            isVertical = false;
                            isUp = true;
                            break;
                        }
                        yIndex = index;
                    }    
                }
            }
            
            else
            {
                var horizontalLine = lines[yIndex];
                if (isLeft)
                {
                    for (int index = xIndex; index >= 0; index--)
                    {
                        if (horizontalLine[index] != '#')
                        {
                            if (!listOfIndexes.ContainsKey(index))
                                listOfIndexes[index] = new HashSet<int>();

                            if (!listOfIndexes[index].Contains(yIndex))
                            {
                                listOfIndexes[index].Add(yIndex);
                            }
                        }
                        else
                        {
                            hitObsticle = true;
                            isVertical = true;
                            isLeft = false;
                            break;
                        }
                        xIndex = index;
                    }
                }
                else
                {
                    for (int index = xIndex; index < horizontalLine.Length; index++)
                    {
                        if (horizontalLine[index] != '#')
                        {
                            if (!listOfIndexes.ContainsKey(index))
                                listOfIndexes[index] = new HashSet<int>();

                            if (!listOfIndexes[index].Contains(yIndex))
                            {
                                listOfIndexes[index].Add(yIndex);
                            }
                        }
                        else
                        {
                            hitObsticle = true;
                            isVertical = true;
                            isLeft = true;
                            break;
                        }
                        xIndex = index;
                    }
                }
            }
            if (!hitObsticle)
                break;
        }

        Console.WriteLine(listOfIndexes.Select(k => k.Value.Count()).Sum());
        return listOfIndexes;
    }
    
    // Part is not working, number is to large...
    public static void SecondPart()
    {
        var lines = ParseFile();
        var lineWithGuard = lines.First(s => s.Contains("^"));
        var tileWithGuard = lineWithGuard.First(c => c == '^');

        var pathLines = FirstPart();

        var yIndex = lines.IndexOf(lineWithGuard);
        var xIndex = lineWithGuard.IndexOf(tileWithGuard);
        // remove starting position
        pathLines[xIndex].Remove(yIndex);
        var xPaths = pathLines.Keys.ToList();
        
        var skipTiles = new Dictionary<int, HashSet<int>>();
        
        var isVertical = true;
        var isUp = true;
        var isLeft = false;
        var goodObsticles = 0;

        // The guard needs to hit 3 times the obsticle
        var newObsticleCounter = 0;
        var xObsticle = xPaths[0];
        xPaths.RemoveAt(0);
        var yObstivle = pathLines[xObsticle].Min();
        pathLines[xObsticle].Remove(pathLines[xObsticle].Min());

        var passedBarriers = new Dictionary<int, List<int>>();
        passedBarriers[xObsticle] = new List<int>() { yObstivle };

        var obsticles = new Dictionary<int, List<int>>();
        while (true)
        {
            var hitObsticle = false;
            if (isVertical)
            {
                var verticalLines = lines.Select(s => s[xIndex]).ToList();
                // We are finding if # exists on the vertical path
                if (isUp)
                {
                    for (int index = yIndex; index >= 0; index--) // this is horisontal index (key)
                    {
                        if (verticalLines[index] == '#' || (xIndex == xObsticle && index == yObstivle))
                        {
                            if (passedBarriers.ContainsKey(xIndex))
                            {
                                if (passedBarriers[xIndex].Contains(index))
                                    newObsticleCounter++;
                                else
                                {
                                    passedBarriers[xIndex] = new List<int>() {index};
                                }
                            }
                            else
                            {
                                passedBarriers[xIndex] = new List<int>() {index};

                                if (verticalLines[index] == '#')
                                {
                                    if (!skipTiles.ContainsKey(xIndex))
                                        skipTiles[xIndex] = new HashSet<int>() { index };
                            
                                    else
                                        skipTiles[xIndex].Add(index);
                                }
                            }
                            hitObsticle = true;
                            isVertical = false;
                            isUp = false;
                            break;
                        }
                        yIndex = index;
                    }    
                }
                
                else
                {
                    for (int index = yIndex; index < lines.Count(); index++) // this is horisontal index (key)
                    {
                        if (verticalLines[index] == '#' || (xIndex == xObsticle && index == yObstivle))
                        {
                            if (passedBarriers.ContainsKey(xIndex))
                            {
                                if (passedBarriers[xIndex].Contains(index))
                                    newObsticleCounter++;
                                else
                                {
                                    passedBarriers[xIndex] = new List<int>() {index};
                                }
                            }
                            else
                            {
                                passedBarriers[xIndex] = new List<int>() {index};

                                if (verticalLines[index] == '#')
                                {
                                    if (!skipTiles.ContainsKey(xIndex))
                                        skipTiles[xIndex] = new HashSet<int>() { index };
                                    else
                                        skipTiles[xIndex].Add(index);

                                }
                            }
                            hitObsticle = true;
                            isVertical = false;
                            isUp = true;
                            break;
                        }
                        yIndex = index;
                    }    
                }
            }
            
            else
            {
                var horizontalLine = lines[yIndex];
                if (isLeft)
                {
                    for (int index = xIndex; index >= 0; index--)
                    {
                        if (horizontalLine[index] == '#' || (index == xObsticle && yIndex == yObstivle))
                        {
                            if (passedBarriers.ContainsKey(index))
                            {
                                if (passedBarriers[index].Contains(yIndex))
                                    newObsticleCounter++;
                                else
                                {
                                    passedBarriers[index] = new List<int>() {yIndex};
                                }
                            }
                            else
                            {
                                passedBarriers[index] = new List<int>() {yIndex};

                                if (horizontalLine[index] == '#')
                                {
                                    if (!skipTiles.ContainsKey(index))
                                        skipTiles[index] = new HashSet<int>() { yIndex };
                                    else
                                        skipTiles[index].Add(yIndex);
                                }
                                
                            }
                            hitObsticle = true;
                            isVertical = true;
                            isLeft = false;
                            break;
                        }
                        xIndex = index;
                    }
                }
                else
                {
                    for (int index = xIndex; index < horizontalLine.Length; index++)
                    {
                        if (horizontalLine[index] == '#' || (index == xObsticle && yIndex == yObstivle))
                        {
                       
                            if (passedBarriers.ContainsKey(index))
                            {
                                if (passedBarriers[index].Contains(yIndex))
                                    newObsticleCounter++;
                                else
                                {
                                    passedBarriers[index] = new List<int>() {yIndex};
                                }
                            }
                            else
                            {
                                passedBarriers[index] = new List<int>() {yIndex};
                                if (horizontalLine[index] == '#')
                                {
                                    if (!skipTiles.ContainsKey(index))
                                        skipTiles[index] = new HashSet<int>() { yIndex };
                                    else
                                        skipTiles[index].Add(yIndex);
                                }
                            }
                            
                            hitObsticle = true;
                            isVertical = true;
                            isLeft = true;
                            break;
                        }
                        xIndex = index;
                    }
                }
            }

            if (newObsticleCounter == 3)
            {
                goodObsticles++;
                if (!obsticles.ContainsKey(xObsticle))
                    obsticles[xObsticle] = new List<int>();
                obsticles[xObsticle].Add(yObstivle);
                hitObsticle = false;
            }
            
            if (!hitObsticle)
            {
                yIndex = lines.IndexOf(lineWithGuard);
                xIndex = lineWithGuard.IndexOf(tileWithGuard);
                isVertical = true;
                isUp = true;
                isLeft = false;
                newObsticleCounter = 0;

                if (pathLines[xObsticle].Count == 0)
                {
                    if (xPaths.Count > 0)
                    {
                        xObsticle = xPaths[0];
                        xPaths.RemoveAt(0);
                        yObstivle = pathLines[xObsticle].Min();
                        pathLines[xObsticle].Remove(pathLines[xObsticle].Min());
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    yObstivle = pathLines[xObsticle].Min();
                    pathLines[xObsticle].Remove(pathLines[xObsticle].Min());
                }
                
                passedBarriers = new Dictionary<int, List<int>>();
                passedBarriers[xObsticle] = new List<int>() { yObstivle };
            }
        }

        // Mora biti 1928
    }
}