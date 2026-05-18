# tg-resource-template-csharp

Reusable RedM C# resource template for conservative CitizenFX Mono runtime usage.

## Architecture

The repository contains two runtime projects:

- `client/tg-resource-template-csharp.client.csproj`
- `server/tg-resource-template-csharp.server.csproj`

Shared code lives under `shared/src` and is compiled directly into both runtime assemblies:

- `shared/src/ResourceConfiguration.cs`
- `shared/src/Logging/LogHelper.cs`
- `shared/src/Data/ResourceData.cs`

There is no shared project, no shared DLL, and no dependency injection container. This keeps assembly loading simple for CitizenFX and avoids runtime dependencies that are not explicitly needed.

## Build Workflow

Use the platform script from the repository root:

```bat
build.cmd
```

```sh
./build.sh
```

The scripts publish both projects in `Release`:

```sh
dotnet publish ./client/tg-resource-template-csharp.client.csproj -c Release
dotnet publish ./server/tg-resource-template-csharp.server.csproj -c Release
```

## Publish Workflow

Use `dotnet publish`, not only `dotnet build`.

The manifest loads published assemblies from:

- `client/bin/Release/netstandard2.0/publish/*.net.dll`
- `server/bin/Release/netstandard2.0/publish/*.net.dll`

Publishing is required because it creates the final runtime folder layout with the compiled `.net.dll` assemblies and required dependency files. A normal build output is not the deployment contract for this template.

## Deployment

Place this repository folder in the server `resources` directory using the exact resource folder name:

```text
tg-resource-template-csharp
```

Build or publish the resource before starting FXServer. Then add the resource to `server.cfg`:

```text
ensure tg-resource-template-csharp
```

On Linux, keep path casing exactly as shown. Linux filesystems are case-sensitive, so `Client`, `client`, `Release`, and `release` are different paths.

## Runtime Expectations

When the resource starts, the client logs:

```text
[tg-resource-template-csharp] [INFO] Client runtime initialized.
```

The server logs:

```text
[tg-resource-template-csharp] [INFO] Server runtime initialized.
```

Runtime logging is performed only inside the client and server projects through `CitizenFX.Core.Debug.WriteLine`. Shared helpers only return formatted strings.

## Why `.net.dll` Is Required

CitizenFX C# resources expect managed script assemblies to use the `.net.dll` naming convention. Both projects set:

```xml
<TargetName>$(AssemblyName).net</TargetName>
```

This produces:

- `tg-resource-template-csharp.client.net.dll`
- `tg-resource-template-csharp.server.net.dll`

The `fxmanifest.lua` script entries intentionally load only `*.net.dll` files.

## Linux Compatibility Notes

The build workflow uses relative paths and works on Linux FXServer environments with the .NET SDK installed. The shell script uses LF line endings and does not rely on Windows-only shell behavior.

Do not rename publish folders or change casing in `fxmanifest.lua`. Keep generated outputs under:

- `client/bin/Release/netstandard2.0/publish`
- `server/bin/Release/netstandard2.0/publish`

## Runtime Compatibility Notes

This template targets `netstandard2.0` and C# 7.3 to remain compatible with the CitizenFX Mono runtime. Avoid introducing modern runtime features, nullable reference types, records, implicit usings, LINQ-heavy shared abstractions, or shared runtime DLLs unless the target runtime requirements are deliberately changed.
Reference RedM C# resource template for CitizenFX/FXServer runtime environments.
