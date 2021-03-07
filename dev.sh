#!bin/bash

echo "************** Preparing to build docker image *****************"
docker build -t shortest_path:latest -f Dockerfile . --progress=plain
echo "******************* Completed Building Docker Image *******************"

echo "******************* Running Image without Start Time *******************"
docker run -it docker.io/library/shortest_path:latest --start="Holland Village" --end="Bugis" --csvpath=StationMap.csv

echo "******************* Running Image With Start Time *******************"
docker run -it docker.io/library/shortest_path:latest --start="Boon Lay" --end="Little India" --csvpath=StationMap.csv --starttime=2021-03-05T20:30
