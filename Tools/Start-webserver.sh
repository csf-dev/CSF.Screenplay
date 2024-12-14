#!/bin/bash

SERVER_PORT="8080"
SERVER_ADDR="127.0.0.1"
SERVER_WEB_APP="/:CSF.Screenplay.WebTestWebsite/"
SERVER_PID=".xsp4.pid"
APP_HOMEPAGE="http://localhost:8080/Home"
SECONDS_BETWEEN_CONNECT_ATTEMPTS="1"
MAX_ATTEMPTS="4"

app_available=1

stop_if_already_started()
{
  if [ -f "$SERVER_PID" ]
  then
    echo "Stopping a running webserver first ..."
    Tools/Stop-webserver.sh
  fi
}

start_webserver()
{
  echo "Starting the app on a web server ..."
  xsp4 \
    --nonstop \
    --address "$SERVER_ADDR" \
    --port "$SERVER_PORT" \
    --applications "$SERVER_WEB_APP" \
    --pidfile "$SERVER_PID" \
    &
}

wait_for_app_to_become_available()
{
  echo "Waiting for the app to become available ..."
  for attempt in $(seq 1 "$MAX_ATTEMPTS")
  do
    sleep "$SECONDS_BETWEEN_CONNECT_ATTEMPTS"
    echo "Connection attempt $attempt of $MAX_ATTEMPTS ..."
    try_web_app_connection
    if [ "$app_available" -eq "0" ]
    then
      echo "Connection successful!"
      break
    fi
  done
}

try_web_app_connection()
{
  wget \
    -T 90 \
    -O - \
    "$APP_HOMEPAGE" \
    >/dev/null
  if [ "$?" -eq "0" ]
  then
    app_available=0
  fi
}

stop_if_already_started
start_webserver
wait_for_app_to_become_available

if [ "$app_available" -ne "0" ]
then
  echo "ERROR: Web application did not come up after $MAX_ATTEMPTS attempt(s)"
  echo "The webserver is still running, you may attempt to diagnose the problem at:"
  echo "  $APP_HOMEPAGE"
fi

exit "$app_available"
