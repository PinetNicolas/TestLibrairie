namespace Librairie.Interface
{
    /// <summary>
    /// Interface for storage
    /// </summary>
    public interface IStore
    {
        /// <summary>
        /// Import a catalog. 
        /// </summary>
        /// <param name="catalogAsJson">the catalog in json format</param>
        void Import(string catalogAsJson);
        /// <summary>
        /// Calculate quantity of a book
        /// </summary>
        /// <param name="name">title of the book</param>
        /// <returns>the quantity in store</returns>
        int Quantity(string name);
        /// <summary>
        /// Calculate the amount to pay for a basket of book
        /// </summary>
        /// <param name="basketByNames">A list of book title</param>
        /// <returns>the amount of basket</returns>
        /// <exception cref="NotEnoughInventoryException">
        ///     Exception return if one book is missing in store
        /// </exception>
        double Buy(params string[] basketByNames);
    }
}
