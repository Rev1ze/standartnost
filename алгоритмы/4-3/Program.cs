Console.Write("Введите количество смартфонов: ");
int n = int.Parse(Console.ReadLine());

double[] basePrice = new double[n];
int[] stock = new int[n];
double[] salesPerDay = new double[n];
string[] names = new string[n];

for (int i = 0; i < n; i++)
{
    Console.Write($"Название смартфона {i + 1}: ");
    names[i] = Console.ReadLine();
    Console.Write($"Базовая цена {names[i]}: ");
    basePrice[i] = double.Parse(Console.ReadLine());
    Console.Write($"Остаток на складе (шт.): ");
    stock[i] = int.Parse(Console.ReadLine());
    Console.Write($"Продажи за последние 24 часа (шт.): ");
    salesPerDay[i] = double.Parse(Console.ReadLine());
}

Console.Write("Цена конкурента 1: ");
double comp1 = double.Parse(Console.ReadLine());
Console.Write("Цена конкурента 2: ");
double comp2 = double.Parse(Console.ReadLine());
Console.Write("Цена конкурента 3: ");
double comp3 = double.Parse(Console.ReadLine());
double avgComp = (comp1 + comp2 + comp3) / 3.0;

Console.WriteLine("\n--- Пересчёт цен (категория: Смартфоны) ---\n");

for (int i = 0; i < n; i++)
{
    double price = basePrice[i];

    if (stock[i] < 10)
        price *= 1.05;

    if (salesPerDay[i] > 5)
        price *= 1.03;
    else if (salesPerDay[i] < 1)
        price *= 0.95;

    double maxPrice = avgComp * 1.10;
    if (price > maxPrice)
        price = maxPrice;

    price = Math.Round(price, 2);

    Console.WriteLine($"{names[i]}: базовая: {basePrice[i]} руб., итоговая: {price} руб.");
}