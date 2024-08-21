namespace StarBuzzCoffeeApp.Pizza;

public abstract class Pizza
{
    public virtual string GetDescription() => "Unknown Pizza";
    public abstract double Cost();
}
