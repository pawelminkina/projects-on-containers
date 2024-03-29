﻿namespace Issues.API.Infrastructure.Database.Seeding
{
    public class IssueServiceSeedingOptions
    {
        public IssueServiceSeedingOptionsObject CsvSeed { get; set; }
    }

    public class IssueServiceSeedingOptionsObject
    {
        public bool SeedTypeOfGroupsOfIssues { get; set; }
        public bool SeedGroupsOfIssues { get; set; }
        public bool SeedIssues { get; set; }
        public bool SeedStatusFlows { get; set; }
        public bool SeedStatusesInFlow { get; set; }
        public string SeedingFolder { get; set; }
    }
}
