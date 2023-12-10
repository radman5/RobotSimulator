// See https://aka.ms/new-console-template for more information

using RobotSimulator.ConsoleApp;
using RobotSimulator.ConsoleApp.RobotCommands;

Console.WriteLine("Robot booting up...");

var board = new Board(5);
var robot = new Robot(board);

Thread.Sleep(1500);
Console.WriteLine("Robot booted up successfully!");
Thread.Sleep(1500);

Console.WriteLine("Downloading commands...");
Thread.Sleep(1500);

string?[] commandsAsString = await File.ReadAllLinesAsync("commands.txt");
var commandQueue = CommandParser.Parse(commandsAsString);

Console.WriteLine("Commands downloaded successfully!");
Thread.Sleep(1500);

Console.WriteLine("Executing commands...");
Thread.Sleep(1500);

robot.Execute(commandQueue);

Console.WriteLine("Commands executed successfully!");
Thread.Sleep(1500);

Console.WriteLine("Robot shutting down...");
Thread.Sleep(1500);