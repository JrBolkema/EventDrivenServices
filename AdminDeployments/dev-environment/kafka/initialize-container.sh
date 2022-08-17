#!/bin/sh


echo "Getting MongoDB container status..."
curl --connect-timeout 5 \
     --max-time 10 \
     --retry 6 \
     --retry-delay 0 \
     --retry-max-time 80 \
     --retry-connrefused \
     -X GET http://connect:8083/connectors

     

