 
 
  
  
  
namespace FastFrame.Repository.System
{
    using FastFrame.Database;
    using FastFrame.Entity.System;
    using FastFrame.Infrastructure.Interface;
    /// <summary>
	///表外键信息仓储 
	/// </summary>
    public partial class ForeignRepository : BaseRepository<Foreign>, IRepository<Foreign>
    {
        public ForeignRepository(DataBase context, ICurrentUserProvider currentUserProvider) 
            : base(context, currentUserProvider)
        {
        }
    }
}
namespace FastFrame.Repository.System
{
    using FastFrame.Database;
    using FastFrame.Entity.System;
    using FastFrame.Infrastructure.Interface;
    /// <summary>
	///登陆用户仓储 
	/// </summary>
    public partial class UserRepository : BaseRepository<User>, IRepository<User>
    {
        public UserRepository(DataBase context, ICurrentUserProvider currentUserProvider) 
            : base(context, currentUserProvider)
        {
        }
    }
}
 


