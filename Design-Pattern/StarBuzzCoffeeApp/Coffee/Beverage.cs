namespace StarBuzzCoffeeApp.Coffee;

public abstract class Beverage
{
    public string Description = "Unknown Beverage";

    public string GetDescription()
    {
        return Description;
    }

    public abstract double Cost();
}
