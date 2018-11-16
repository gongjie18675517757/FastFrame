namespace FastFrame.Database.Mapping.Basis
{
    using FastFrame.Entity.Basis;
    using Microsoft.EntityFrameworkCore;

    public partial class OrganizeMapping
    {
        public override void ModelCreating(ModelBuilder modelBuilder)
        {
            base.ModelCreating(modelBuilder);
            modelBuilder.Entity<Organize>().HasData(new Organize()
            {
                EnCode = "default",
                Id = "00F6P5G2VC2SAP1UJV7HTBYGU",
                Name = "默认组织",
                OrganizeId = "00F6P5G2VC2SAP1UJV7HTBYGU"
            });

            modelBuilder.Entity<OrganizeHost>().HasData(new OrganizeHost()
            {
                Host = "192.168.1.100:8081",
                OrganizeId = "00F6P5G2VC2SAP1UJV7HTBYGU",
                Id = "00F6P5G2VC2SAP1UJV7HTBYGB"
            },
            new OrganizeHost()
            {
                Host = "192.168.1.100:82",
                OrganizeId = "00F6P5G2VC2SAP1UJV7HTBYGU", 
                Id = "00F6P5G2VC2SAP1UJV7HTBYGc"
            });
        }
    }
}



