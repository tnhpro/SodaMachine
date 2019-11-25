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

        public string addQuantity(int amount)
        {
            if(amount <= 0)
            {
                return $"Can't add amount {amount}";
            }

            Quantity += amount;
            return $"Quantity of {Name} is now {Quantity}";
        }

        #endregion

        #region Price

        public string ChangePrice(int price)
        {
            if(price <= 0)
            {
                return "Price must be greater than 0";
            }
            Price = price;

            return $"Price on {Name} was changed to {Price}";
        }

        #endregion

        public string Buy(ref int money)
        {
            if (money >= Price && Quantity > 0)
            {     
                var moneyLeft = money - Price;
                Quantity--;
                money = 0;
                return $"Giving {Name} out.\nGiving {moneyLeft} out in change";
            }
            else if (Quantity == 0)
            {
                return $"No {Name} left";
            }
            else
            {
                return $"Need " + (Price - money) + " more";
            }
        }

        public string SmsBuy()
        {
            if (Quantity > 0)
            {
                Quantity--;
                return $"Giving {Name} out";
            }
            else
            {
                return $"No {Name} left";
            }
        }
    }
}
