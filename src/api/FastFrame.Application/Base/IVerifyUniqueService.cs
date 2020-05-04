using System.Threading.Tasks;

namespace FastFrame.Application
{
    public interface IVerifyUniqueService
    {
        /// <summary>
        /// 验证唯一性
        /// </summary> 
        Task<bool> VerifyUnique(UniqueInput uniqueInput);
    }
}
