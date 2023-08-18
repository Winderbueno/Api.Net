# Reset db
dotnet ef --startup-project ../Api/ database update 0

# Delete migrations
dotnet ef --startup-project ../Api/ migrations remove
 
# Create initial migration
dotnet ef --startup-project ../Api/ migrations add UserDb-Init -c UserDbContext -o Migrations/UserDb

# Update db with migration
dotnet ef --startup-project ../Api/ database update --context UserDbContext