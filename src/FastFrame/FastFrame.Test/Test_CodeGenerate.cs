using FastFrame.Entity;
using FastFrame.Entity.Basis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FastFrame.UnitTest
{
    [TestClass]
    public class Test_CodeGenerate
    {
        [Infrastructure.Attrs.RelatedField("EnCode", "Name")]
        class TestEntity1 : BaseEntity
        {
            public string EnCode { get; set; }

            public string Name { get; set; }
        }

        class TestEntity2 : BaseEntity
        {
            [Infrastructure.Attrs.RelatedTo(typeof(TestEntity1))]
            public string TestEntity1_Id { get; set; }
        }

        [TestMethod]
        public void TestDtoBuild()
        {
            var dtoBuild = new CodeGenerate.Build.DtoBuild(@"D:\CoreProject\FastFrame\src\FastFrame", typeof(IEntity));
            var targetInfo = dtoBuild.GetTargetInfo(typeof(TestEntity2));
            Assert.AreEqual(5, targetInfo.PropInfos.Count());
            var prop = targetInfo.PropInfos.FirstOrDefault(x => x.Name == "TestEntity1");
            Assert.AreNotEqual(null, prop);           
        }
    }
}
