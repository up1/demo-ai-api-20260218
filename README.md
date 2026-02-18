# Steps

## 1. Create webapi project
```
$mkdir api
$dotnet new webapi
$dotnet run
```

## 2. Create test project with xunit
```
$mkdir tests
$dotnet new xunit
$dotnet test

// Add project to solution
$cd ..
$dotnet sln add api/api.csproj
```
