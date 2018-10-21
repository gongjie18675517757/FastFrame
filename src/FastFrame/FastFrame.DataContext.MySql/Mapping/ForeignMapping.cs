namespace FastFrame.Database.Mapping.System
{
    using FastFrame.Entity.System;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///表外键信息实体映射 
    /// </summary>
    public partial class ForeignMapping
    {
        public override void ModelCreating(ModelBuilder modelBuilder)
        {
            base.ModelCreating(modelBuilder);
            modelBuilder.Entity<Foreign>().HasIndex(x => x.EntityId).HasName("Index_EntityId");
        }
    }
}



