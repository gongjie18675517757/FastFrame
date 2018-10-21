using Microsoft.EntityFrameworkCore;

namespace FastFrame.Database
{
    internal interface IMapping
    {
        void ModelCreating(ModelBuilder modelBuilder);
    }
    internal interface IMapping<T> : IMapping
    {
    }
}
