using System.Numerics;

namespace RobotSimulator.App;

public record PlaceCommand (Vector2 Position, Direction Direction) : IRobotCommand;