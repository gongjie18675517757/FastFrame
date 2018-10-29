using Microsoft.AspNetCore.Mvc;

namespace FastFrame.Application.Controllers
{
    public abstract class BaseController : Controller
    {
    }


    //public abstract class BaseController<TEntity, TDto> : BaseController
    //    where TEntity : class, IEntity, new()
    //    where TDto : class, IDto<TEntity>, new()
    //{
    //    public BaseController(IService<TEntity, TDto> service)
    //    {
    //        Service = service;
    //    }

    //    public IService<TEntity, TDto> Service { get; } 
    //}
}
