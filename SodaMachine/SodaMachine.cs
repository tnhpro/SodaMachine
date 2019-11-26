using SodaMachine.Core.Entities;
using SodaMachine.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SodaMachine
{
    public class SodaMachine
    {
        private static int money;
        private readonly IStorageService _storageService;

        public SodaMachine()
        {
            _storageService = new StorageService
            (
                new List<Soda>
                {
                    new Soda("coke", 20, 5),
                    new Soda("sprite", 15, 3),
                    new Soda("fanta", 15, 3)
                }
            );
        }
        /// <summary>
        /// This is the starter method for the machine
        /// </summary>
        public void Start()
        {

            while (true)
            {
                Console.WriteLine("\n\nAvailable commands:");
                Console.WriteLine("admin - Gives access to adding new inventory");
                Console.WriteLine("inventory - Show the inventory in the Soda machine");
                Console.WriteLine("insert (money) - Money put into money slot");
                Console.WriteLine("order ({0}) - Order from machines buttons", _storageService.GetInventoryNames());
                Console.WriteLine("sms order (coke, sprite, fanta) - Order sent by sms");
                Console.WriteLine("recall - gives money back");
                Console.WriteLine("-------");
                Console.WriteLine("Inserted money: " + money);
                Console.WriteLine("-------\n\n");

                var input = Console.ReadLine();

                if (input.StartsWith("admin"))
                {
                    Admin();
                }

                if (input.StartsWith("inventory"))
                {
                    ShowInventory();
                }

                if (input.StartsWith("insert"))
                {
                    Insert(input);   
                }

                if (input.StartsWith("order"))
                {
                    Order(input); 
                }

                if (input.StartsWith("sms order"))
                {
                    SmsOrder(input);   
                }

                if (input.Equals("recall"))
                {
                    Recall();
                }
            }
        }

        //Admin commands
        private void Admin()
        {
            //Add to inventory
            Console.WriteLine("\n\nAvailable commands for Admin:");
            Console.WriteLine("1. Show inventory");
            Console.WriteLine("2. Add new soda");
            Console.WriteLine("3. Change price of soda");
            Console.WriteLine("4. Add quantity of soda");
            Console.WriteLine("Hit return to go back");
            var adminInput = Console.ReadLine();
            if (adminInput.StartsWith("1"))
            {
                ShowInventory();
                Admin();
            }

            if (adminInput.StartsWith("2"))
            {
                Console.WriteLine("Name of soda:");
                var sodaName = Console.ReadLine();
                var sodaPrice = CheckForValidNumber("Price of soda (only valid numbers, greater than 0):");
                var sodaQuantity = CheckForValidNumber("Quantity of soda (only valid numbers, greater than 0):");

                var soda = new Soda(sodaName, sodaPrice, sodaQuantity);
                if (_storageService.AddInventory(soda))
                {
                    Console.WriteLine("Soda {0} was added", soda.Name);
                }
                else
                {
                    Console.WriteLine("Soda {0} could not be added", soda.Name);
                }
                Admin();
            }

            if (adminInput.StartsWith("3"))
            {
                Console.WriteLine("Name of soda:");
                var sodaName = Console.ReadLine();

                var soda = _storageService.GetSoda(sodaName);

                if (soda != null)
                {
                    var sodaPrice = CheckForValidNumber("New price (only valid numbers, greater than 0):");
 
                    Console.WriteLine(soda.ChangePrice(sodaPrice));
                }
                else
                {
                    Console.WriteLine("No such soda");
                }
                Admin();
            }
            
            if (adminInput.StartsWith("4"))
            {
                Console.WriteLine("Name of soda:");
                var sodaName = Console.ReadLine();
                var soda = _storageService.GetSoda(sodaName);

                if (soda != null)
                {
                    var sodaQuantity = CheckForValidNumber("Add amaount (only valid numbers, greater than 0):");
                    
                    Console.WriteLine(soda.AddQuantity(sodaQuantity));
                }
                else
                {
                    Console.WriteLine("No such soda");
                }
                Admin();
            }
        }

        //Checking if input number is valid
        private int CheckForValidNumber(string outputText)
        {
            bool validNumber = false;
            int number = 0;
            while (!validNumber)
            {
                Console.WriteLine(outputText);
                if (int.TryParse(Console.ReadLine(), out number))
                {
                    if (number > 0)
                    {
                        validNumber = true;
                    }
                }
            }

            return number;
        }

        //Show the inventory
        private void ShowInventory()
        {
            var inventory = _storageService.GetInventory();
            Console.WriteLine("--------------------Soda inventory----------------------");
            Console.WriteLine();
            foreach (var soda in inventory)
            {                                             
                Console.WriteLine(soda.ToString());
                Console.WriteLine();
            }
            Console.WriteLine("--------------------Soda inventory----------------------");
        }

        //Insert money
        private void Insert(string input)
        {
            //Add to credit
            money += int.Parse(input.Split(' ')[1]);
            Console.WriteLine("Adding " + int.Parse(input.Split(' ')[1]) + " to credit");
        }

        //Order soda from inventory
        private void Order(string input)
        {
            // split string on space
            var csoda = input.Split(' ')[1];
            //Find out witch kind
            var soda = _storageService.GetSoda(csoda);

            if (soda != null)
            {
                Console.WriteLine(soda.Buy(ref money));
            }
            else
            {
                Console.WriteLine("No such soda");
            }
        }

        //Sms order soda from inventory
        private void SmsOrder(string input)
        {
            var csoda = input.Split(' ')[2];
            //Find out witch kind
            var soda = _storageService.GetSoda(csoda);

            if (soda != null)
            {
                Console.WriteLine(soda.SmsBuy());
            }
            else
            {
                Console.WriteLine("No such soda");
            }
        }

        //Recal inserted money
        private void Recall()
        {
            //Give money back
            Console.WriteLine("Returning " + money + " to customer");
            money = 0;
        }
    }
}
