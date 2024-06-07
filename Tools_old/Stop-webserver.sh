#!/bin/bash

WEBSERVER_PID=".xsp4.pid"

shutdown_webserver()
{
  echo "Shutting down webserver ..."
  kill $(cat "$WEBSERVER_PID")
  rm "$WEBSERVER_PID"
}

shutdown_webserver