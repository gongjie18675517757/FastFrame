using FastFrame.Dto.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Repository;
using FastFrame.Service.Events;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Service.Services.Basis
{
    public partial class DeptMemberService : IService,
        IEventHandle<DoMainAdding<DeptDto>>,
        IEventHandle<DoMainDeleteing<DeptDto>>,
        IEventHandle<DoMainUpdateing<DeptDto>>,
        IEventHandle<DoMainResulting<DeptDto>>,
        IEventHandle<DoMainResultListing<DeptDto>>,

        IEventHandle<DoMainAdding<UserDto>>,
        IEventHandle<DoMainDeleteing<UserDto>>,
        IEventHandle<DoMainUpdateing<UserDto>>,
        IEventHandle<DoMainResulting<UserDto>>,
        IEventHandle<DoMainResultListing<UserDto>>
    {
        private readonly IRepository<DeptMember> deptMembers;
        private readonly DeptService deptService;
        private readonly UserService userService;
        private readonly HandleOne2ManyService<DeptDto, DeptMember> handleUserDeptService;
        private readonly HandleOne2ManyService<UserDto, DeptMember> handleDeptMemberService;

        public DeptMemberService(
            IRepository<DeptMember> deptMembers,
            DeptService deptService,
            UserService userService,
            HandleOne2ManyService<DeptDto, DeptMember> handleUserDeptService,
            HandleOne2ManyService<UserDto, DeptMember> handleDeptMemberService
            )
        {
            this.deptMembers = deptMembers;
            this.deptService = deptService;
            this.userService = userService;
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
                IsManager = input.Managers.Any(r => r.Id == v.Id)
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
                        IsManager = input.Managers.Any(r => r.Id == v.Id)
                    },
                    (before, after) =>
                    {
                        before.IsManager = input.Managers.Any(r => r.Id == before.User_Id);
                    }
                );
        }

        public async Task HandleEventAsync(DoMainResulting<DeptDto> @event)
        {
            var input = @event.Data;
            input.Members = await userService
                        .Query()
                        .Where(v =>
                            deptMembers.Any(r => r.User_Id == v.Id && r.Dept_Id == input.Id)
                         ).ToListAsync();
            input.Managers = await userService
                        .Query()
                        .Where(v =>
                            deptMembers.Any(r => r.User_Id == v.Id && r.Dept_Id == input.Id && r.IsManager)
                         ).ToListAsync();
        }

        public async Task HandleEventAsync(DoMainResultListing<DeptDto> @event)
        {
            var input = @event.Data;
            var query = from a in userService.Query()
                        join b in deptMembers on a.Id equals b.User_Id
                        select new
                        {
                            Item = a,
                            b.Dept_Id,
                            b.IsManager
                        };
            var list = await query.ToListAsync();
            foreach (var item in input)
            {
                item.Members = list.Where(v => v.Dept_Id == item.Id).Select(v => v.Item);
                item.Managers = list.Where(v => v.Dept_Id == item.Id && v.IsManager).Select(v => v.Item);
            }
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

        public async Task HandleEventAsync(DoMainResulting<UserDto> @event)
        {
            var input = @event.Data;
            input.Depts = await deptService
                        .Query()
                        .Where(v =>
                            deptMembers.Any(r => r.Dept_Id == v.Id && r.User_Id == input.Id))
                        .ToListAsync();
        }

        public async Task HandleEventAsync(DoMainResultListing<UserDto> @event)
        {
            var input = @event.Data;
            var query = from a in deptService.Query()
                        join b in deptMembers on a.Id equals b.Dept_Id
                        select new { Item = a, b.User_Id };

            var list = await query.ToListAsync();
            foreach (var item in input)
            {
                item.Depts = list.Where(v => v.User_Id == item.Id).Select(r => r.Item);
            }
        }
    }
}
