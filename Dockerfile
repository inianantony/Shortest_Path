FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY *.sln .
COPY Shortest_Path/*.csproj ./Shortest_Path/
COPY ShortestPath.UnitTests/*.csproj ./ShortestPath.UnitTests/
COPY ShortestPath.IntegrationTest/*.csproj ./ShortestPath.IntegrationTest/
RUN dotnet restore
COPY . /app
RUN dotnet build

WORKDIR /app/ShortestPath.UnitTests
RUN echo "Running Unit Tests!"
RUN dotnet test --logger:html

WORKDIR /app/ShortestPath.IntegrationTest
RUN echo "Running Integration Tests!"
RUN dotnet test --logger:html

FROM build AS publish
WORKDIR /app/Shortest_Path
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=publish /app/Shortest_Path/out ./
COPY --from=publish /app/Shortest_Path/StationMap.csv ./
ENTRYPOINT ["dotnet", "Shortest_Path.dll"]