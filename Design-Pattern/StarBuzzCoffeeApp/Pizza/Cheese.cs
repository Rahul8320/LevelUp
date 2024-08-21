namespace StarBuzzCoffeeApp.Pizza;

public sealed class Cheese(Pizza pizza) : ToppingDecorator(pizza)
{
    public override double Cost()
    {
        return pizza.Cost() + 25;
    }

    public override string GetDescription()
    {
        return $"{pizza.GetDescription()}, with extra Cheese";
    }
}
