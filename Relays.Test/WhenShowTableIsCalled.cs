using System.Collections.Generic;
using System.Linq;
using Extensions.Logging.ListOfString;
using TestBase;
using Xunit;

namespace Relays.Test
{
    public class WhenShowTableIsCalled
    {
        [Fact]
        public void ShouldGetSomethingOutOfTheFile()
        {
            var outputs = unitUnderTest.ShowTables();
            outputs.ShouldNotBeEmpty();
            var output = outputs.FirstOrDefault().ShouldNotBeNull();
            output.Tables.ShouldNotBeEmpty();
            output.Tables[0].Columns.ShouldNotBeEmpty();
            output.Tables[0].Rows.Count.ShouldBeGreaterThan(0);

        }
        
        [Fact]
        public void ShouldLog()
        {
            //Act
            unitUnderTest.Do();

            //Assert
            log
                .ShouldNotBeEmpty()
                .First()
                .ShouldNotBeEmpty();
        }
        public WhenShowTableIsCalled()
        {
            unitUnderTest = new Relays(
                new StringListLogger(log = new List<string>()),
                new Settings());
        }

        readonly Relays unitUnderTest;
        readonly List<string> log;

    }
}