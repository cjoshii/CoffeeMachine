using CoffeeMachine;

public interface ICoffeBuilder
{
    ICoffee Build();
    ICoffeBuilder AddShot();
    ICoffeBuilder AddSweetner(SweetnerType type);
    ICoffeBuilder AddMilk(MilkType type);
}

public class CoffeeBuilder : ICoffeBuilder
{
    private ICoffee _coffee;
    public CoffeeBuilder(Size size)
    {
        this._coffee = new Coffee(size);
    }

    public ICoffeBuilder AddShot()
    {
        this._coffee = new ExpressoShotDecorator(this._coffee);
        return this;
    }

    public ICoffeBuilder AddSweetner(SweetnerType type)
    {
        this._coffee = new SweetnerDecorator(this._coffee, type);
        return this;
    }

    public ICoffeBuilder AddMilk(MilkType type)
    {
        this._coffee = new MilkDecorator(this._coffee, type);
        return this;
    }

    public ICoffee Build()
    {
        return this._coffee;
    }
}