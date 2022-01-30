using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.Issues;
using Issues.Domain.StatusesFlow;

namespace Issues.Domain
{
    /// <summary>
    /// It should be used only for seeding purpose!!!
    /// </summary>
    public static class WholeEntityObjectCreator
    {
        public static GroupOfIssues CreateGroupOfIssues(string id, string name, string shortName, string typeOfGroupId, string connectedStatusFlowId, bool isDeleted, DateTimeOffset? timeOfDelete) => 
            GroupOfIssues.CreateWholeObject(id,name,shortName,typeOfGroupId,connectedStatusFlowId,isDeleted,timeOfDelete);

        public static TypeOfGroupOfIssues CreateTypeOfGroupOfIssues(string id, string name, string organizationId, bool isDefault) =>
            TypeOfGroupOfIssues.CreateWholeObject(id,name,organizationId,isDefault);

        public static Issue CreateIssue(string id, string name, string creatingUserId, string groupOfIssueId, DateTimeOffset timeOfCreation, string textContent, bool isDeleted, string statusInFlowId) =>
            Issue.CreateWholeObject(id,name,creatingUserId,groupOfIssueId,timeOfCreation,textContent,isDeleted,statusInFlowId);

        public static StatusFlow CreateStatusFlow(string id, string name, string organizationId, bool isDefault, bool isDeleted) =>
            StatusFlow.CreateWholeObject(id,name,organizationId,isDefault,isDeleted);

        public static StatusInFlow CreateStatusInFlow(string id, string statusFlowId, string name, bool isDefault) =>
            StatusInFlow.CreateWholeObject(id,statusFlowId,name,isDefault);

        public static StatusInFlowConnection CreateStatusInFlowConnection(string id, string parentId, string connectedId) =>
            StatusInFlowConnection.CreateWholeObject(id,parentId,connectedId);
    }
}
