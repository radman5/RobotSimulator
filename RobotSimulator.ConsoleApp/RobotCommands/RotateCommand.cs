using RobotSimulator.ConsoleApp.Enums;

namespace RobotSimulator.ConsoleApp.RobotCommands;

public record RotateCommand (RotationDirection RotationDirection) : IRobotCommand;