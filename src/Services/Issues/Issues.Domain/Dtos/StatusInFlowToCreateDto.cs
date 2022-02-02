using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD;
using Architecture.DDD.Exceptions;
using Microsoft.VisualBasic;

namespace Issues.Domain.Dtos
{
    public class StatusInFlowToCreateDto : ValueObjectBase
    {
        public string StatusName { get; set; }
        public bool IsDefault { get; set; }
        public IEnumerable<string> ConnectedStatuses { get; set; }

        public StatusInFlowToCreateDto()
        {
            ConnectedStatuses = new List<string>();
        }

        public static bool IsCollectionOfStatusesValid(IEnumerable<StatusInFlowToCreateDto> statuses, out string reasonWhyNot)
        {
            reasonWhyNot = string.Empty;
            var allStatusNames = statuses.Select(s => s.StatusName);

            if (allStatusNames.Count() != allStatusNames.Distinct().Count())
                reasonWhyNot = ErrorMessages.GivenStatusNameListIsNotUnique(statuses);

            if (!statuses.Any(s => s.IsDefault))
                reasonWhyNot = ErrorMessages.NoneOfGivenStatusesToCreateIsDefault(statuses);

            if (statuses.Any(status => status.ConnectedStatuses.Select(connectedStatus => allStatusNames.Any(d => d == connectedStatus)).Any(anyOfGivenStatusesIsNotInStatusList => !anyOfGivenStatusesIsNotInStatusList)))
                reasonWhyNot = ErrorMessages.AnyOfGivenStatusesIsNotInStatusList(statuses);
            
            var anyOfGivenStatusesHasConnectionToItSelf = statuses.Any(s => s.ConnectedStatuses.Any(d => d == s.StatusName));
            if (anyOfGivenStatusesHasConnectionToItSelf)
                reasonWhyNot = ErrorMessages.AnyOfGivenStatusesHasConnectionToItself(statuses);

            return reasonWhyNot == string.Empty;
        }

        public override string ToString() =>
            new StringBuilder("Status in flow to create dto: ")
                .Append("Name: ").Append(StatusName)
                .Append(" ")
                .Append("IsDefault: ").Append(IsDefault)
                .Append(" ")
                .Append("Connected statuses: ").Append(string.Join(", ", ConnectedStatuses.OrderBy(s=>s))).ToString();

        private static string GetCollectionAsString(IEnumerable<StatusInFlowToCreateDto> statuses) =>
            string.Join(", ", statuses.Select(a => a.ToString()));


        public static class ErrorMessages
        {
            public static string AnyOfGivenStatusesIsNotInStatusList(IEnumerable<StatusInFlowToCreateDto> allStatuses) =>
                $"Some of given statuses to connect are not included in list of statuses to create, statuses to create: {GetCollectionAsString(allStatuses)}";

            public static string NoneOfGivenStatusesToCreateIsDefault(IEnumerable<StatusInFlowToCreateDto> allStatuses) =>
                $"None of given statuses to create is default, statuses: {GetCollectionAsString(allStatuses)}";

            public static string AnyOfGivenStatusesHasConnectionToItself(IEnumerable<StatusInFlowToCreateDto> allStatuses) =>
                $"Some of given statuses has added connections to itself, statuses: {GetCollectionAsString(allStatuses)}";

            public static string GivenStatusNameListIsNotUnique(IEnumerable<StatusInFlowToCreateDto> allStatuses) =>
                $"Given status name list is not unique. Given statuses: {GetCollectionAsString(allStatuses)}";

        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return StatusName;
            yield return ConnectedStatuses;
            yield return IsDefault;
        }
    }
}
