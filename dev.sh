#!bin/bash

echo "************** Preparing to build docker image *****************"
docker build -t shortest_path:latest -f Dockerfile . --progress=plain
echo "******************* Completed Building Docker Image *******************"

echo "******************* Running Image *******************"
docker run -it docker.io/library/shortest_path:latest --start="Holland Village" --end="Bugis" --csvpath=StationMap.csv
