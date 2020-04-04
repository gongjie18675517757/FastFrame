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
                Password = "9557847e0632e2f167a143b7ab3d668a",  //000000
                Email = "gongjie@qq.com",
                Id = "00fm5yfgq3q893ylku6uzb57i",
                IsAdmin = true,
                tenant_id = "00fm5yfgzpgp93ylkuxshsc73",
                IsRoot = true,
                Name = "管理员",
                PhoneNumber = "18675517757",
                CreateTime = DateTime.Parse("2019-6-23"),
                Create_User_Id = "00fm5yfgq3q893ylku6uzb57i",
                //Dept_Id = null,
                EncryptionKey = "7d9d7edd6727912ce10b976818dd2856",
                IsDisabled = false,
                //HandIcon_Id = null,
                isdeleted = false,
                ModifyTime = DateTime.Parse("2019-6-23"),
                Modify_User_Id = "00fm5yfgq3q893ylku6uzb57i",
            }; 

            modelBuilder.Entity<User>().HasData(new[] {
                user
            });
        }
    }
}



