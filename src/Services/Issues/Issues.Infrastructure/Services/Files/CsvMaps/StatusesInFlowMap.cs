﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Issues.Application.Common.Models.Files.Csv;
using Issues.Domain.StatusesFlow;

namespace Issues.Infrastructure.Services.Files.CsvMaps
{
    public class StatusesInFlowMap : ClassMap<StatusInFlowCsvDto>
    {
        public StatusesInFlowMap()
        {
            Map(s => s.Id).Name("Id");
            Map(s => s.StatusFlowId).Name("StatusFlowId");
            Map(s => s.Name).Name("Name");
            Map(s => s.IsDefault).Name("IsDefault");

        }
    }
}
