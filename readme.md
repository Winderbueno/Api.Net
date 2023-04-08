# Introduction 
This api integrates in User/Identity solution (Related doc [available here](Todo))  
As defined in architecture, it is a client of :
* [identity.auth](Todo). Authentication
* [identity.api](Todo). Manage Identities

# Getting Started

## Configuration
* Startup Projects. 'User.Api'
* ./User.Api/Properties/launchSettings.json. Configure Env (Development | Integration | Production) in `Profiles/Xxx/environmentVariables/ASPNETCORE_ENVIRONMENT`
* ./User.Api/appsettings.<env>.json. By Env, configure Log, ApiUrls... 

## Build & Run
* App. Just clic play in Visual Studio 20XX
* Test. No test for now :(