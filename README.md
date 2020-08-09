
Key Dependencies(Managed through nuget):
1. Microsoft .NET Core 3.1.0
2. Microsoft Asp.Net Core 3.1.3
3. EntityFrameworkCore 3.1.6
4. EntityFramework 6.4.4

Complete set of dependencies can be found here under Package reference:
https://github.com/balajiren/XeroDocumentIntaker/blob/initial/XeroDocumentIntaker/XeroDocumentIntaker.csproj

Steps to build and run the code:
1. First build the code using the command "dotnet build"
2. Then use command "dotnet run --project "XeroDocumentIntaker.csproj" --urls "https://localhost:5001" to run the project
3. If you wish to use a different port, it can be modified in the config file under https://github.com/balajiren/XeroDocumentIntaker/blob/initial/XeroDocumentIntaker/ClientApp/src/config.js 

Steps to run Unit tests:
1. First build the code using the command "dotnet build"
2. Then use command "dotnet run --project XeroDocumentIntaker.tests" to run the project
