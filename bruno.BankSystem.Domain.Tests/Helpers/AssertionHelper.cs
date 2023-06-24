using bruno.BankSystem.Domain.Exceptions;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace bruno.BankSystem.Domain.Tests.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class AssertionHelper
    {
        public static void AssertDomainException(Exception exception, string message)
        {
            Assert.IsType<DomainException>(exception);
            Assert.Equal(message, ((DomainException)exception).Message);
        }

        public static void AssertNotFoundException(Exception exception, string message)
        {
            Assert.IsType<NotFoundException>(exception);
            Assert.Equal(message, ((NotFoundException)exception).Message);
        }
    }
}
