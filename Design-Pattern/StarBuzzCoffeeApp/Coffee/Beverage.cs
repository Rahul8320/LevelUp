namespace StarBuzzCoffeeApp.Coffee;

public abstract class Beverage
{
    public virtual string GetDescription() => "Unknown Beverage";

    public abstract double Cost();
}
