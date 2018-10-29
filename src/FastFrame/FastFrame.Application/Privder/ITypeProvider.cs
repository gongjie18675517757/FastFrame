using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;

namespace FastFrame.Application.Privder
{
    public interface ITypeProvider
    {
        Type GetTypeByName(string name);
        IEnumerable<Type> GetTypes();
    }
}