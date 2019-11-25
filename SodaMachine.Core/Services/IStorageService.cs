using SodaMachine.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine.Core.Services
{
    public interface IStorageService
    {
        bool AddInventory(Soda soda);
        Soda GetSoda(string sodaName);
        List<Soda> GetInventory();
        string GetInventoryNames();
    }
}
