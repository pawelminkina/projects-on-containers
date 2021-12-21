using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Issues.Domain.Issues;

namespace Issues.Infrastructure.Services.Files.CsvMaps
{
    public class IssuesMap : ClassMap<Issue>
    {
    }
}
