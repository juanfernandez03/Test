using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.Strategies.Tests
{
    [TestClass()]
    public class MatrixTests
    {
        [TestMethod()]
        public void CreateMatrixTest()
        {
            Matrix matrix = new Matrix();
            var m = matrix.CreateMatrix();
            Assert.IsNotNull(m);
        }
    }
}