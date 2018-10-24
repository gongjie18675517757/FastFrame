using FastFrame.Entity;

namespace FastFrame.Dto
{
    /// <summary>
    /// DTO接口
    /// </summary>
    public interface IDto
    { 
    }

    /// <summary>
    /// DTO接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDto<T> where T : class, IEntity
    {

    }
}
