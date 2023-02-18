# Reset db
dotnet ef --startup-project ../Bisa.Api/ database update 0

# Delete migrations
dotnet ef --startup-project ../Bisa.Api/ migrations remove
 
# Create initial migration
dotnet ef --startup-project ../Bisa.Api/ migrations add BisaDb-Init -c BisaDbContext -o Migrations/BisaDb

# Update db with migration
dotnet ef --startup-project ../Bisa.Api/ database update --context BisaDbContext