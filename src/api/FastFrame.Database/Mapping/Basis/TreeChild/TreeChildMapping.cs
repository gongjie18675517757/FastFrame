using FastFrame.Entity.Basis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace FastFrame.Database.Mapping.Basis
{
    public partial class TreeChildMapping
    {
        public override void ModelEntityCreating(EntityTypeBuilder<TreeChild> entityTypeBuilder)
        {
            base.ModelEntityCreating(entityTypeBuilder);

            entityTypeBuilder.HasIndex(v => v.TreeName).HasDatabaseName($"Index_TreeChild_TreeName");
        }
    }
}
