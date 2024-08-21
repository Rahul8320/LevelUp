namespace StarBuzzCoffeeApp.Pizza;

public sealed class ThinCrustPizza : Pizza
{
    public override string GetDescription() => "Thin Crust Pizza";

    public override double Cost() => 250;
}
