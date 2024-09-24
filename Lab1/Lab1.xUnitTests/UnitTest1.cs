namespace Lab1.xUnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            string[] inputLines = new string[] { "???", "abc" };
            int expected = 64;

            // Act
            int actual = Program.GetResult(inputLines);

            // Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Test2()
        {
            // Arrange
            string[] inputLines = new string[] { "???", "000" };
            int expected = 1;

            // Act
            int actual = Program.GetResult(inputLines);

            // Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Test3()
        {
            // Arrange
            string[] inputLines = new string[] { "abc", "999" };
            int expected = 0;

            // Act
            int actual = Program.GetResult(inputLines);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}