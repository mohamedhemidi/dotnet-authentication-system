## Settings up user-secrets data :
```sh
cd ./src/backend-core.Api

dotnet user-secrets set "JwtSettings:Secret" "[128bitKey]"

dotnet user-secrets set "ConnectionString:mysql" "server=localhost;user=<user_name>;password=<password></password>%;database=<database_name>"

dotnet user-secrets set "EmailSettings" ""

```

## Create migration 

```sh
// create migration
dotnet ef migrations add init -s ./src/backend-core.Api/backend-core.Api.csproj -p ./src/backend-core.Infrastructure/backend-core.Infrastructure.csproj
// update database
dotnet ef database update init -s ./src/backend-core.Api/backend-core.Api.csproj -p ./src/backend-core.Infrastructure/backend-core.Infrastructure.csproj
```