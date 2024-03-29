﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Architecture.DDD;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.Issues;
using Issues.Domain.StatusesFlow;
using Issues.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Issues.API.Infrastructure.Database.Seeding
{
    //If I want to use csv's I should use boolen value added in appsettings
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

                    if (options.CsvSeed.SeedStatusFlows && !dbContext.StatusFlows.Any())
                    {
                        LogSeeding(logger, "StatusFlows");
                        dbContext.StatusFlows.AddRange(seedService.GetStatusFlowsFromSeed());
                    }

                    if (options.CsvSeed.SeedStatusesInFlow && !dbContext.StatusesInFlow.Any())
                    {
                        LogSeeding(logger, "StatusesInFlow");
                        dbContext.StatusesInFlow.AddRange(seedService.GetStatusesInFlowFromSeed());
                        LogSeeding(logger, "StatusesInFlowConnection");
                        dbContext.StatusInFlowConnections.AddRange(seedService.GetStatusesInFlowConnectionFromSeed());
                    }

                    if (_anyItemSeeded)
                    {
                        ClearDomainEvents(dbContext);
                        await dbContext.SaveChangesAsync();
                    }
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
            dbContext.StatusFlows.RemoveRange(dbContext.StatusFlows);
            dbContext.StatusesInFlow.RemoveRange(dbContext.StatusesInFlow);
            dbContext.StatusInFlowConnections.RemoveRange(dbContext.StatusInFlowConnections);

            dbContext.SaveChanges();
        }

        private void ClearDomainEvents(IssuesServiceDbContext dbContext)
        {
            //Because seeding is used, then domain events are not needed for the state of creating db.
            dbContext.ChangeTracker
                .Entries<EntityBase>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList()
                .ForEach(s=>s.Entity.ClearDomainEvents());
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