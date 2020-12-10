using System;
using System.Collections.Generic;
using System.Text;

namespace EFAssets
{
    public class App
    {
        public void Run()
        {
            MainMenu();
        }

        private void MainMenu()
        {
            Header("Main Menu");

            Console.WriteLine("What do you want to do?");
            Console.WriteLine("a) Assets menu");
            Console.WriteLine("b) Category menu");
            Console.WriteLine("c) Offices");
            Console.WriteLine("d) Currencies");
            Console.WriteLine("e) Reports");
            Console.WriteLine("\nq) Exit program");

            ConsoleKey command = Console.ReadKey(true).Key;

            // Using switch eliminates all other keypresses as invalid
            switch (command)
            {
                case ConsoleKey.A:
                    AssetMenu();
                    break;
                case ConsoleKey.B:
                    CategoryMenu();
                    break;
                case ConsoleKey.C:
                    OfficeMenu();
                    break;
                case ConsoleKey.D:
                    CurrencyMenu();
                    break;
                case ConsoleKey.E:
                    ReportMenu();
                    break;
               
                case ConsoleKey.Q:
                    break;
               
                default:
                    MainMenu();
                    break;
            }

            //if (command == ConsoleKey.Enter)
            //    MainMenu();

            //if(command == ConsoleKey.A)
            //    AssetMenu();
            //if (command == ConsoleKey.B)
            //    CategoryMenu();
            //if (command == ConsoleKey.C)
            //    OfficeMenu();
            //if (command == ConsoleKey.D)
            //    CurrencyMenu();
            //if (command == ConsoleKey.E)
            //    ReportMenu();
            //if (command == ConsoleKey.Q)
            //    return;
        }

        private void AssetMenu()
        {
            Header("Asset Menu");

            Console.WriteLine("What do you want to do");
            Console.WriteLine("a) Add new asset");
            Console.WriteLine("b) Update existing asset");
            Console.WriteLine("c) Delete existing asset");
            Console.WriteLine("\nq) Return to Main menu");

            ConsoleKey command = Console.ReadKey(true).Key;

            // Using switch eliminates all other keypresses as invalid
            switch (command)
            {
                case ConsoleKey.A:
                    AssetMenu();
                    break;
                case ConsoleKey.B:
                    CategoryMenu();
                    break;
                case ConsoleKey.C:
                    OfficeMenu();
                    break;
                case ConsoleKey.D:
                    CurrencyMenu();
                    break;
                case ConsoleKey.E:
                    ReportMenu();
                    break;

                case ConsoleKey.Q:
                    MainMenu();
                    break;

                default:
                    AssetMenu();
                    break;
            }
        }

        private void CategoryMenu()
        {
            Header("Category Menu");

            Console.WriteLine("What do you want to do");
            Console.WriteLine("a) Add new category");
            Console.WriteLine("b) Update existing category");
            Console.WriteLine("c) Delete existing category");
            Console.WriteLine("\nq) Return to Main menu");

            ConsoleKey command = Console.ReadKey(true).Key;

            if (command == ConsoleKey.Enter)
                CategoryMenu();

            if (command == ConsoleKey.Q)
                MainMenu();
        }

        private void OfficeMenu()
        {
            Header("Office Menu");

            Console.WriteLine("What do you want to do");
            Console.WriteLine("a) Add new office");
            Console.WriteLine("b) Update existing office");
            Console.WriteLine("c) Delete existing office");
            Console.WriteLine("\nq) Return to Main menu");

            ConsoleKey command = Console.ReadKey(true).Key;

            if (command == ConsoleKey.Enter)
                OfficeMenu();

            if (command == ConsoleKey.Q)
                MainMenu();
        }

        private void CurrencyMenu()
        {
            Header("Currency Menu");

            Console.WriteLine("What do you want to do");
            Console.WriteLine("a) Add new currency");
            Console.WriteLine("b) Update existing currency");
            Console.WriteLine("c) Delete existing currency");
            Console.WriteLine("\nq) Return to Main menu");

            ConsoleKey command = Console.ReadKey(true).Key;

            if (command == ConsoleKey.Enter)
                CurrencyMenu();

            if (command == ConsoleKey.Q)
                MainMenu();
        }

        private void ReportMenu()
        {
            Header("Reports");

            Console.WriteLine("What do you want to do");
            Console.WriteLine("a) Show categories");
            Console.WriteLine("b) Show all assets");
            Console.WriteLine("c) Show assets due to be replaced");
            Console.WriteLine("\nq) Return to Main menu");

            ConsoleKey command = Console.ReadKey(true).Key;

            if (command == ConsoleKey.Enter)
                ReportMenu();

            if (command == ConsoleKey.Q)
                MainMenu();
        }

        private void Header(string text)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine(text.ToUpper());
            Console.WriteLine("------------------------------------");
        }

        private void WriteLine(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
        }

        private void Write(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
        }
    }
}
