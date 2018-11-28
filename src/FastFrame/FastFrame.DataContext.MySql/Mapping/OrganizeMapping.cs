namespace FastFrame.Database.Mapping.Basis
{
    using FastFrame.Entity.Basis;
    using Microsoft.EntityFrameworkCore;

    public partial class OrganizeMapping
    {
        public override void ModelCreating(ModelBuilder modelBuilder)
        {
            base.ModelCreating(modelBuilder);
            modelBuilder.Entity<Tenant>().HasData(new Tenant()
            {
                EnCode = "default",
                Id = "00F6P5G2VC2SAP1UJV7HTBYGU",
                Name = "默认组织",              
            });

            modelBuilder.Entity<TenantHost>().HasData(new TenantHost()
            {
                Host = "192.168.1.100:8081",
                Tenant_Id = "00F6P5G2VC2SAP1UJV7HTBYGU",
                Id = "00F6P5G2VC2SAP1UJV7HTBYGB"
            },
            new TenantHost()
            {
                Host = "192.168.1.100:82",
                Tenant_Id = "00F6P5G2VC2SAP1UJV7HTBYGU", 
                Id = "00F6P5G2VC2SAP1UJV7HTBYGc"
            });
        }
    }
}



