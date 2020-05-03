using System.Threading.Tasks;

namespace FastFrame.Application
{
    public interface IVerifyUniqueService
    {
        /// <summary>
        /// 验证唯一性
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> VerifyUnique(UniqueInput uniqueInput);
    }
}
