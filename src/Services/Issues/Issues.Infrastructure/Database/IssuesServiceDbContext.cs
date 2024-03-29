﻿using Issues.Domain.GroupsOfIssues;
using Issues.Domain.Issues;
using Issues.Domain.StatusesFlow;
using Microsoft.EntityFrameworkCore;

namespace Issues.Infrastructure.Database
{
    public class IssuesServiceDbContext : DbContext
    {
        public IssuesServiceDbContext(DbContextOptions<IssuesServiceDbContext> options):base(options)
        {

        }

        public DbSet<GroupOfIssues> GroupsOfIssues { get; set; }
        public DbSet<TypeOfGroupOfIssues> TypesOfGroupsOfIssues { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<StatusFlow> StatusFlows { get; set; }
        public DbSet<StatusInFlow> StatusesInFlow { get; set; }
        public DbSet<StatusInFlowConnection> StatusInFlowConnections { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Applies all EntityTypeConfiguration classes defined in this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IssuesServiceDbContext).Assembly);
        }
    }
}