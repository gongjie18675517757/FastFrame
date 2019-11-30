using System.Collections.Generic;

namespace FastFrame.Dto.Basis
{
    public partial class RoleDto
    {
        public IEnumerable<UserDto> Members { get; set; }

        public IEnumerable<PermissionDto> Permissions { get; set; }
    } 
    
}
