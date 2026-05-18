# redm-resource-template-csharp

RedM C# resource template structured for CitizenFX runtime compatibility and deterministic packaging.

## Repository Structure

```text
redm-resource-template-csharp/
+-- client/
|   +-- src/
|   +-- redm-resource-template-csharp.client.csproj
+-- server/
|   +-- src/
|   +-- redm-resource-template-csharp.server.csproj
+-- shared/
|   +-- src/
|   +-- redm-resource-template-csharp.shared.csproj
+-- build.cmd
+-- build.sh
+-- fxmanifest.lua
+-- README.md
+-- redm-resource-template-csharp.slnx
```

- `client/` contains the RedM client runtime entry point. It targets `net452`, uses `CitizenFX.Sdk.Client/0.2.3`, and includes shared source files at compile time.
- `server/` contains the FXServer runtime entry point. It targets `netstandard2.0`, references the shared project with `ProjectReference`, and uses `CitizenFX.Core.Server` version `1.0.26803`.
- `shared/` contains common configuration, metadata, and logging helpers. It targets `netstandard2.0` as a standalone shared project.
- `fxmanifest.lua` declares the RedM resource and loads the packaged client and server assemblies.
- `build.cmd` and `build.sh` publish the projects and assemble the deployable `build/` package.
- `redm-resource-template-csharp.slnx` references the client, server, and shared projects.

## Runtime Architecture

The client and server are built as separate CitizenFX script assemblies:

- `redm-resource-template-csharp.client.net.dll`
- `redm-resource-template-csharp.server.net.dll`

The `.net.dll` suffix is intentional. CitizenFX uses that naming convention to identify managed C# script assemblies loaded by the resource manifest.

### Client

The client project targets `net452` because the CitizenFX client runtime is constrained by the client-side Mono/.NET Framework compatibility surface. The client project uses `CitizenFX.Sdk.Client/0.2.3` and emits:

```text
client/redm-resource-template-csharp.client.net.dll
```

The client does not reference or load the shared runtime assembly. Instead, it compiles shared source files directly:

```xml
<Compile Include="../shared/src/**/*.cs" />
```

This keeps the client runtime assembly self-contained for shared template code and avoids introducing an additional shared DLL into the client loading path.

### Server

The server project targets `netstandard2.0` and uses `CitizenFX.Core.Server` version `1.0.26803`. It emits:

```text
server/redm-resource-template-csharp.server.net.dll
```

The server references the shared project:

```xml
<ProjectReference Include="../shared/redm-resource-template-csharp.shared.csproj" />
```

The server runtime can safely use this project reference because the server package explicitly includes the shared assembly beside the server assembly:

```text
server/redm-resource-template-csharp.shared.dll
```

### Shared

The shared project targets `netstandard2.0` and emits:

```text
redm-resource-template-csharp.shared.dll
```

It provides shared configuration, metadata, and logging helpers. Server code consumes it as a normal referenced assembly. Client code consumes the same source files through compile-time inclusion instead of runtime assembly loading.

## Framework Targets

| Module | Target framework | Runtime dependency model |
| --- | --- | --- |
| Client | `net452` | Shared source compiled directly into the client assembly |
| Server | `netstandard2.0` | `ProjectReference` to the shared project |
| Shared | `netstandard2.0` | Standalone shared assembly used by the server |

## Build Workflow

Run builds from the repository root.

### Windows

```bat
dotnet restore
build.cmd
```

### Linux

```sh
dotnet restore
chmod +x build.sh
./build.sh
```

The build scripts publish each project in `Release`, clear the previous `build/` directory, copy `fxmanifest.lua` and `README.md`, and copy the exact runtime DLLs required by the resource.

## Build Output

The deployable package is written to `build/`:

```text
build/
+-- client/
|   +-- CitizenFX.Core.Client.dll
|   +-- redm-resource-template-csharp.client.net.dll
+-- server/
|   +-- CitizenFX.Core.Server.dll
|   +-- redm-resource-template-csharp.server.net.dll
|   +-- redm-resource-template-csharp.shared.dll
+-- fxmanifest.lua
+-- README.md
```

`build/` is the deployment contract for this repository. Do not deploy intermediate `bin/` or `obj/` outputs as the resource package.

## Deployment

Build the resource first, then deploy the contents of `build/` to a RedM server resource folder named:

```text
redm-resource-template-csharp
```

Enable the resource in `server.cfg`:

```text
ensure redm-resource-template-csharp
```

Keep path casing unchanged on Linux systems. Linux filesystems are case-sensitive, and the manifest paths must match the packaged folder and file names exactly.

## Logging

Client and server initialization are separate runtime paths. Each runtime writes its own startup messages through `CitizenFX.Core.Debug.WriteLine`.

Shared logging helpers centralize log formatting and use the configured resource name:

```text
[redm-resource-template-csharp] [LEVEL] Message
```

The helper code is compiled into the client assembly and included as a shared assembly for the server.

## Compatibility Goals

- Preserve CitizenFX client runtime compatibility by targeting `net452` and avoiding a client-side shared runtime DLL.
- Preserve FXServer compatibility by targeting `netstandard2.0` on the server.
- Keep packaging deterministic with explicit copy paths in `build.cmd` and `build.sh`.
- Support both Windows and Linux build environments.
- Keep client and server assemblies split by runtime responsibility.
