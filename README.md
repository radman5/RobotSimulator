# Robot Simulator
## How to run console application
1. Open a console window
2. Clone this repo `git clone https://github.com/radman5/RobotSimulator.git`

### If you have .NET 8 SDK installed
3. Navigate to the cloned repo's Console App directory `cd RobotSimulator-main/RobotSimulator.ConsoleApp`
4. Run application `dotnet run`
### Using Docker
3. Navigate to the cloned repo's root  directory `cd RobotSimulator-main`
4. Ensure Docker is running
5. Run command: `docker run --rm -it $(docker build -q -f RobotSimulator.ConsoleApp/Dockerfile .)`
## Process
 - Work through each robot command:
	 - Work through each requirement of the command:
		 - Write a test to test a requirement of the command.
		 - Write the code to pass the test
		 - Repeat for the other requirements
	 - Repeat for the other commands.
 - Implement a class to parse strings into commands.
 - Code the program class to read from a commands.txt file, parse the commands and send to the Robot and test output.
 - Do a few cleanup/refactor passes to simplify code and remove code smells

## Next Steps
 - Thorough error/exception handling pass to ensure known errors and exceptions are handled appropriately and tested to ensure the application doesn't crash unnecessarily.
 - Add tests to CommandParser
	 - Refactor CommandParser to move parsing logic into the commands themselves so it'll turn more into a command factory.
 - Add .NET ILogger to replace calls to Console.WriteLine in order to have more control over what logs get output to Console and also to make ReportCommand easier to test.
 - Investigate options for moving command behaviours into the commands themselves
