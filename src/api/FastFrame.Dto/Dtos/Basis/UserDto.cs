using System.Collections.Generic;

namespace FastFrame.Dto.Basis
{
    public partial class UserDto
    {
        public IEnumerable<DeptViewModel> Depts { get; set; }

        public IEnumerable<RoleViewModel> Roles { get; set; }
    }

}
