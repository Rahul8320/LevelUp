namespace StarBuzzCoffeeApp.Pizza;

public sealed class ThickCrustPizza : Pizza
{
    public override string GetDescription() => "Thick Crust Pizza";

    public override double Cost() => 350;
}
