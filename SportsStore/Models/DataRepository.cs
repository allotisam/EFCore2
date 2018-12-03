using Microsoft.EntityFrameworkCore;
using SportsStore.Models.Pages;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    public class DataRepository : IRepository
    {
        #region Private Properties

        private readonly DataContext context;

        #endregion Private Properties

        #region Public Properties

        public IEnumerable<Product> Products => context.Products.Include(p => p.Category).ToArray();

        #endregion Public Properties

        #region Constructor

        public DataRepository(DataContext ctx) => context = ctx;

        #endregion Constructor

        #region Methods

        public PagedList<Product> GetProducts(QueryOptions options, long category = 0)
        {
            IQueryable<Product> query = context.Products.Include(p => p.Category);

            if (category != 0)
            {
                query = query.Where(p => p.CategoryId == category);
            }

            return new PagedList<Product>(query, options);
        }

        public Product GetProduct(long key) => context.Products.Include(p => p.Category).First(p => p.Id == key);

        public void AddProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            Product p = context.Products.Find(product.Id);

            p.Name = product.Name;
            p.PurchasePrice = product.PurchasePrice;
            p.RetailPrice = product.RetailPrice;
            p.CategoryId = product.CategoryId;
            
            context.SaveChanges();
        }

        public void UpdateAll(Product[] products)
        {
            Dictionary<long, Product> data = products.ToDictionary(p => p.Id);
            IEnumerable<Product> baseline = context.Products.Where(p => data.Keys.Contains(p.Id));

            foreach(Product databaseProduct in baseline)
            {
                Product requestProduct = data[databaseProduct.Id];
                databaseProduct.Name = requestProduct.Name;
                databaseProduct.Category = requestProduct.Category;
                databaseProduct.PurchasePrice = requestProduct.PurchasePrice;
                databaseProduct.RetailPrice = requestProduct.RetailPrice;
            }
            
            context.SaveChanges();
        }

        public void Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }

        #endregion Methods
    }
}
