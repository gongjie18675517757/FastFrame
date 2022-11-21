using FastFrame.Entity;
using System.Collections.Generic;

namespace FastFrame.Application.Basis
{
    public partial class RoleDto
    {
        public IEnumerable<IViewModel> Members { get; set; }

        public IEnumerable<string> Permissions { get; set; }
    }  
}
