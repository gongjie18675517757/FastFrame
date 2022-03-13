using FastFrame.Entity.Flow;

namespace FastFrame.Application.Flow
{
    public class FlowStepModel: FlowStep
    {
        /// <summary>
        /// 审核人
        /// </summary>
        public IEnumerable<KeyValuePair<string,string>> Checker { get; set; }
    }


}
