using System.Collections.Generic;

namespace FastFrame.Dto.Basis
{
    public partial class DeptDto
    {
        public IEnumerable<UserDto> Members { get; set; }

        public IEnumerable<UserDto> Managers { get; set; }
    }
    
}
