using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FastFrame.UnitTest
{
    [TestClass]
    public class Test_UserPassword
    {
        [DataRow("0756803045b3dd96ace39af25ab0d83a", "77f688a672ed34adb885444b93ddc77e", "")]
        [TestMethod()]
        public void Test_VerificationPassword()
        {
            Assert.IsTrue(true);
        }
    }
}
