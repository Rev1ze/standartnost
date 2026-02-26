Console.Write("Введите число a: ");
int a = Convert.ToInt32(Console.ReadLine());
Console.Write("Введите число b: ");
int b = Convert.ToInt32(Console.ReadLine());
Console.Write("Введите число c: ");
int c = Convert.ToInt32(Console.ReadLine());

double d = (b * b) - 4 * a * c;
Console.WriteLine($"Дискриминант: {d}");

if (d == 0)
{
    double x = -b / (2 * a);
    Console.WriteLine($"x: {x}");
}
else if (d < 0)
{
    Console.WriteLine("Корней нет");
}
else
{
    double x1 = (-b + Math.Sqrt(d)) / (2 * a);
    double x2 = (-b - Math.Sqrt(d)) / (2 * a);

    Console.WriteLine($"x1: {x1}");
    Console.WriteLine($"x2: {x2}");
}
