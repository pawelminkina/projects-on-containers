using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD.Exceptions;
using FluentAssertions;
using Issues.Domain.Dtos;
using NUnit.Framework;

namespace Issues.Tests.Unit.Domain.Dtos
{
    public class StatusInFlowToCreateDtoTests
    {
        [Test]
        public void ShouldNotBeValid_NoneOfStatuses_IsDefault()
        {
            //Arrange
            var dtos = GetGivenDtos();

            //Act
            var result = StatusInFlowToCreateDto.IsCollectionOfStatusesValid(dtos, out var reasonWhyNot);

            //Assert
            result.Should().BeFalse();
            reasonWhyNot.Should().Be(StatusInFlowToCreateDto.ErrorMessages.NoneOfGivenStatusesToCreateIsDefault(dtos));

            #region Local methods

            IEnumerable<StatusInFlowToCreateDto> GetGivenDtos() => new[]
            {
                new StatusInFlowToCreateDto() {IsDefault = false, StatusName = "Some name 1"},
                new StatusInFlowToCreateDto() {IsDefault = false, StatusName = "Some name 2"},
            };

            #endregion
        }

        //ShouldNotBeValid_OneOfConnectedStatus_IsNotInStatuses
        [Test]
        public void ShouldNotBeValid_OneOfConnectedStatuses_IsNotInStatuses()
        {
            //Arrange 
            var dtos = GetGivenDtos();

            //Act 
            var result = StatusInFlowToCreateDto.IsCollectionOfStatusesValid(dtos, out var reasonWhyNot);

            //Assert
            result.Should().BeFalse();
            reasonWhyNot.Should().Be(StatusInFlowToCreateDto.ErrorMessages.AnyOfGivenStatusesIsNotInStatusList(dtos));

            #region Local methods

            IEnumerable<StatusInFlowToCreateDto> GetGivenDtos() => new[]
            {
                new StatusInFlowToCreateDto() {IsDefault = false, StatusName = "Some name 1", ConnectedStatuses = new List<string>()
                    {
                        "Some name 2", "Some status 3"
                    }},
                new StatusInFlowToCreateDto() {IsDefault = true, StatusName = "Some name 2", ConnectedStatuses = new List<string>()
                    {
                        "Some name 1"
                    }}
            };

            #endregion
        }

        [Test]
        public void ShouldNotBeValid_OneOfStatuses_HasConnectionToItself()
        {
            //Arrange 
            var dtos = GetGivenDtos();

            //Act 
            var result = StatusInFlowToCreateDto.IsCollectionOfStatusesValid(dtos, out var reasonWhyNot);

            //Assert
            result.Should().BeFalse();
            reasonWhyNot.Should().Be(StatusInFlowToCreateDto.ErrorMessages.AnyOfGivenStatusesHasConnectionToItself(dtos));

            #region Local methods

            IEnumerable<StatusInFlowToCreateDto> GetGivenDtos() => new[]
            {
                new StatusInFlowToCreateDto() {IsDefault = false, StatusName = "Some name 1", ConnectedStatuses = new List<string>()
                {
                    "Some name 2", "Some name 1"
                }},
                new StatusInFlowToCreateDto() {IsDefault = true, StatusName = "Some name 2", ConnectedStatuses = new List<string>()
                {
                    "Some name 1"
                }}
            };

            #endregion
        }


        [Test]
        public void ShouldBeValid_AllOfStatusesAreConnected_OtherRequirementsPassed()
        {
            //Arrange 
            var dtos = GetGivenDtos();

            //Act 
            var result = StatusInFlowToCreateDto.IsCollectionOfStatusesValid(dtos, out _);

            //Assert
            result.Should().BeTrue();

            #region Local methods

            IEnumerable<StatusInFlowToCreateDto> GetGivenDtos() => new[]
            {
                new StatusInFlowToCreateDto() {IsDefault = false, StatusName = "Some name 1", ConnectedStatuses = new List<string>()
                {
                    "Some name 2"
                }},
                new StatusInFlowToCreateDto() {IsDefault = true, StatusName = "Some name 2", ConnectedStatuses = new List<string>()
                {
                    "Some name 1"
                }}
            };

            #endregion
        }

        [Test]
        public void ShouldBeValid_OneOfStatusesHasNoConnections_OtherRequirementsPassed()
        {
            //Arrange 
            var dtos = GetGivenDtos();

            //Act 
            var result = StatusInFlowToCreateDto.IsCollectionOfStatusesValid(dtos, out _);

            //Assert
            result.Should().BeTrue();

            #region Local methods

            IEnumerable<StatusInFlowToCreateDto> GetGivenDtos() => new[]
            {
                new StatusInFlowToCreateDto() {IsDefault = false, StatusName = "Some name 1", ConnectedStatuses = new List<string>()
                {
                    "Some name 2"
                }},
                new StatusInFlowToCreateDto() {IsDefault = true, StatusName = "Some name 2"}
            };

            #endregion
        }

        [Test]
        public void ShouldBeValid_NoneOfStatusesHasConnections_OtherRequirementsPassed()
        {
            //Arrange 
            var dtos = GetGivenDtos();

            //Act 
            var result = StatusInFlowToCreateDto.IsCollectionOfStatusesValid(dtos, out _);

            //Assert
            result.Should().BeTrue();

            #region Local methods

            IEnumerable<StatusInFlowToCreateDto> GetGivenDtos() => new[]
            {
                new StatusInFlowToCreateDto() {IsDefault = false, StatusName = "Some name 1"},
                new StatusInFlowToCreateDto() {IsDefault = true, StatusName = "Some name 2"}
            };

            #endregion
        }

    }
}
