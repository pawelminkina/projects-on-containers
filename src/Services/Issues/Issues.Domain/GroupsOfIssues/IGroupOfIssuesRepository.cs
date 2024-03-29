﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issues.Domain.GroupsOfIssues
{
    public interface IGroupOfIssuesRepository
    {
        Task<GroupOfIssues> GetGroupOfIssuesByIdAsync(string id);
        Task<bool> AnyOfGroupHasGivenShortNameAsync(string shortName, string organizationId);
        Task<bool> AnyOfGroupHasGivenNameAsync(string name, string organizationId);
    }
}
