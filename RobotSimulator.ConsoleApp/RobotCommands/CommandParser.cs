using RobotSimulator.ConsoleApp.Enums;

namespace RobotSimulator.ConsoleApp.RobotCommands;

public static class CommandParser
{
    public static Queue<IRobotCommand> Parse(string?[] commandsAsStrings)
    {
        var commandQueue = new Queue<IRobotCommand>();

        foreach (var commandAsString in commandsAsStrings)
        {
            switch (commandAsString)
            {
                case not null when commandAsString.StartsWith(CommandName.Place, StringComparison.OrdinalIgnoreCase):
                    var placeCommand = PlaceCommand.Parse(commandAsString);
                    commandQueue.Enqueue(placeCommand);
                    break;
                case not null when commandAsString.Equals(CommandName.Move, StringComparison.OrdinalIgnoreCase):
                    var moveCommand = new MoveCommand();
                    commandQueue.Enqueue(moveCommand);
                    break;
                case not null when commandAsString.Equals(CommandName.Left, StringComparison.OrdinalIgnoreCase):
                    var rotateLeftCommand = new RotateCommand(RotationDirection.Left);
                    commandQueue.Enqueue(rotateLeftCommand);
                    break;
                case not null when commandAsString.Equals(CommandName.Right, StringComparison.OrdinalIgnoreCase):
                    var rotateRightCommand = new RotateCommand(RotationDirection.Right);
                    commandQueue.Enqueue(rotateRightCommand);
                    break;
                case not null when commandAsString.Equals(CommandName.Report, StringComparison.OrdinalIgnoreCase):
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