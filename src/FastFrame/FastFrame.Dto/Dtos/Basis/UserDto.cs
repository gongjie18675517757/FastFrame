using System.Collections.Generic;

namespace FastFrame.Dto.Basis
{
    public partial class UserDto
    {
        public IEnumerable<DeptDto> Depts { get; set; }

        public IEnumerable<RoleDto> Roles { get; set; }
    }
    
}
