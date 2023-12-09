// See https://aka.ms/new-console-template for more information

using RobotSimulator.App;
using RobotSimulator.App.RobotCommands;

Console.WriteLine("Robot booting up...");
var board = new Board(5);
var robot = new Robot(board);
Thread.Sleep(1500);
Console.WriteLine("Robot booted up successfully!");

Console.WriteLine("Downloading commands...");
string?[] commandsAsString = await File.ReadAllLinesAsync("commands.txt");
var commandQueue = CommandParser.Parse(commandsAsString);
Console.WriteLine("Commands downloaded successfully!");

Console.WriteLine("Executing commands...");
robot.Execute(commandQueue);
Console.WriteLine("Commands executed successfully!");

Thread.Sleep(1500);
while(true)
{
    Console.WriteLine("Enter a command or type 'exit' to quit:");
    var command = Console.ReadLine();
    if (command != null && 
        command.Equals("exit", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }
    commandQueue = CommandParser.Parse(new[]{command});
    robot.Execute(commandQueue);
    Thread.Sleep(1500);
}
Console.WriteLine("Robot shutting down...");
Thread.Sleep(1500);