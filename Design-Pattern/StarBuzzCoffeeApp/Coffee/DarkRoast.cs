namespace StarBuzzCoffeeApp.Coffee;

public sealed class DarkRoast : Beverage
{
    public override string GetDescription() => "Dark Roast Coffee";

    public override double Cost() => 0.99;
}
