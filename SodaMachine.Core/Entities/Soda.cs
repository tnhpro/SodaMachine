using SodaMachine.Core.Resources;
using System;

namespace SodaMachine.Core.Entities
{
    public class Soda
    {
        public Soda(string name, int price, int quantity)
        {
            Name = name; 
            Price = price;
            Quantity = quantity;
        }

        public string Name { get; }
        public int Price { get; private set; }
        public int Quantity { get; private set; }

        #region Quantity

        //Add amount of soda
        public string AddQuantity(int amount)
        {
            var minAmount = 1;
            if(amount < minAmount)
            {
                return string.Format(Messages.CantAddAmount, amount);
            }

            Quantity += amount;
            return string.Format(Messages.QuantityChanged, Name, Quantity);
        }

        #endregion

        #region Price

        //Change the price of soda
        public string ChangePrice(int newPrice)
        {
            var minPrice = 1;
            if(newPrice < minPrice)
            {
                return string.Format(Messages.PriceGreaterThan, minPrice);
            }
            Price = newPrice;

            return string.Format(Messages.PriceChanged, Name, Price);
        }

        #endregion

        //Try to buy a soda
        public string Buy(ref int money)
        {
            if (money >= Price && Quantity > 0)
            {     
                var moneyLeft = money - Price;
                Quantity--;
                money = 0;
                return $"{string.Format(Messages.GivingSodaOut, Name)}\n" +
                        $"{string.Format(Messages.GivingChangeOut, moneyLeft)}";
            }
            else if (Quantity == 0)
            {
                return string.Format(Messages.NoSodaLeft, Name);
            }
            else
            {
                return string.Format(Messages.NotEnoughMoney, Price-money);
            }
        }

        //Sms buy a soda
        public string SmsBuy()
        {
            if (Quantity > 0)
            {
                Quantity--;
                return string.Format(Messages.GivingSodaOut, Name);
            }
            else
            {
                return string.Format(Messages.NoSodaLeft, Name);
            }
        }

        public override string ToString()
        {
            return $"Name: {Name}, Price: {Price}, Quantity: {Quantity}";
        }
    }
}
