using PensoCore;
using Xunit;

namespace PensoCoreTest
{
    public class CalcTest
    {
        [Fact]
        public void TestAdd()
        {
            Assert.Equal(5, Calc.Add(2, 3));
        }
    }
}