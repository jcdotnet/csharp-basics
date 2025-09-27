using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class SalesAnalysis
    {
        // readonly prop to store the list of items sold
        public readonly List<string> ItemsSold;

        // const to represent the separator character
        public const char Separator = ':';

        public SalesAnalysis(List<string> itemsSold)
        {
            ItemsSold = itemsSold;
        }

        public double ComputeTotalIncome()
        {

            // Handle null or empty list.
            if (ItemsSold == null || !ItemsSold.Any()) return 0;

            double totalIncome = 0;

            // Iterate through prices.
            foreach (var item in ItemsSold)
            {
                // Variables, expressions and operators.
                var itemParts = item.Split(Separator);

                // Check if the item format is incorrect or the price is missing.
                if (itemParts.Length < 2 || string.IsNullOrWhiteSpace(itemParts[1])) continue;

                if (double.TryParse(itemParts[1], out double price))
                {
                    // Use price if it's non-negative. Ignore negative prices.
                    if (price >= 0)
                    {
                        totalIncome += price; // Add each price to our current total.
                    }
                }
            }

            return totalIncome;
        }
    }
}
