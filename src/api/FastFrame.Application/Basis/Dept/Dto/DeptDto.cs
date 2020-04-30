using System.Collections.Generic;

namespace FastFrame.Application.Basis
{
    public partial class DeptDto
    {
        public IEnumerable<UserViewModel> Members { get; set; }

        public IEnumerable<string> Managers { get; set; }
    }

}
