using System;

namespace Librairie.Inventory
{
    /// <summary>
    /// Book category
    /// </summary>
    public class CategoryItem : IComparable
    {
        /// <summary>
        /// Name of category
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Discount apply when book are buy twice
        /// </summary>
        public double Discount { get; set; }

        public int CompareTo(object obj)
        {
            CategoryItem comp = obj as CategoryItem;
            if (comp != null)
                return Name.CompareTo(comp.Name);
            throw new ArgumentException("obj is not a CategoryItem");
        }
    }

    
}
