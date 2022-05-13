using FastFrame.Infrastructure.Lock;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;

namespace FastFrame.UnitTest
{
    [TestClass]
    public class Test_UserPassword
    {
        [DataRow("0756803045b3dd96ace39af25ab0d83a", "77f688a672ed34adb885444b93ddc77e", "")]
        [TestMethod()]
        public void Test_VerificationPassword()
        {
            var user = new FastFrame.Entity.Basis.User
            {

            };

            user.VerificationPassword();
        }
    }

    public class TestLockFacatoryProvider : LockFacatoryProvider
    {
        private readonly CancellationTokenSource cts;

        public TestLockFacatoryProvider(ConnectionMultiplexer redisClient) : base(redisClient)
        {
            this.cts = new CancellationTokenSource();
            Task.Run(() => this.ExecuteAsync(cts.Token));
        }

        public override void Dispose()
        {
            cts.Cancel();
            GC.SuppressFinalize(this);
        }
    }


    [TestClass]
    public class Test_LockFacatoryProvider
    {

        [TestMethod()]
        public async Task TestLock()
        {
            var configurationOptions = ConfigurationOptions.Parse("localhost,channelPrefix=f:,defaultDatabase=0");
            var connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(configurationOptions);

            using TestLockFacatoryProvider testLockFacatoryProvider = new(connectionMultiplexer);

            var lock_key = Infrastructure.IdGenerate.NetId();

            var lockHolder = await testLockFacatoryProvider.TryCreateLockAsync(lock_key, TimeSpan.FromSeconds(5), true); 
            Assert.IsTrue(lockHolder != null);


            var lockHolder2 = await testLockFacatoryProvider.TryCreateLockAsync(lock_key, TimeSpan.FromSeconds(5), true);

            Assert.IsTrue(lockHolder2 == null);

            lockHolder.LockRelease();

            var lockHolder3 = await testLockFacatoryProvider.TryCreateLockAsync(lock_key, TimeSpan.FromSeconds(5), true);

            Assert.IsTrue(lockHolder3 != null);

            lockHolder3.LockRelease();
        }
    }
}
