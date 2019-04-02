using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librairie
{
    /// <summary>
    /// Interface for storage
    /// </summary>
    public interface IStore
    {
        /// <summary>
        /// Import a catalog. 
        /// 
        /// </summary>
        /// <param name="catalogAsJson"></param>
        void Import(string catalogAsJson);
        int Quantity(string name);
        double Buy(params string[] basketByNames);
    }
}
