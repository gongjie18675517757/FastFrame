using FastFrame.Application.Events;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Basis
{
    public partial class DeptMemberService : IService,
        IEventHandle<DoMainAdding<DeptDto>>,
        IEventHandle<DoMainDeleteing<DeptDto>>,
        IEventHandle<DoMainUpdateing<DeptDto>>,
        IRequestHandle<(UserViewModel[], string[]), DeptDto>,
        IRequestHandle<IEnumerable<KeyValuePair<string, (UserViewModel[], string[])>>, DeptDto[]>,

        IEventHandle<DoMainAdding<UserDto>>,
        IEventHandle<DoMainDeleteing<UserDto>>,
        IEventHandle<DoMainUpdateing<UserDto>>,
        IRequestHandle<DeptViewModel[], UserDto>,
        IRequestHandle<IEnumerable<KeyValuePair<string, DeptViewModel[]>>, UserDto[]>
    {
        private readonly IRepository<DeptMember> deptMembers;
        private readonly IRepository<User> users;
        private readonly IRepository<Dept> depts;
        private readonly HandleOne2ManyService<DeptViewModel, DeptMember> handleUserDeptService;
        private readonly HandleOne2ManyService<UserViewModel, DeptMember> handleDeptMemberService;

        public DeptMemberService(
            IRepository<DeptMember> deptMembers,
            IRepository<User> users,
            IRepository<Dept> depts,
            HandleOne2ManyService<DeptViewModel, DeptMember> handleUserDeptService,
            HandleOne2ManyService<UserViewModel, DeptMember> handleDeptMemberService
            )
        {
            this.deptMembers = deptMembers;
            this.users = users;
            this.depts = depts;
            this.handleUserDeptService = handleUserDeptService;
            this.handleDeptMemberService = handleDeptMemberService;
        }

        public async Task HandleEventAsync(DoMainAdding<DeptDto> @event)
        {
            var input = @event.Data;
            await handleDeptMemberService.AddManyAsync(input.Members, v => new DeptMember
            {
                User_Id = v.Id,
                Dept_Id = input.Id,
                IsManager = input.Managers.Any(r => r == v.Id)
            });
        }

        public async Task HandleEventAsync(DoMainDeleteing<DeptDto> @event)
        {
            var input = @event;
            await handleDeptMemberService.DelManyAsync(v => v.Dept_Id == input.Id);
        }

        public async Task HandleEventAsync(DoMainUpdateing<DeptDto> @event)
        {
            var input = @event.Data;
            await handleDeptMemberService.UpdateManyAsync(
                    v => v.Dept_Id == input.Id,
                    input.Members,
                    (a, b) => a.User_Id == b.Id,
                    v => new DeptMember
                    {
                        User_Id = v.Id,
                        Dept_Id = input.Id,
                        IsManager = input.Managers.Any(r => r == v.Id)
                    },
                    (before, after) =>
                    {
                        before.IsManager = input.Managers.Any(r => r == before.User_Id);
                    }
                );
        }

        public async Task<(UserViewModel[], string[])> HandleRequestAsync(DeptDto request)
        {
            var userQuery = users.MapTo<User, UserViewModel>();

            return (
               await userQuery
                       .Where(v =>
                           deptMembers.Any(r => r.User_Id == v.Id && r.Dept_Id == request.Id)
                        ).ToArrayAsync(),
                await userQuery
                       .Where(v =>
                           deptMembers.Any(r => r.User_Id == v.Id && r.Dept_Id == request.Id && r.IsManager)
                        ).Select(v => v.Id).ToArrayAsync()
               );
        }

        public async Task<IEnumerable<KeyValuePair<string, (UserViewModel[], string[])>>> HandleRequestAsync(DeptDto[] request)
        {
            var keys = request.Select(v => v.Id).ToArray();
            var userQuery = users.MapTo<User, UserViewModel>();
            var query = from a in userQuery
                        join b in deptMembers on a.Id equals b.User_Id
                        where keys.Contains(b.Dept_Id)
                        select new
                        {
                            Item = a,
                            b.Dept_Id,
                            b.IsManager
                        };
            var list = await query.ToListAsync();

            return list
                        .GroupBy(v => v.Dept_Id)
                        .Select(v =>
                                new KeyValuePair<string, (UserViewModel[], string[])>(
                                    v.Key,
                                    (v.Select(v => v.Item).ToArray(),
                                        v.Where(v => v.IsManager).Select(v => v.Item.Id).ToArray())
                                ));
        }


        public async Task HandleEventAsync(DoMainAdding<UserDto> @event)
        {
            var input = @event.Data;
            await handleUserDeptService.AddManyAsync(input.Depts, v => new DeptMember
            {
                Dept_Id = v.Id,
                User_Id = input.Id
            });
        }

        public async Task HandleEventAsync(DoMainDeleteing<UserDto> @event)
        {
            var input = @event;
            await handleUserDeptService.DelManyAsync(v => v.User_Id == input.Id);
        }

        public async Task HandleEventAsync(DoMainUpdateing<UserDto> @event)
        {
            var input = @event.Data;
            await handleUserDeptService.UpdateManyAsync(
                     v => v.User_Id == input.Id,
                     input.Depts,
                     (a, b) => a.User_Id == input.Id,
                     v => new DeptMember
                     {
                         Dept_Id = v.Id,
                         User_Id = input.Id
                     }
                );
        }

        async Task<DeptViewModel[]> IRequestHandle<DeptViewModel[], UserDto>.HandleRequestAsync(UserDto request)
        {
            var deptQuery = depts.MapTo<Dept, DeptViewModel>();
            return await deptQuery
                         .Where(v =>
                             deptMembers.Any(r => r.Dept_Id == v.Id && r.User_Id == request.Id)) 
                         .ToArrayAsync();
        }

        async Task<IEnumerable<KeyValuePair<string, DeptViewModel[]>>> IRequestHandle<IEnumerable<KeyValuePair<string, DeptViewModel[]>>, UserDto[]>.HandleRequestAsync(UserDto[] request)
        {
            var deptQuery = depts.MapTo<Dept, DeptViewModel>();
            var keys = request.Select(v => v.Id).ToArray();
            var query = from a in deptQuery
                        join b in deptMembers on a.Id equals b.Dept_Id
                        where keys.Contains(b.User_Id)
                        select new
                        {
                            b.User_Id,
                            Dept = a
                        };

            var list = await query.ToListAsync();
            return list
                    .GroupBy(v => v.User_Id)
                    .Select(v => new KeyValuePair<string, DeptViewModel[]>(v.Key, v.Select(r => r.Dept).ToArray()));
        }
    }
}
