using SodaMachine.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine.Core.Services
{
    public class StorageService : IStorageService
    {
        private List<Soda> _inventory;

        public StorageService()
        {
            _inventory = new List<Soda>
            {
                new Soda("coke", 20, 5),
                new Soda("sprite", 15, 3),
                new Soda("fanta", 15, 3)
            };
        }

        public bool AddInventory(Soda soda)
        {
            try
            {
                var existingSoda = GetSoda(soda.Name);
                if(existingSoda != null)
                {
                    return false;
                }
                _inventory.Add(soda);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public Soda GetSoda(string sodaName)
        {
            var soda = _inventory.FirstOrDefault(x => x.Name == sodaName);
            return soda;
        }

        public List<Soda> GetInventory()
        {
            return _inventory;
        }

        public string GetInventoryNames()
        {
            return string.Join(", ", _inventory.Select(x => x.Name));
        }
    }
}
