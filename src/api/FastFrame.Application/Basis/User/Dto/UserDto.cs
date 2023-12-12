using FastFrame.Entity;
using System.Collections.Generic;

namespace FastFrame.Application.Basis
{
    public partial class UserDto
    {
        /// <summary>
        /// 所属科室
        /// </summary>
        public IEnumerable<IViewModel> Depts { get; set; }

        /// <summary>
        /// 所属角色
        /// </summary>
        public IEnumerable<IViewModel> Roles { get; set; }
    }

}
