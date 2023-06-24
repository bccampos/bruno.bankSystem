using FluentAssertions;
using NetArchTest.Rules;
using Xunit;

namespace bruno.Architecture.Tests
{
    public class ArchitectureTests
    {
        private const string DomainNamespace = "bruno.BankSystem.Domain";
        private const string ApplicationNamespace = "bruno.BankSystem.Application";
        private const string InfrastructureNamespace = "bruno.BankSystem.Infrastructure";
        private const string ContractsNamespace = "bruno.BankSystem.Contracts";
        private const string WebApiNamespace = "bruno.BankSystem.WebApi";

        [Fact]
        public void Domain_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange 
            var assembly = typeof(Domain.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                ApplicationNamespace,
                InfrastructureNamespace,
                ContractsNamespace,
                WebApiNamespace
            };

            // Act
            var testResult = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
    }
}