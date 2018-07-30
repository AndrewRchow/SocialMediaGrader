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
    public class ConfigTest : BaseUnitTest
    {
        [TestMethod]
        public void ConfigService_Insert() {
            var service = this.GetService<IConfigService>();
            var result = service.Insert(
                new ConfigAddRequest()
                {
                    ConfigName = "UnitTestName01",
                    ConfigValue = "UnitTestValue01",
                    ConfigCategoryId = 1,
                    ConfigTypeId = 1,
                    Description = "Unit Test Description 01",
                    ModifiedBy = "UnitTestUser01"
                });
            Assert.IsInstanceOfType(result, typeof(int), "Expected result to be an int");
            Assert.IsTrue(result > 0, "Expected result to be greater than 0");
        }

        [TestMethod]
        public void ConfigService_SelectAll()
        {
            IConfigService service = this.GetService<IConfigService>();
            List<ConfigIndexModel> configList = service.Get();
            Assert.IsTrue(configList.Count > 0, "There are no records in the Config Table");
        }

        [TestMethod]
        public void ConfigService_SelectById()
        {
            IConfigService service = this.GetService<IConfigService>();
            Config result = service.GetById(59);
            Assert.IsNotNull(result, "We a null value");
        }

        [TestMethod]
        public void ConfigService_Update()
        {
            IConfigService service = this.GetService<IConfigService>();
            service.Update(new ConfigUpdateRequest() {
                Id=61,
                ConfigName = "UnitTestName02_changed",
                ConfigValue="UnitTestValue02_changed",
                ConfigCategoryId = 1,
                ConfigTypeId = 1,
                Description = "Unit Test Description 02 Changed",
                ModifiedBy = "UnitTestUser02_changed"
            });
        }

        [TestMethod]
        public void ConfigService_Delete()
        {
            IConfigService service = this.GetService<IConfigService>();
            service.Delete(61);
        }
    }
}
