# Shortest Path Solution

## Flow of the Solution

- The application starts by the `Program` class `Main` method, receiving the api call
- The input parameters are parsed and validated
- A map is generated based on the `StationMap.csv` file
- Using `Dijkstra` algorithm shortest path is calculated based on the lowest cost
- The generated shortest path is then printed out to the standard output

## Assumptions

- `StationMap.csv` file is present in the root of the `Shortest_Path` project directory, to copy it into the docker container
- The stations present in the csv file are in the order for each mrt line
- The application will help users to find the route on the future network and thus all future stations are also included in the calculation. Opening date of the station is not included in calculations
- Base Cost of travelling between stations is 1 minute
- When calculating the cost based on start time, the `peak`, `non-peak` and `night` times are inclusive of the time range given. example) 5:59pm is `non-peak` but 6:00pm is `peak`

## Decision

- Application is built using `.Net Core 5` `console app`
- Application will return the shortest route only
- Application will accept the start, end & start time as standard input argument
- Application will output the result as standard output
- Shortest path is found using the concept taken from `Dijkstra` algorithm
- Tests are split into `unit` test and `integration` tests, with more unit tests and little integration tests

### Architectural Decision

- To adapt for reading the station data from other modes, the FileReader has an interface to swap it to other readers.
- To adapt for finding shortest route using other algorithm, there is an interface in place to swap to any other future algorithms
- The code is driven using TDD
- Duplication in test code is chosen to have better test readability
- The container for running the application is only built if the `unit` and `integration` tests passes

## Libraries & Tools Used

| Tool Name | Description |
|---------- | ----------- |
|.Net Core 5 | Core framework of choice|
|CsvHelper | To read CSV files|
|CommandLineParser | To parse the input arguments|
|ExpectedObjects| To assert the complex object in testing|
|FluentAssertions| To assert complex objects and to continue the assertion |
|Moq| Mocking the dependency|
|NUnit| Test runner|

## Prerequisites for Ubuntu 16.04

- Relevant permissions to install and run docker images
- Install `docker` by following [How To Install and Use Docker on Ubuntu 16.04](https://www.digitalocean.com/community/tutorials/how-to-install-and-use-docker-on-ubuntu-16-04)
- A running docker in `Ubuntu 16.04`
- Internet connectivity to download images from docker hub

## How to Run

- From the root of the directory run the dev.sh file using `sh dev.sh`

## Future Changes

- Refactor the unit tests to improve readability and maintenance
- Adapt the API output to follow examples.md file

## References

- https://github.com/e-tinkers/mrt_map/blob/master/graph.py
- https://github.com/ken-hu/hui-core-algorithm-dijkstra
- https://www.programmersought.com/article/1843233717/
- https://www.codeproject.com/Articles/1221034/Pathfinding-Algorithms-in-Csharp
- https://joehonour.medium.com/a-guide-to-setting-up-a-net-core-project-using-docker-with-integrated-unit-and-component-tests-a326ca5a0284
- https://docs.microsoft.com/en-us/dotnet/core/docker/build-container?tabs=windows
- https://www.digitalocean.com/community/tutorials/how-to-install-and-use-docker-on-ubuntu-16-04
