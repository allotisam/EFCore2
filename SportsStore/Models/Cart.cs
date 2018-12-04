using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    public class Cart
    {
        private List<OrderLine> selections = new List<OrderLine>();

        public IEnumerable<OrderLine> Selections { get => selections; }

        public Cart AddItem(Product p, int quantity)
        {
            OrderLine line = selections.Where(s => s.ProductId == p.Id).FirstOrDefault();

            if (line != null)
            {
                line.Quantity += quantity;
            }
            else
            {
                selections.Add(new OrderLine
                {
                    ProductId = p.Id,
                    Product = p,
                    Quantity = quantity                    
                });
            }

            return this;
        }

        public Cart RemoveItem(long productId)
        {
            selections.RemoveAll(s => s.Product.Id == productId);

            return this;
        }

        public void Clear() => selections.Clear();
    }
}
