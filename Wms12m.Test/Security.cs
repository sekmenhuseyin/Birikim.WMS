using Shouldly;
using System;
using Wms12m.Security;
using Xunit;

namespace Wms12m.Test
{
    public class Security
    {
        [Fact]
        public void EnsureEndsWith_Test()
        {
            //create guid for random test
            var guid = Guid.NewGuid().ToString();
            //encrypt string
            var encrypted = CryptographyExtension.Sifrele(guid);
            var decrypted = CryptographyExtension.Cozumle(encrypted);
            //decrypy and test
            CryptographyExtension.Cozumle(encrypted).ShouldBe(guid);
            Assert.Equal(guid, decrypted);
        }

    }
}
