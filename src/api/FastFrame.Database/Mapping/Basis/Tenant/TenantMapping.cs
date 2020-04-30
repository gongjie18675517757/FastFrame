namespace FastFrame.Database.Mapping.Basis
{
    using FastFrame.Entity.Basis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public partial class TenantMapping
    {
        public override void ModelEntityCreating(EntityTypeBuilder<Tenant> entityTypeBuilder)
        {
            base.ModelEntityCreating(entityTypeBuilder);

            entityTypeBuilder.HasData(new
            {
                ShortName = "default",
                Id = "00fm5yfgzpgp93ylkuxshsc73",
                FullName = "默认组织",
                isdeleted = false,
                HandIcon_Id = (string)null,
                Super_Id = (string)null,
                Tenant_Id = (string)null,
                UrlMark = "*"
            });

        } 
    } 
}





