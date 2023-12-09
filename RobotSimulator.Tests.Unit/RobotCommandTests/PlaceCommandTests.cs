using System.Numerics;
using FluentAssertions;
using RobotSimulator.App;

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
        var validPlaceCommand = new PlaceCommand(new Vector2(1, 1), Direction.South);
        
        // Pre-Assert
        _sut.Position.Should().NotBe(validPlaceCommand.Position);
        _sut.Direction.Should().NotBe(validPlaceCommand.Direction);
        
        // Act
        _sut.Execute(new [] {validPlaceCommand});
        
        // Assert
        _sut.Position.Should().Be(validPlaceCommand.Position);
        _sut.Direction.Should().Be(validPlaceCommand.Direction);
    }

    [Fact]
    public void WhenGivenInvalidPlace_RobotShouldNotUpdate()
    {
        // Arrange
        var startingPosition = new Vector2(0, 0);
        const Direction startingDirection = Direction.North;
        var validPlaceCommand = new PlaceCommand(new Vector2(100, 1), Direction.South);
        
        // Pre-Assert
        _sut.Position.Should().Be(startingPosition);
        _sut.Direction.Should().Be(startingDirection);
        startingPosition.Should().NotBe(validPlaceCommand.Position);
        startingDirection.Should().NotBe(validPlaceCommand.Direction);
        
        // Act
        _sut.Execute(new [] {validPlaceCommand});
        
        // Assert
        _sut.Position.Should().Be(startingPosition);
        _sut.Direction.Should().Be(startingDirection);
    }
}