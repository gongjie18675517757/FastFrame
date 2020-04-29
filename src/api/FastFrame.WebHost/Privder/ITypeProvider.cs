using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;

namespace FastFrame.WebHost.Privder
{
    public interface ITypeProvider
    {
        Type GetTypeByName(string name);
        IEnumerable<Type> GetTypes();
    }
}