namespace FastFrame.Database.Mapping.Basis
{
    using FastFrame.Entity.Basis;
    using Microsoft.EntityFrameworkCore;

    public partial class TenantMapping
    {
        public override void ModelCreating(ModelBuilder modelBuilder)
        {
            base.ModelCreating(modelBuilder);

            modelBuilder.Entity<Tenant>().HasData(new
            {
                ShortName = "default",
                Id = "00fm5yfgzpgp93ylkuxshsc73",
                FullName = "默认组织",
                CanHaveChildren = true,
                Parent_Id = "",
                isdeleted = false,
            });

            modelBuilder.Entity<TenantHost>().HasData(new TenantHost()
            {
                Host = "*",
                Tenant_Id = "00fm5yfgzpgp93ylkuxshsc73",
                Id = "00fm5yfh942593ylkueadrwah"
            });

            modelBuilder.Entity<TenantHost>().HasData(new TenantHost()
            {
                Host = "192.168.1.100:8081",
                Tenant_Id = "00fm5yfgzpgp93ylkuxshsc73",
                Id = "00fm5yfhhpy393ylku9dk1u5b"
            },
             new TenantHost()
             {
                 Host = "192.168.1.100:82",
                 Tenant_Id = "00fm5yfgzpgp93ylkuxshsc73",
                 Id = "00fm5yfhqq1h93ylkumx3gm53"
             });
        }
    }


}





