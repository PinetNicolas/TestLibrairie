using Librairie.Interface;
using System.Collections.Generic;

namespace Librairie.Exception
{
    /// <summary>
    /// Exception return if inventory doesn't contains 
    /// enough quantity of a book to fill a client's basket
    /// </summary>
    public class CatalogMalFormException : System.Exception
    {
    }
}
