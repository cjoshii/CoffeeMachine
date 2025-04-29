namespace CoffeeMachine;
public interface ICoffeeMachine
{
    MachineState State { get; }
    IDictionary<string, int> Ingredients { get; }
    event EventHandler<OrderEventArgs> OnOrder;
    bool CreateOrder(ICoffee coffee, string name);
    void RefillIngridients();
}

public class CoffeeMachine : ICoffeeMachine, IDisposable
{
    public MachineState State { get; private set; }
    public IDictionary<string, int> Ingredients { get; private set; }
    public event EventHandler<OrderEventArgs>? OnOrder;
    public static ICoffeeMachine Instance => _instance.Value;
    private Queue<(ICoffee, string)> OrderQueue { get; set; }
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly Task _queueProcessorTask;
    private static readonly Lazy<ICoffeeMachine> _instance = new(() => new CoffeeMachine());
    private readonly static Lock _lock = new();

    private CoffeeMachine()
    {
        State = MachineState.Idle;

        OrderQueue = new Queue<(ICoffee, string)>();

        Ingredients = new Dictionary<string, int>
        {
            ["water"] = 200,
            ["milk"] = 150,
            ["coffee"] = 30,
            ["suger"] = 50,
            ["carmel"] = 20,
            ["honey"] = 20,
            ["splenda"] = 50,
            ["cream"] = 20,
            ["soymilk"] = 10,
            ["almondmilk"] = 10,
            ["shot"] = 20
        };

        _cancellationTokenSource = new CancellationTokenSource();
        _queueProcessorTask = Task.Run(ProcessQueueAsync);
    }

    public bool CreateOrder(ICoffee coffee, string name)
    {
        var args = new OrderEventArgs(coffee.GetCost());
        OnOrder?.Invoke(this, args);

        if (!args.Paid)
        {
            Console.WriteLine("Payment failed");
            return false;
        }
        lock (_lock)
        {
            OrderQueue.Enqueue((coffee, name));
        }
        Console.WriteLine("Order placed successfully");
        return true;
    }

    private async Task ProcessQueueAsync()
    {
        while (!_cancellationTokenSource.Token.IsCancellationRequested)
        {
            (ICoffee? order, string? name) = (null, null);
            lock (_lock)
            {
                if (OrderQueue.Count > 0 && State == MachineState.Idle)
                {
                    (order, name) = OrderQueue.Dequeue();
                    State = MachineState.Brewing;
                }
            }

            if (order != null && name != null)
            {
                await Task.Run(() => Brew(order, name));
            }
            await Task.Delay(100, _cancellationTokenSource.Token);
        }
    }

    private void Brew(ICoffee coffee, string name)
    {
        Console.WriteLine($"preparing order for {name}");
        var ingredients = coffee.GetIngredients();

        foreach (var ingredient in ingredients)
        {
            if (Ingredients[ingredient.Key] < ingredient.Value)
            {
                Console.WriteLine($"Refill {ingredient.Key}");
                State = MachineState.OutofOrder;
                return;
            }
        }

        foreach (var ingredient in ingredients)
        {
            Ingredients[ingredient.Key] -= ingredient.Value;
        }

        Console.WriteLine($"Order is ready for {name}");
        State = MachineState.Idle;
    }

    public void Dispose()
    {
        _cancellationTokenSource.Cancel();
        _queueProcessorTask.Wait();
        _cancellationTokenSource.Dispose();
    }

    public void RefillIngridients()
    {
        Ingredients["water"] = 200;
        Ingredients["milk"] = 150;
        Ingredients["coffee"] = 30;
        Ingredients["suger"] = 50;
        Ingredients["carmel"] = 20;
        Ingredients["honey"] = 20;
        Ingredients["splenda"] = 50;
        Ingredients["cream"] = 20;
        Ingredients["soymilk"] = 10;
        Ingredients["almondmilk"] = 10;
        Ingredients["shot"] = 20;
        State = MachineState.Idle;
    }
}

public enum MachineState
{
    Idle,
    Brewing,
    OutofOrder
}

public class OrderEventArgs(double amount) : EventArgs
{
    public bool Paid { get; set; }
    public double Amount { get; private set; } = amount;
}