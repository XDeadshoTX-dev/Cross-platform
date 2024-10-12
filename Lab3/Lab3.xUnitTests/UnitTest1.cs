using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using Xunit;
using Xunit.Sdk;

namespace Lab3.xUnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            int N = 5;
            int x1 = 1;
            int y1 = 1;
            int x2 = 3;
            int y2 = 1;

            int expected = 2;
            // Act
            int actual = Program.GetResult(N, x1, y1, x2, y2);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}