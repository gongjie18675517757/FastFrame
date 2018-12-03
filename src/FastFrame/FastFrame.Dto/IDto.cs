using FastFrame.Entity;

namespace FastFrame.Dto
{
    /// <summary>
    /// DTO接口
    /// </summary>
    public interface IDto
    {
        string Id { get; set; }
    }

    /// <summary>
    /// DTO接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDto<T> : IDto where T : class, IEntity
    {

    }
}
