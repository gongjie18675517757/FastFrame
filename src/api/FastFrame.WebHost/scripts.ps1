--mysql hangfire创建表失败时
SET GLOBAL INNODB_LARGE_PREFIX = ON;
SET GLOBAL innodb_file_format = BARRACUDA;

--mysql 删除全部的表
SELECT concat('DROP TABLE IF EXISTS ', table_name, ';')
FROM information_schema.tables
WHERE table_schema = 'fastframedb';

--mysql 更新时提示需要使用主键
SET SQL_SAFE_UPDATES = 0;

//升级EF工具
dotnet tool update -g dotnet-ef 
 
//生成迁移
dotnet ef migrations add 221216.1  --project fastframe.database --startup-project fastframe.webhost
dotnet ef database update  --project fastframe.database --startup-project fastframe.webhost
dotnet ef migrations script   20221205153110_221205.1 --project fastframe.database --startup-project fastframe.webhost  -o migration.sql

//发布

rename table __efmigrationshistory to __EFMigrationsHistory;




