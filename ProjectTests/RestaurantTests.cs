using Project;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ProjectTests
{
    [TestClass]
    public class RestaurantTests {
        public Restaurant restaurant;

        [TestInitialize]
        public void InitialTestData()
        {
            restaurant = new Restaurant();

            // wstawia domyślne dane do bazy nadpisując przy tym stare
            // domyślne dane zawarte są w pliku 4_insert_data.sql
            restaurant.InsertDefaultData();
        }

        [TestMethod]
        public void TestInitialDataQuantity()
        {
            Assert.AreEqual(restaurant.GetAllProducts().Count, 33);
            Assert.AreEqual(restaurant.GetAllCategories().Count, 8);
        }

        [TestMethod]
        public void TestGetProductByID()
        {
            Product product = restaurant.GetProductByID(2);
            Assert.AreEqual(product.ID, 2);
            Assert.AreEqual(product.Name, "Zupa pieczarkowa");
            Assert.AreEqual(product.Vegetarian.IsVegetarian, true);
            Assert.AreEqual(product.PriceSmall, null);
            Assert.AreEqual(product.PriceMedium.Value, 12m);
            Assert.AreEqual(product.PriceLarge, null);

            product = restaurant.GetProductByID(21);
            Assert.AreEqual(product.ID, 21);
            Assert.AreEqual(product.Name, "Cola");
            Assert.AreEqual(product.Vegetarian, null);
            Assert.AreEqual(product.PriceSmall.Value, 6m);
            Assert.AreEqual(product.PriceMedium.Value, 9m);
            Assert.AreEqual(product.PriceLarge.Value, 12m);

            product = restaurant.GetProductByID(27);
            Assert.AreEqual(product.ID, 27);
            Assert.AreEqual(product.Name, "Pepperoni");
            Assert.AreEqual(product.Vegetarian.IsVegetarian, false);
            Assert.AreEqual(product.PriceSmall.Value, 21m);
            Assert.AreEqual(product.PriceMedium.Value, 27m);
            Assert.AreEqual(product.PriceLarge.Value, 33m);
        }

        [TestMethod]
        public void TestGetCategoryByID()
        {
            Category category = restaurant.GetCategoryByID(1);
            Assert.AreEqual(category.ID, 1);
            Assert.AreEqual(category.Name, "Zupy");


            List<Product> products = restaurant.GetProductsFromCategory(category);
            Assert.AreEqual(restaurant.GetAllProducts().Count, 33);
            Assert.AreEqual(restaurant.GetAllCategories().Count, 8);
        }

        [TestMethod]
        public void TestGetProductsFromCategory()
        {
            Category category = restaurant.GetCategoryByID(3);

            List<Product> products = restaurant.GetProductsFromCategory(category);
            Assert.AreEqual(products.Count, 2);
        }

        [TestMethod]
        public void TestGetProductsByName()
        {
            List<Product> products = restaurant.GetProductsByName("Pierogi");

            // Pierogi z serem
            // Pierogi ruskie
            // Pierogi z mięsem
            // Pierogi ze szpinakiem
            Assert.AreEqual(products.Count, 4);
        }

        [TestMethod]
        public void GetVegetarianProducts()
        {
            List<Product> products = restaurant.GetVegetarianProducts();
            Assert.AreEqual(products.Count, 12);
        }

        [TestMethod]
        public void TestGetVegetarianProductsInCategory()
        {
            Category category = restaurant.GetCategoryByID(6);
            List<Product> allProducts = restaurant.GetProductsFromCategory(category);
            List<Product> vegetarianProducts = restaurant.GetVegetarianProductsInCategory(category);

            Assert.AreEqual(category.Name, "Napoje zimne");
            Assert.AreEqual(allProducts.Count, 4);
            Assert.AreEqual(vegetarianProducts.Count, 0);

            category = restaurant.GetCategoryByID(7);
            allProducts = restaurant.GetProductsFromCategory(category);
            vegetarianProducts = restaurant.GetVegetarianProductsInCategory(category);

            Assert.AreEqual(category.Name, "Pizza");
            Assert.AreEqual(allProducts.Count, 7);
            Assert.AreEqual(vegetarianProducts.Count, 3);
        }

        [TestMethod]
        public void TestGetProductByPriceIfThereAreNoParams()
        {
            Category category = restaurant.GetCategoryByID(6);
            List<Product> allProducts = restaurant.GetAllProducts();
            List<Product> filteredProducts = restaurant.GetProductByPrice();

            Assert.AreEqual(allProducts.Count, filteredProducts.Count);
        }

        [TestMethod]
        public void TestGetProductByPriceInCategoryIfThereAreNoParams()
        {
            Category category = restaurant.GetCategoryByID(6);
            List<Product> allProductsInCategory = restaurant.GetProductsFromCategory(category);
            List<Product> filteredProducts = restaurant.GetProductByPriceInCategory(category);

            Assert.AreEqual(allProductsInCategory.Count, filteredProducts.Count);
        }

        [TestMethod]
        public void TestAddCategory()
        {
            Assert.AreEqual(restaurant.GetAllProducts().Count, 33);
            Assert.AreEqual(restaurant.GetAllCategories().Count, 8);

            restaurant.AddCategory("Dania i napoje");

            Assert.AreEqual(restaurant.GetAllProducts().Count, 33);
            Assert.AreEqual(restaurant.GetAllCategories().Count, 9);
        }

        [TestMethod]
        public void TestAddProduct()
        {
            Category category = restaurant.GetCategoryByID(1);
            int numOfProductsInCategory = restaurant.GetProductsFromCategory(category).Count; 
            
            Assert.AreEqual(restaurant.GetAllProducts().Count, 33);
            Assert.AreEqual(restaurant.GetAllCategories().Count, 8);

            Product dummyProduct = new Product(0, "Danie", new Vegetarian(true),
                new Price(12), new Price(34), new Price(56), category.ID, category.Name);

            restaurant.AddProduct(dummyProduct);

            Assert.AreEqual(restaurant.GetAllProducts().Count, 34);
            Assert.AreEqual(restaurant.GetAllCategories().Count, 8);

            Assert.AreEqual(restaurant.GetProductsFromCategory(category).Count, 
                numOfProductsInCategory + 1);

            Product addedProduct = restaurant.GetProductByID(34);

            Assert.AreEqual(addedProduct.ID, 34);
            Assert.AreEqual(addedProduct.Name, dummyProduct.Name);
            Assert.AreEqual(addedProduct.Vegetarian.IsVegetarian, new Vegetarian(true).IsVegetarian);
            // zamierzone: do produktu jest dodawana tylko cena za średni rozmiar
            Assert.AreEqual(addedProduct.PriceSmall, null); 
            Assert.AreEqual(addedProduct.PriceMedium.Value, new Price(34).Value);
            Assert.AreEqual(addedProduct.PriceLarge, null);
            Assert.AreEqual(addedProduct.CategoryID, category.ID);
            Assert.AreEqual(addedProduct.Category, category.Name);
        }

        [TestMethod]
        public void TestAddPrice()
        {
            Product product = restaurant.GetProductByID(1);

            Assert.AreEqual(product.PriceSmall, null);
            Assert.AreEqual(product.PriceMedium.Value, 10);
            Assert.AreEqual(product.PriceLarge, null);

            restaurant.AddPrice(product, 8, "small");
            product = restaurant.GetProductByID(1);
            Assert.AreEqual(product.PriceSmall.Value, 8);

            restaurant.AddPrice(product, 8, "large");
            product = restaurant.GetProductByID(1);
            Assert.AreEqual(product.PriceLarge, null);

            restaurant.AddPrice(product, 15, "large");
            product = restaurant.GetProductByID(1);
            Assert.AreEqual(product.PriceLarge.Value, 15);
        }

        [TestMethod]
        public void TestModifyCategoryName()
        {
            Category category = restaurant.GetCategoryByID(1);

            Assert.AreEqual(category.Name, "Zupy");

            string newName = "Dania i napoje";

            restaurant.ModifyCategoryName(category, newName);

            category = restaurant.GetCategoryByID(1);
            Assert.AreEqual(category.Name, newName);
        }

        [TestMethod]
        public void TestModifyProductName()
        {
            Product product = restaurant.GetProductByID(21);

            Assert.AreEqual(product.Name, "Cola");

            string newName = "Pepsi";

            restaurant.ModifyProductName(product, newName);
            product = restaurant.GetProductByID(21);
            Assert.AreEqual(product.Name, newName);
        }

        [TestMethod]
        public void TestModifyProductPrice()
        {
            Product product = restaurant.GetProductByID(18);
            Assert.AreEqual(product.Name, "Kawa");
            Assert.AreEqual(product.PriceSmall.Value, 5);
            Assert.AreEqual(product.PriceMedium.Value, 7.5m);
            Assert.AreEqual(product.PriceLarge.Value, 10);

            restaurant.ModifyProductPrice(product, 6, "small");
            product = restaurant.GetProductByID(18);
            Assert.AreEqual(product.PriceSmall.Value, 6);

            restaurant.ModifyProductPrice(product, 8, "medium");
            product = restaurant.GetProductByID(18);
            Assert.AreEqual(product.PriceMedium.Value, 8);

            restaurant.ModifyProductPrice(product, 12, "large");
            product = restaurant.GetProductByID(18);
            Assert.AreEqual(product.PriceLarge.Value, 12);
        }

        [TestMethod]
        public void TestDeleteProductPriceLargeSmall()
        {
            Product product = restaurant.GetProductByID(18);
            Assert.AreEqual(product.Name, "Kawa");
            Assert.AreEqual(product.PriceSmall.Value, 5);
            Assert.AreEqual(product.PriceLarge.Value, 10);

            restaurant.DeleteProductPriceLarge(product.ID);
            product = restaurant.GetProductByID(18);
            Assert.AreEqual(product.PriceLarge, null);

            restaurant.DeleteProductPriceSmall(product.ID);
            product = restaurant.GetProductByID(18);
            Assert.AreEqual(product.PriceSmall, null);
        }

        [TestMethod]
        public void TestDeleteProduct()
        {
            Category category = restaurant.GetCategoryByID(1);
            Product product = restaurant.GetProductByID(1);

            int initialNumOfProducts = restaurant.GetAllProducts().Count;
            int initialNumOfCategoryProducts = restaurant.GetProductsFromCategory(category).Count;

            restaurant.DeleteProduct(product);

            int numOfProducts = restaurant.GetAllProducts().Count;
            int numOfCategoryProducts = restaurant.GetProductsFromCategory(category).Count;

            Assert.AreEqual(numOfProducts, initialNumOfProducts - 1);
            Assert.AreEqual(numOfCategoryProducts, initialNumOfCategoryProducts - 1);
        }

        [TestMethod]
        public void TestDeleteCategory()
        {
            Category category = restaurant.GetCategoryByID(1);

            int initialNumOfProducts = restaurant.GetAllProducts().Count;
            int initialNumOfCategories = restaurant.GetAllCategories().Count;
            int numOfCategoryProducts = restaurant.GetProductsFromCategory(category).Count;

            restaurant.DeleteCategory(category);

            int numOfProducts = restaurant.GetAllProducts().Count;
            int numOfCategories = restaurant.GetAllCategories().Count;

            Assert.AreEqual(numOfProducts, initialNumOfProducts - numOfCategoryProducts);
            Assert.AreEqual(numOfCategories, initialNumOfCategories - 1);
        }

        [TestMethod]
        public void TestDeleteMenu()
        {
            Assert.AreEqual(restaurant.GetAllProducts().Count, 33);
            Assert.AreEqual(restaurant.GetAllCategories().Count, 8);

            restaurant.DeleteMenu();

            Assert.AreEqual(restaurant.GetAllProducts().Count, 0);
            Assert.AreEqual(restaurant.GetAllCategories().Count, 0);
        }

    }
}
