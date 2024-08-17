namespace StarBuzzCoffeeApp.Coffee;

public sealed class Whip(Beverage beverage) : CondimentDecorator
{
    public override double Cost()
    {
        return beverage.Cost() + 0.10;
    }

    public override string GetDescription()
    {
        return beverage.GetDescription() + ", Whip";
    }
}
