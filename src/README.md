## Settings up user-secrets data :
```sh
cd ./src/backend-core.Api

dotnet user-secrets set "JwtSettings:Secret" "[128bitKey]"

dotnet user-secrets set "ConnectionString:mysql" "server=localhost;user=<user_name>;password=<password>;database=<database_name>"

dotnet user-secrets set "ConnectionString:postgresql" "Host=<localhost>;Database=<database_name>;Username=<user_name>;Password=<password>"

dotnet user-secrets set "EmailSettings:Username" ""
dotnet user-secrets set "EmailSettings:Password" ""
dotnet user-secrets set "EmailSettings:From" ""

dotnet user-secrets set "FacebookSettings:AppId" ""
dotnet user-secrets set "FacebookSettings:AppSecret" ""

dotnet user-secrets set "GoogleSettings:ClientId" ""
dotnet user-secrets set "GoogleSettings:ClientSecret" ""

```

## Create migration 

```sh
# create migration

# For MySQL
dotnet ef migrations add <migration_name> -s ./src/backend-core.Api/backend-core.Api.csproj -p ./src/backend-core.Infrastructure/backend-core.Infrastructure.csproj -o Migrations/MySQL

# For PostgreSQL:
dotnet ef migrations add <migration_name> -s ./src/backend-core.Api/backend-core.Api.csproj -p ./src/backend-core.Infrastructure/backend-core.Infrastructure.csproj -o Migrations/PostgreSQL


# update database
dotnet ef database update -s ./src/backend-core.Api/backend-core.Api.csproj -p ./src/backend-core.Infrastructure/backend-core.Infrastructure.csproj
```