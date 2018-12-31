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
        /*字段*/
        private readonly IRepository<Dept> deptRepository;
        private readonly IRepository<User> userRepository;

        /*构造函数*/
        public UserService(IRepository<Dept> deptRepository, IRepository<User> userRepository, IScopeServiceLoader loader)
            : base(userRepository, loader)
        {
            this.deptRepository = deptRepository;
            this.userRepository = userRepository;
        }

        /*属性*/

        /*方法*/
        protected override IQueryable<UserDto> QueryMain()
        {
            var deptQueryable = deptRepository.Queryable;
            var userQueryable = userRepository.Queryable;
            var query = from _user in userQueryable
                        join _dept_Id in deptQueryable.MapTo<Dept, DeptDto>() on _user.Dept_Id equals _dept_Id.Id into t__dept_Id
                        from _dept_Id in t__dept_Id.DefaultIfEmpty()
                        join _create_User_Id in userQueryable.MapTo<User, UserDto>() on _user.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
                        from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
                        join _modify_User_Id in userQueryable.MapTo<User, UserDto>() on _user.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
                        from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
                        select new UserDto
                        {
                            Account = _user.Account,
                            Password = _user.Password,
                            Name = _user.Name,
                            Dept_Id = _user.Dept_Id,
                            Email = _user.Email,
                            PhoneNumber = _user.PhoneNumber,
                            HandIconId = _user.HandIconId,
                            IsAdmin = _user.IsAdmin,
                            IsDisabled = _user.IsDisabled,
                            Id = _user.Id,
                            Create_User_Id = _user.Create_User_Id,
                            CreateTime = _user.CreateTime,
                            Modify_User_Id = _user.Modify_User_Id,
                            ModifyTime = _user.ModifyTime,
                            Dept = _dept_Id,
                            Create_User = _create_User_Id,
                            Modify_User = _modify_User_Id,
                        };
            return query;
        }

    }
}
