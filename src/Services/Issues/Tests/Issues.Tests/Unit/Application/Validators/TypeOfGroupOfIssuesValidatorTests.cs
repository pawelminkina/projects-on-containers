using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Issues.Application.TypeOfGroupOfIssues.CreateType;
using Xunit;

namespace Issues.Tests.Unit.Application.Validators
{
    public class TypeOfGroupOfIssuesValidatorTests
    {
        [Fact]
        public void Create_Should_Have_Error_When_Name_Is_Null()
        {
            var validator = new CreateTypeOfGroupOfIssuesCommandValidator();
            var command = new CreateTypeOfGroupOfIssuesCommand(null, "notEmpty");
            var validateResult = validator.TestValidate(command);
            validateResult.ShouldHaveValidationErrorFor(cmd => cmd.Name);
        }

        [Fact]
        public void Create_Should_Have_Error_When_OrganizationId_Is_Empty()
        {
            var validator = new CreateTypeOfGroupOfIssuesCommandValidator();
            var command = new CreateTypeOfGroupOfIssuesCommand("notEmpty", string.Empty);
            var validateResult = validator.TestValidate(command);
            validateResult.ShouldHaveValidationErrorFor(cmd => cmd.OrganizationId);
        }

        [Fact]
        public void Create_Should_Not_Have_Error_Name_Is_Specified()
        {
            var validator = new CreateTypeOfGroupOfIssuesCommandValidator();
            var command = new CreateTypeOfGroupOfIssuesCommand("notEmpty", string.Empty);
            var validateResult = validator.TestValidate(command);
            validateResult.ShouldNotHaveValidationErrorFor(cmd => cmd.Name);
        }

        [Fact]
        public void Create_Should_Not_Have_Error_OrganizationId_Is_Specified()
        {
            var validator = new CreateTypeOfGroupOfIssuesCommandValidator();
            var command = new CreateTypeOfGroupOfIssuesCommand(string.Empty, "notEmpty");
            var validateResult = validator.TestValidate(command);
            validateResult.ShouldNotHaveValidationErrorFor(cmd => cmd.OrganizationId);
        }
    }
}
