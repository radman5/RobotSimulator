using System.Numerics;
using FluentAssertions;
using RobotSimulator.ConsoleApp;
using RobotSimulator.ConsoleApp.Enums;
using RobotSimulator.ConsoleApp.RobotCommands;

namespace RobotSimulator.Tests.Unit.RobotCommandTests;

public class MoveCommandTests
{
    private Robot _sut;

    public MoveCommandTests()
    {
        var board = new Board(5);
        _sut = new Robot(board);
    }
    
    [Theory]
    [InlineData(FacingDirection.North, 1, 1, 1, 2)]
    [InlineData(FacingDirection.East, 1, 1, 2, 1)]
    [InlineData(FacingDirection.South, 1, 1, 1, 0)]
    [InlineData(FacingDirection.West, 1, 1, 0, 1)]
    public void GivenRobotIsPlaced_WhenGivenValidMoveCommand_RobotShouldMoveOneUnitInFacingDirection(
        FacingDirection initialFacingDirection, int initialX, int initialY, int expectedX, int expectedY)
    {
        // Arrange
        var initialPosition = new Vector2(initialX, initialY);
        var placeCommand = new PlaceCommand(initialPosition, initialFacingDirection);
        var moveCommand = new MoveCommand();
        var commandQueue = new Queue<IRobotCommand>();
        commandQueue.Enqueue(placeCommand);
        commandQueue.Enqueue(moveCommand);
        var expectedPosition = new Vector2(expectedX, expectedY);
        
        // Pre-Assert
        _sut.Position.Should().NotBe(expectedPosition);
        
        // Act
        _sut.Execute(commandQueue);
        
        // Assert
        _sut.Position.Should().Be(expectedPosition);
    }
    
    [Theory]
    [InlineData(FacingDirection.North)]
    [InlineData(FacingDirection.East)]
    [InlineData(FacingDirection.South)]
    [InlineData(FacingDirection.West)]
    public void GivenRobotIsPlaced_WhenGivenInvalidMoveCommand_RobotShouldNotMove(
        FacingDirection initialFacingDirection)
    {
        // Arrange
        var smallBoard = new Board(1);
        _sut = new Robot(smallBoard);
        var initialPosition = new Vector2(0, 0);
        var placeCommand = new PlaceCommand(initialPosition, initialFacingDirection);
        var moveCommand = new MoveCommand();
        var commandQueue = new Queue<IRobotCommand>();
        commandQueue.Enqueue(placeCommand);
        commandQueue.Enqueue(moveCommand);
        var expectedPosition = initialPosition;
        
        // Pre-Assert
        _sut.Position.Should().Be(initialPosition);
        
        // Act
        _sut.Execute(commandQueue);
        
        // Assert
        _sut.Position.Should().Be(expectedPosition);
    }
    
    [Fact]
    public void GivenRobotNotPlaced_WhenGivenValidMoveCommand_RobotShouldIgnoreMoveCommand()
    {
        // Arrange
        var moveCommand = new MoveCommand();
        var commandQueue = new Queue<IRobotCommand>();
        commandQueue.Enqueue(moveCommand);
        var initialPosition = new Vector2(0, 0);
        
        // Pre-Assert
        _sut.Position.Should().Be(initialPosition);
        
        // Act
        _sut.Execute(commandQueue);
        
        // Assert
        _sut.Position.Should().Be(initialPosition);
    }
}