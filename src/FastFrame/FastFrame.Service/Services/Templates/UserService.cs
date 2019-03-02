namespace FastFrame.Service.Services.Basis
{
    using FastFrame.Entity.Basis;
    using FastFrame.Dto.Basis;
    using FastFrame.Infrastructure.Interface;
    using FastFrame.Infrastructure;
    using FastFrame.Repository;
    using System.Linq;
    /// <summary>
    ///用户 服务类 
    /// </summary>
    public partial class UserService : BaseService<User, UserDto>
    {
        private readonly IRepository<Resource> resourceRepository;
        private readonly IRepository<User> userRepository;

        public UserService(IRepository<Resource> resourceRepository, IRepository<User> userRepository, IScopeServiceLoader loader)
            : base(userRepository, loader)
        {
            this.resourceRepository = resourceRepository;
            this.userRepository = userRepository;
        }


        protected override IQueryable<UserDto> QueryMain()
        {
            var resourceQueryable = resourceRepository.Queryable;
            var userQueryable = userRepository.Queryable;
            var query = from _user in userQueryable
                        join _handIconId in resourceQueryable.MapTo<Resource, ResourceDto>() on _user.HandIcon_Id equals _handIconId.Id into t__handIconId
                        from _handIconId in t__handIconId.DefaultIfEmpty()
                        join _create_User_Id in userQueryable.MapTo<User, UserDto>() on _user.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
                        from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
                        join _modify_User_Id in userQueryable.MapTo<User, UserDto>() on _user.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
                        from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
                        select new UserDto
                        {
                            Account = _user.Account,
                            Password = _user.Password,
                            Name = _user.Name,
                            Email = _user.Email,
                            PhoneNumber = _user.PhoneNumber,
                            HandIcon_Id = _user.HandIcon_Id,
                            IsAdmin = _user.IsAdmin,
                            IsDisabled = _user.IsDisabled,
                            Id = _user.Id,
                            Create_User_Id = _user.Create_User_Id,
                            CreateTime = _user.CreateTime,
                            Modify_User_Id = _user.Modify_User_Id,
                            ModifyTime = _user.ModifyTime,
                            HandIcon = _handIconId,
                            Create_User = _create_User_Id,
                            Modify_User = _modify_User_Id,
                        };
            return query;
        }

    }
}
