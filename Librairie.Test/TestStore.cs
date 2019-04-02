using System;
using System.IO;
using System.Linq;
using Librairie.Exception;
using Librairie.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Librairie.Test
{
    [TestClass]
    public class TestStore
    {
        [TestMethod]
        public void CatalogJsonImportEmpty()
        {
            IStore store = new Store();
            store.Import("");
            Assert.AreEqual<int>(store.Quantity("test"),0);
        }

        [TestMethod]
        public void CatalogJsonImportBadStructure()
        {
            Action a = new Action(ImportBadStrutucture);
            Assert.ThrowsException<CatalogMalFormException>(a);
        }

        public void ImportBadStrutucture()
        {
            IStore store = new Store();
            string test = System.Text.Encoding.UTF8.GetString(Properties.Resources.BadStructure);
            store.Import(test);
        }

        [TestMethod]
        public void CatalogJsonImportSample()
        {
            IStore store = new Store();
            string test = System.Text.Encoding.UTF8.GetString(Properties.Resources.Sample);
            store.Import(test);
            Assert.AreEqual<int>(store.Quantity("test"), 0);
            Assert.AreEqual<int>(store.Quantity("J.K Rowling - Goblet Of fire"), 2);
            Assert.AreEqual<int>(store.Quantity("Ayn Rand - FountainHead"), 10);
        }


        [TestMethod]
        public void CatalogJsonImportTwice()
        {
            IStore store = new Store();
            string test = System.Text.Encoding.UTF8.GetString(Properties.Resources.Sample);
            store.Import(test);
            
            test = System.Text.Encoding.UTF8.GetString(Properties.Resources.Sample__Replace);
            store.Import(test);
            Assert.AreEqual<int>(store.Quantity("test"), 0);
            Assert.AreEqual<int>(store.Quantity("J.K Rowling - Goblet Of fire"), 0);
            Assert.AreEqual<int>(store.Quantity("Ayn Rand - FountainHead"), 0);
            Assert.AreEqual<int>(store.Quantity("J.K Rowling - Harry Potter and the Philosopher's Stone"), 12);
        }

        [TestMethod]
        public void CatalogJsonCalculateBasketNoCatalog()
        {
            IStore store = new Store();
            string test = System.Text.Encoding.UTF8.GetString(Properties.Resources.Sample);
            NotEnoughInventoryException e = Assert.ThrowsException<NotEnoughInventoryException>(()=>(store.Buy("test", "test")));
            Assert.IsNotNull(e.Missing);
            foreach (INameQuantity m in e.Missing)
            {
                Assert.AreEqual(m.Name, "test");
                Assert.AreEqual(m.Quantity, 2);
            }
        }

        [TestMethod]
        public void CatalogJsonCalculateBasketEmpty()
        {
            IStore store = new Store();
            string test = System.Text.Encoding.UTF8.GetString(Properties.Resources.Sample);
            store.Import(test);
            double res = store.Buy();
            Assert.AreEqual(res, 0.0);
        }

        [TestMethod]
        public void CatalogJsonCalculateBasketMissingBook()
        {
            IStore store = new Store();
            string test = System.Text.Encoding.UTF8.GetString(Properties.Resources.Sample__Replace);
            store.Import(test);
            NotEnoughInventoryException e = Assert.ThrowsException<NotEnoughInventoryException>(()=>store.Buy("Isaac Asimov - Foundation", "Isaac Asimov - Robot series", "J.K Rowling - Goblet Of fire", "J.K Rowling - Goblet Of fire"));
            Assert.IsNotNull(e.Missing);
            foreach (INameQuantity m in e.Missing)
            {
                if(m.Name == "J.K Rowling - Goblet Of fire")
                    Assert.AreEqual(m.Quantity, 2);
            }
        }

        [TestMethod]
        public void CatalogJsonCalculateBasketNotEnoughQuantityBook()
        {
            IStore store = new Store();
            string test = System.Text.Encoding.UTF8.GetString(Properties.Resources.Sample__Replace);
            store.Import(test);
            NotEnoughInventoryException e = Assert.ThrowsException<NotEnoughInventoryException>(() => store.Buy("Isaac Asimov - Foundation", "Isaac Asimov - Robot series", "Robin Hobb - Assassin Apprentice", "Robin Hobb - Assassin Apprentice"));
            Assert.IsNotNull(e.Missing);
            foreach (INameQuantity m in e.Missing)
            {
                if (m.Name == "Robin Hobb - Assassin Apprentice")
                    Assert.AreEqual(m.Quantity, 1);
            }
        }

        [TestMethod]
        public void CatalogJsonCalculateSimpleBasket()
        {
            IStore store = new Store();
            string test = System.Text.Encoding.UTF8.GetString(Properties.Resources.Sample);
            store.Import(test);
            double res = store.Buy("J.K Rowling - Goblet Of fire", "Isaac Asimov - Foundation");
            Assert.AreEqual(res, 24.0);
        }

        [TestMethod]
        public void CatalogJsonCalculateBasketSample1()
        {
            IStore store = new Store();
            string test = System.Text.Encoding.UTF8.GetString(Properties.Resources.Sample);
            store.Import(test);
            double res = store.Buy("J.K Rowling - Goblet Of fire", "Robin Hobb - Assassin Apprentice", "Robin Hobb - Assassin Apprentice");
            Assert.AreEqual(res, 30.0);
        }

        [TestMethod]
        public void CatalogJsonCalculateBasketSample2()
        {
            IStore store = new Store();
            string test = System.Text.Encoding.UTF8.GetString(Properties.Resources.Sample);
            store.Import(test);
            double res = store.Buy("Ayn Rand - FountainHead", "Isaac Asimov - Foundation","Isaac Asimov - Robot series","J.K Rowling - Goblet Of fire", "J.K Rowling - Goblet Of fire", "Robin Hobb - Assassin Apprentice", "Robin Hobb - Assassin Apprentice");
            Assert.AreEqual(res, 69.95);
        }
    }
}
