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
            Console.WriteLine("d) Show existing assets");
            Console.WriteLine("\nq) Return to Main menu");

            ConsoleKey command = Console.ReadKey(true).Key;

            // Using switch eliminates all other keypresses as invalid
            switch (command)
            {
                case ConsoleKey.A:
                    PageAddNewAsset();
                    break;
                case ConsoleKey.B:
                    PageUpdateAsset();
                    break;
                case ConsoleKey.C:
                    PageDeleteAsset();
                    break;
                case ConsoleKey.D:
                    Header("Existing assets");
                    ShowAssets(0);
                    Console.ReadKey();
                    AssetMenu();
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
            Console.WriteLine("d) Show existing categories");
            Console.WriteLine("\nq) Return to Main menu");

            ConsoleKey command = Console.ReadKey(true).Key;
            // Using switch eliminates all other keypresses as invalid
            switch (command)
            {
                case ConsoleKey.A:
                    PageAddNewCategory();
                    break;
                case ConsoleKey.B:
                    PageUpdateCategory();
                    break;
                case ConsoleKey.C:
                    PageDeleteCategory();
                    break;
                case ConsoleKey.D:
                    {
                        Header("Existing categories");
                        ShowCategories(0);
                        Console.ReadKey();
                        CategoryMenu();
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

        private void PageAddNewAsset()
        {
            // Date calculations
            var DF = new DateFunctions();

            // Check if any categories exist in category table. Assets MUST have a category.
            // If no category exist, redirect user to Category menu to first create categories
            if (!_context.Category.Any())
            {
                WriteLine("No categories exist.. Asset must have a category.");
                Write("Press any key to go to Category menu.");
                Console.ReadKey();
                CategoryMenu();
                return;
            }
            else
            {
                Header("Add a new asset");
                ShowCategories(1);
                ShowOffices(1);
                ShowAssets(1);

                var newAsset = new Asset();

                Write("Asset name: ");
                string newAssetName = Console.ReadLine();

                Write("Purchase date (yyyy-mm-dd): ");
                DateTime newPurchaseDate;
                try
                {
                    newPurchaseDate = DateTime.Parse(Console.ReadLine());
                }
                catch
                {
                    WriteLine("Invalid date format, should be yyyy-mm-dd...");
                    Console.ReadKey();
                    AssetMenu();
                    return;
                }
                Write("Purchase price in USD: ");
                double newPurchasePrice;
                try
                {
                    newPurchasePrice = double.Parse(Console.ReadLine());
                }
                catch
                {
                    WriteLine("Must be numerical value...");
                    Console.ReadKey();
                    AssetMenu();
                    return;
                }

                Write("Select category for asset: ");
                int newCategoryID;
                try
                {
                    newCategoryID = int.Parse(Console.ReadLine());
                }
                catch
                {
                    WriteLine("Must be numeric...");
                    Console.ReadKey();
                    AssetMenu();
                    return;
                }

                Write("Select office asset belongs to: ");
                int newOfficeID;
                try
                {
                    newOfficeID = int.Parse(Console.ReadLine());
                }
                catch
                {

                    Write("Numerical values only...");
                    Console.ReadKey();
                    AssetMenu();
                    return;
                }

                // Find the category to determine LifeSpan of asset
                var category = _context.Category.Find(newCategoryID);

                // Find the officeName
                var office = _context.Offices.Find(newOfficeID);

                // Calculate ExpirationDate and a WarningDate
                DateTime newExpirationDate = DateFunctions.CalculateEolDate(newPurchaseDate, category.CategoryEOLMonths);
                DateTime newWarningDate = DateFunctions.CalculateWarningDate(newPurchaseDate, category.CategoryEOLMonths - 3);

                // All done, load user input for creation of asset
                newAsset.AssetName = newAssetName;
                newAsset.AssetPrice = newPurchasePrice;
                newAsset.AssetPurchaseDate = newPurchaseDate;
                newAsset.AssetExpirationDate = newExpirationDate;
                newAsset.AssetWarningDate = newWarningDate;
                newAsset.CategoryId = newCategoryID;
                newAsset.OfficeId = newOfficeID;
                // We assume all assets are active, for convinience...
                newAsset.AssetActive = true;

                // Do it!
                _context.Assets.Add(newAsset);
                _context.SaveChanges();

                Write("Asset: ");
                WriteBlue(newAssetName);
                Write(" with replacement date: ");
                WriteBlue(newExpirationDate.ToString("d"));
                Write(" added to category: ");
                WriteBlue(category.CategoryName);
                Write(" and office: ");
                WriteBlue(office.OfficeName);

                Console.ReadKey();
                AssetMenu();
                return;
            }

        }

        private void PageUpdateAsset()
        {
            // stuff happening
        }

        private void PageDeleteAsset()
        {
            // Stuff happening
            Header("Delete existing asset");
            ShowAssets(1);

            Write("Which asset do you want to delete (ID = 0 to abort): ");
            int assetID;
            try
            {
                assetID = int.Parse(Console.ReadLine());
            }
            catch
            {
                Write("Only numerical values...");
                Console.ReadKey();
                AssetMenu();
                return;
            }
            // User aborts deletion with ID = 0
            if (assetID == 0)
            {
                AssetMenu();
                return;
            }

            // Check if assetID exists in database
            var asset = _context.Assets.Find(assetID);
            if (asset == null)
            {
                Write("That asset does not exist...");
                Console.ReadKey();
                AssetMenu();
                return;
            }
            else
            {
                _context.Assets.Remove(asset);
                _context.SaveChanges();

                Write("Asset: " + asset.AssetName + " deleted...");
                Console.ReadKey();
            }
            AssetMenu();

        }

        private void ShowAssets(int displayHeader)
        {
            if (displayHeader == 1)
                WriteLine("Existing assets:");
            WriteLine("id".PadRight(5) + "Name".PadRight(25) + "Purchase price USD".PadRight(20) + "Purchase date".PadRight(15) + "Replacement date".PadRight(20));
            foreach (var x in _context.Assets)
            {

                WriteLineBlue(x.AssetId.ToString().PadRight(5) + x.AssetName.ToString().PadRight(25) + x.AssetPrice.ToString().PadRight(20) + x.AssetPurchaseDate.ToString("d").PadRight(15) + x.AssetExpirationDate.ToString("d").PadRight(20));
            }

        }

        /// <summary>
        /// Add new Category with LifeSpan in months for assets
        /// </summary>
        private void PageAddNewCategory()
        {
            Header("Add new category");
            // If no prevous categories in database, notify user
            if (!_context.Category.Any())
            {
                WriteLine("No prevous categories exist...");
            }
            else
            {
                ShowCategories(1);
            }

            var newCategory = new Category();

            Write("New category name: ");
            string newCatName = Console.ReadLine();

            Write("Lifespan of assets in category (months): ");
            int newLifespan;
            try
            {
                newLifespan = int.Parse(Console.ReadLine());
            }
            catch
            {
                Write("Numerical values only...");
                Console.ReadKey();
                CategoryMenu();
                return;
            }

            // All done load user input for creation
            newCategory.CategoryName = newCatName;
            newCategory.CategoryEOLMonths = newLifespan;

            _context.Category.Add(newCategory);
            _context.SaveChanges();

            Write("New category: " + newCategory.CategoryName + " with asset lifespan of " + newLifespan + " months added.");
            Console.ReadKey();
            CategoryMenu();
        }

        private void PageUpdateCategory()
        {
            Header("Update existing category");
            ShowCategories(1);

            Write("Which category would like to update? (ID = 0 to abort): ");
            int categoryID;
            try
            {
                categoryID = int.Parse(Console.ReadLine());
            }
            catch
            {
                Write("Numerical values only...");
                Console.ReadKey();
                CategoryMenu();
                return;
            }

            // User aborts operation
            if(categoryID == 0 )
            {
                CategoryMenu();
                return;
            }

            // Check if selected category exists in database
            var category = _context.Category.Find(categoryID);
            if (category == null)
            {
                Write("That category does not exist...");
                CategoryMenu();
                return;
            }
            else
            {
                WriteLine("Existing category name is: " + category.CategoryName);
                Write("Enter new name: ");
                string newName = Console.ReadLine();

                WriteLine("Existing lifespan of attached assets (in months) is: " + category.CategoryEOLMonths);
                Write("Enter new lifespan of assets in category (months)");
                int newLifespan;
                try
                {
                    newLifespan = int.Parse(Console.ReadLine());
                }
                catch
                {
                    WriteLine("Numerical values only...");
                    Console.ReadKey();
                    CategoryMenu();
                    return;
                }

                // ** Should implement functionality here to check if LifeSpan is changed
                // ** and calculate new endOfLife for attached assets


                // All done load user input for update to database
                category.CategoryName = newName;
                category.CategoryEOLMonths = newLifespan;

                _context.Category.Update(category);
                _context.SaveChanges();

                Write("Category is updaed...");
                Console.ReadKey();
                CategoryMenu();
            }
        }


        /// <summary>
        /// Deletes a category
        /// **Note: Categories will not be deleted if any assets are attached to it...
        /// </summary>
        private void PageDeleteCategory()
        {
            // *** MUST check if any assets are connected to category before deletion
            // *** Category cannot be deleted if ANY assets are connected.

            Header("Delete existing category");
            ShowCategories(1);

            Write("Which category would you like to delete? (ID = 0 to abort): ");
            int categoryID;
            try
            {
                categoryID = int.Parse(Console.ReadLine());
            }
            catch
            {
                Write("Numerical values only...");
                Console.ReadKey();
                CategoryMenu();
                return;
            }

            // User aborts operation
            if (categoryID == 0)
            {
                CategoryMenu();
                return;
            }

            // Check if selected category exists in database
            var category = _context.Category.Find(categoryID);
            var asset = _context.Assets.Find(category.CategoryId);
            if (category == null)
            {
                Write("That category does not exist...");
                CategoryMenu();
                return;
            }

            // Check if any assets are connected to the category
            if(asset != null)
            {
                WriteLineRed(" ** There are assets attached to the category: " + category.CategoryName);
                WriteLineRed(" Update assets and attach them to a different category,");
                WriteLineRed(" before trying to delete this category.");
                Console.ReadKey();
                CategoryMenu();
                return;
            }
            else
            {
                _context.Category.Remove(category);
                _context.SaveChanges();

                Write("Category: " + category.CategoryName + " is deleted");
                Console.ReadKey();
                CategoryMenu();
                return;
            }

        }

        private void ShowCategories(int displayHeader)
        {
            // If category table empty, don't bother trying to read from db
            if (!_context.Category.Any())
            {
                WriteLine("No prevous categories exist... Nothing to show..");
                Console.ReadKey();
                //CategoryMenu();
                //return;
            }
            else
            {
                // To avoid unnecessary info
                if (displayHeader == 1)
                    WriteLine("Existing categories:");
                WriteLine("ID".PadRight(5) + "Name".PadRight(25) + "Lifespan (months)".PadRight(20));
                foreach (var x in _context.Category)
                {
                    WriteLineBlue(x.CategoryId.ToString().PadRight(5) + x.CategoryName.ToString().PadRight(25) + x.CategoryEOLMonths.ToString().PadRight(20));
                }
            }
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
                Console.ReadKey();
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

        private void WriteLineRed(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
        }
        private void Write(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
        }

        private void WriteBlue(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(text);
        }
    }
}
