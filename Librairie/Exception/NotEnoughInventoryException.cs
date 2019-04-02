using Librairie.Interface;
using System.Collections.Generic;

namespace Librairie.Exception
{
    /// <summary>
    /// Exception return if inventory doesn't contains 
    /// enough quantity of a book to fill a client's basket
    /// </summary>
    public class NotEnoughInventoryException : System.Exception
    {
        /// <summary>
        /// A list of book missing with quantity
        /// </summary>
        public IEnumerable<INameQuantity> Missing { get; }

        /// <summary>
        /// Constructor with missing item
        /// </summary>
        /// <param name="basketitemMissing">list of missing item</param>
        public NotEnoughInventoryException(IEnumerable<INameQuantity> missing)
        {
            Missing = missing;
        }
    }
}
