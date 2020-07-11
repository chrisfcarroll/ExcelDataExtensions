using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Extensions.Logging.ListOfString;
using Relays.Extensions;
using TestBase;
using Xunit;
using Xunit.Abstractions;

namespace Relays.Test
{
    public class FindContiguousBlocksShoulds
    {
        readonly ITestOutputHelper @out;

        public static readonly object[][] Example1ExpectedDataRanges =
            new []
            {
                new object[]{"ExampleFile1.xlsx", (1, 2, 1, 2)}
            };

        [Theory]
        [MemberData(nameof(Example1ExpectedDataRanges))]
        public void FindTopLeftBottomRightSequences(string file,(int,int,int,int) topLeftBottomRight)
        {
            var firstFile = 
                unitUnderTest.ListSourceFiles().Where(f => f.Name == file)
                    .ShouldNotBeEmpty()
                    .First();
            var outputs = unitUnderTest.FindContiguousBlocks(firstFile);

            outputs.ShouldNotBeEmpty();
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
        public FindContiguousBlocksShoulds(ITestOutputHelper @out)
        {
            this.@out = @out;
            unitUnderTest = new Relays(
                new StringListLogger(log = new List<string>()),
                new Settings
                {
                    InputPath = "TestFiles",
                    FileSearchPattern = "*.xlsx"
                });
        }

        readonly Relays unitUnderTest;
        readonly List<string> log;

    }
}