using FastFrame.Entity;
using Microsoft.EntityFrameworkCore;

namespace FastFrame.Database
{
    internal interface IEntityMapping
    {
        void ModelCreating(ModelBuilder modelBuilder);
    }
    internal interface IEntityMapping<T> : IEntityMapping where T : IQuery
    {

    }
}
