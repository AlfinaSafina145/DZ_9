using System;
using System.Xml.Linq;
interface IFigure
{
    void MoveHorizontal(double dx);
    void MoveVertical(double dy);
    void ChangeColor(string newColor);
    bool IsVisible();
    void Print();
}
abstract class Figure : IFigure
{
    protected string name;
    protected string color;
    protected bool visible;
    protected double x;
    protected double y;

    public Figure(string name, double x, double y, string color)
    {
        this.name = name;
        this.x = x;
        this.y = y;
        this.color = color;
        this.visible = true;
    }
    public void MoveHorizontal(double dx)
    {
        x += dx;
        Console.WriteLine($"Сдвиг по горизонтали на {dx}: теперь x = {x}");
    }

    public void MoveVertical(double dy)
    {
        y += dy;
        Console.WriteLine($"Сдвиг по вертикали на {dy}: теперь y = {y}");
    }

    public void ChangeColor(string newColor)
    {
        color = newColor;
        Console.WriteLine($"Цвет изменён на {color}");
    }
    public void Hide()
    {
        visible = false;
        Console.WriteLine($"{name} скрыта");
    }

    public void Show()
    {
        visible = true;
        Console.WriteLine($"{name} снова видима");
    }


    public bool IsVisible()
    {
        return visible;
    }
    public virtual void Print()
    {
        string state;
        if (visible)
        {
            state = "видимый";
        }
        else
        {
            state = "невидимый";
        }
        Console.WriteLine(name);
        Console.WriteLine($"Цвет: {color}, Состояние: {state}, Позиция: ({x}, {y})");
    }
    public abstract double Area();

}
class Point : Figure
{
    public Point(string name,double x, double y, string color) : base(name, x, y, color) { }
    public Point(double x, double y,string  color) : this("Точка", x, y, "черный") { }

    public override double Area()
    {
        return 0;
    }
}
class Circle : Point
{
    private double radius;

    public Circle(double x, double y, double radius, string color) : base("Окружность", x, y, color)
    {
        this.radius = radius;
    }

    public override void Print()
    {
        base.Print();
        Console.WriteLine($"Радиус: {radius}, Площадь: {Math.Round(Area(), 2)}");
    }

    public override double Area()
    {
        return Math.PI * radius * radius;
    }
}
class Rectangle : Point
{
    private double width;
    private double height;

    public Rectangle(double x, double y, double width, double height, string color)
        : base("Прямоугльник",x, y, color)
    {
        this.width = width;
        this.height = height;
    }

    public override void Print()
    {
        base.Print();
        Console.WriteLine($"Ширина: {width}, Высота: {height}, Площадь: {Area()}");
    }

    public override double Area()
    {
        return width * height;
    }
}
class Program
{
    static void Main()
    {
        Figure[] figures =
        {
            new Point(1, 2, "красный"),
            new Circle(3, 4, 5, "синий"),
            new Rectangle(0, 0, 10, 20, "зеленый")
        };

        foreach (Figure f in figures)
        {
            f.Print();
            f.MoveHorizontal(2);
            f.MoveVertical(3);
            f.ChangeColor("желтый");
            f.Hide();
            Console.WriteLine($"Видима? {f.IsVisible()}");
            f.Show();
            Console.WriteLine($"Видима? {f.IsVisible()}");
            f.Print();

            Console.WriteLine();
        }
    }
}