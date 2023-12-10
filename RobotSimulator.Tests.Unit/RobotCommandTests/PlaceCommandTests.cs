using System.Numerics;
using FluentAssertions;
using RobotSimulator.ConsoleApp;
using RobotSimulator.ConsoleApp.Enums;
using RobotSimulator.ConsoleApp.RobotCommands;

namespace RobotSimulator.Tests.Unit.RobotCommandTests;

public class PlaceCommandTests
{
    private readonly Robot _sut;

    public PlaceCommandTests()
    {
        var board = new Board(5);
        _sut = new Robot(board);
    }
    
    [Fact]
    public void WhenGivenValidPlace_RobotShouldBeAtPlaceLocationWithDirection()
    {
        // Arrange
        var validPlaceCommand = new PlaceCommand(new Vector2(1, 1), FacingDirection.South);
        
        // Pre-Assert
        _sut.Position.Should().NotBe(validPlaceCommand.Position);
        _sut.FacingDirection.Should().NotBe(validPlaceCommand.FacingDirection);
        
        // Act
        _sut.Execute(new [] {validPlaceCommand});
        
        // Assert
        _sut.Position.Should().Be(validPlaceCommand.Position);
        _sut.FacingDirection.Should().Be(validPlaceCommand.FacingDirection);
    }

    [Fact]
    public void WhenGivenInvalidPlace_RobotShouldNotUpdate()
    {
        // Arrange
        var startingPosition = new Vector2(0, 0);
        const FacingDirection startingDirection = FacingDirection.North;
        var validPlaceCommand = new PlaceCommand(new Vector2(100, 1), FacingDirection.South);
        
        // Pre-Assert
        _sut.Position.Should().Be(startingPosition);
        _sut.FacingDirection.Should().Be(startingDirection);
        startingPosition.Should().NotBe(validPlaceCommand.Position);
        startingDirection.Should().NotBe(validPlaceCommand.FacingDirection);
        
        // Act
        _sut.Execute(new [] {validPlaceCommand});
        
        // Assert
        _sut.Position.Should().Be(startingPosition);
        _sut.FacingDirection.Should().Be(startingDirection);
    }
}