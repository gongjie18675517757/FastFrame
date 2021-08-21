//升级EF工具
dotnet tool update -g dotnet-ef 
 
//生成迁移
dotnet ef migrations add 210821.1  --project fastframe.database --startup-project fastframe.webhost
dotnet ef database update  --project fastframe.database --startup-project fastframe.webhost

//发布
dotnet publish -c release