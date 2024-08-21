namespace StarBuzzCoffeeApp.Pizza;

public sealed class Olives(Pizza pizza) : ToppingDecorator(pizza)
{
    public override double Cost()
    {
        return pizza.Cost() + 55;
    }

    public override string GetDescription()
    {
        return $"{pizza.GetDescription()}, with Olives";
    }
}
