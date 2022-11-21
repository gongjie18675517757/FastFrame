using FastFrame.Entity;
using System.Collections.Generic;

namespace FastFrame.Application.Basis
{
    public partial class UserDto
    {
        public IEnumerable<IViewModel> Depts { get; set; }

        public IEnumerable<IViewModel> Roles { get; set; }
    }

}
