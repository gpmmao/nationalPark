using System;
using System.Collections.Generic;
using WiredBrainCoffee.DataAccess.Model;

namespace WiredBrainCoffee.ShopInfoTool
{
  internal class HelpCommandHandler : ICommandHandler
  {
    private IEnumerable<CoffeeShop> coffeeShops;

    public HelpCommandHandler(IEnumerable<CoffeeShop> coffeeShops)
    {
      this.coffeeShops = coffeeShops;
    }

    public void HandleCommand()
    {
      Console.WriteLine("> Available coffee shop commands:");
      foreach (var coffeeShop in coffeeShops)
      {
        Console.WriteLine($"> {coffeeShop.Location}");
      }
    }
  }
}