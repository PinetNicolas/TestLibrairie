using Librairie.Interface;

namespace Librairie
{
    /// <summary>
    /// Implementation of interface INameQuantity
    /// </summary>
    public class NameQuantity : INameQuantity
    {
        /// <summary>
        /// Title of book
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Quantity missing
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Constructor with parameter
        /// </summary>
        /// <param name="name">Name of book</param>
        /// <param name="quantity">quantity missing</param>
        public NameQuantity(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

    }
}
