using System;
using System.Collections.Generic;
using System.Text;

namespace FastFrame.Infrastructure.Interface
{
    /// <summary>
    /// 当前用户提供者
    /// </summary>
    public interface ICurrentUserProvider
    {
        ICurrUser GetCurrUser();
    }
}
