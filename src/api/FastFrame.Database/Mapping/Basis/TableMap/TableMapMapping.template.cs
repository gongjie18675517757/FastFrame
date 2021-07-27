using FastFrame.Entity.Basis;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace FastFrame.Database.Mapping.Basis
{

    /// <summary>
    /// 表映射 
    /// </summary>
    public partial class TableMapMapping : BaseEntityMapping<TableMap>
    {



    }

    public partial class TableMapMapping : BaseEntityMapping<TableMap>
    {

        public override void ModelEntityCreating(EntityTypeBuilder<TableMap> entityTypeBuilder)
        {
            base.ModelEntityCreating(entityTypeBuilder);
            entityTypeBuilder
                .Property("Discriminator")
                .HasMaxLength(200);
        }

    }
}
