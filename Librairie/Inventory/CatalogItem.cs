namespace Librairie.Inventory
{
    /// <summary>
    /// Catalog Item. Information for one book
    /// </summary>
    public class CatalogItem
    {
        /// <summary>
        /// Title of book
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Category of book
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Category of book with true structure
        /// </summary>
        public CategoryItem TrueCategory { get; set; }

        /// <summary>
        /// Price of a book
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Quantity in catalog
        /// </summary>
        public int Quantity { get; set; }
    }

}
