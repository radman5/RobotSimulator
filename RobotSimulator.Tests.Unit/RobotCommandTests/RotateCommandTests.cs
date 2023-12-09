using System.Numerics;
using FluentAssertions;
using RobotSimulator.App;
using RobotSimulator.App.Enums;
using RobotSimulator.App.RobotCommands;

namespace RobotSimulator.Tests.Unit.RobotCommandTests;

public class RotateCommandTests
{
    private readonly Robot _sut;

    public RotateCommandTests()
    {
        var board = new Board(5);
        _sut = new Robot(board);
    }

    [Fact]
    public void GivenRobotIsPlaced_WhenGivenLeftCommand_RobotShouldRotateAntiClockwise()
    {
        // Arrange
        var placeCommand = new PlaceCommand(new Vector2(3, 2), FacingDirection.South);
        var leftCommand = new RotateCommand(RotationDirection.Left);
        var commandQueue = new Queue<IRobotCommand>();
        commandQueue.Enqueue(placeCommand);
        commandQueue.Enqueue(leftCommand);
        const FacingDirection expectedFacingDirection = FacingDirection.East;
        
        // Pre-Assert
        _sut.FacingDirection.Should().NotBe(expectedFacingDirection);

        // Act
        _sut.Execute(commandQueue);

        // Assert
        _sut.FacingDirection.Should().Be(expectedFacingDirection);
    }

    [Fact]
    public void GivenRobotIsPlaced_WhenGivenRightCommand_RobotShouldRotateAntiClockwise()
    {
        // Arrange
        var placeCommand = new PlaceCommand(new Vector2(3, 2), FacingDirection.South);
        var rightCommand = new RotateCommand(RotationDirection.Right);
        var commandQueue = new Queue<IRobotCommand>();
        commandQueue.Enqueue(placeCommand);
        commandQueue.Enqueue(rightCommand);
        const FacingDirection expectedFacingDirection = FacingDirection.West;
        
        // Pre-Assert
        _sut.FacingDirection.Should().NotBe(expectedFacingDirection);

        // Act
        _sut.Execute(commandQueue);

        // Assert
        _sut.FacingDirection.Should().Be(expectedFacingDirection);
    }

    [Fact]
    public void GivenRobotIsNotPlaced_WhenGivenRotateCommand_RobotShouldIgnoreRotate()
    {
        // Arrange
        var leftCommand = new RotateCommand(RotationDirection.Right);
        var commandQueue = new Queue<IRobotCommand>();
        commandQueue.Enqueue(leftCommand); // Rotate before valid place
        const FacingDirection expectedFacingDirection = FacingDirection.North;
        
        // Pre-Assert
        _sut.FacingDirection.Should().Be(expectedFacingDirection);

        // Act
        _sut.Execute(commandQueue);

        // Assert
        _sut.FacingDirection.Should().Be(expectedFacingDirection);
    }
}