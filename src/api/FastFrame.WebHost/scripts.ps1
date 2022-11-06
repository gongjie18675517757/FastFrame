--mysql hangfire创建表失败时
SET GLOBAL INNODB_LARGE_PREFIX = ON;
SET GLOBAL innodb_file_format = BARRACUDA;


//升级EF工具
dotnet tool update -g dotnet-ef 
 
//生成迁移
dotnet ef migrations add 220116.1  --project fastframe.database --startup-project fastframe.webhost
dotnet ef database update  --project fastframe.database --startup-project fastframe.webhost
dotnet ef migrations script   20210924152831_210924.1 --project fastframe.database --startup-project fastframe.webhost  -o migration.sql

//发布

rename table __efmigrationshistory to __EFMigrationsHistory;




