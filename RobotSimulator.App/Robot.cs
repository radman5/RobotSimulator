using System.Numerics;
using RobotSimulator.App.Enums;
using RobotSimulator.App.RobotCommands;

namespace RobotSimulator.App;

public class Robot(Board board)
{
    public Vector2 Position { get; private set; }
    public FacingDirection FacingDirection { get; private set; }
    private bool _hasBeenPlaced = false;

    public void Execute(IEnumerable<IRobotCommand> commands)
    {
        foreach (var command in commands)
        {
            switch (command)
            {
                case PlaceCommand placeCommand:
                    if (board.IsPositionValid(placeCommand.Position))
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
                case MoveCommand moveCommand:
                    if (_hasBeenPlaced)
                    {
                        var movementVector = FacingDirection switch
                        {
                            FacingDirection.North => new Vector2(0, 1),
                            FacingDirection.East => new Vector2(1, 0),
                            FacingDirection.South => new Vector2(0, -1),
                            FacingDirection.West => new Vector2(-1, 0),
                            _ => throw new ArgumentOutOfRangeException()
                        };

                        var newPosition = Position + movementVector;
                        if (board.IsPositionValid(newPosition))
                        {
                            Position = newPosition;
                        }
                    }

                    break;
                case ReportCommand reportCommand:
                    if(_hasBeenPlaced)
                    {
                        Console.WriteLine($"Robot Position:( {Position.X} , {Position.Y} )" +
                                          $"Robot Facing: {FacingDirection}");
                    }
                    break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(command));
            }
        }
    }

    private FacingDirection Rotate(RotationDirection rotationDirection)
    {
        var newRotation = ((int)FacingDirection + (int)rotationDirection) % 4;
        return FacingDirection = newRotation < 0 ? (FacingDirection)3 : (FacingDirection)newRotation;
    }
}