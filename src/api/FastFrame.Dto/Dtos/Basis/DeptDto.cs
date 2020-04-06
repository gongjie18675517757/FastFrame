using System.Collections.Generic;

namespace FastFrame.Dto.Basis
{
    public partial class DeptDto
    {
        public IEnumerable<UserViewModel> Members { get; set; }

        public IEnumerable<string> Managers { get; set; }
    }

}
