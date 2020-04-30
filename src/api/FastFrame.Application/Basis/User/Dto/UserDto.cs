using System.Collections.Generic;

namespace FastFrame.Application.Basis
{
    public partial class UserDto
    {
        public IEnumerable<DeptViewModel> Depts { get; set; }

        public IEnumerable<RoleViewModel> Roles { get; set; }
    }

}
