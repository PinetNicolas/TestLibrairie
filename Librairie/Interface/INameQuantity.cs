namespace Librairie.Interface
{
    /// <summary>
    /// The content of expection to know which book is missing
    /// </summary>
    public interface INameQuantity
    {
        /// <summary>
        /// Title of book
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Quantity missing
        /// </summary>
        int Quantity { get; }
    }
}
