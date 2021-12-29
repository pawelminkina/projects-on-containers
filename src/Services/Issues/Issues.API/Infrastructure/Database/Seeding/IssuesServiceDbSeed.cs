using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.Issues;
using Issues.Domain.StatusesFlow;
using Issues.Domain.TypesOfIssues;
using Issues.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Issues.API.Infrastructure.Database.Seeding
{
    //If I want to use csv's I should use boolen value added in appsettings
    //TODO it should be refactored to seed each entity by one
    public class IssuesServiceDbSeed
    {
        private bool _anyItemSeeded;

        public async Task SeedAsync(IssuesServiceDbContext dbContext, IWebHostEnvironment env, ILogger<IssuesServiceDbSeed> logger, IIssueSeedItemService seedService, IssueServiceSeedingOptions options, bool clearDatabaseFirst = false)
        {
            var policy = CreatePolicy(logger, nameof(IssuesServiceDbSeed));

            await policy.ExecuteAsync(async () =>
            {
                using (dbContext)
                {
                    dbContext.Database.Migrate();

                    if (clearDatabaseFirst)
                    {
                        logger.LogInformation("Removing items from Db");
                        ClearDb(dbContext);
                    }

                    if (options.CsvSeed.SeedTypeOfGroupsOfIssues && !dbContext.TypesOfGroupsOfIssues.Any())
                    {
                        LogSeeding(logger, "TypeOfGroupsOfIssues");
                        dbContext.TypesOfGroupsOfIssues.AddRange(seedService.GetTypeOfGroupOfIssuesFromSeed());
                    }

                    if (options.CsvSeed.SeedGroupsOfIssues && !dbContext.GroupsOfIssues.Any())
                    {
                        LogSeeding(logger, "GroupsOfIssues");
                        dbContext.GroupsOfIssues.AddRange(seedService.GetGroupsOfIssuesFromSeed());
                    }

                    if (options.CsvSeed.SeedIssues && !dbContext.Issues.Any())
                    {
                        LogSeeding(logger, "Issues");
                        dbContext.Issues.AddRange(seedService.GetIssuesFromSeed());
                    }

                    if (options.CsvSeed.SeedTypesOfIssues && !dbContext.TypesOfIssues.Any())
                    {
                        LogSeeding(logger, "TypesOfIssues");
                        dbContext.TypesOfIssues.AddRange(seedService.GetTypesOfIssuesFromSeed());
                    }

                    if (options.CsvSeed.SeedTypesOfIssueInTypeOfGroups && !dbContext.TypesOfIssueInTypeOfGroups.Any())
                    {
                        LogSeeding(logger, "TypesOfIssueInTypeOfGroups");
                        dbContext.TypesOfIssueInTypeOfGroups.AddRange(seedService.GetTypesOfIssueInTypeOfGroupsFromSeed());
                    }

                    if (options.CsvSeed.SeedStatuses && !dbContext.Statuses.Any())
                    {
                        LogSeeding(logger, "Statuses");
                        dbContext.Statuses.AddRange(seedService.GetStatusesFromSeed());
                    }

                    if (options.CsvSeed.SeedStatusFlows && !dbContext.StatusFlows.Any())
                    {
                        LogSeeding(logger, "StatusFlows");
                        dbContext.StatusFlows.AddRange(seedService.GetStatusFlowsFromSeed());
                    }

                    if (options.CsvSeed.SeedStatusesInFlow && !dbContext.StatusesInFlow.Any())
                    {
                        LogSeeding(logger, "StatusesInFlow");
                        dbContext.StatusesInFlow.AddRange(seedService.GetStatusesInFlowFromSeed());
                    }
                    
                    if(_anyItemSeeded)
                        await dbContext.SaveChangesAsync();
                    else
                        logger.LogInformation($"Database has items. Seeder {this.GetType().Name} was not applied.");

                }
            });
        }

        private void LogSeeding(ILogger logger, string nameOfEntity)
        {
            logger.LogInformation($"Seeding database with {this.GetType().Name} seeder on type {nameOfEntity} entity");
            _anyItemSeeded = true;
        }

        private void ClearDb(IssuesServiceDbContext dbContext)
        {
            dbContext.TypesOfGroupsOfIssues.RemoveRange(dbContext.TypesOfGroupsOfIssues);
            dbContext.GroupsOfIssues.RemoveRange(dbContext.GroupsOfIssues);
            dbContext.Issues.RemoveRange(dbContext.Issues);
            dbContext.TypesOfIssues.RemoveRange(dbContext.TypesOfIssues);
            dbContext.TypesOfIssueInTypeOfGroups.RemoveRange(dbContext.TypesOfIssueInTypeOfGroups);
            dbContext.StatusFlows.RemoveRange(dbContext.StatusFlows);
            dbContext.StatusesInFlow.RemoveRange(dbContext.StatusesInFlow);
            dbContext.Statuses.RemoveRange(dbContext.Statuses);
            dbContext.SaveChanges();

        }

        private void SeedIssuesDb(IssuesServiceDbContext dbContext)
        {
            var typeOfIssue = new TypeOfIssue("MOCKEDORGANIZATION", "SOMENAME");
            var firstStatus = new Status("someStatusName", "MOCKEDORGANIZATION");
            var secondStatus = new Status("someStatusName", "MOCKEDORGANIZATION");
            var type = new TypeOfGroupOfIssues("MOCKEDORGANIZATION", "SOMENAME");
            var flow = new StatusFlow("SOMENAME", "MOCKEDORGANIZATION");
            var status = new Status("FirstStatus", "MOCKEDORGANIZATION");
            var statusInFlow = flow.AddNewStatusToFlow(status);
            statusInFlow.AddConnectedStatus(firstStatus);
            type.SetIsDefaultToTrue();
            flow.SetIsDefaultToTrue();
            var firstGroup = type.AddNewGroupOfIssues("nameOfGroup", "SHN");
            var secondGroup = type.AddNewGroupOfIssues("nameOfGroupTwo", "SHN2");
            var firstIssue = firstGroup.AddIssue("firstIssue", "MOCKEDUSER", "someTextContent", typeOfIssue.Id, firstStatus.Id);
            var secondIssue = firstGroup.AddIssue("secondIssue", "MOCKEDUSER", "someTextContent2", typeOfIssue.Id, secondStatus.Id);
            var typeInGroup = typeOfIssue.AddNewTypeOfGroupToCollection(type.Id, flow.Id);
            dbContext.TypesOfGroupsOfIssues.Add(type);
            dbContext.TypesOfIssues.Add(typeOfIssue);
            dbContext.Statuses.AddRange(new[]{ firstStatus, secondStatus, status});
            dbContext.GroupsOfIssues.AddRange(new []{firstGroup, secondGroup});
            dbContext.Issues.AddRange(new []{firstIssue, secondIssue});
            dbContext.TypesOfIssueInTypeOfGroups.AddRange(new[] { typeInGroup });
            dbContext.StatusFlows.AddRange(new[] { flow });
            dbContext.StatusesInFlow.AddRange(new[] { statusInFlow });
        }

        private AsyncRetryPolicy CreatePolicy(ILogger<IssuesServiceDbSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }
}