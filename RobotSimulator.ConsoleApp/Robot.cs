using System.Numerics;
using RobotSimulator.ConsoleApp.Enums;
using RobotSimulator.ConsoleApp.RobotCommands;

namespace RobotSimulator.ConsoleApp;

public class Robot(Board board)
{
    public Vector2 Position { get; private set; }
    public FacingDirection FacingDirection { get; private set; }
    private bool _hasBeenPlaced;

    public void Execute(IEnumerable<IRobotCommand> commands)
    {
        foreach (var command in commands)
        {
            Thread.Sleep(200);
            switch (command)
            {
                case PlaceCommand placeCommand:
                    ExecutePlaceCommand(board, placeCommand);

                    break;
                case RotateCommand rotateCommand:
                    ExecuteRotateCommand(rotateCommand);

                    break;
                case MoveCommand moveCommand:
                    ExecuteMoveCommand(board, moveCommand);

                    break;
                case ReportCommand reportCommand:
                    ExecuteReportCommand(reportCommand);
                    break;
                default:
                    Console.WriteLine($"Command not recognised: {command.GetType().Name}");
                    break;
            }
        }
    }

    private void ExecuteReportCommand(ReportCommand reportCommand)
    {
        if (_hasBeenPlaced)
        {
            Console.WriteLine($"Robot Position: ( {Position.X} , {Position.Y} ). " +
                              $"Robot Facing: {FacingDirection}");
        }
        else
        {
            LogRobotNotPlaced(reportCommand);
        }
    }

    private void ExecuteMoveCommand(Board board, MoveCommand moveCommand)
    {
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
        else
        {
            LogRobotNotPlaced(moveCommand);
        }
    }

    private void ExecuteRotateCommand(RotateCommand rotateCommand)
    {
        if (_hasBeenPlaced)
        {
            FacingDirection = GetFacingDirectionAfterRotation(rotateCommand.RotationDirection);
        }
        else
        {
            LogRobotNotPlaced(rotateCommand, $" Direction: {rotateCommand.RotationDirection}");
        }
    }

    private void ExecutePlaceCommand(Board board, PlaceCommand placeCommand)
    {
        if (board.IsPositionValid(placeCommand.Position))
        {
            Position = placeCommand.Position;
            FacingDirection = placeCommand.FacingDirection;
            _hasBeenPlaced = true;
        }
    }

    private static void LogRobotNotPlaced(IRobotCommand discardedCommand, string additionalInfo = "")
    {
        Console.Error.WriteLine($"Robot has not been placed yet. " +
                          $"Discarding command: {discardedCommand.GetType().Name}." +
                          $"{additionalInfo}");
    }

    private FacingDirection GetFacingDirectionAfterRotation(RotationDirection rotationDirection)
    {
        var newRotation = ((int)FacingDirection + (int)rotationDirection) % 4;
        return FacingDirection = newRotation < 0 ? (FacingDirection)3 : (FacingDirection)newRotation;
    }
}