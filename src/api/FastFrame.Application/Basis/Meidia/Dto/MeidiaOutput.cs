using FastFrame.Entity.Basis;
using System.Collections.Generic;

namespace FastFrame.Application.Basis
{
    public partial class MeidiaOutput
    {
        public string Super_Id { get; set; }

        public IEnumerable<MeidiaModel> Children { get; set; }
    }
}
