using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CartAPI.Models
{
    public class Item
    {
        public string UPC { get; set; }
        public string BrandName { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ImageURL { get; set; }
    }

    // Custom comparer for the Product class
    class ItemComparer : IEqualityComparer<Item>
    {

        public bool Equals(Item x, Item y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.UPC == y.UPC;
        }


        public int GetHashCode(Item obj)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(obj, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashUPC = obj.ItemName == null ? 0 : obj.ItemName.GetHashCode();

            //Get hash code for the Code field.
            int hashName = obj.UPC.GetHashCode();

            //Calculate the hash code for the product.
            return hashUPC ^ hashName;
        }
    }
}