using FastFrame.Entity;
using Microsoft.EntityFrameworkCore;

namespace FastFrame.Database
{
    internal interface IQueryMapping
    {
        void ModelCreating(ModelBuilder modelBuilder);
    }

    internal interface IQueryMapping<T> : IEntityMapping where T : IQuery
    {

    }
}
