using FastFrame.Entity;
using FastFrame.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
}
