namespace StarBuzzCoffeeApp.Pizza;

public sealed class Peppers(Pizza pizza) : ToppingDecorator(pizza)
{
    public override double Cost()
    {
        return pizza.Cost() + 5;
    }

    public override string GetDescription()
    {
        return $"{pizza.GetDescription()}, with Peppers";
    }
}
