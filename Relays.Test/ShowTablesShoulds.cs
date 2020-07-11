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
    public class ShowTablesShoulds
    {
        readonly ITestOutputHelper @out;

        [Fact]
        public void ShouldGetSomethingOutOfTheFile()
        {
            var outputs = unitUnderTest.ShowTables();
            outputs.ShouldNotBeEmpty();
            var output = outputs.FirstOrDefault().ShouldNotBeNull();
            output.Tables.ShouldNotBeEmpty();
            output.Tables[0].Columns.ShouldNotBeEmpty();
            output.Tables[0].Rows.Count.ShouldBeGreaterThan(0);
            @out.WriteLine(output.AsJsonElseEmpty());
            foreach (var outputTable in outputs.SelectMany(o=>o.Tables.Cast<DataTable>()))
            {
                @out.WriteLine(outputTable.TableName);
                @out.WriteLine(string.Join(" | ",
                    outputTable.Columns.Cast<DataColumn>()));
                foreach (DataRow row in outputTable.Rows)
                {
                    @out.WriteLine(string.Join(" | ", row.ItemArray));
                }
            }
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
        public ShowTablesShoulds(ITestOutputHelper @out)
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