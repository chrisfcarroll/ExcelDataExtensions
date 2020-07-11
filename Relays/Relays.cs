using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using ExcelDataReader;
using Microsoft.Extensions.Logging;

namespace Relays
{
    /// <summary>A component which … </summary>
    public class Relays
    {
        public FileInfo[] ListSourceFiles()
        {
            return new DirectoryInfo(settings.InputPath)
                .GetFiles(settings.FileSearchPattern);
        }

        public IEnumerable<DataSet> ShowTables()
        {
            var todo = ListSourceFiles();
            foreach (var file in todo)
            using (var stream = file.OpenRead())
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                yield return reader.AsDataSet();
            }
        }

        public void Do()
        {
            log.LogDebug("Called on {@OS}", Environment.OSVersion);
        }

        public Relays(ILogger log, Settings settings)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            this.log = log;
            this.settings = settings;
            log.LogDebug("Created with Settings={@Settings}", settings);
        }

        readonly ILogger log;
        readonly Settings settings;
    }
}