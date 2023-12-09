using System.Numerics;

namespace RobotSimulator.App;

public record Board(int Height = 5, int Width = 5)
{
    public Board(int size) : this(size, size)
    {
        if(size < 1) throw new ArgumentException("Board size must be greater than 0", nameof(size));
    }

    public bool IsPositionValid(Vector2 positionToCheck)
    {
        if (positionToCheck.X >= 0 && positionToCheck.X < Width &&
            positionToCheck.Y >= 0 && positionToCheck.Y < Height)
        {
            return true;
        }
        Console.WriteLine($"Invalid position: {positionToCheck}. Discarding command.");
        return false;
    }
}