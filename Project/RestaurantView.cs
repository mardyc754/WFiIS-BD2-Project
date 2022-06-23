using System;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;


namespace Project
{
    class RestaurantView
    {
        private Restaurant restaurant;

        public RestaurantView()
        {
            restaurant = new Restaurant();
        }

        public void WaitForReturnToPreviousMenu()
        {
            Console.WriteLine("Naciśnij dowolny klawisz, aby powrócić do poprzedniego menu...");
            Console.ReadKey();
        }

        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("----- Symulator restauracji - program do przetwarzania danych XML ----");
                Console.WriteLine("1. Wybierz kategorię");
                Console.WriteLine("2. Dodaj kategorię");
                Console.WriteLine("3. Usuń kategorię");
                Console.WriteLine("4. Wybierz produkt");
                Console.WriteLine("5. Przeszukuj menu o podanych parametrach");
                Console.WriteLine("6. Wyczyść menu");
                Console.WriteLine("q. Wyjście z programu");
                string key = Console.ReadLine().ToLower();


                switch (key) {
                    case "1":
                        ChooseCategory();
                        break;
                    case "2":
                        AddCategory();
                        break;
                    case "3":
                        DeleteCategory();
                        break;
                    case "4":
                        ChooseProduct();
                        break;
                    case "5":
                        SearchInMenu();
                        break;
                    case "6":
                        ClearMenu();
                        break;
                    case "q":
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private void ChooseCategory()
        {
            string choice = "";

            while (choice != "r")
            {
                Console.Clear();
                Console.WriteLine("Wybierz kategorię: (r - powrót, q - wyjście z programu)");
                List<Category> categories = restaurant.GetAllCategories();

                for (int i = 1; i <= categories.Count; i++) 
                {
                    Console.WriteLine(string.Format("{0}. {1}", i, categories[i-1].Name));
                }

                choice = Console.ReadLine().ToLower();

                if (choice == "q")
                {
                    Environment.Exit(0);
                    break;
                }

                bool isNumber = int.TryParse(choice, out int numericChoice);

                if (isNumber && numericChoice <= categories.Count)
                {
                    CategorySubmenu(categories[numericChoice - 1]);
                }

            }
        }

        private void AddCategory()
        {
            Console.Clear();
            string newName = "";

            while (newName.Length == 0)
            {
                Console.WriteLine("Podaj nazwę nowej kategorii: (r - powrót, q - wyjście z programu)");
                newName = Console.ReadLine();
                if (newName.Length == 0)
                {
                    Console.WriteLine("Nazwa kategorii nie może być pusta");
                    Thread.Sleep(1000);
                }
            }

            if (newName.ToLower() == "q")
            {
                Environment.Exit(0);
            }
            else if (newName.Length > 0 && newName.ToLower() != "r")
            {
                restaurant.AddCategory(newName);
                Console.WriteLine("Pomyślnie dodano nową kategorię");
                Thread.Sleep(1000);
            }
        }

        private void DeleteCategory()
        {
            string choice = "";

            while (choice != "r")
            {
                Console.Clear();
                Console.WriteLine("Usuń kategorię: (r - powrót, q - wyjście z programu)");
                List<Category> categories = restaurant.GetAllCategories();

                for (int i = 1; i <= categories.Count; i++)
                {
                    Console.WriteLine(string.Format("{0}. {1}", i, categories[i - 1].Name));
                }

                choice = Console.ReadLine().ToLower();

                if (choice == "q")
                {
                    Environment.Exit(0);
                    break;
                }

                bool isNumber = int.TryParse(choice, out int numericChoice);

                if (isNumber && numericChoice <= categories.Count)
                {
                    restaurant.DeleteCategory(categories[numericChoice - 1]);
                    Console.WriteLine("Pomyślnie usunięto kategorię");
                    WaitForReturnToPreviousMenu();
                }
            }
        }


        private void SearchInMenu()
        {
            string choice = "";
            while (choice != "r")
            {
                Console.Clear();

                Console.WriteLine("1. Wyświetl wszystkie produkty");
                Console.WriteLine("2. Wyświetl produkty wegetariańskie");
                Console.WriteLine("3. Wyświetl produkty w zadanym przedziale cenowym");
                Console.WriteLine("4. Wyszukaj produkty po nazwie");
                Console.WriteLine("r. Powrót");
                Console.WriteLine("q. Wyjście z programu");

                choice = Console.ReadLine().ToLower();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Wszystkie produkty: ");
                        Printer.PrintProducts(restaurant.GetAllProducts());
                        WaitForReturnToPreviousMenu();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Produkty wegetariańskie: ");
                        Printer.PrintProducts(restaurant.GetVegetarianProducts());
                        WaitForReturnToPreviousMenu();
                        break;
                    case "3":
                        SearchProductsByPrice();
                        break;
                    case "4":
                        SearchProductsByName();
                        break;
                    case "q":
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private void SearchProductsByName()
        {
            string choice = "";
            while (choice != "\\")
            {
                Console.Clear();
                Console.WriteLine("Podaj wzorzec: (\\ - anuluj)");
                choice = Console.ReadLine();

                List<Product> foundProducts = restaurant.GetProductsByName(choice);
                if (choice.Length > 0 && foundProducts.Count > 0)
                {
                    Console.WriteLine("Znalezione produkty");
                    Printer.PrintProducts(restaurant.GetProductsByName(choice));
                    WaitForReturnToPreviousMenu();
                    choice = "\\";
                }
                else if (foundProducts.Count == 0)
                {
                    Console.WriteLine("Nie znaleziono produktów o nazwie pasującej do wzorca");
                    WaitForReturnToPreviousMenu();
                }
            }
        }

        private void ClearMenu()
        {
            Console.Clear();
            Console.WriteLine("Czy na pewno chcesz wyczyścić całe menu?");
            Console.WriteLine("1. Tak");
            Console.WriteLine("2. Nie");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                restaurant.DeleteMenu();
                Console.WriteLine("Pomyślnie wyczyszczono menu");
                WaitForReturnToPreviousMenu();
            }

        }

        private void CategorySubmenu(Category category)
        {
            string choice = "";

            while (choice != "r")
            {
                Console.Clear();
                Console.WriteLine(string.Format("Wybrana kategoria: {0}", category.Name));
                Console.WriteLine("1. Zmień nazwę kategorii");
                Console.WriteLine("2. Wyświetl wszystkie produkty");
                Console.WriteLine("3. Wyświetl produkty wegetariańskie");
                Console.WriteLine("4. Wyświetl produkty w wybranym przedziale cenowym");
                Console.WriteLine("5. Dodaj produkt");
                Console.WriteLine("6. Wybierz produkt");
                Console.WriteLine("r. Powrót");
                Console.WriteLine("q. Wyjście");

                choice = Console.ReadLine().ToLower();

                switch (choice)
                {
                    case "1":
                        ChangeCategoryName(category);
                        choice = "r";
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine(string.Format("Wszystkie produkty w {0}: ", category.Name));
                        Printer.PrintProducts(restaurant.GetProductsFromCategory(category));
                        WaitForReturnToPreviousMenu();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine(string.Format("Produkty wegetariańskie w {0}: ", category.Name));
                        Printer.PrintProducts(restaurant.GetVegetarianProductsInCategory(category));
                        WaitForReturnToPreviousMenu();
                        break;
                    case "4":
                        Console.Clear();
                        SearchProductsByPrice(category);
                        break;
                    case "5":
                        Console.Clear();
                        CreateNewProduct(category);
                        break;
                    case "6":
                        Console.Clear();
                        ChooseProduct(category);
                        break;
                }
            }
        }

        private void ChangeCategoryName(Category category)
        {
            string newName = "";

            while (newName != "\\")
            {
                Console.Clear();
                Console.WriteLine(string.Format("Wybrana kategoria: {0}", category.Name));
                Console.WriteLine("Podaj nową nazwę kategorii (\\ - anuluj)");
                newName = Console.ReadLine();

                if (newName == "")
                {
                    Console.WriteLine("Nazwa kategorii nie może być pusta");
                    Thread.Sleep(1000);
                }
                else if (newName != "\\")
                {
                    restaurant.ModifyCategoryName(category, newName);
                    Console.WriteLine("Pomyślnie zmieniono nazwę kategorii");
                    WaitForReturnToPreviousMenu();
                    break;
                }

            }
        }

        private void SearchProductsByPrice(Category category = null)
        {
            decimal priceMinValue = SetPrice("min");
            decimal priceMaxValue = SetPrice("max");

            if (priceMinValue > priceMaxValue)
            {
                Console.WriteLine("Cena minimalna musi być mniejsza od maksymalnej");
                WaitForReturnToPreviousMenu();
            }
            else
            {
                var foundProducts = category != null ?
                    restaurant.GetProductByPriceInCategory(category, priceMinValue, priceMaxValue) :
                    restaurant.GetProductByPrice(priceMinValue, priceMaxValue);
                Console.WriteLine("Znalezione produkty:");
                Printer.PrintProductsWithPrices(foundProducts, priceMinValue, priceMaxValue);
                WaitForReturnToPreviousMenu();

            }
        }

        private decimal SetPrice(string priceType)
        {
            bool isPriceValid = false;
            string potentialPrice = "";
            decimal maxSqlMoneyValue = (decimal)SqlMoney.MaxValue;
            decimal priceValue = priceType == "min" ? 0 : maxSqlMoneyValue;

            while (potentialPrice != "\\" && !isPriceValid)
            {
                Console.Clear();
                Console.WriteLine(string.Format("Podaj cenę {0}: (\\ - pomiń)",
                    priceType == "min" ? "minimalną" : "maksymalną"));
                potentialPrice = Console.ReadLine();

                isPriceValid = decimal.TryParse(potentialPrice,
                    NumberStyles.AllowDecimalPoint,
                    CultureInfo.CreateSpecificCulture("en-US"),
                    out priceValue) && priceValue >= 0 && priceValue <= maxSqlMoneyValue;
            }
            if (potentialPrice == "\\" && priceType == "max")
            {
                priceValue = maxSqlMoneyValue;
            }
            return priceValue;
        }

        private void CreateNewProduct(Category category)
        {
            Console.Clear();
            Console.WriteLine("Podaj nazwę produktu:");
            string productName = Console.ReadLine();

            Console.WriteLine("Czy jest wegetariański?");
            Console.WriteLine("1) Tak");
            Console.WriteLine("2) Nie");
            Console.WriteLine("cokolwiek innego) Nie dotyczy");

            string isVegetarian = Console.ReadLine();

            bool isVegetarianValid = int.TryParse(isVegetarian, out int vegetarianChoice);

            Vegetarian vegetarian = null;
            if (isVegetarianValid && vegetarianChoice > 0 && vegetarianChoice <= 2)
            {
                vegetarian = vegetarianChoice == 1 ? new Vegetarian(true) : new Vegetarian(false);
            }

            Console.Write("Podaj cenę produktu:");

            string priceInput = Console.ReadLine();
            bool isPriceValid = decimal.TryParse(priceInput,
                NumberStyles.AllowDecimalPoint,
                CultureInfo.CreateSpecificCulture("en-US"),
                out decimal price);
            Price priceMedium = null;
            if (isPriceValid)
            {
                priceMedium = new Price(price);
            }

            Console.WriteLine("Wybrane parametry\n");
            Console.WriteLine(string.Format("Nazwa: {0}\n", productName));
            Console.WriteLine(string.Format("Kategoria: {0}\n", category.Name));
            Console.WriteLine(string.Format("Wegetariański: {0}\n", vegetarian));
            Console.WriteLine(string.Format("Cena: {0}\n", priceMedium != null ? priceMedium.Value : 0));

            if (priceMedium != null)
            {
                Console.WriteLine("Naciśnij Enter, aby dodać produkt do bazy lub inny przycisk, aby anulować");
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Enter)
                {
                    Product product = new Product(0, productName, vegetarian, null, priceMedium, null, category.ID, category.Name);
                    restaurant.AddProduct(product);
                    Console.WriteLine("Pomyślnie dodano produkt do bazy");
                    WaitForReturnToPreviousMenu();
                }
                else
                {
                    Console.WriteLine("Anulowano dodanie produktu do bazy");
                    WaitForReturnToPreviousMenu();
                }
            }
            else
            {
                Console.WriteLine("Produkt nie może być dodany do bazy - niewłaściwa cena");
                WaitForReturnToPreviousMenu();
            }
        }

        private void ChooseProduct(Category category = null)
        {
            string choice = "";

            while (choice != "r")
            {
                Console.Clear();
                Console.WriteLine("Wybierz produkt: (r - powrót, q - wyjście z programu)");
                List<Product> products = category != null ?
                    restaurant.GetProductsFromCategory(category) :
                    restaurant.GetAllProducts();

                for (int i = 1; i <= products.Count; i++)
                {
                    Console.WriteLine(string.Format("{0}. {1}", i, products[i - 1].Name));
                }

                choice = Console.ReadLine().ToLower();

                if (choice == "q")
                {
                    Environment.Exit(0);
                }

                bool isNumber = int.TryParse(choice, out int numericChoice);

                if (isNumber && numericChoice <= products.Count)
                {
                    ProductSubmenu(products[numericChoice - 1]);
                }
            }
        }

        private void ProductSubmenu(Product product)
        {
            string choice = "";

            while (choice != "r")
            {
                Console.Clear();
                Console.WriteLine("Wybrany produkt: ");
                product = restaurant.GetProductByID(product.ID);
                Printer.PrintProductWithPriceRange(product);
                Console.WriteLine("1. Zmień nazwę produktu");
                Console.WriteLine("2. Zmień cenę za mały rozmiar");
                Console.WriteLine("3. Zmień cenę za średni rozmiar");
                Console.WriteLine("4. Zmień cenę za duży rozmiar");
                Console.WriteLine("5. Usuń cenę za mały rozmiar");
                Console.WriteLine("6. Usuń cenę za duży rozmiar");
                Console.WriteLine("7. Usuń produkt");
                Console.WriteLine("r. Powrót");
                Console.WriteLine("q. Wyjście");

                choice = Console.ReadLine();

                if (choice.ToLower() == "q")
                {
                    Environment.Exit(0);
                }

                switch (choice)
                {
                    case "1":
                        ChangeProductName(product);
                        choice = "r";
                        break;
                    case "2":
                        AddOrChangeProductPrice(product, "small");
                        break;
                    case "3":
                        AddOrChangeProductPrice(product, "medium");
                        break;
                    case "4":
                        AddOrChangeProductPrice(product, "large");
                        break;
                    case "5":
                        restaurant.DeleteProductPriceSmall(product.ID);
                        Console.WriteLine("Pomyślnie usunięto cenę za mały rozmiar");
                        WaitForReturnToPreviousMenu();
                        break;
                    case "6":
                        restaurant.DeleteProductPriceLarge(product.ID);
                        Console.WriteLine("Pomyślnie usunięto cenę za duży rozmiar");
                        WaitForReturnToPreviousMenu();
                        break;
                    case "7":
                        restaurant.DeleteProduct(product);
                        Console.WriteLine("Pomyślnie usunięto produkt");
                        WaitForReturnToPreviousMenu();
                        choice = "r";
                        break;
                }

            }
        }

        private void ChangeProductName(Product product)
        {
            string newName = "";

            while (newName != "\\")
            {
                Console.Clear();
                Console.WriteLine(string.Format("Wybrany produkt: {0}", product.Name));
                Console.WriteLine("Podaj nową nazwę produktu (\\ - anuluj)");
                newName = Console.ReadLine();

                if (newName == "")
                {
                    Console.WriteLine("Nazwa produktu nie może być pusta");
                    WaitForReturnToPreviousMenu();
                }
                else if (newName != "\\")
                {
                    restaurant.ModifyProductName(product, newName);
                    Console.WriteLine("Pomyślnie zmieniono nazwę produktu");
                    WaitForReturnToPreviousMenu();
                    break;
                }

            }
        }

        private void AddOrChangeProductPrice(Product product, string priceType)
        {
            bool isPriceValid = false;
            string sizeInPolish = PriceHandler.SizeInPolish(priceType);
            decimal priceValue = 0;
            var productPrices = PriceHandler.PricesDict(product);
            string firstLineOfSubmenu, successMessage;
            bool priceExists = productPrices[priceType] != null;

            if (priceExists)
            {
                firstLineOfSubmenu = string.Format("Zmień cenę za rozmiar {0}: (\\ - powrót)", sizeInPolish);
                successMessage = "Pomyślnie zmieniono cenę produktu";
            }
            else
            {
                firstLineOfSubmenu = string.Format("Dodaj cenę za rozmiar {0}: (\\ - powrót)", sizeInPolish);
                successMessage = "Pomyślnie dodano cenę produktu";
            }

            string inputPrice = "";
            while (inputPrice != "\\" && !isPriceValid)
            {
                Console.Clear();
                Console.Write("Wybrany produkt: ");
                Printer.PrintProductWithPriceRange(product);
                Console.WriteLine(firstLineOfSubmenu);
                inputPrice = Console.ReadLine();

                isPriceValid = decimal.TryParse(inputPrice,
                    NumberStyles.AllowDecimalPoint,
                    CultureInfo.CreateSpecificCulture("en-US"),
                    out priceValue) && priceValue >= 0 && priceValue <= (decimal)SqlMoney.MaxValue;
            }

            var (hasPriceProperValue, errorMessage) =
                PriceHandler.IsNewPriceValueValid(priceValue, product, priceType);

            if (hasPriceProperValue)
            {
                restaurant.ModifyProductPrice(product, priceValue, priceType);
                Console.WriteLine(successMessage);
                if (priceExists)
                {
                    restaurant.ModifyProductPrice(product, priceValue, priceType);
                }
                else
                {
                    restaurant.AddPrice(product, priceValue, priceType);
                }
            }
            else
            {
                Console.WriteLine(errorMessage);
            }
            WaitForReturnToPreviousMenu();

        }

    }
}
