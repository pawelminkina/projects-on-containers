﻿// <auto-generated />
using System;
using Issues.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Issues.Infrastructure.Migrations
{
    [DbContext(typeof(IssuesServiceDbContext))]
    partial class IssuesServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1023)
                        .HasColumnType("nvarchar(1023)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("TimeOfDeleteUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("_connected StatusFlowId")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<string>("_typeOfGroupId")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

                    b.HasIndex("_typeOfGroupId");

                    b.ToTable("GroupsOfIssues");
                });

            modelBuilder.Entity("Issues.Domain.GroupsOfIssues.TypeOfGroupOfIssues", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

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

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1023)
                        .HasColumnType("nvarchar(1023)");

                    b.Property<DateTimeOffset>("TimeOfCreation")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("_groupOfIssueId")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<string>("_statusInFlowId")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

                    b.HasIndex("_groupOfIssueId");

                    b.HasIndex("_statusInFlowId");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("Issues.Domain.StatusesFlow.StatusFlow", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

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

                    b.Property<string>("_connectedStatusFlowId")
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

                    b.HasIndex("_connectedStatusFlowId")
                        .IsUnique()
                        .HasFilter("[_connectedStatusFlowId] IS NOT NULL");

                    b.ToTable("StatusFlows");
                });

            modelBuilder.Entity("Issues.Domain.StatusesFlow.StatusInFlow", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<string>("_statusFlowId")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

                    b.HasIndex("_statusFlowId");

                    b.ToTable("StatusesInFlow");
                });

            modelBuilder.Entity("Issues.Domain.StatusesFlow.StatusInFlowConnection", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<int>("Direction")
                        .HasColumnType("int");

                    b.Property<string>("_connectedStatusInFlowId")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<string>("_parentStatusInFlowId")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

                    b.HasIndex("_connectedStatusInFlowId");

                    b.HasIndex("_parentStatusInFlowId");

                    b.ToTable("StatusInFlowConnections");
                });

            modelBuilder.Entity("Issues.Domain.GroupsOfIssues.GroupOfIssues", b =>
                {
                    b.HasOne("Issues.Domain.GroupsOfIssues.TypeOfGroupOfIssues", "TypeOfGroup")
                        .WithMany("Groups")
                        .HasForeignKey("_typeOfGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeOfGroup");
                });

            modelBuilder.Entity("Issues.Domain.Issues.Issue", b =>
                {
                    b.HasOne("Issues.Domain.GroupsOfIssues.GroupOfIssues", "GroupOfIssue")
                        .WithMany("Issues")
                        .HasForeignKey("_groupOfIssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Issues.Domain.StatusesFlow.StatusInFlow", "StatusInFlow")
                        .WithMany()
                        .HasForeignKey("_statusInFlowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Issues.Domain.Issues.IssueContent", "Content", b1 =>
                        {
                            b1.Property<string>("IssueId")
                                .HasColumnType("nvarchar(63)");

                            b1.Property<string>("TextContent")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("IssueId");

                            b1.ToTable("Issues");

                            b1.WithOwner()
                                .HasForeignKey("IssueId");
                        });

                    b.Navigation("Content");

                    b.Navigation("GroupOfIssue");

                    b.Navigation("StatusInFlow");
                });

            modelBuilder.Entity("Issues.Domain.StatusesFlow.StatusFlow", b =>
                {
                    b.HasOne("Issues.Domain.GroupsOfIssues.GroupOfIssues", "ConnectedGroupOfIssues")
                        .WithOne("ConnectedStatusFlow")
                        .HasForeignKey("Issues.Domain.StatusesFlow.StatusFlow", "_connectedStatusFlowId");

                    b.Navigation("ConnectedGroupOfIssues");
                });

            modelBuilder.Entity("Issues.Domain.StatusesFlow.StatusInFlow", b =>
                {
                    b.HasOne("Issues.Domain.StatusesFlow.StatusFlow", "StatusFlow")
                        .WithMany("StatusesInFlow")
                        .HasForeignKey("_statusFlowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StatusFlow");
                });

            modelBuilder.Entity("Issues.Domain.StatusesFlow.StatusInFlowConnection", b =>
                {
                    b.HasOne("Issues.Domain.StatusesFlow.StatusInFlow", "ConnectedStatusInFlow")
                        .WithMany()
                        .HasForeignKey("_connectedStatusInFlowId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Issues.Domain.StatusesFlow.StatusInFlow", "ParentStatusInFlow")
                        .WithMany("ConnectedStatuses")
                        .HasForeignKey("_parentStatusInFlowId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ConnectedStatusInFlow");

                    b.Navigation("ParentStatusInFlow");
                });

            modelBuilder.Entity("Issues.Domain.GroupsOfIssues.GroupOfIssues", b =>
                {
                    b.Navigation("ConnectedStatusFlow");

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
#pragma warning restore 612, 618
        }
    }
}
