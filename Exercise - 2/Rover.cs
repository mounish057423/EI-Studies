
// Receiver
public class Rover
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public char Direction { get; private set; }
    private readonly Grid _grid;

    public Rover(int x, int y, char direction, Grid grid)
    {
        X = x;
        Y = y;
        Direction = direction;
        _grid = grid;
    }

    public void MoveForward()
    {
        int newX = X, newY = Y;
        switch (Direction)
        {
            case 'N': newY++; break;
            case 'S': newY--; break;
            case 'E': newX++; break;
            case 'W': newX--; break;
        }
        if (_grid.IsWithinBounds(newX, newY) && !_grid.HasObstacle(newX, newY))
        {
            X = newX;
            Y = newY;
        }
    }

    public void TurnLeft()
    {
        Direction = Direction switch
        {
            'N' => 'W',
            'W' => 'S',
            'S' => 'E',
            'E' => 'N',
            _ => Direction
        };
    }

    public void TurnRight()
    {
        Direction = Direction switch
        {
            'N' => 'E',
            'E' => 'S',
            'S' => 'W',
            'W' => 'N',
            _ => Direction
        };
    }

    public string GetStatus()
    {
        return $"Rover is at ({X}, {Y}) facing {Direction}.";
    }
}

// Command Interface
public interface ICommand
{
    void Execute();
}

// Concrete Command for moving forward
public class MoveCommand : ICommand
{
    private readonly Rover _rover;

    public MoveCommand(Rover rover)
    {
        _rover = rover;
    }

    public void Execute()
    {
        _rover.MoveForward();
    }
}

// Concrete Command for turning left
public class TurnLeftCommand : ICommand
{
    private readonly Rover _rover;

    public TurnLeftCommand(Rover rover)
    {
        _rover = rover;
    }

    public void Execute()
    {
        _rover.TurnLeft();
    }
}

// Concrete Command for turning right
public class TurnRightCommand : ICommand
{
    private readonly Rover _rover;

    public TurnRightCommand(Rover rover)
    {
        _rover = rover;
    }

    public void Execute()
    {
        _rover.TurnRight();
    }
}

// Invoker
public class RoverController
{
    private readonly List<ICommand> _commands = new List<ICommand>();

    public void AddCommand(ICommand command)
    {
        _commands.Add(command);
    }

    public void ExecuteCommands()
    {
        foreach (var command in _commands)
        {
            command.Execute();
        }
    }
}

// Composite Pattern Implementation
public class Grid
{
    private readonly int _width;
    private readonly int _height;
    private readonly HashSet<(int, int)> _obstacles = new HashSet<(int, int)>();

    public Grid(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public void AddObstacle(int x, int y)
    {
        _obstacles.Add((x, y));
    }

    public bool IsWithinBounds(int x, int y)
    {
        return x >= 0 && x < _width && y >= 0 && y < _height;
    }

    public bool HasObstacle(int x, int y)
    {
        return _obstacles.Contains((x, y));
    }
}

// Main Program
public class Program
{
    public static void Main(string[] args)
    {
        Grid grid = new Grid(10, 10);
        grid.AddObstacle(2, 5);
        grid.AddObstacle(8, 5);

        Rover rover = new Rover(0, 0, 'N', grid);
        RoverController controller = new RoverController();

        string commands = "MMRMMLM";

        foreach (char command in commands)
        {
            switch (command)
            {
                case 'M':
                    controller.AddCommand(new MoveCommand(rover));
                    break;
                case 'L':
                    controller.AddCommand(new TurnLeftCommand(rover));
                    break;
                case 'R':
                    controller.AddCommand(new TurnRightCommand(rover));
                    break;
            }
        }

        controller.ExecuteCommands();

        Console.WriteLine(rover.GetStatus());
    }
}