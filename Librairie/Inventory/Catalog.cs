using Librairie.Exception;
using Librairie.Interface;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Librairie.Inventory
{
    /// <summary>
    /// Catalog of bookstore
    /// </summary>
    public class Catalog
    {
        /// <summary>
        /// List of book categories
        /// </summary>
        [JsonProperty(PropertyName ="Category")]
        public IList<CategoryItem> CategoryItems { get; set; }

        /// <summary>
        /// List of catalog items
        /// </summary>
        [JsonProperty(PropertyName = "Catalog")]
        public IList<CatalogItem> CatalogItems { get; set; }


        /// <summary>
        /// Calculate quantity of a book
        /// </summary>
        /// <param name="name">title of the book</param>
        /// <returns>the quantity in store</returns>
        public int Quantity(string name)
        {
            // If item doesn't exist
            // that imply Find(name) and Find(name)?.Quantity are null so we return 0
            return Find(name)?.Quantity ?? 0;
        }

        /// <summary>
        /// Calculate a basket with catalog data
        /// </summary>
        /// <param name="basket">An enumarable of name/quantity object</param>
        /// <returns>value of basket</returns>
        /// <exception cref="NotEnoughInventoryException">
        /// Occurs when some book are missing
        /// </exception>
        internal double Calculate(IEnumerable<INameQuantity> basket)
        {
            double valCalculate = 0.0;
            List<INameQuantity> missing = new List<INameQuantity>();
            Dictionary<CategoryItem, int> categoryQuantity = new Dictionary<CategoryItem, int>();
            foreach (INameQuantity item in basket)
            {
                CatalogItem available = Find(item.Name);
                if (available == null || available.Quantity < item.Quantity)
                {
                    missing.Add(new NameQuantity(item.Name, item.Quantity - (available?.Quantity??0)));
                    continue;
                }

                CategoryItem cat = available.TrueCategory;
                if (categoryQuantity.ContainsKey(cat))
                {
                    categoryQuantity[cat] += item.Quantity;
                }
                else
                {
                    categoryQuantity.Add(cat, item.Quantity);
                }
            }

            if (missing.Count > 0)
                throw new NotEnoughInventoryException(missing);

            // Calculation
            foreach (INameQuantity item in basket)
            {
                CatalogItem book = Find(item.Name);
                if(categoryQuantity.ContainsKey(book.TrueCategory) 
                    && categoryQuantity[book.TrueCategory] > 1)
                {
                    valCalculate += book.Price * (1 - book.TrueCategory.Discount);
                }
                else
                {
                    valCalculate += book.Price;
                }

                valCalculate += book.Price * (item.Quantity - 1);
            }

            return valCalculate;
        }

        /// <summary>
        /// Check structure of the catalog
        /// Verify that all categories exists
        /// </summary>
        /// <exception cref="CatalogMalFormException">
        /// Occurs if one or more category missing
        /// </exception>
        internal void CheckStructure()
        {
            foreach (CatalogItem item in CatalogItems)
            {
                CategoryItem cat = FindCategory(item.Category);
                if (cat == null)
                {
                    throw new CatalogMalFormException();
                }

                item.TrueCategory = cat;
            }
        }


        /// <summary>
        /// Calculate quantity of a book
        /// </summary>
        /// <param name="name">title of the book</param>
        /// <returns>the quantity in store</returns>
        private CatalogItem Find(string name)
        {
            foreach (CatalogItem item in CatalogItems)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// Calculate quantity of a book
        /// </summary>
        /// <param name="name">title of the book</param>
        /// <returns>the quantity in store</returns>
        private CategoryItem FindCategory(string name)
        {
            foreach (CategoryItem item in CategoryItems)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
