// Subject
public class Stock
{
    private readonly List<IInvestor> _investors = new List<IInvestor>();
    private decimal _price;

    public Stock(string symbol, decimal price)
    {
        Symbol = symbol;
        _price = price;
    }

    public string Symbol { get; }
    
    public decimal Price
    {
        get => _price;
        set
        {
            _price = value;
            Notify();
        }
    }

    public void Attach(IInvestor investor)
    {
        _investors.Add(investor);
    }

    public void Detach(IInvestor investor)
    {
        _investors.Remove(investor);
    }

    public void Notify()
    {
        foreach (var investor in _investors)
        {
            investor.Update(this);
        }
    }
}

// Observer
public interface IInvestor
{
    void Update(Stock stock);
}

// Concrete Observer
public class Investor : IInvestor
{
    private readonly string _name;

    public Investor(string name)
    {
        _name = name;
    }

    public void Update(Stock stock)
    {
        Console.WriteLine($"Notified {_name} of {stock.Symbol}'s change to {stock.Price:C}");
    }
}

// Client
public class Program
{
    public static void Main()
    {
        Stock stock = new Stock("AAPL", 120.00m);
        Investor investor1 = new Investor("Alice");
        Investor investor2 = new Investor("Bob");

        stock.Attach(investor1);
        stock.Attach(investor2);

        stock.Price = 121.00m;
        stock.Price = 119.50m;
    }
}
