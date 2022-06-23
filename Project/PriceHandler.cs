using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class PriceHandler
    {
        public static string SizeInPolish(string productSize)
        {
            string sizeInPolish = "";
            switch (productSize)
            {
                case "small":
                    sizeInPolish = "mały";
                    break;
                case "medium":
                    sizeInPolish = "średni";
                    break;
                case "large":
                    sizeInPolish = "duży";
                    break;
            }
            return sizeInPolish;
        }

        public static Dictionary<string, Price> PricesDict(Product product)
        {
            var prices = new Dictionary<string, Price>();
            prices["small"] = product.PriceSmall;
            prices["medium"] = product.PriceMedium;
            prices["large"] = product.PriceLarge;
            return prices;
        }

        public static (bool hasPriceProperValue, string errorMessage)
            IsNewPriceValueValid(decimal newPrice, Product product, string priceType)
        {
            bool isValidPrice = false;
            string errorMessage = "";
            switch (priceType)
            {
                case "small":
                    isValidPrice = newPrice < product.PriceMedium.Value;
                    errorMessage = "Cena za mały rozmiar musi być mniejsza od ceny za średni rozmiar";
                    break;
                case "medium":
                    bool isGreaterThanPriceSmall = product.PriceSmall != null ?
                        newPrice > product.PriceSmall.Value : true;
                    bool isLessThanPriceLarge = product.PriceLarge != null ?
                        newPrice < product.PriceLarge.Value : true;
                    isValidPrice = isGreaterThanPriceSmall && isLessThanPriceLarge;
                    errorMessage = @"Cena za średni rozmiar musi być większa od ceny za mały rozmiar
                                     oraz mniejsza od ceny za duży rozmiar";
                    break;
                case "large":
                    isValidPrice = newPrice > product.PriceMedium.Value;
                    errorMessage = "Cena za duży rozmiar musi być większa od ceny za średni rozmiar";
                    break;
            }

            return (isValidPrice, errorMessage);
        }
    }
}
