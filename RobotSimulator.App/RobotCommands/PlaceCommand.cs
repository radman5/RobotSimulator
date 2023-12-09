using System.Numerics;
using RobotSimulator.App.Enums;

namespace RobotSimulator.App.RobotCommands;

public record PlaceCommand (Vector2 Position, FacingDirection FacingDirection) : IRobotCommand;