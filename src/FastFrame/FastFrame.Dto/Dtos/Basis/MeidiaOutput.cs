using FastFrame.Entity.Basis;
using System.Collections.Generic;

namespace FastFrame.Dto.Basis
{
    public partial class MeidiaOutput
    {
        public Meidia Curr { get; set; }

        public IEnumerable<MeidiaDto> Children { get; set; }
    }
}
