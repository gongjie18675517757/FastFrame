using System;
using System.Collections.Generic;
using System.Text;

namespace FastFrame.Application.Services
{
    public partial class FlowCenterService : IService
    {
        private readonly IServiceProvider serviceProvider;

        public FlowCenterService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
    }
}
