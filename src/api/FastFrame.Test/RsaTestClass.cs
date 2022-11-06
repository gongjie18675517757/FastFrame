using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Lock;
using FastFrame.Infrastructure.RSAOperate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;
using System.Text;

namespace FastFrame.UnitTest
{ 

    [TestClass]
    public class RsaTestClass
    {
        private const string privateKey = "MIIEowIBAAKCAQEAj2V4bsBex1YenRdjpoC5q0kMuCG00rfE5NA9XPep3j4CL5lLIFlx0CPkGUL+uOa2hsiXVHi45c6r2FO3fSGZfjCFeC/Xa/aLNoBCTUVUo3LtLmmixYhiokJot710OwjpCFTeUtPJkg9fc7ULJxgThx2nsVg6fWJ92Cu7oLfQB66v3UOw7y0N9FPnUKQ99yE1eNHxzdWO71lAM6AerBKqL2QypTpZXKCOe3aMztEndlnC1wPOrZvfppWrfc9t2ZsggXUA8iiqMYRYt4JXIpoA5ANnDRCPAPLoAPr82r9ZyBPCyD7fET/wAJa6kYt+6ztBR3h5ysjMo3g5+0rjOydqawIDAQABAoIBAH4xs2HsI7zTWk+vRMBEKqHFwM7Owv5qQXmIlWgagMCX236eLlJCxPmCjHt6zQZV664EG8F4GGgNkIfpbOJPTnCSLPOPTsqxhuQozbCI4iqRuo+M4XVrSDo5rUR+Eyrc9Y+ieKF1xr+mjb8bRbxa4NyYyXo2BgtQQSWbnxyahxYqVmRm6/fWc+kRc9DookUcaY8jz71NKFgUbo19ylS3cSRON4xa5ezh/IbMlVkWLfjt4eAnhHgGQN0auqZe1cWHEBRbi8K7ADzune52n5WIWB4+6V7jIAkjDu/wpOraj9WO3+OndyYl5klAcL2MJLpxH4QgxsdahA0x6ZQe4GNkvPECgYEA9w/Slun2P7+FBicRv4sfBMofj2uSCxGrjjqzqHVl/imVlTmEO5Ltum8Lft/78xFgukcUey2cYWBIOl9AfT6gyBJ22HijeEc9y/XenDN/vhamnsVfpnRT9nSJ++ec/lS7qoxrpeM6/fUWX7wOKQnP33tCtWmIBW9X3dzHRpZUM/MCgYEAlJWLV2UVO4hgsLDJCQ0qIXf5rb406dj/J7wjmqX4eL+Tsl1Ar5BA9P0hhLaMV/31/UiPNED6pZBoZ0YiwpVtBkfoV289zJgSeLlvOHjZFcvDrmGemWggMVUMejrH8EYmoxctisy10wkmbXDdljG3L4vkeS8TunPex5H75Jx1JakCgYEAlBbc4O6+VvnCj6yNe+W1IxbQkhQlhxMBZRCUrc4o448jhT3joB3y90QmfNdfWxQ4iY+fnDH7wXaH9M0xh6EpmKNQr3KocakqRn5LAA/yawuCtjqSmeCyj1DNgLwVI3HAx3rB775jluP4lEvRpRGnk0p78uedy7wwy+DRZeMn97cCgYA3pLVVGk7UR6NmKB5xXFO9yu9fCI9KT/BmEggHX0Zo+d22+0NBPDWHSdCmobJ3NW1M2EKA4CC9phHjMxnLYyg8JOu26rrrBrxMJ62mKWOqzO9QO9CoRJ1hvCb7E05TBgJsKz7r7vPcv117uLvTBnVCwhHi7CVoOwJgHKSHnawlWQKBgGek+Fs3OrELGcHr9Ma7QnIwtHzSHUcLdUot0ktI9pFFtwhJVfqtuFmfvbkvDDYTIJ1GqrKkiKkZ8m2GzNm0ye5iHMOeIdwk9LcIYb8ZYDGtIXTgBpxYds06SWk/d96NYe/6Key9tU5WiXu1S/bNnRgIhLb3vC+6dEPWUdeqY5iU";

        private const string publicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAj2V4bsBex1YenRdjpoC5q0kMuCG00rfE5NA9XPep3j4CL5lLIFlx0CPkGUL+uOa2hsiXVHi45c6r2FO3fSGZfjCFeC/Xa/aLNoBCTUVUo3LtLmmixYhiokJot710OwjpCFTeUtPJkg9fc7ULJxgThx2nsVg6fWJ92Cu7oLfQB66v3UOw7y0N9FPnUKQ99yE1eNHxzdWO71lAM6AerBKqL2QypTpZXKCOe3aMztEndlnC1wPOrZvfppWrfc9t2ZsggXUA8iiqMYRYt4JXIpoA5ANnDRCPAPLoAPr82r9ZyBPCyD7fET/wAJa6kYt+6ztBR3h5ysjMo3g5+0rjOydqawIDAQAB";

        [DataRow("test1")]
        [TestMethod()]
        public void Test(string str)
        {
            var rsa = new RSAHelper(RSAType.RSA2, Encoding.UTF8, privateKey, publicKey);

            string enStr = rsa.Encrypt(str);

            string deStr = rsa.Decrypt(enStr);

            Assert.AreEqual(deStr, str);
        }
    }
}
