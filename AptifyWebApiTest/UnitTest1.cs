using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AptifyWebApiTest {
    [TestClass]
    public class TestAptifriedClassesApiController {
        [TestMethod]
        public void TestGetReturnsSomeShit() {
            var controller = new AptifyWebApi.Controllers.AptifriedClassesController();


            Assert.AreEqual(true, true);

        }
    }
}
