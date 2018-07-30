using Memeni.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Memeni.Test.Services
{
    [TestClass]
    public class TnCTest : BaseUnitTest
    {
        [TestMethod]
        public void TncService_Insert()
        {
            ITncService service = this.GetService<ITncService>();
        }
    }
}
