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
            _storageService = new StorageService();
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
            Console.WriteLine("1. Add new soda");
            var adminInput = Console.ReadLine();
            if (adminInput.StartsWith("1"))
            {
                Console.WriteLine("Name of soda:");
                var sodaName = Console.ReadLine();

                bool sodaPriceValid = false;
                int sodaPrice = 0;
                while (!sodaPriceValid)
                {
                    Console.WriteLine("Price of soda (only valid numbers, greater than 0):");
                    if (int.TryParse(Console.ReadLine(), out sodaPrice))
                    {
                        if (sodaPrice > 0)
                        {
                            sodaPriceValid = true;
                        }
                    }
                }

                bool sodaQuantityValid = false;
                int sodaQuantity = 0;
                while (!sodaQuantityValid)
                {
                    Console.WriteLine("Quantity of soda (only valid numbers, greater than 0):");
                    if (int.TryParse(Console.ReadLine(), out sodaQuantity))
                    {
                        if (sodaQuantity > 0)
                        {
                            sodaQuantityValid = true;
                        }
                    }
                }

                var soda = new Soda(sodaName, sodaPrice, sodaQuantity);
                if (_storageService.AddInventory(soda))
                {
                    Console.WriteLine("Soda {0} was added", soda.Name);
                }
                else
                {
                    Console.WriteLine("Soda {0} could not be added", soda.Name);
                }
            }
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
