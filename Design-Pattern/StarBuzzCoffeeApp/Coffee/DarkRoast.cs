namespace StarBuzzCoffeeApp.Coffee;

public sealed class DarkRoast : Beverage
{
    public DarkRoast()
    {
        Description = "Dark Roast Coffee";
    }

    public override double Cost()
    {
        return 0.99;
    }
}
