using FastFrame.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.UnitTest
{
    [TestClass]
    public class Test_IdGenerate
    {
        [TestMethod]
        public void Test_NewId()
        {
            //var id= Infrastructure.IdGenerate.NetId();
            var list = new List<string>();
            var syncLock = new object();
            Task.WaitAll(Enumerable.Range(1, 100).Select(x => Task.Run(() =>
            {
                var ids = new List<string>();
                for (int i = 0; i < 10000; i++)
                {
                    ids.Add(IdGenerate.NetId());
                }
                lock (syncLock)
                {
                    list.AddRange(ids);
                }
            })).ToArray());
            var dic = list.Distinct();
            Assert.AreEqual(dic.Count(), list.Count);
        }

        public void Test_NewId2()
        {
            var id = IdGenerate.NetId();
            Assert.AreEqual(id.Length, 25);
        }
    }
}
