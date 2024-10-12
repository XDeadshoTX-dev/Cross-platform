using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using Xunit;
using Xunit.Sdk;

namespace Lab2.xUnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            int n = 2;
            int expected = 2;

            // Act
            int actual = Program.GetResult(n);

            // Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Test2()
        {
            // Arrange
            int n = 9;
            int expected = 10;

            // Act
            int actual = Program.GetResult(n);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}