using Memeni.Models.Domain;
using Memeni.Models.Requests;
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
    public class FaqCategoryTest : BaseUnitTest
    {
        [TestMethod]
        public void FaqCategoryService_Insert()
        {
            var service = this.GetService<IFaqCategoryService>();
            var result = service.Insert(
                new FaqCategoryAddRequest()
                {
                    DisplayName = "UnitTestDisplayName01",
                    Description = "UnitTestDescription01",
                    ModifiedBy = "Sam01"
                });
            Assert.IsInstanceOfType(result, typeof(int), "Expected result to be an int");
            Assert.IsTrue(result > 0, "Expected result to be greater than 0");
        }
        [TestMethod]
        public void FaqCategoryService_GetAll()
        {
            IFaqCategoryService service = this.GetService<IFaqCategoryService>();
            List<FaqCategory> configList = service.Get();
            Assert.IsTrue(configList.Count > 0, "There are no records in the Config Table");
        }
        [TestMethod]
        public void FaqCategoryService_SelectById()
        {
            IFaqCategoryService service = this.GetService<IFaqCategoryService>();
            FaqCategory result = service.GetById(7);
            Assert.IsNotNull(result, "We a null value");
        }
        [TestMethod]
        public void FaqCategoryService_Update()
        {
            IFaqCategoryService service = this.GetService<IFaqCategoryService>();
            service.Update(new FaqCategoryUpdateRequest()
            {
                Id = 7,
                DisplayName = "UnitTestName02_changed",
                Description = "UnitTestValue02_changed",
                ModifiedBy = "UnitTestUser02_changed"
            });
        }
        [TestMethod]
        public void FaqCategoryService_Delete()
        {
            IFaqCategoryService service = this.GetService<IFaqCategoryService>();
            FaqCategory result = service.GetById(7);
            Assert.IsNotNull(result, "We a null value");
        }
    }
}
