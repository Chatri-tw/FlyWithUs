#!/bin/sh

#docker run --rm \
#  -v $(pwd):/project \
#  -w /project pactfoundation/pact-cli:latest publish pacts \
#  --consumer-app-version 0.0.1 \
#  --branch=main \
#  --broker-base-url http://localhost:9292/

./Tools/pact/bin/pact-broker publish pacts \
  --consumer-app-version 0.0.1 \
  --branch=main \
  --broker-base-url http://localhost:9292/
