using RobotSimulator.App.Enums;

namespace RobotSimulator.App.RobotCommands;

public record RotateCommand (RotationDirection RotationDirection) : IRobotCommand;