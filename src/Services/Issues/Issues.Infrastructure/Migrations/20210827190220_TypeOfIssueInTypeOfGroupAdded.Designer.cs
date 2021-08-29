﻿// <auto-generated />
using System;
using Issues.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Issues.Infrastructure.Migrations
{
    [DbContext(typeof(IssuesServiceDbContext))]
    [Migration("20210827190220_TypeOfIssueInTypeOfGroupAdded")]
    partial class TypeOfIssueInTypeOfGroupAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Issues.Domain.GroupsOfIssues.GroupOfIssues", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1023)
                        .HasColumnType("nvarchar(1023)");

                    b.Property<string>("StatusFlowId")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<string>("TypeOfGroupId")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

                    b.HasIndex("StatusFlowId");

                    b.HasIndex("TypeOfGroupId");

                    b.ToTable("GroupsOfIssues");
                });

            modelBuilder.Entity("Issues.Domain.GroupsOfIssues.TypeOfGroupOfIssues", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1023)
                        .HasColumnType("nvarchar(1023)");

                    b.Property<string>("OrganizationId")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

                    b.ToTable("TypesOfGroupsOfIssues");
                });

            modelBuilder.Entity("Issues.Domain.Issues.Issue", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<string>("CreatingUserId")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<string>("GroupOfIssueId")
                        .HasColumnType("nvarchar(63)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1023)
                        .HasColumnType("nvarchar(1023)");

                    b.Property<string>("StatusId")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<DateTimeOffset>("TimeOfCreation")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("TypeOfIssueId")
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

                    b.HasIndex("GroupOfIssueId");

                    b.HasIndex("TypeOfIssueId");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("Issues.Domain.Issues.IssueContent", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<string>("ParentIssueId")
                        .HasColumnType("nvarchar(63)");

                    b.Property<string>("TextContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentIssueId")
                        .IsUnique()
                        .HasFilter("[ParentIssueId] IS NOT NULL");

                    b.ToTable("IssueContents");
                });

            modelBuilder.Entity("Issues.Domain.StatusesFlow.Status", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1023)
                        .HasColumnType("nvarchar(1023)");

                    b.Property<string>("OrganizationId")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("Issues.Domain.StatusesFlow.StatusFlow", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1023)
                        .HasColumnType("nvarchar(1023)");

                    b.Property<string>("OrganizationId")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

                    b.ToTable("StatusFlows");
                });

            modelBuilder.Entity("Issues.Domain.StatusesFlow.StatusInFlow", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<int>("IndexInFlow")
                        .HasColumnType("int");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<string>("ParentStatusId")
                        .HasColumnType("nvarchar(63)");

                    b.Property<string>("StatusFlowId")
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

                    b.HasIndex("ParentStatusId");

                    b.HasIndex("StatusFlowId");

                    b.ToTable("StatusesInFlow");
                });

            modelBuilder.Entity("Issues.Domain.StatusesFlow.StatusInFlowConnection", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<string>("ConnectedWithParentId")
                        .HasColumnType("nvarchar(63)");

                    b.Property<string>("ParentStatusId")
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

                    b.HasIndex("ConnectedWithParentId")
                        .IsUnique()
                        .HasFilter("[ConnectedWithParentId] IS NOT NULL");

                    b.HasIndex("ParentStatusId");

                    b.ToTable("StatusInFlowConnection");
                });

            modelBuilder.Entity("Issues.Domain.TypesOfIssues.TypeOfIssue", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1023)
                        .HasColumnType("nvarchar(1023)");

                    b.Property<string>("OrganizationId")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

                    b.ToTable("TypesOfIssues");
                });

            modelBuilder.Entity("Issues.Domain.TypesOfIssues.TypeOfIssueInTypeOfGroup", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<string>("ParentId")
                        .HasColumnType("nvarchar(63)");

                    b.Property<string>("StatusFlowId")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<string>("TypeOfGroupOfIssuesId")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("StatusFlowId");

                    b.HasIndex("TypeOfGroupOfIssuesId");

                    b.ToTable("TypesOfIssueInTypeOfGroups");
                });

            modelBuilder.Entity("Issues.Domain.GroupsOfIssues.GroupOfIssues", b =>
                {
                    b.HasOne("Issues.Domain.StatusesFlow.StatusFlow", "Flow")
                        .WithMany()
                        .HasForeignKey("StatusFlowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Issues.Domain.GroupsOfIssues.TypeOfGroupOfIssues", "TypeOfGroup")
                        .WithMany("Groups")
                        .HasForeignKey("TypeOfGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flow");

                    b.Navigation("TypeOfGroup");
                });

            modelBuilder.Entity("Issues.Domain.Issues.Issue", b =>
                {
                    b.HasOne("Issues.Domain.GroupsOfIssues.GroupOfIssues", "GroupOfIssue")
                        .WithMany("Issues")
                        .HasForeignKey("GroupOfIssueId");

                    b.HasOne("Issues.Domain.TypesOfIssues.TypeOfIssue", "TypeOfIssue")
                        .WithMany()
                        .HasForeignKey("TypeOfIssueId");

                    b.Navigation("GroupOfIssue");

                    b.Navigation("TypeOfIssue");
                });

            modelBuilder.Entity("Issues.Domain.Issues.IssueContent", b =>
                {
                    b.HasOne("Issues.Domain.Issues.Issue", "ParentIssue")
                        .WithOne("Content")
                        .HasForeignKey("Issues.Domain.Issues.IssueContent", "ParentIssueId");

                    b.Navigation("ParentIssue");
                });

            modelBuilder.Entity("Issues.Domain.StatusesFlow.StatusInFlow", b =>
                {
                    b.HasOne("Issues.Domain.StatusesFlow.Status", "ParentStatus")
                        .WithMany()
                        .HasForeignKey("ParentStatusId");

                    b.HasOne("Issues.Domain.StatusesFlow.StatusFlow", "StatusFlow")
                        .WithMany("StatusesInFlow")
                        .HasForeignKey("StatusFlowId");

                    b.Navigation("ParentStatus");

                    b.Navigation("StatusFlow");
                });

            modelBuilder.Entity("Issues.Domain.StatusesFlow.StatusInFlowConnection", b =>
                {
                    b.HasOne("Issues.Domain.StatusesFlow.Status", "ConnectedWithParent")
                        .WithOne()
                        .HasForeignKey("Issues.Domain.StatusesFlow.StatusInFlowConnection", "ConnectedWithParentId");

                    b.HasOne("Issues.Domain.StatusesFlow.StatusInFlow", "ParentStatus")
                        .WithMany("ConnectedStatuses")
                        .HasForeignKey("ParentStatusId");

                    b.Navigation("ConnectedWithParent");

                    b.Navigation("ParentStatus");
                });

            modelBuilder.Entity("Issues.Domain.TypesOfIssues.TypeOfIssueInTypeOfGroup", b =>
                {
                    b.HasOne("Issues.Domain.TypesOfIssues.TypeOfIssue", "Parent")
                        .WithMany("TypesInGroups")
                        .HasForeignKey("ParentId");

                    b.HasOne("Issues.Domain.StatusesFlow.StatusFlow", "Flow")
                        .WithMany()
                        .HasForeignKey("StatusFlowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Issues.Domain.GroupsOfIssues.TypeOfGroupOfIssues", "TypeOfGroup")
                        .WithMany()
                        .HasForeignKey("TypeOfGroupOfIssuesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flow");

                    b.Navigation("Parent");

                    b.Navigation("TypeOfGroup");
                });

            modelBuilder.Entity("Issues.Domain.GroupsOfIssues.GroupOfIssues", b =>
                {
                    b.Navigation("Issues");
                });

            modelBuilder.Entity("Issues.Domain.GroupsOfIssues.TypeOfGroupOfIssues", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("Issues.Domain.Issues.Issue", b =>
                {
                    b.Navigation("Content");
                });

            modelBuilder.Entity("Issues.Domain.StatusesFlow.StatusFlow", b =>
                {
                    b.Navigation("StatusesInFlow");
                });

            modelBuilder.Entity("Issues.Domain.StatusesFlow.StatusInFlow", b =>
                {
                    b.Navigation("ConnectedStatuses");
                });

            modelBuilder.Entity("Issues.Domain.TypesOfIssues.TypeOfIssue", b =>
                {
                    b.Navigation("TypesInGroups");
                });
#pragma warning restore 612, 618
        }
    }
}