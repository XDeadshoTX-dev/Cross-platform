using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Lab3.xUnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange

            // Act
            int actual = Program.GetResult();

            // Assert
            //Assert.Equal(expected, actual);
        }
    }
}