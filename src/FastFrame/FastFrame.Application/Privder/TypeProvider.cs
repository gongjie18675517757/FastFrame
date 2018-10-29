using FastFrame.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace FastFrame.Application.Privder
{
    public class TypeProvider : ITypeProvider
    {
        private readonly object syncLook = new object();
        private readonly Dictionary<string, Type> typeDic = new Dictionary<string, Type>();

        public IEnumerable<Type> GetTypes()
        {
            lock (syncLook)
            {
                if (typeDic.Count == 0)
                {
                    var types = typeof(IEntity).Assembly.GetTypes()
                            .Where(x => typeof(IEntity).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract);
                    foreach (var type in types)
                    {
                        typeDic.Add(type.Name.ToLower(), type);
                    }
                }
            }

            return typeDic.Values;

        }

        public Type GetTypeByName(string name)
        {
            typeDic.TryGetValue(name, out var type);
            return type;
        }
    }
}
