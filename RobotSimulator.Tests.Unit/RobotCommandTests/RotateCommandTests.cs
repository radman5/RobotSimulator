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

    [Theory]
    [InlineData(FacingDirection.North, 1, FacingDirection.West)]
    [InlineData(FacingDirection.North, 2, FacingDirection.South)]
    [InlineData(FacingDirection.North, 3, FacingDirection.East)]
    [InlineData(FacingDirection.North, 4, FacingDirection.North)]
    public void GivenRobotIsPlaced_WhenGivenLeftCommand_RobotShouldRotateAntiClockwise(
        FacingDirection initialFacingDirection, int numberOfRotations, FacingDirection expectedFacingDirection)
    {
        // Arrange
        var commandQueue = new Queue<IRobotCommand>();
        var placeCommand = new PlaceCommand(new Vector2(3, 2), initialFacingDirection);
        commandQueue.Enqueue(placeCommand);
        for (var i = 0; i < numberOfRotations; i++)
        {
            var leftCommand = new RotateCommand(RotationDirection.Left);
            commandQueue.Enqueue(leftCommand);
        }

        // Pre-Assert
        _sut.FacingDirection.Should().Be(initialFacingDirection);

        // Act
        _sut.Execute(commandQueue);

        // Assert
        _sut.FacingDirection.Should().Be(expectedFacingDirection);
    }

    [Theory]
    [InlineData(FacingDirection.North, 1, FacingDirection.East)]
    [InlineData(FacingDirection.North, 2, FacingDirection.South)]
    [InlineData(FacingDirection.North, 3, FacingDirection.West)]
    [InlineData(FacingDirection.North, 4, FacingDirection.North)]
    public void GivenRobotIsPlaced_WhenGivenRightCommand_RobotShouldRotateAntiClockwise(
        FacingDirection initialFacingDirection, int numberOfRotations, FacingDirection expectedFacingDirection)
    {
        // Arrange
        var placeCommand = new PlaceCommand(new Vector2(3, 2), initialFacingDirection);
        var commandQueue = new Queue<IRobotCommand>();
        commandQueue.Enqueue(placeCommand);
        for (var i = 0; i < numberOfRotations; i++)
        {
            var rightCommand = new RotateCommand(RotationDirection.Right);
            commandQueue.Enqueue(rightCommand);
        }

        // Pre-Assert
        _sut.FacingDirection.Should().Be(initialFacingDirection);

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