-- Resource manifest for tg-resource-template-csharp.
--
-- fxmanifest.lua is required by FXServer/RedM.
-- It tells the server what game this resource targets and which compiled C# scripts to load.

fx_version 'cerulean'
game 'rdr3'

-- Required warning for RedM resources.
-- This is a standard RedM manifest line.
rdr3_warning 'I acknowledge that this is a prerelease build of RedM, and I am aware my resources *will* become incompatible once RedM ships.'

author 'Trembita Games'
description 'Canonical standalone C# resource template for Trembita Games RedM repositories'
version '0.1.0'

-- This template is intentionally compiled before runtime.
-- FXServer does not build C# resources from source when a resource starts.
--
-- Build command from the repository root:
-- dotnet build .\redm-server-data.slnx -c Release
--
-- Client and server code are emitted as separate .net.dll assemblies.
-- The .net.dll suffix tells the CitizenFX runtime to load each assembly as a C# script.

-- Client scripts run on each connected player's RedM client.
-- This assembly references CitizenFX.Core.Client only.
client_script {
    'client/CitizenFX.Core.Client.dll',
    'client/tg-resource-template-csharp.client.net.dll'
}

-- Server scripts run on the FXServer runtime.
-- This assembly references CitizenFX.Core.Server only.
server_script {
    'server/CitizenFX.Core.Server.dll',
    'server/tg-resource-template-csharp.server.net.dll',
    'server/tg-resource-template-csharp.shared.dll'
}

-- Client runtime dependency assemblies are listed as files so clients receive the exact DLLs
-- that the client assembly was built with. Server-only assemblies are intentionally not sent
-- to clients.
files {}
