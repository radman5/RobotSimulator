FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["RobotSimulator.ConsoleApp/RobotSimulator.ConsoleApp.csproj", "RobotSimulator.ConsoleApp/"]
RUN dotnet restore "RobotSimulator.ConsoleApp/RobotSimulator.ConsoleApp.csproj"
COPY . .
WORKDIR "/src/RobotSimulator.ConsoleApp"
RUN dotnet build "RobotSimulator.ConsoleApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RobotSimulator.ConsoleApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RobotSimulator.ConsoleApp.dll"]
