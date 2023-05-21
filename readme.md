# Introduction 
This api integrates in [User/Identity Demo](Todo:DocLink))  
As defined in architecture, it is a client of :
* [identity.auth](Todo). Authentication
* [identity.api](Todo). Identities

# Getting Started

## Configuration
* Startup Projects. 'User.Api'
* ./User.Api/Properties/launchSettings.json. Configure Env (Development | Integration | Production) in `Profiles/Xxx/environmentVariables/ASPNETCORE_ENVIRONMENT`
* ./User.Api/appsettings.<env>.json. By Env, configure Log, ApiUrls... 

## Build & Run
* App. Just clic play in Visual Studio 20XX
* Test. No test for now :(

# Manage Database
Database is managed by running commands in a specific folder via a terminal (Either powershell, Package Manager Console...)  
This project offers you these different options :

* Native EF Command Tools (Run in « ./src/User.Persistence »)
  * `dotnet ef --startup-project ../User.Api/ <cmd>` with `<cmd>`
    * Create Migration. `migrations add <name> -c UserDbContext -o Migrations/UserDb`
    * Delete Migration. `migrations remove`
    * Update database with Migration. `database update --context UserDbContext`
* Specific Script (Run in « ./src/User.Persistence » with powershell)
  * `./Scripts/ResetDb.ps1`. 
  * This script reset the db, delete all migration, create a new init migration and finally update the db with this migration, it is useful during db model design step  
* Executable Tools (Run in « ./src/User.Api/bin/Debug/net6.0 »)
  * `./User.Api.Exe -<cmd>` with `<cmd>`. (Help. –h | Check Migration. –v | Update db with Migration. –m)
  * This tool allow (app & db mngt) logs centralisation in Kibana and avoid installing EFTools on all deployed environment (Int | Acc | Prd)