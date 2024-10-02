// Component
public abstract class FileSystemComponent
{
    public string Name { get; }

    protected FileSystemComponent(string name)
    {
        Name = name;
    }

    public abstract void Display();
}

// Leaf
public class File : FileSystemComponent
{
    public File(string name) : base(name) { }

    public override void Display()
    {
        Console.WriteLine(Name);
    }
}

// Composite
public class Directory : FileSystemComponent
{
    private readonly List<FileSystemComponent> _children = new List<FileSystemComponent>();

    public Directory(string name) : base(name) { }

    public void Add(FileSystemComponent component)
    {
        _children.Add(component);
    }

    public override void Display()
    {
        Console.WriteLine($"{Name}/");
        foreach (var child in _children)
        {
            child.Display();
        }
    }
}

// Client
public class Program
{
    public static void Main()
    {
        Directory root = new Directory("root");
        Directory folder1 = new Directory("folder1");
        File file1 = new File("file1.txt");
        File file2 = new File("file2.txt");

        folder1.Add(file1);
        root.Add(folder1);
        root.Add(file2);

        root.Display();
    }
}
