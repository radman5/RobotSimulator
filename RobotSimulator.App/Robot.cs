using System.Numerics;
using RobotSimulator.App.Enums;
using RobotSimulator.App.RobotCommands;

namespace RobotSimulator.App;

public class Robot
{
    public Vector2 Position { get; private set; }
    public FacingDirection FacingDirection { get; private set; }
    private readonly Board _board;
    private bool _hasBeenPlaced = false;

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
                        FacingDirection = placeCommand.FacingDirection;
                        _hasBeenPlaced = true;
                    }
                    
                    break;
                case RotateCommand rotateCommand:
                    if (_hasBeenPlaced)
                    {
                        FacingDirection = Rotate(rotateCommand.RotationDirection);
                    }

                    break;
            }
        }
    }

    private FacingDirection Rotate(RotationDirection rotationDirection)
    {
        return FacingDirection = (FacingDirection)(((int)FacingDirection + (int)rotationDirection) % 4);
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