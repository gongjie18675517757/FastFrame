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
                Id = "001_root",
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
}



