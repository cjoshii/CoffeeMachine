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
        coffeeMachine.CreateOrder(new CoffeeBuilder(Size.medium)
         .AddMilk(MilkType.cream)
         .AddMilk(MilkType.cream)
         .AddSweetner(SweetnerType.suger)
         .AddSweetner(SweetnerType.suger)
         .Build(), "Krupa");

        coffeeMachine.CreateOrder(new CoffeeBuilder(Size.large)
          .AddMilk(MilkType.cream)
          .AddMilk(MilkType.cream)
          .AddSweetner(SweetnerType.suger)
          .AddSweetner(SweetnerType.suger)
         .AddShot().AddShot()
         .Build(), "Chintan");
    }
}