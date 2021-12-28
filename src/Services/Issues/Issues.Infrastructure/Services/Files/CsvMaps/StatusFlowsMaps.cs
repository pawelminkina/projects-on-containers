using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Issues.Domain.StatusesFlow;

namespace Issues.Infrastructure.Services.Files.CsvMaps
{
    public class StatusFlowsMaps : ClassMap<StatusFlow>
    {
        public StatusFlowsMaps()
        {
            AutoMap(System.Globalization.CultureInfo.CurrentCulture);

        }
    }
}
