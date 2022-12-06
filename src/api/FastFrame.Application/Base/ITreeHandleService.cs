using FastFrame.Entity;

namespace FastFrame.Application
{
    /// <summary>
    /// 树处理
    /// </summary>
    public interface ITreeHandleService
    {
        /// <summary>
        /// 验证循环引用
        /// </summary>
        /// <param name="treeEntity"></param>
        /// <returns></returns>
        Task VerifyLoopRefByViewModelableAsync<T>(T treeEntity) where T : class, ITreeEntity;
       

        /// <summary>
        /// 验证循环引用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="treeEntity"></param>
        /// <param name="value_func"></param>
        /// <returns></returns>
        Task VerifyLoopRefAsync<T>(T treeEntity, System.Linq.Expressions.Expression<Func<T, IViewModel>> value_func) where T : class, ITreeEntity;


        /// <summary>
        /// 重新编树状码
        /// </summary>
        /// <param name="tree_type"></param>
        /// <param name="super_id"></param>
        /// <returns></returns>
        Task MakeTreeCodeAsync(string tree_type, string super_id);
    }
}
