# Reset db
dotnet ef --startup-project ../User.Api/ database update 0

# Delete migrations
dotnet ef --startup-project ../User.Api/ migrations remove
 
# Create initial migration
dotnet ef --startup-project ../User.Api/ migrations add UserDb-Init -c UserDbContext -o Migrations/UserDb

# Update db with migration
dotnet ef --startup-project ../User.Api/ database update --context UserDbContext