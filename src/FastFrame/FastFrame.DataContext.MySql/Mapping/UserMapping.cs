namespace FastFrame.Database.Mapping.Basis
{
    using FastFrame.Entity.Basis;
    using Microsoft.EntityFrameworkCore;

    public partial class UserMapping
    {
        public override void ModelCreating(ModelBuilder modelBuilder)
        {
            base.ModelCreating(modelBuilder);
            var user = new User()
            {
                Account = "root",
                Password = "123456",
                Email = "gongjie@qq.com",
                Id = "00F6P5G2VC2SAP1UJV7HTBYGA",
                IsAdmin = true,
                OrganizeId = "root",
                IsRoot = true,
                Name = "超级管理员",
                PhoneNumber = "18675517757",
            };
            user.GeneratePassword();
            modelBuilder.Entity<User>().HasData(new User[] {
                user
            });
        }
    }

    public partial class OrganizeMapping
    {
        public override void ModelCreating(ModelBuilder modelBuilder)
        {
            base.ModelCreating(modelBuilder);
            modelBuilder.Entity<Organize>().HasData(new Organize()
            {
                EnCode = "default",
                Host = "192.168.1.100:8081",
                Id = "00F6P5G2VC2SAP1UJV7HTBYGU",
                Name="默认组织",
                OrganizeId= "00F6P5G2VC2SAP1UJV7HTBYGU"
            });
        }
    }
}



