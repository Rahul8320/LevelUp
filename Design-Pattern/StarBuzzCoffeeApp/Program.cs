using StarBuzzCoffeeApp.Coffee;
using StarBuzzCoffeeApp.Pizza;

Console.WriteLine("Hey, Welcome to Star Buzz Coffee Shop!");

// Get an order for double mocha dark roast coffee with whip

Beverage beverage = new DarkRoast();
beverage = new Mocha(beverage);
beverage = new Mocha(beverage);
beverage = new Whip(beverage);

Console.WriteLine($"{beverage.GetDescription()} ${beverage.Cost()}");
Console.WriteLine("Have a nice day.");

Console.WriteLine("--------------------------------------------------------");

Pizza pizza = new ThickCrustPizza();
pizza = new Cheese(pizza);
pizza = new Peppers(pizza);

Console.WriteLine($"{pizza.GetDescription()} Rs. {pizza.Cost()}");
Console.WriteLine("Have a nice day.");