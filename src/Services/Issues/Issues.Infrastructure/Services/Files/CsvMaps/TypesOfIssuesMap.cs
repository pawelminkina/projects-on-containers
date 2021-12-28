using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Issues.Domain.TypesOfIssues;

namespace Issues.Infrastructure.Services.Files.CsvMaps
{
    public class TypesOfIssuesMap : ClassMap<TypeOfIssue>
    {
        public TypesOfIssuesMap()
        {
            AutoMap(System.Globalization.CultureInfo.CurrentCulture);

        }
    }
}
