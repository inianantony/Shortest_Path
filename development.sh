#!/bin/bash

docker build -t shortest_path -f Dockerfile . --progress=plain

docker run -it docker.io/library/shortest_path --start="Holland Village" --end="Bugis" --csvpath=StationMap.csv