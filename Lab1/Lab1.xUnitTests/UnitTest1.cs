namespace Lab1.xUnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            string p1 = "???";
            string p2 = "abc";
            int expected = 64;

            // Act
            int actual = Program.GetResult(p1, p2);

            // Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Test2()
        {
            // Arrange
            string p1 = "???";
            string p2 = "000";
            int expected = 1;

            // Act
            int actual = Program.GetResult(p1, p2);

            // Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Test3()
        {
            // Arrange
            string p1 = "abc";
            string p2 = "999";
            int expected = 0;

            // Act
            int actual = Program.GetResult(p1, p2);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}