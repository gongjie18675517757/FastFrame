using System.Threading.Tasks;

namespace FastFrame.Application.OA
{
    public partial class OaLeaveService
    {
        public override Task<string> AddAsync(OaLeaveDto input)
        { 
            return base.AddAsync(input);
        }
    }
}
