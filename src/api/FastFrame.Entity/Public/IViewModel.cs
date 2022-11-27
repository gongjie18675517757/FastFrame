namespace FastFrame.Entity
{ 
    public interface IViewModel
    {
        string Id { get; }


        string Value { get; }
    }

    /// <summary>
    /// 缺省的viewmodel
    /// </summary>
    public class DefaultViewModel : IViewModel
    {
        public DefaultViewModel()
        {

        }

        public DefaultViewModel(string id, string value)
        {
            Id = id;
            Value = value;
        }

        public string Id { get; set; }

        public string Value { get; set; }
    }


    public interface IViewModelable<TEntity> : IEntity
    {
        /// <summary>
        /// 构建vm表达式
        /// </summary>
        abstract static System.Linq.Expressions.Expression<Func<TEntity, IViewModel>> BuildExpression();
    }
}
