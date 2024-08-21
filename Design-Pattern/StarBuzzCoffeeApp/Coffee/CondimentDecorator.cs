namespace StarBuzzCoffeeApp.Coffee;

public abstract class CondimentDecorator(Beverage beverage) : Beverage
{
    protected Beverage beverage = beverage;
    public override abstract string GetDescription();
}
