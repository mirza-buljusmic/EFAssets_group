using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFAssets
{
    
    public class App
    {
        static AssetContext _context = new AssetContext();

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
            Console.WriteLine("d) Show existing offices");
            Console.WriteLine("\nq) Return to Main menu");

            ConsoleKey command = Console.ReadKey(true).Key;

            // Using switch eliminates all other keypresses as invalid
            switch (command)
            {
                case ConsoleKey.A:
                    PageAddNewOffice();
                    break;
                case ConsoleKey.B:
                    PageUpdateOffice();
                    break;
                case ConsoleKey.C:
                    PageDeleteOffice();
                    break;
                case ConsoleKey.D:
                    {
                        Header("Existing offices");
                        ShowOffices(0);
                        Console.ReadKey();
                        OfficeMenu();
                        break;
                    }

                case ConsoleKey.Q:
                    MainMenu();
                    break;

                default:
                    OfficeMenu();
                    break;
            }
        }

        private void CurrencyMenu()
        {
            Header("Currency Menu");

            Console.WriteLine("What do you want to do");
            Console.WriteLine("a) Add new currency");
            Console.WriteLine("b) Update existing currency");
            Console.WriteLine("c) Delete existing currency");
            Console.WriteLine("d) Show existing currencies");
            Console.WriteLine("\nq) Return to Main menu");

            ConsoleKey command = Console.ReadKey(true).Key;

            // Using switch eliminates all other keypresses as invalid
            switch (command)
            {
                case ConsoleKey.A:
                    PageAddNewCurrency();
                    break;
                case ConsoleKey.B:
                    PageUpdateCurrency();
                    break;
                case ConsoleKey.C:
                    PageDeleteCurrency();
                    break;
                case ConsoleKey.D:
                    {
                        Header("Existing currencies");
                        ShowCurrencies();
                        Console.ReadKey();
                        CurrencyMenu();
                        break;
                    }
               
                case ConsoleKey.Q:
                    MainMenu();
                    break;

                default:
                    CurrencyMenu();
                    break;
            }
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

        private void PageAddNewOffice()
        {
            Header("Add new office");
            ShowCurrencies();
            ShowOffices(1);
            
            // Check if any currencies exist in currency table. Offices MUST have a currency.
            // If no currency exist, redirect user to Currency menu
            if( !_context.Currency.Any())
            {
                WriteLine("No currencies exist.. Office must have a currency.");
                Write("Press any key to go to Currency menu.");
                Console.ReadKey();
                CurrencyMenu();
            }

           
            Write("Office name: ");
            string newName = Console.ReadLine();
            

            Write("Country (ex USA, GER, SWE): ");
            string newCountry = Console.ReadLine();
            

            Write("Select currencyID from above: ");
            int currencyId = 0;
            try
            {
                currencyId = int.Parse(Console.ReadLine());

                // Check if selected CurrencyID exists in table
                // Otherwise start again
                var chkCurrencyId = _context.Currency.Find(currencyId);
                if (chkCurrencyId == null)
                {
                    Write("Only existing currency IDs are valid..");
                    Console.ReadKey();
                    OfficeMenu();
                    return;
                }
            }
            catch
            {
                Write("Only numerical values ...");
                Console.ReadKey();
                OfficeMenu();
                return;
            }

            // All done, load object with user entries
            var office = new Office();
            office.OfficeName = newName;
            office.OfficeCountry = newCountry;
            office.CurrencyId = currencyId;

            _context.Offices.Add(office);
            _context.SaveChanges();

            Write("Office " + office.OfficeName + " " + office.OfficeCountry + " added...");
            Console.ReadKey();
            OfficeMenu();
        }

        private void PageDeleteOffice()
        {
            Header("Delete existing office");
            ShowOffices(0);

            Write("Which office do you want to delete (ID= 0 to abort): ");
            int officeId = 0;
            try
            {
                officeId = int.Parse(Console.ReadLine());
            }
            catch
            {
                Write("Only numerical values...");
                Console.ReadKey();
                OfficeMenu();
                return;
            }

            // User aborts deletion with ID = 0
            if (officeId == 0)
            {
                OfficeMenu();
                return;
            }
            // Check if officeId exists in database
            var office = _context.Offices.Find(officeId);
            if(office == null)
            {
                Write("That office does not exist...");
                OfficeMenu();
                return;
            }
            else
            {
                _context.Offices.Remove(office);
                _context.SaveChanges();

                Write("Office " + office.OfficeName + ", " + office.OfficeCountry + " deleted...");
                Console.ReadKey();
            }
            OfficeMenu();
        }

        private void PageUpdateOffice()
        {
            Header("Update existing office");
            ShowOffices(0);
            ShowCurrencies();
            int officeId;

            Write("Which office do you want to update (ID = 0 to abort): ");
            try
            {
                officeId = int.Parse(Console.ReadLine());
            }
            catch
            {
                Write("Only numerical values...");
                Console.ReadKey();
                OfficeMenu();
                return;
            }

            // User aborts update with ID = 0
            if (officeId == 0)
            {
                OfficeMenu();
                return;
            }
            // Check if officeId exists in database
            var office = _context.Offices.Find(officeId);
            var curr = _context.Currency.Find(office.CurrencyId);
            if (office == null)
            {
                Write("That office does not exist...");
                OfficeMenu();
                return;
            }
            string currName = curr.CurrencyName;
            WriteLine("Current office name: " + office.OfficeName);
            Write("Enter new name: ");
            string newName = Console.ReadLine();
            WriteLine("Current country is " + office.OfficeCountry);
            Write("Enter new country (ex USA, GER, SWE): ");
            string newCountry = Console.ReadLine();
            WriteLine("Current currency is: " + office.CurrencyId + " " + currName);
            Write("Enter new currency ID: ");
            int newCurrencyId;
            try
            {
                newCurrencyId = int.Parse(Console.ReadLine());
            }
            catch
            {
                Write("Only numerical values...");
                Console.ReadKey();
                OfficeMenu();
                return;
            }

            // All done load user input for update
            office.OfficeName = newName;
            office.OfficeCountry = newCountry;
            office.CurrencyId = newCurrencyId;

            _context.Offices.Update(office);
            _context.SaveChanges();

            Write("Office " + office.OfficeName + ", " + office.OfficeCountry + " updated...");
            Console.ReadKey();
            OfficeMenu();
            return;
            
        }

        private void ShowOffices(int showHeader)
        {
            if (showHeader == 1)
                WriteLine("Existing offices:");
            WriteLine("id".PadRight(5) + "Name".PadRight(20) + "Country".PadRight(20) + "Currency".PadRight(10));
            foreach (var x in _context.Offices)
            {
                
                WriteLineBlue(x.OfficeId.ToString().PadRight(5) + x.OfficeName.ToString().PadRight(20) + x.OfficeCountry.ToString().PadRight(20) + x.CurrencyId.ToString().PadRight(10));
            }
        }

        private void PageAddNewCurrency()
        {
            Header("Add new currency");
            ShowCurrencies();

            var currency =  new Currency();
            Write("Currency name: ");
            string curName = Console.ReadLine();
            currency.CurrencyName = curName;

            double xRate = 0;
            Write("Exchange rate vs USD: ");
            try
            {
                xRate = double.Parse(Console.ReadLine());
            }
            catch (Exception)
            {

                Write("Numerical values only...");
                Console.ReadKey();
                PageAddNewCurrency();
            }
            currency.CurrencyToUSD = xRate;

            _context.Currency.Add(currency);
            _context.SaveChanges();

            Write("Currency " + currency.CurrencyName + " added...");
            Console.ReadKey();
            CurrencyMenu();
        }

        private void PageUpdateCurrency()
        {
            Header("Update currency");
            ShowCurrencies();
            Write("Which currency do you want to update? ");
            int currencyId = int.Parse(Console.ReadLine());
            var currency = _context.Currency.Find(currencyId);

            WriteLine("Current name: " + currency.CurrencyName);
            Write("New name: ");
            string newName = Console.ReadLine();
            currency.CurrencyName = newName;

            WriteLine("Old exchange rate to USD: " + currency.CurrencyToUSD.ToString());
            Write("New exchange rate: ");
            double newRate = 0;
            try
            {
                newRate = double.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Write("Numerical values only...");
                Console.ReadKey();
                PageUpdateCurrency();
            }
            currency.CurrencyToUSD = newRate;

            _context.Currency.Update(currency);
            _context.SaveChanges();

            Write("Currency " + currency.CurrencyName + " updated");
            Console.ReadKey();
            CurrencyMenu();
        }

        private void PageDeleteCurrency()
        {
            Header("Delete currency");
            ShowCurrencies();

            Write("Which currency do you want to delete? (0) to abort ");
            int currencyId = int.Parse(Console.ReadLine());
            if (currencyId == 0)
                return;

            var currency = _context.Currency.Find(currencyId);
            _context.Currency.Remove(currency);
            _context.SaveChanges();

            Write("Currency " + currency.CurrencyName + " deleted...");
            Console.ReadKey();
            CurrencyMenu();
        }

        private void ShowCurrencies()
        {
            WriteLine("Existing currencies:");
            WriteLine("id".PadRight(5) + "Name".PadRight(20) + "Exchange rate".PadRight(20));
            foreach(var x in _context.Currency)
            {
                WriteLineBlue(x.CurrencyId.ToString().PadRight(5) + x.CurrencyName.ToString().PadRight(20) + x.CurrencyToUSD.ToString().PadRight(20));
            }
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

        private void WriteLineBlue(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(text);
        }

        private void Write(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
        }
    }
}
