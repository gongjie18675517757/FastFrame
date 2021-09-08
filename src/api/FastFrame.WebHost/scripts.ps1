--mysql hangfire创建表失败时
SET GLOBAL INNODB_LARGE_PREFIX = ON;
SET GLOBAL innodb_file_format = BARRACUDA;


//升级EF工具
dotnet tool update -g dotnet-ef 
 
//生成迁移
dotnet ef migrations add 210907.1  --project fastframe.database --startup-project fastframe.webhost
dotnet ef database update  --project fastframe.database --startup-project fastframe.webhost
dotnet ef migrations script   20210902151308_210902.1 --project fastframe.database --startup-project fastframe.webhost

//发布
dotnet publish -c release