using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Issues.Domain.Issues;

namespace Issues.Infrastructure.Services.Files.CsvMaps.TypeConverters
{
    public class IssueContentTypeConverter : ITypeConverter
    {
        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData) => new IssueContent(text);

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData) => (value as IssueContent)?.TextContent;
    }
}
