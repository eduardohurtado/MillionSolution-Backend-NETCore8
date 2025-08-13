#!/bin/bash

set -e

if [ -z "$1" ]; then
  echo "Usage: ./run-tests.sh [unit|integration|all]"
  exit 1
fi

case "$1" in
  unit)
    echo "🔍 Running Unit Tests only..."
    dotnet test --filter TestCategory=Unit
    ;;
  api)
    echo "🎯 Running Api Tests only..."
    dotnet test --filter TestCategory=Api
    ;;
  integration)
    echo "🌐 Running Integration Tests only..."
    dotnet test --filter TestCategory=Integration
    ;;
  all)
    echo "✅ Running ALL tests..."
    dotnet test
    ;;
  *)
    echo "Invalid option: $1"
    echo "Usage: ./run-tests.sh [unit|api|integration|all]"
    exit 1
    ;;
esac
