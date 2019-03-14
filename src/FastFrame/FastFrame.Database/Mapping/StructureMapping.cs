using FastFrame.Entity.Module;
using Microsoft.EntityFrameworkCore;

namespace FastFrame.Database.Mapping.Module
{
    public partial class StructureMapping
    {
        public override void ModelCreating(ModelBuilder modelBuilder)
        {
            base.ModelCreating(modelBuilder);
            var entity = modelBuilder.Entity<Structure>();
            entity.HasIndex(r => r.Name).HasName("Key_Structure_Name");
        }
    }
}



