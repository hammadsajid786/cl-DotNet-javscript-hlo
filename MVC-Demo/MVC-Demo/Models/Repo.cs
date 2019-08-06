using System;
using System.Collections.Generic;
using System.Linq;

namespace MVC_Demo.Models
{
    public class Repo
    {
        private static Entities dbContext;
        public Repo()
        {
            if (dbContext == null)
                dbContext = new Entities();
        }

        public void AddProduct(Product product)
        {
            if (dbContext.Products.Where(x => x.product_name == product.product_name && x.prodyct_brand == product.prodyct_brand).ToList().Count > 0)
            {
                throw new Exception("Product Already exists with this name and brand");
            }

            dbContext.Products.Add(product);
            dbContext.SaveChanges();
        }

        public IList<Product> GetAllProducts()
        {
            return dbContext.Products.ToList<Product>();
        }
    }
}
