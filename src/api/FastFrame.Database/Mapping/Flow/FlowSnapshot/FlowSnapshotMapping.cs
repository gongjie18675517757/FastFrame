namespace FastFrame.Database.Mapping.Flow
{
    using FastFrame.Entity.Flow;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public partial class FlowSnapshotMapping : BaseEntityMapping<FlowSnapshot>
	{
        public override void ModelEntityCreating(EntityTypeBuilder<FlowSnapshot> entityTypeBuilder)
        {
            base.ModelEntityCreating(entityTypeBuilder); 
        }
    }
}
