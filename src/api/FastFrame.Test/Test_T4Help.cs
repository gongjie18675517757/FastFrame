using FastFrame.Entity;
using FastFrame.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FastFrame.UnitTest
{
    [TestClass]
    public class Test_T4Help
    {
        [TestMethod]
        public void Test_GetClassSummary()
        {
            var type = typeof(IEntity);
            var path = @"D:\CoreProject\FastFrame\src\FastFrame\Lib";
            var summany = T4Help.GetClassSummary(type, path);
            Assert.AreEqual("表外键信息", summany);
        }

        [TestMethod]
        public void Test_GetTypeName()
        {
            var name = T4Help.GetTypeName(typeof(Nullable<int>));
            Assert.AreEqual("int?", name);
            name = T4Help.GetTypeName(typeof(string));
            Assert.AreEqual("string", name);
        }
    }

    [TestClass]
    public class Test_Extension
    {
        private readonly static Random random;

        static Test_Extension()
        {
            random = new Random();
        }

        public Test_Extension()
        {

        }

        public Test_Extension(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public IEnumerable<Test_Extension> Children { get; set; }


        [TestMethod]
        public void Test_EachLoopChild()
        {
             
        }

        [TestMethod]
        public void Test_SelectLoopChild()
        {

        }
    }
}
