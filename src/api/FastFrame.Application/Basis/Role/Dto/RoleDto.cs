using System.Collections.Generic;

namespace FastFrame.Application.Basis
{
    public partial class RoleDto
    {
        public IEnumerable<UserViewModel> Members { get; set; }

        public IEnumerable<string> Permissions { get; set; }
    }  
}
