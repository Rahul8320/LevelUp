namespace StarBuzzCoffeeApp.Pizza;

public abstract class ToppingDecorator(Pizza pizza) : Pizza
{
    protected Pizza pizza = pizza;

    public override abstract string GetDescription();
}
