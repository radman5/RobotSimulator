using System.Numerics;

namespace RobotSimulator.App;

public class Robot
{
    public Vector2 Position { get; private set; }
    public Direction Direction { get; private set; }
    private readonly Board _board;

    public Robot(Board board)
    {
        _board = board;
    }

    public void Execute(IEnumerable<IRobotCommand> commands)
    {
        foreach (var command in commands)
        {
            switch (command)
            {
                case PlaceCommand placeCommand:
                    if (_board.IsPositionValid(placeCommand.Position))
                    {
                        Position = placeCommand.Position;
                        Direction = placeCommand.Direction;
                    }

                    break;
            }
        }
    }
}

public record Board(int Height = 5, int Width = 5)
{
    public Board(int size) : this(size, size){}
    public bool IsPositionValid(Vector2 positionToCheck)
    {
        return positionToCheck.X >= 0 && positionToCheck.X <= Width &&
               positionToCheck.Y >= 0 && positionToCheck.Y <= Height;
    }
}