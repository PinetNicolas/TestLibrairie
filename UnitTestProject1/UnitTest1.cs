using System;
using Librairie;
using Librairie.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CatalogJsonImportEmpty()
        {
            IStore store = new Store();
            store.Import("");
            Assert.AreEqual<int>(store.Quantity("test"), 0);
        }
    }
}
