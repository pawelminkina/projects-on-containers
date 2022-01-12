using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Issues.Domain.StatusesFlow;

namespace Issues.Infrastructure.Services.Files.CsvMaps.TypeConverters
{
    public class StatusInFlowDirectionTypeConverter : ITypeConverter
    {
        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData) =>
                text == StatusInFlowDirection.In.ToString() ? StatusInFlowDirection.In : StatusInFlowDirection.Out;

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData) =>
                value is StatusInFlowDirection direction ? direction == StatusInFlowDirection.In ? StatusInFlowDirection.In.ToString() : StatusInFlowDirection.Out.ToString() : throw new InvalidOperationException("Value to convert is not status in flow direction");

    }
}
