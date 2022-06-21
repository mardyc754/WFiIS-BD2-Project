using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class RestaurantHandler
    {
        private Restaurant restaurant;

        public RestaurantHandler()
        {
            restaurant = new Restaurant();
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
                Console.WriteLine("q. Wyjście z programu");
                ConsoleKey key = Console.ReadKey().Key;


                switch (key) {
                    case ConsoleKey.D1:
                        ChooseCategory();
                        break;
                    case ConsoleKey.D2:
                        AddCategory();
                        break;
                    case ConsoleKey.D3:
                        DeleteCategory();
                        break;
                    case ConsoleKey.D4:
                        ChooseProduct();
                        break;
                    case ConsoleKey.Q:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        public void ChooseCategory()
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

        public void AddCategory()
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

        public void SearchInMenu()
        {
            Console.Clear();

            Console.WriteLine("1. Wyświetl wszystkie produkty");
            Console.WriteLine("2. Wyświetl produkty wegetariańskie");
            Console.WriteLine("3. Wyświetl produkty w zadanym przedziale cenowym");
            Console.WriteLine("4. Wyszukaj produkty po nazwie");
            Console.WriteLine("r. Powrót");
            Console.WriteLine("q. Wyjście z programu");
        }
        public void DeleteCategory()
        {
            string choice = "";

            while (choice != "r")
            {
                Console.Clear();
                Console.WriteLine("Wybierz kategorię: (r - powrót, q - wyjście z programu)");
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
                    Thread.Sleep(1000);
                }
            }
        }

        public void CategorySubmenu(Category category)
        {
            Console.Clear();
            Console.WriteLine(string.Format("Wybrana kategoria: {0}", category.Name));
            Console.WriteLine("1. Zmień nazwę kategorii");
            Console.WriteLine("2. Wyświetl wszystkie produkty");
            Console.WriteLine("3. Wyszukaj produkty w kategorii po nazwie");
            Console.WriteLine("4. Wyświetl produkty wegetariańskie");
            Console.WriteLine("5. Wyświetl produkty w wybranym przedziale cenowym");
            Console.WriteLine("6. Dodaj produkt");
            Console.WriteLine("7. Wybierz produkt");
            Console.WriteLine("r. Powrót");
            Console.WriteLine("q. Wyjście");
        }

        public void ProductSubmenu(Product product)
        {
            Console.Clear();
            Console.WriteLine(string.Format("Wybrany produkt: {0}", product.Name));
            Console.WriteLine("1. Zmień nazwę produktu");
            Console.WriteLine("2. Zmień cenę za mały rozmiar");
            Console.WriteLine("3. Zmień cenę za średni rozmiar");
            Console.WriteLine("4. Zmień cenę za duży rozmiar");
            Console.WriteLine("5. Usuń cenę za mały rozmiar");
            Console.WriteLine("6. Usuń cenę za duży rozmiar");
            Console.WriteLine("7. Usuń produkt");
            Console.WriteLine("r. Powrót");
            Console.WriteLine("q. Wyjście");
        }

        public void ChooseProduct(Category category = null)
        {
            List<Product> products; 


            if (category != null)
            {
                // List<Category> categories = restaurant.GetProductsInCategory();

                //for (int i = 1; i <= categories.Count; i++)
                //{
                //    Console.WriteLine(string.Format("{0}. {1}", i, categories[i - 1].Name));
                //}
            }
        }
    }
}
