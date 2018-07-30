using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Models.ViewModels;
using Memeni.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Test.Services
{
    [TestClass]
    public class FaqTest : BaseUnitTest
    {
        [TestMethod]
        public void FaqService_Insert()
        {
            var service = this.GetService<IFaqService>();
            var result = service.Insert(
                new FaqAddRequest()
                {
                    Title = "UnitTestTitle01",
                    Description = "UnitTestDescription01",
                    DisplayOrder = 1,
                    CategoryId = 1,
                    ModifiedBy = "Sam01"
                });
            Assert.IsInstanceOfType(result, typeof(int), "Expected result to be an int");
            Assert.IsTrue(result > 0, "Expected result to be greater than 0");
        }
        [TestMethod]
        public void FaqService_GetAll()
        {
            IFaqService service = this.GetService<IFaqService>();
            List<FaqIndexModel> configList = service.Get();
            Assert.IsTrue(configList.Count > 0, "There are no records in the Config Table");
        }
        [TestMethod]
        public void FaqService_SelectById()
        {
            IFaqService service = this.GetService<IFaqService>();
            Faq result = service.GetById(15);
            Assert.IsNotNull(result, "We a null value");
        }
        [TestMethod]
        public void FaqService_Update()
        {
            IFaqService service = this.GetService<IFaqService>();
            service.Update(new FaqUpdateRequest()
            {
                Id = 15,
                Title = "UnitTestName02_changed",
                Description = "UnitTestValue02_changed",
                DisplayOrder = 1,
                CategoryId = 1,
                ModifiedBy = "UnitTestUser02_changed"
            });
        }
        [TestMethod]
        public void FaqService_Delete()
        {
            IFaqService service = this.GetService<IFaqService>();
            Faq result = service.GetById(15);
            Assert.IsNotNull(result, "We a null value");
        }
    }
}
