using System.Numerics;
using RobotSimulator.App.Enums;

namespace RobotSimulator.App.RobotCommands;

public record PlaceCommand (Vector2 Position, FacingDirection FacingDirection) : IRobotCommand
{
    public static PlaceCommand Parse(string commandAsString)
    {
        var parameters = commandAsString.Substring(6).Split(',');
        var x = int.Parse(parameters[0]);
        var y = int.Parse(parameters[1]);
        var facingDirection = Enum.Parse<FacingDirection>(parameters[2], true);

        return new PlaceCommand(new Vector2(x, y), facingDirection);
    }
}