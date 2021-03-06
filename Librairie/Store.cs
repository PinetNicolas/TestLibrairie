﻿using Librairie.Exception;
using Librairie.Interface;
using Librairie.Inventory;
using System.Collections.Generic;

namespace Librairie
{
    /// <summary>
    /// Implementation of IStore
    /// </summary>
    public class Store : IStore
    {
        /// <summary>
        /// Catalog in current state
        /// </summary>
        private Catalog _currentCatalog;

        /// <summary>
        /// Calculate the amount to pay for a basket of book
        /// </summary>
        /// <param name="basketByNames">A list of book title</param>
        /// <returns>the amount of basket</returns>
        /// <exception cref="NotEnoughInventoryException">
        ///     Exception return if one book is missing in store
        /// </exception>
        public double Buy(params string[] basketByNames)
        {
            IEnumerable<INameQuantity> basket = BasketAnalyse(basketByNames);
            if (_currentCatalog != null)
                return _currentCatalog.Calculate(basket);

            throw new NotEnoughInventoryException(basket);
        }

        /// <summary>
        /// Analyse basket in order to reorganise it in name quantity object
        /// </summary>
        /// <param name="basketByNames">the basket in string array format</param>
        /// <returns>The basket in an enumarable of NameQuantity</returns>
        private IEnumerable<INameQuantity> BasketAnalyse(params string[] basketByNames)
        {
            Dictionary<string, NameQuantity> mapping = new Dictionary<string, NameQuantity>();

            foreach (string item in basketByNames)
            {
                if (mapping.ContainsKey(item))
                {
                    mapping[item].Quantity++;
                }
                else
                {
                    mapping.Add(item, new NameQuantity(item, 1));
                }
            }

            return mapping.Values;
        }

        /// <summary>
        /// Import a catalog. 
        /// </summary>
        /// <param name="catalogAsJson">the catalog in json format</param>
        public void Import(string catalogAsJson)
        {
            _currentCatalog = Newtonsoft.Json.JsonConvert.DeserializeObject<Catalog>(catalogAsJson);
            if(_currentCatalog != null)
                _currentCatalog.CheckStructure();
        }

        /// <summary>
        /// Calculate quantity of a book
        /// </summary>
        /// <param name="name">title of the book</param>
        /// <returns>the quantity in store</returns>
        public int Quantity(string name)
        {
            if(_currentCatalog != null)
                return _currentCatalog.Quantity(name);
            return 0;
        }
    }
}
