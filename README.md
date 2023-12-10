# Robot Simulator
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
 - Thorough error/exception handling pass to ensure known errors and exceptions are handled appropriately to ensure the application doesn't crash unnecessarily.
 - Add tests to CommandParser
	 - Refactor CommandParser to move parsing logic into the commands themselves so it'll turn more into a command factory.
 - Investigate options for moving command behaviours into the commands themselves
