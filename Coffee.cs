namespace CoffeeMachine;

public interface ICoffee
{
    IDictionary<string, int> Ingredients { get; }
    double Cost { get; }

}

public class Coffee : ICoffee
{
    public IDictionary<string, int> Ingredients { get; private set; }

    public double Cost { get; private set; }
    public Coffee()
    {
        Ingredients = new Dictionary<string, int>
        {
            ["coffee"] = 1,
            ["water"] = 16
        };
        Cost = 1.99d;
    }
}


public class Latte : ICoffee
{
    public IDictionary<string, int> Ingredients { get; private set; }

    public double Cost { get; private set; }
    public Latte()
    {
        Ingredients = new Dictionary<string, int>
        {
            ["coffee"] = 1,
            ["water"] = 4,
            ["milk"] = 12,
        };
        Cost = 3.99d;
    }
}