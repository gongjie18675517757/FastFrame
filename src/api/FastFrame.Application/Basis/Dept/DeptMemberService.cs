using FastFrame.Application.Events;
using FastFrame.Entity;
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


        IEventHandle<DoMainAdding<UserDto>>,
        IEventHandle<DoMainDeleteing<UserDto>>,
        IEventHandle<DoMainUpdateing<UserDto>>


    {
        private readonly IRepository<DeptMember> deptMembers;
        private readonly IRepository<User> users;
        private readonly IRepository<Dept> depts;
        private readonly HandleOne2ManyService<IViewModel, DeptMember> handleUserDeptService;
        private readonly HandleOne2ManyService<IViewModel, DeptMember> handleDeptMemberService;

        public DeptMemberService(
            IRepository<DeptMember> deptMembers,
            IRepository<User> users,
            IRepository<Dept> depts,
            HandleOne2ManyService<IViewModel, DeptMember> handleUserDeptService,
            HandleOne2ManyService<IViewModel, DeptMember> handleDeptMemberService
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


        /// <summary>
        /// 根据科室ids,返回人员信息
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<IReadOnlyDictionary<string, (IEnumerable<IViewModel> members, IEnumerable<string> mangers)>> GetUserViewModelsByDeptIds(params string[] keys)
        {
            var userQuery = users.Select(User.BuildExpression());

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

            return keys
                .Where(v => !v.IsNullOrWhiteSpace())
                .Distinct()
                .ToDictionary(
                    v => v,
                    v => (
                       members: list.Where(x => x.Dept_Id == v).Select(v => v.Item),
                       mangers: list.Where(x => x.Dept_Id == v && x.IsManager).Select(v => v.Item.Id)
                    )
                );
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
                     (a, b) => a.Dept_Id == b.Id,
                     v => new DeptMember
                     {
                         Dept_Id = v.Id,
                         User_Id = input.Id
                     }
                );
        }

        public async Task<IReadOnlyDictionary<string, IEnumerable<IViewModel>>> GetDeptViewModelsByUserIds(params string[] keys)
        {
            var deptQuery = depts.Select(Dept.BuildExpression());
            var query = from a in deptQuery
                        join b in deptMembers on a.Id equals b.Dept_Id
                        where keys.Contains(b.User_Id)
                        select new
                        {
                            b.User_Id,
                            Dept = a
                        };

            var list = await query.ToListAsync();

            return keys
                .Where(v => !v.IsNullOrWhiteSpace())
                .Distinct()
                .ToDictionary(
                    v => v,
                    v => list.Where(x => x.User_Id == v).Select(x => x.Dept)
                );
        } 
    }
}
