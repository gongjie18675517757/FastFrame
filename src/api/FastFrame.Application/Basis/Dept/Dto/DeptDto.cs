﻿using FastFrame.Entity;
using System.Collections.Generic;

namespace FastFrame.Application.Basis
{
    public partial class DeptDto
    {
        public IEnumerable<IViewModel> Members { get; set; }

        public IEnumerable<string> Managers { get; set; }
    }

}
