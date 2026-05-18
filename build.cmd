@echo off

echo ==================================================
echo Building tg-resource-template-csharp
echo ==================================================

pushd client

dotnet publish tg-resource-template-csharp.client.csproj -c Release

popd

if %errorlevel% neq 0 exit /b %errorlevel%

pushd server

dotnet publish tg-resource-template-csharp.server.csproj -c Release

popd

if %errorlevel% neq 0 exit /b %errorlevel%

pushd shared

dotnet publish tg-resource-template-csharp.shared.csproj -c Release

popd

if %errorlevel% neq 0 exit /b %errorlevel%

echo ==================================================
echo Preparing build package
echo ==================================================

rmdir /s /q build 2>nul

mkdir build
mkdir build\client
mkdir build\server

copy /y fxmanifest.lua build
copy /y README.md build\

copy /y client\bin\Release\net452\publish\CitizenFX.Core.Client.dll build\client\
copy /y client\bin\Release\net452\publish\tg-resource-template-csharp.client.net.dll build\client\

copy /y server\bin\Release\netstandard2.0\publish\CitizenFX.Core.Server.dll build\server\
copy /y server\bin\Release\netstandard2.0\publish\tg-resource-template-csharp.server.net.dll build\server\

copy /y shared\bin\Release\netstandard2.0\publish\tg-resource-template-csharp.shared.dll build\server\

echo ==================================================
echo Build completed successfully
echo ==================================================

pause
