using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class DataRepository : IRepository
    {
        #region Private Properties

        private readonly DataContext context;

        #endregion Private Properties

        #region Public Properties

        public IEnumerable<Product> Products => context.Products.ToArray();

        #endregion Public Properties

        #region Constructor

        public DataRepository(DataContext ctx) => context = ctx;

        #endregion Constructor

        #region Methods

        public void AddProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        #endregion Methods
    }
}
