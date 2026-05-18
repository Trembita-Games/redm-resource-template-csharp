#!/usr/bin/env bash

set -e

echo "=================================================="
echo "Building redm-resource-template-csharp"
echo "=================================================="

pushd client > /dev/null

dotnet publish redm-resource-template-csharp.client.csproj -c Release

popd > /dev/null

pushd server > /dev/null

dotnet publish redm-resource-template-csharp.server.csproj -c Release

popd > /dev/null

pushd shared > /dev/null

dotnet publish redm-resource-template-csharp.shared.csproj -c Release

popd > /dev/null

echo "=================================================="
echo "Preparing build package"
echo "=================================================="

rm -rf build

mkdir -p build
mkdir -p build/client
mkdir -p build/server

cp fxmanifest.lua build/
cp README.md build/

cp client/bin/Release/net452/publish/CitizenFX.Core.Client.dll build/client/
cp client/bin/Release/net452/publish/redm-resource-template-csharp.client.net.dll build/client/

cp server/bin/Release/netstandard2.0/publish/CitizenFX.Core.Server.dll build/server/
cp server/bin/Release/netstandard2.0/publish/redm-resource-template-csharp.server.net.dll build/server/

cp shared/bin/Release/netstandard2.0/publish/redm-resource-template-csharp.shared.dll build/server/

echo "=================================================="
echo "Build completed successfully"
echo "=================================================="
