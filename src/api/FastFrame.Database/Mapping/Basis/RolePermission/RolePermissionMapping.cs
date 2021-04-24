namespace FastFrame.Database.Mapping.Basis
{
    using FastFrame.Entity.Basis;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public partial class RolePermissionMapping  
	{
        public override void ModelEntityCreating(EntityTypeBuilder<RolePermission> entityTypeBuilder)
        {
            base.ModelEntityCreating(entityTypeBuilder);

            entityTypeBuilder.HasIndex(v => v.PermissionKey); 
        } 
    }
}
