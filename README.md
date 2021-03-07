# Shortest Path Solution

## Flow of the Solution

- The application starts by the `Program` class `Main` method receiving the api call
- The input parameters and parsed and validated
- A map is generated based on the `StationMap.csv` file
- Using `Dijkstra` algorithm shortest path is calculated based on the lowest cost
- The generated shortest path is then printed out to standard out

## Assumptions

- `StationMap.csv` file is present in the root of the `Shortest_Path` project to copy into the docker container
- The stations present in the csv file are per every line
- The application will help users to find route on the future network and thus all future stations are also included in the calculation. Opening date of station is not included in calculations
- Base Cost of travelling between stations is 1 minute
- When calculating the cost based on star time, the `peak`, `non-peak` and `night` timing range calculation are inclusive of the time given

## Decision

- Application will return the shortest route only
- Application will accept the start and end as standard input argument
- Application will output the result as standard out
- Shortest path is found using the concept taken from `Dijkstra` algorithm

### Architectural Decision

- To adapt for reading the station data from other modes, the Reader has an interface to have this flexibility
- To adapt for finding shortest route using other algorithm, there is an interface in place to swap to any future algorithms
- The code is driven using TDD
- Duplication in test code is chosen to have better test readability

Libraries & Tools Used

Prerequisites

How to Run

Future Changes

Refactor the unit tests to improve readability and maintenance
