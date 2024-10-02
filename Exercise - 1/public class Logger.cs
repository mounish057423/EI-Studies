public class Logger
{
    private static Logger _instance;

    private Logger() { }

    public static Logger GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Logger();
        }
        return _instance;
    }

    public void Log(string message)
    {
        Console.WriteLine($"Log: {message}");
    }
}

// Client
public class Program
{
    public static void Main()
    {
        Logger logger = Logger.GetInstance();
        logger.Log("Application started.");

        Logger anotherLogger = Logger.GetInstance();
        anotherLogger.Log("Application running.");
    }
}
