using FastFrame.Infrastructure.Resource;
using Microsoft.Extensions.Options;
using System.Text;


namespace FastFrame.Infrastructure.RSAOperate
{
    public class RSAProvider : IDisposable
    {
        private readonly IDisposable disposable;
        private RSAHelper rsa;

        /// <summary>
        /// 公钥
        /// </summary>
        public string PublicKey { get; private set; }

        public RSAProvider(IOptionsMonitor<RSAConfig> optionsMonitor)
        {
            disposable = optionsMonitor.OnChange(Init);
            Init(optionsMonitor.CurrentValue);
        }

        private void Init(RSAConfig rSAConfig)
        {
            var privateKey = File.ReadAllText(IResourceProvider.GetRuntimeDirectory(rSAConfig.PrivateKeyFileName));
            PublicKey = File.ReadAllText(IResourceProvider.GetRuntimeDirectory(rSAConfig.PublicKeyFileName));

            rsa?.Dispose();
            rsa = new RSAHelper(RSAType.RSA2, Encoding.UTF8, privateKey, PublicKey);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] input)
        {
            return rsa.Encrypt(input);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Encrypt(string input)
        {
            return rsa.Encrypt(input);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] input)
        {
            return rsa.Decrypt(input);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Decrypt(string input)
        {
            return rsa.Decrypt(input);
        }

        public void Dispose()
        {
            disposable?.Dispose();
            rsa?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
