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
            var list = new List<string>(100 * 10000);
            var syncLock = new object();
            Task.WaitAll(Enumerable.Range(1, 100).Select(x => Task.Run(() =>
            {
                var ids = new List<string>(10000);
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

        [TestMethod]
        public void Test_NewLongId()
        {
            //var id= Infrastructure.IdGenerate.NetId();
            var list = new List<long>(100 * 10000);
            var syncLock = new object();
            Task.WaitAll(Enumerable.Range(1, 100).Select(x => Task.Run(() =>
            {
                var ids = new List<long>(10000);
                for (int i = 0; i < 10000; i++)
                {
                    ids.Add(IdGenerate.NetLongId());
                }
                lock (syncLock)
                {
                    list.AddRange(ids);
                }
            })).ToArray());
            var dic = list.Distinct();
            Assert.AreEqual(dic.Count(), list.Count);
        }

        [TestMethod]
        public void Test_NewId2()
        {
            var id = IdGenerate.NetId();
            System.Diagnostics.Trace.WriteLine(id);
            Assert.AreEqual(id.Length, 25);
        }

        [TestMethod]
        public void Test_NewLongId2()
        {
            var id = IdGenerate.NetLongId();
            System.Diagnostics.Trace.WriteLine(id); 
        }

        //[TestMethod]
        //public async Task Test_NewId3()
        //{
        //    var list = new List<long>();
        //    var syncLock = new object();
        //    await Task.WhenAll(Enumerable.Range(1, 1).Select(x => Task.Run(() =>
        //    {
        //        var ids = new List<long>();
        //        for (int i = 0; i < 1*10000; i++)
        //        {
        //            ids.Add(Snowflake.Instance().GetId());
        //        }
        //        lock (syncLock)
        //        {
        //            list.AddRange(ids);
        //        }
        //    })).ToArray());
        //    var dic = list.Distinct();
        //    Assert.AreEqual(dic.Count(), list.Count);
        //}
    }

    [TestClass]
    public class Test_InterfaceS
    {
        [TestMethod]
        public void Test_NewId()
        {
            
        }
    }
}
