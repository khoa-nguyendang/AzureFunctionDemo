### AzureFunctionDemo
<author>Khoa.NguyenDang@outlook.com</author>

made for testing Azure Function with .NETcore 6, isolation mode and local deploy supported.

### Prequires
1. [.NETcore 6 sdk](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) .NETcore 6 sdk
2. [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) Visual Studio 2022
3. [MS SQL server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) MS SQL server 2017 or higher
4. [Azure Function v4](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=v4%2Cwindows%2Ccsharp%2Cportal%2Cbash) Azure Function v4


### Features
1. Generic type and Standardize response 
2. .NETcore 6, Entity Framework Core 6.0
3. Azure Function v4
4.  EF Core auto migration and data seeding
5. Localize debug supports.
6. Some software design pattern inside: Bridge, Factory, Builder, Prototype
7. SOLID principles
8. Support quickly testing without open Visual studio (Require MSSQL installed, master database)

### Testing
You can start testing if your computer adapted prequires mentioned in above with CMake (you may need to install CMake)
With CMake:
```
make start
```

Without Cmake , just Power shell
```
dotnet run --property:Configuration=Release --project ./AzureFunctionDemo/AzureFunctionDemo.csproj
```
Example curl POST request
```
# Ubuntu/Linux or MacOS Terminal
curl --location --request POST 'http://localhost:7072/api/Transaction' \
--header 'Content-Type: application/json' \
--data-raw '{
	"Id": 10002,
	"Amount": 200,
	"Direction": "Debit",
	"Account":1001
}
'

# Windows Powershell or Cmd
curl --location --request POST 'http://localhost:7072/api/Transaction' `
--header 'Content-Type: application/json' `
--data-raw '{
	"Id": 10002,
	"Amount": 200,
	"Direction": "Debit",
	"Account":1001
}
'
```

### Project structure
```
│   AzureFunctionDemo.sln               # solution file, if already installed VS 2022, just click
│   README.md                           # Description about this project
│
└───AzureFunctionDemo                   # Source code of this project
    │   .gitignore                      # git Ignore
    │   AzureFunctionDemo.csproj        # c sharp project metadata, includes packages, configs, settings, references, packages of this project
    │   FunctionController.cs           # Main controller
    │   host.json                       # Host configuration
    │   local.settings.json             # application configuration
    │   Program.cs                      # Program initial and configuration class.
    │
    ├───Efcore
    │       TransactionDbContext.cs     # Entity Framework database context.
    │
    ├───Models
    │       CommonResponse.cs           # Standardize Http Response 
    │       TransactionEntity.cs        # Entity class match with table transaction in Database.
    │       TransactionInputDTO.cs      # Response Output Data
    │       TransactionOutputDTO.cs     # Request Input Payload
    │       WalletEntity.cs             # Entity class match with table Wallet in database.

```

