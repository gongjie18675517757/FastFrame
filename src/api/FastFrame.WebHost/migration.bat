//升级EF工具
dotnet tool update -g dotnet-ef

//生成迁移
cd fastframe.webhost 
dotnet ef migrations add {{migrationName}}  -p ../fastframe.database
dotnet ef database update

//生成迁移
dotnet ef migrations add 210727.1  --project fastframe.database --startup-project fastframe.webhost
dotnet ef database update  --project fastframe.database --startup-project fastframe.webhost