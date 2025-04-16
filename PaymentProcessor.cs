namespace CoffeeMachine;

public interface IPaymentProcessor
{
    Task<bool> ProcessPaymentAsync(double amount);
}

public class PaymentProcessor : IPaymentProcessor
{
    public async Task<bool> ProcessPaymentAsync(double amount)
    {
        var results = new bool[] { true, false };
        await Task.Delay(500).ConfigureAwait(false);
        Console.WriteLine($"Procesing payment of ${amount}");
        return results[new Random().Next(0, 2)];
    }
}