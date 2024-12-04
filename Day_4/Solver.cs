namespace Day_4;

public class Solver
{
    private enum Direction { Up, Down, Left, Right, UpRight, UpLeft, DownRight, DownLeft }
    private static string GetStringForDir(List<string> input, int xPos, int yPos, Direction dir)
    {
        int x2 = xPos, x3 = xPos, x4 = xPos;
        int y2 = yPos, y3 = yPos, y4 = yPos;
        switch (dir)
        {
            case Direction.Up:
                y2 = yPos - 1;
                y3 = yPos - 2;
                y4 = yPos - 3;
                break;
            case Direction.Down:
                y2 = yPos + 1;
                y3 = yPos + 2;
                y4 = yPos + 3;
                break;
            case Direction.Left:
                x2 = xPos - 1;
                x3 = xPos - 2;
                x4 = xPos - 3;
                break;
            case Direction.Right:
                x2 = xPos + 1;
                x3 = xPos + 2;
                x4 = xPos + 3;
                break;
            case Direction.UpLeft:
                y2 = yPos - 1;
                y3 = yPos - 2;
                y4 = yPos - 3;
                x2 = xPos - 1;
                x3 = xPos - 2;
                x4 = xPos - 3;
                break;
            case Direction.UpRight:
                y2 = yPos - 1;
                y3 = yPos - 2;
                y4 = yPos - 3;
                x2 = xPos + 1;
                x3 = xPos + 2;
                x4 = xPos + 3;
                break;
            case Direction.DownLeft:
                y2 = yPos + 1;
                y3 = yPos + 2;
                y4 = yPos + 3;
                x2 = xPos - 1;
                x3 = xPos - 2;
                x4 = xPos - 3;
                break;
            case Direction.DownRight:  
                y2 = yPos + 1;
                y3 = yPos + 2;
                y4 = yPos + 3;
                x2 = xPos + 1;
                x3 = xPos + 2;
                x4 = xPos + 3;
                break;
        }

        return $"{input[yPos][xPos]}{input[y2][x2]}{input[y3][x3]}{input[y4][x4]}";
    }

    private static bool IsDirValid(List<string> characters, int xPos, int yPos, Direction dir, int maxLength)
    {
        switch (dir)
        {
            case Direction.Up:
                return yPos - maxLength >= 0;
            case Direction.Down:
                return yPos < characters.Count - maxLength;
            case Direction.Left:
                return xPos - maxLength >= 0;
            case Direction.Right:
                return xPos < characters[yPos].Length - maxLength;
            case Direction.UpLeft:
                return xPos - maxLength >= 0 && yPos - maxLength >= 0;
            case Direction.UpRight:
                return xPos < characters[yPos].Length - maxLength && yPos - maxLength >= 0;
            case Direction.DownLeft:
                return xPos - maxLength >= 0 && yPos < characters.Count - maxLength;
            case Direction.DownRight:
                return xPos < characters[yPos].Length - maxLength && yPos < characters.Count - maxLength;
            default: 
                return false;
        }
    }
    
    public static int Run_PartOne(List<string> input)
    {
        var matchedString = "XMAS";
        int matchedCount = 0;
        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                var character = input[y][x].ToString();
                if (character == "X")
                {
                    foreach (var dir in new List<Direction>(){ Direction.Up, Direction.Down, Direction.Left, Direction.Right, Direction.UpRight, Direction.UpLeft, Direction.DownRight, Direction.DownLeft })
                    {
                        if (IsDirValid(input, x, y, dir, matchedString.Length - 1))
                        {
                           var res = GetStringForDir(input, x, y, dir);
                           if (res == matchedString) matchedCount++;
                        } 
                    }
                }
            }
        }
        return matchedCount;
    }
    public static int Run_PartTwo(List<string> input)
    {
        int matchedCount = 0;
        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                var character = input[y][x].ToString();
                if (character == "A")
                {
                    var topLeftX = x - 1;
                    var topLeftY = y - 1;
                    var topRightX = x + 1;
                    var topRightY = y - 1;
                    var bottomLeftX = x - 1;
                    var bottomLeftY = y + 1;
                    var bottomRightX = x + 1;
                    var bottomRightY = y + 1;
                    if (IsDirValid(input, x, y, Direction.UpLeft, 1) && IsDirValid(input, x, y, Direction.UpRight, 1)
                        && IsDirValid(input, x, y, Direction.DownLeft, 1) && IsDirValid(input, x, y, Direction.DownRight, 1))
                    {
                        if(((input[topLeftY][topLeftX] == 'M' && input[bottomRightY][bottomRightX] == 'S') || (input[topLeftY][topLeftX] == 'S' && input[bottomRightY][bottomRightX] == 'M')) &&
                           ((input[topRightY][topRightX] == 'M' && input[bottomLeftY][bottomLeftX] == 'S') || (input[topRightY][topRightX] == 'S' && input[bottomLeftY][bottomLeftX] == 'M')))
                        {
                            matchedCount++;
                        }
                    } 
                }
            }
        }
        return matchedCount;
    }
}