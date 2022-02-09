using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Users.Tests.Unit.Constants
{
    public class RegexTests
    {
        private const string _passwordRule = Core.Constants.Regex.PasswordRule;

        [Test]
        public void PasswordRule_InputIsEmpty_ResultShouldBeFalse()
        {
            //Arrange
            var passwordToValidate = "";

            //Act
            var passwordValidationResult = Regex.IsMatch(passwordToValidate, _passwordRule);

            //Assert
            passwordValidationResult.Should().BeFalse();
;       }

        [Test]
        public void PasswordRule_InputHasLessThen8Characters_ResultShouldBeFalse()
        {
            //Arrange
            var passwordToValidate = "1234567";

            //Act
            var passwordValidationResult = Regex.IsMatch(passwordToValidate, _passwordRule);

            //Assert
            passwordValidationResult.Should().BeFalse();
        }

        [Test]
        public void PasswordRule_InputHasNoNumbers_ResultShouldBeFalse()
        {
            //Arrange
            var passwordToValidate = "ABCDEFG@";

            //Act
            var passwordValidationResult = Regex.IsMatch(passwordToValidate, _passwordRule);

            //Assert
            passwordValidationResult.Should().BeFalse();
        }

        [Test]
        public void PasswordRule_InputHasNoSpecialCharacter_ResultShouldBeFalse()
        {
            //Arrange
            var passwordToValidate = "ABCDEFG1";

            //Act
            var passwordValidationResult = Regex.IsMatch(passwordToValidate, _passwordRule);

            //Assert
            passwordValidationResult.Should().BeFalse();
        }

        [Test]
        public void PasswordRule_InputHasOnlyUpperCaseCharacters_ResultShouldBeFalse()
        {
            //Arrange
            var passwordToValidate = "ABCDEF1@";

            //Act
            var passwordValidationResult = Regex.IsMatch(passwordToValidate, _passwordRule);

            //Assert
            passwordValidationResult.Should().BeFalse();
        }

        [Test]
        public void PasswordRule_InputHasOnlyLowerCaseCharacters_ResultShouldBeFalse()
        {
            //Arrange
            var passwordToValidate = "abcdef1@";

            //Act
            var passwordValidationResult = Regex.IsMatch(passwordToValidate, _passwordRule);

            //Assert
            passwordValidationResult.Should().BeFalse();
        }

        [Test]
        public void PasswordRule_InputIsCorrect_ResultShouldBeTrue()
        {
            //Arrange
            var passwordToValidate = "ABCDEf1@";

            //Act
            var passwordValidationResult = Regex.IsMatch(passwordToValidate, _passwordRule);

            //Assert
            passwordValidationResult.Should().BeTrue();
        }
    }
}
