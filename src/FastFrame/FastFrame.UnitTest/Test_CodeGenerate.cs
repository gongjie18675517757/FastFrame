using FastFrame.Entity;
using FastFrame.Entity.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FastFrame.UnitTest
{
    [TestClass]
    public class Test_CodeGenerate
    {
        [TestMethod]
        public void TestDtoBuild()
        {
            var dtoBuild= new CodeGenerate.Build.DtoBuild(@"D:\CoreProject\FastFrame\src\FastFrame", typeof(IEntity));
            var userTargetInfo= dtoBuild.GetTargetInfo(typeof(User));

            var x = typeof(User).GetCustomAttributes(typeof(Infrastructure.Attrs.UniqueAttribute), true);

            Assert.AreEqual(2, userTargetInfo.AttrInfos.Count());
        }
    }
}
