START TRANSACTION;

ALTER TABLE `basis_role` MODIFY COLUMN `treecode` varchar(200) CHARACTER SET utf8mb4 NULL COMMENT '树状码';

ALTER TABLE `basis_role` ADD `encode` varchar(50) CHARACTER SET utf8mb4 NULL COMMENT '编码';

ALTER TABLE `basis_enumitem` MODIFY COLUMN `treecode` varchar(200) CHARACTER SET utf8mb4 NULL COMMENT '树状码';

update basis_enumitem set intkey=0 where intkey is null

ALTER TABLE `basis_enumitem` MODIFY COLUMN `intkey` int NOT NULL DEFAULT 0 COMMENT '字典键';

ALTER TABLE `basis_dept` MODIFY COLUMN `treecode` varchar(200) CHARACTER SET utf8mb4 NULL COMMENT '树状码';

ALTER TABLE `basis_dept` ADD `encode` varchar(50) CHARACTER SET utf8mb4 NOT NULL DEFAULT '' COMMENT '部门代码';

UPDATE `basis_user` SET `create_user_id` = '00fm5yfgq3q893ylku6uzb57i', `modify_user_id` = '00fm5yfgq3q893ylku6uzb57i'
WHERE `id` = '00fm5yfgq3q893ylku6uzb57i';
SELECT ROW_COUNT();


INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221205153110_221205.1', '7.0.0');

COMMIT;

