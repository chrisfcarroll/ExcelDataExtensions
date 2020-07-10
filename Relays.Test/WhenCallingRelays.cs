using System.Collections.Generic;
using System.Linq;
using Extensions.Logging.ListOfString;
using TestBase;
using Xunit;

namespace Relays.Test
{
    public class WhenCallingRelays
    {
        [Fact]
        public void ShouldLog()
        {
            //Act
            unitUnderTest.Do();
            
            //Assert
            log
                .ShouldNotBeEmpty()
                .First().ShouldNotBeEmpty();
        }

        public WhenCallingRelays()
        {
            unitUnderTest = new Relays(
                new StringListLogger(log = new List<string>()), 
                new Settings());
        }

        readonly Relays unitUnderTest;
        readonly List<string> log;
    }
}