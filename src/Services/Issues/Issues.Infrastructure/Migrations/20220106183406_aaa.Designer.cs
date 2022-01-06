﻿// <auto-generated />
using System;
using Issues.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Issues.Infrastructure.Migrations
{
    [DbContext(typeof(IssuesServiceDbContext))]
    [Migration("20220106183406_aaa")]
    partial class aaa
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("TypeOfGroupId")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

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

                    b.Property<bool>("IsDefault")
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
                        .IsRequired()
                        .HasColumnType("nvarchar(63)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
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

            modelBuilder.Entity("Issues.Domain.StatusesFlow.Status", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<bool>("IsDeleted")
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

                    b.Property<bool>("IsDefault")
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

                    b.Property<string>("ConnectedStatusId")
                        .HasColumnType("nvarchar(63)");

                    b.Property<int>("Direction")
                        .HasColumnType("int");

                    b.Property<string>("ParentStatusId")
                        .HasColumnType("nvarchar(63)");

                    b.Property<string>("ParentStatusInFlowId")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

                    b.HasIndex("ConnectedStatusId")
                        .IsUnique()
                        .HasFilter("[ConnectedStatusId] IS NOT NULL");

                    b.HasIndex("ParentStatusId")
                        .IsUnique()
                        .HasFilter("[ParentStatusId] IS NOT NULL");

                    b.HasIndex("ParentStatusInFlowId");

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
                        .HasColumnType("nvarchar(63)");

                    b.Property<string>("TypeOfGroupOfIssuesId")
                        .IsRequired()
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("StatusFlowId");

                    b.HasIndex("TypeOfGroupOfIssuesId");

                    b.ToTable("TypesOfIssueInTypeOfGroups");
                });

            modelBuilder.Entity("Issues.Domain.GroupsOfIssues.GroupOfIssues", b =>
                {
                    b.HasOne("Issues.Domain.GroupsOfIssues.TypeOfGroupOfIssues", "TypeOfGroup")
                        .WithMany("Groups")
                        .HasForeignKey("TypeOfGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeOfGroup");
                });

            modelBuilder.Entity("Issues.Domain.Issues.Issue", b =>
                {
                    b.HasOne("Issues.Domain.GroupsOfIssues.GroupOfIssues", "GroupOfIssue")
                        .WithMany("Issues")
                        .HasForeignKey("GroupOfIssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Issues.Domain.TypesOfIssues.TypeOfIssue", "TypeOfIssue")
                        .WithMany()
                        .HasForeignKey("TypeOfIssueId");

                    b.OwnsOne("Issues.Domain.Issues.IssueContent", "Content", b1 =>
                        {
                            b1.Property<string>("IssueId")
                                .HasColumnType("nvarchar(63)");

                            b1.Property<bool>("IsArchived")
                                .HasColumnType("bit");

                            b1.Property<bool>("IsDeleted")
                                .HasColumnType("bit");

                            b1.Property<string>("TextContent")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("IssueId");

                            b1.ToTable("Issues");

                            b1.WithOwner()
                                .HasForeignKey("IssueId");
                        });

                    b.Navigation("Content");

                    b.Navigation("GroupOfIssue");

                    b.Navigation("TypeOfIssue");
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
                    b.HasOne("Issues.Domain.StatusesFlow.Status", "ConnectedStatus")
                        .WithOne()
                        .HasForeignKey("Issues.Domain.StatusesFlow.StatusInFlowConnection", "ConnectedStatusId");

                    b.HasOne("Issues.Domain.StatusesFlow.Status", "ParentStatus")
                        .WithOne()
                        .HasForeignKey("Issues.Domain.StatusesFlow.StatusInFlowConnection", "ParentStatusId");

                    b.HasOne("Issues.Domain.StatusesFlow.StatusInFlow", "ParentStatusInFlow")
                        .WithMany("ConnectedStatuses")
                        .HasForeignKey("ParentStatusInFlowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ConnectedStatus");

                    b.Navigation("ParentStatus");

                    b.Navigation("ParentStatusInFlow");
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
