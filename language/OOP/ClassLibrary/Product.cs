using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Product
    {
        // encapsulation (private fields and public properties / methods)
        private int id;
        private string name;
        private string description;
        private double price;
        private double discountPercentage;
        private double tax;
        public const string categoryName = "Electronics"; // compile-time constant
        private readonly string purchaseDate; // runtime constant

        // constructor
        public Product()
        {
            purchaseDate = System.DateTime.Now.ToShortDateString();
        }

        // getters and setters
        public int GetId()
        {
            return id;
        }
        public void SetId(int value)
        {
            id = value;         // this.id
        }
        public string GeName()
        {
            return name;
        }
        public void SetName(string value)
        {
            name = value;       // this.name
        }
        public string GetDescription()
        {
            return description;
        }
        public void SetDescription(string value)
        {
            description = value; // this.description
        }
        public double GetPrice()
        {
            return price;
        }
        public void SetPrice(double value)
        {
            price = value;  // this.price
        }

        public double GetDiscountPercentage()
        {
            return discountPercentage;
        }
        public void SetDiscountPercentage(double value)
        {
            discountPercentage = value;
        }
        public double GetTax()
        {
            return price;
        }
        public string GetPurchaseDate()
        {
            return purchaseDate;
        }


        // public methods
        public void CalculateTax(double percentage = 12) // default parameter value (C#4)
        {
            if (price <= 2000) // this.price 
                tax = price * 10 / 100;
            else tax = price * percentage / 100; 
        }
    }
}
