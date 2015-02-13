#!/bin/sh
mkdir build
cp var/html/* build/
open /Applications/Unity/Unity.app --args -projectPath $PWD/c0533tt3 -nographics -batchmode -buildWebPlayer $PWD/build -quit
