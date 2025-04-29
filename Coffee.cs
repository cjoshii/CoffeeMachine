namespace CoffeeMachine;

public interface ICoffee
{
    IDictionary<string, int> GetIngredients();
    double GetCost();
}

public class Coffee : ICoffee
{
    private IDictionary<string, int> Ingredients { get; set; }
    private double Cost { get; set; }
    public Coffee(Size size)
    {
        Ingredients = new Dictionary<string, int>();

        if (size.Equals(Size.small))
        {
            Cost = 1.99d;
            Ingredients["coffee"] = 1;
            Ingredients["water"] = 8;
        }
        else if (size.Equals(Size.medium))
        {
            Cost = 2.49d;
            Ingredients["coffee"] = 2;
            Ingredients["water"] = 12;
        }
        else
        {
            Cost = 2.99d;
            Ingredients["coffee"] = 3;
            Ingredients["water"] = 16;
        }
    }

    public double GetCost()
    {
        return this.Cost;
    }

    public IDictionary<string, int> GetIngredients()
    {
        return this.Ingredients;
    }
}

public class CoffeeDecorator : ICoffee
{
    protected ICoffee _coffee;

    public CoffeeDecorator(ICoffee coffee)
    {
        _coffee = coffee;
    }

    public virtual double GetCost()
    {
        return _coffee.GetCost();
    }

    public virtual IDictionary<string, int> GetIngredients()
    {
        return _coffee.GetIngredients();
    }
}

public class ExpressoShotDecorator : CoffeeDecorator
{
    public ExpressoShotDecorator(ICoffee coffee) : base(coffee)
    {

    }

    public override double GetCost()
    {
        return _coffee.GetCost() + .50;
    }

    public override IDictionary<string, int> GetIngredients()
    {
        if (_coffee.GetIngredients().ContainsKey("shot"))
            _coffee.GetIngredients()["shot"] += 1;
        else
            _coffee.GetIngredients()["shot"] = 1;
        return _coffee.GetIngredients();
    }
}

public class SweetnerDecorator : CoffeeDecorator
{
    public SweetnerDecorator(ICoffee coffee, SweetnerType type) : base(coffee)
    {
        if (_coffee.GetIngredients().ContainsKey(type.ToString()))
            _coffee.GetIngredients()[type.ToString()] += 1;
        else
            _coffee.GetIngredients()[type.ToString()] = 1;
    }

    public override double GetCost()
    {
        return _coffee.GetCost();
    }

    public override IDictionary<string, int> GetIngredients()
    {
        return _coffee.GetIngredients();
    }
}

public class MilkDecorator : CoffeeDecorator
{
    public MilkDecorator(ICoffee coffee, MilkType type) : base(coffee)
    {
        if (_coffee.GetIngredients().ContainsKey(type.ToString()))
            _coffee.GetIngredients()[type.ToString()] += 1;
        else
            _coffee.GetIngredients()[type.ToString()] = 1;
    }

    public override double GetCost()
    {
        return _coffee.GetCost();
    }

    public override IDictionary<string, int> GetIngredients()
    {
        return _coffee.GetIngredients();
    }
}

public enum SweetnerType
{
    suger,
    splenda,
    honey,
    carmel
}

public enum MilkType
{
    milk,
    cream,
    almondmilk,
    soymilk
}

public enum Size
{
    small,
    medium,
    large
}