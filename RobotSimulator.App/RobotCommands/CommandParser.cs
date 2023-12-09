using RobotSimulator.App.Enums;

namespace RobotSimulator.App.RobotCommands;

public static class CommandParser
{
    public static Queue<IRobotCommand> Parse(string?[] commandsAsStrings)
    {
        var commandQueue = new Queue<IRobotCommand>();

        foreach (var commandAsString in commandsAsStrings)
        {
            Thread.Sleep(100);
            Console.WriteLine($"Parsing command: {commandAsString}");
            switch (commandAsString)
            {
                case not null when commandAsString.StartsWith("PLACE", StringComparison.OrdinalIgnoreCase):
                    var placeCommand = PlaceCommand.Parse(commandAsString);
                    commandQueue.Enqueue(placeCommand);
                    break;
                case not null when commandAsString.StartsWith("MOVE", StringComparison.OrdinalIgnoreCase):
                    var moveCommand = new MoveCommand();
                    commandQueue.Enqueue(moveCommand);
                    break;
                case not null when commandAsString.StartsWith("LEFT", StringComparison.OrdinalIgnoreCase):
                    var rotateLeftCommand = new RotateCommand(RotationDirection.Left);
                    commandQueue.Enqueue(rotateLeftCommand);
                    break;
                case not null when commandAsString.StartsWith("RIGHT", StringComparison.OrdinalIgnoreCase):
                    var rotateRightCommand = new RotateCommand(RotationDirection.Right);
                    commandQueue.Enqueue(rotateRightCommand);
                    break;
                case not null when commandAsString.StartsWith("REPORT", StringComparison.OrdinalIgnoreCase):
                    var reportCommand = new ReportCommand();
                    commandQueue.Enqueue(reportCommand);
                    break;
                default:
                    Console.WriteLine($"Invalid command: {commandAsString} could not be parsed.");
                    break;
            }
        }

        return commandQueue;
    }
}