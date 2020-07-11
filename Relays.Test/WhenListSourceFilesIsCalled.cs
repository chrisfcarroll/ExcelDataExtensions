using System.Collections.Generic;
using System.IO;
using Extensions.Logging.ListOfString;
using TestBase;
using Xunit;

namespace Relays.Test
{
    public class WhenListSourceFilesIsCalled
    {
        Relays unitUnderTest;
        List<string> logLines;
        const string PathToExampleDocs = "TestFiles";
        const string ExampleFile1 = "ExampleFile1.xlsx";
        const string ExampleFile2 = "ExampleFile2.xlsx";
        const string ExampleFile3 = "ExampleFile3.csv";

        [Fact]
        public void GivenXlsxFilesInTheInputDirectoryFindsThem()
        {
            var settings = new Settings
            {
                InputPath = "TestFiles",
                FileSearchPattern = "*.xlsx"
            };
            unitUnderTest =
                new Relays(
                    new StringListLogger(logLines = new List<string>()),
                    settings);

            var inputDir = new DirectoryInfo(PathToExampleDocs);
            var foundFiles = inputDir.GetFiles("*.xlsx");
            unitUnderTest
                .ListSourceFiles()
                .ShouldEqualByValue(foundFiles);
        }

        static void EnsureTestDependencies()
        {
            foreach (var file in new[] {ExampleFile1, ExampleFile2, ExampleFile3})
            {
                var expectedFile = Path.Combine(PathToExampleDocs, file);
                File.Exists(expectedFile)
                    .ShouldBeTrue(
                        $"Expected to find TestDependency \n\n\"{ExampleFile1}\"\n\n at " +
                        new FileInfo(expectedFile).FullName +
                        " but didn't. \n" +
                        "Include it in the test project and mark it as as BuildAction=Content, " +
                        "CopyToOutputDirectory=Copy if Newer.\n" +
                        "Then the project build will copy your directory structure of test " +
                        "documents into the right place for tests to find them at the same" +
                        "relative path."
                    );
            }
        }
        
        public WhenListSourceFilesIsCalled() => EnsureTestDependencies();
    }
}