using CoffeeMachine;

ICoffeeMachine coffeeMachine = CoffeeMachine.CoffeeMachine.Instance;
IPaymentProcessor paymentProcessor = new PaymentProcessor();

coffeeMachine.OnOrder += ProcessPayment;

void ProcessPayment(Object? sender, OrderEventArgs args)
{
    args.Paid = paymentProcessor.ProcessPaymentAsync(args.Amount).Result;
}

while (Console.ReadLine() != "x")
{
    if (coffeeMachine.State == MachineState.OutofOrder)
    {
        coffeeMachine.RefillIngridients();
    }
    else
    {
        coffeeMachine.CreateOrder("coffee", "Chintan");
        coffeeMachine.CreateOrder("latte", "Krupa");
    }
}