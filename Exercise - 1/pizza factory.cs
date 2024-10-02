// Product
public abstract class Pizza
{
    public abstract void Prepare();
}

// Concrete Products
public class CheesePizza : Pizza
{
    public override void Prepare() => Console.WriteLine("Preparing Cheese Pizza.");
}

public class PepperoniPizza : Pizza
{
    public override void Prepare() => Console.WriteLine("Preparing Pepperoni Pizza.");
}

// Creator
public abstract class PizzaFactory
{
    public abstract Pizza CreatePizza(string type);
}

// Concrete Creator
public class ConcretePizzaFactory : PizzaFactory
{
    public override Pizza CreatePizza(string type)
    {
        return type switch
        {
            "cheese" => new CheesePizza(),
            "pepperoni" => new PepperoniPizza(),
            _ => throw new ArgumentException("Invalid pizza type")
        };
    }
}

// Client
public class Program
{
    public static void Main()
    {
        PizzaFactory pizzaFactory = new ConcretePizzaFactory();
        Pizza pizza = pizzaFactory.CreatePizza("cheese");
        pizza.Prepare();

        Pizza anotherPizza = pizzaFactory.CreatePizza("pepperoni");
        anotherPizza.Prepare();
    }
}
