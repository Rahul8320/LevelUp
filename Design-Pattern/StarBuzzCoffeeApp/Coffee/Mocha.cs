namespace StarBuzzCoffeeApp.Coffee;

public sealed class Mocha(Beverage beverage) : CondimentDecorator(beverage)
{
    public override double Cost()
    {
        return beverage.Cost() + 0.25;
    }

    public override string GetDescription()
    {
        return beverage.GetDescription() + ", Mocha";
    }
}
