//升级EF工具
dotnet tool update -g dotnet-ef

//生成迁移
cd fastframe.webhost 
dotnet ef migrations add {{migrationName}}  -p ../fastframe.database
dotnet ef database update