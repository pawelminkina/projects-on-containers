using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Issues.Domain.StatusesFlow;

namespace Issues.Infrastructure.Services.Files.CsvMaps
{
    public class StatusesMap : ClassMap<Status>
    {
        public StatusesMap()
        {
            Map(s=>s.Id).Name("Id");
            Map(s=>s.Name).Name("Name");
            Map(s=>s.OrganizationId).Name("OrganizationId");
            Map(s=>s.IsDeleted).Name("IsDeleted");
        }
    }
}
