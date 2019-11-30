namespace FastFrame.Database.Mapping.Basis
{
    using FastFrame.Entity.Basis;
    using Microsoft.EntityFrameworkCore;
    using System;

    public partial class UserMapping
    {
        public override void ModelCreating(ModelBuilder modelBuilder)
        {
            base.ModelCreating(modelBuilder);
            var user = new
            {
                Account = "admin",
                Password = "123456",
                Email = "gongjie@qq.com",
                Id = "00F6P5G2VC2SAP1UJV7HTBYGA",
                IsAdmin = true,
                tenant_id = "00F6P5G2VC2SAP1UJV7HTBYGU",
                IsRoot = true,
                Name = "超级管理员",
                PhoneNumber = "18675517757",
                CreateTime = DateTime.Parse("2019-6-23"),
                Create_User_Id = "00F6P5G2VC2SAP1UJV7HTBYGA",
                //Dept_Id = null,
                EncryptionKey = "0ee3dcf0e832334f63876a30b45fdece",
                IsDisabled = false,
                //HandIcon_Id = null,
                isdeleted = false,
                ModifyTime = DateTime.Parse("2019-6-23"),
                Modify_User_Id = "00F6P5G2VC2SAP1UJV7HTBYGA",
            };

            modelBuilder.Entity<User>().HasData(new[] {
                user
            });
        }
    }
}



