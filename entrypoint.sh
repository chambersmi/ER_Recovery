#!/bin/bash

set -e

# Apply EF Core migrations
echo "Applying EF Core migrations..."
dotnet ef database update

# Start the app
echo "Starting ER_Recovery.Web..."
exec dotnet ER_Recovery.Web.dll
