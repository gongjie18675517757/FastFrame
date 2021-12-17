using FastFrame.Entity;

namespace FastFrame.Application
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
 

    public interface IHaveCheckModel: IDto
    {
        /// <summary>
        /// 下一步审核人
        /// </summary>
        public IEnumerable<string> CheckerIds { get; set; }

        /// <summary>
        /// 流程步骤
        /// </summary>
        public IEnumerable<Flow.FlowStepModel> StepList { get; set; }
    }
}
