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
                Console.WriteLine("3. Przeszukuj menu o podanych parametrach");
                Console.WriteLine("4. Usuń kategorię");
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
                        SearchInMenu();
                        break;
                    case ConsoleKey.D4:
                        DeleteCategory();
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

            Console.WriteLine("1. Wybierz kategorię");
            Console.WriteLine("2. Dodaj kategorię");
            Console.WriteLine("3. Przeszukuj menu o podanych parametrach");
            Console.WriteLine("4. Usuń kategorię");
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

        }
    }
}
