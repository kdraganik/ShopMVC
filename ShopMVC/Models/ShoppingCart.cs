using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMVC.Models
{
    public class ShoppingCart
    {
        public List<Item> Items { get; set; }

        public int Length { get; set; }

        public void Add(Item item)
        {
            if (Items == null)
                Items = new List<Item>();
            Items.Add(item);
            Length = Items.Count;
        }

        public void Remove(int id)
        {
            var index = ShoppingCartIndex(id);
            Items.RemoveAt(index);
            Length = Items.Count;
        }
        public bool UpdateNumber(int index, int number)
        {
            var item = Items[index];

            if (item.Product.NumberInStock - item.Number < number)
                return false;
            item.Number += number;
            return true;
        }
        public int ShoppingCartIndex(int id)
        {
            for (int i = 0; i < Length; i++)
            {
                if (Items[i].Product.Id.Equals(id))
                    return i;
            }
            return -1;
        }

    }
}