Console.Write("Введите количество показаний: ");
int n = int.Parse(Console.ReadLine());

string[] times = new string[n];
string[] places = new string[n];
string[] descriptions = new string[n];
bool[] contradictory = new bool[n];

for (int i = 0; i < n; i++)
{
    Console.WriteLine($"\nПоказание {i + 1}:");
    Console.Write("Время (например 10:00): ");
    times[i] = Console.ReadLine().Trim();
    Console.Write("Место: ");
    places[i] = Console.ReadLine().Trim();
    Console.Write("Описание человека: ");
    descriptions[i] = Console.ReadLine().Trim();
}

for (int i = 0; i < n; i++)
{
    for (int j = i + 1; j < n; j++)
    {
        if (times[i] == times[j] && places[i] == places[j] && descriptions[i] != descriptions[j])
        {
            contradictory[i] = true;
            contradictory[j] = true;
        }
    }
}

Console.WriteLine("\n=== Противоречивые показания ===");
bool hasContradictions = false;
for (int i = 0; i < n; i++)
{
    if (contradictory[i])
    {
        Console.WriteLine($"Показание {i + 1}: время={times[i]}, место={places[i]}, описание={descriptions[i]}");
        hasContradictions = true;
    }
}
if (!hasContradictions)
    Console.WriteLine("Противоречий не найдено.");

int coreCount = 0;
for (int i = 0; i < n; i++)
    if (!contradictory[i])
        coreCount++;

string[] coreTimes = new string[coreCount];
string[] corePlaces = new string[coreCount];
string[] coreDescriptions = new string[coreCount];
int idx = 0;
for (int i = 0; i < n; i++)
{
    if (!contradictory[i])
    {
        coreTimes[idx] = times[i];
        corePlaces[idx] = places[i];
        coreDescriptions[idx] = descriptions[i];
        idx++;
    }
}

Console.WriteLine("\n=== Ядро непротиворечивых показаний ===");
if (coreCount == 0)
{
    Console.WriteLine("Нет непротиворечивых показаний.");
}
else
{
    for (int i = 0; i < coreCount; i++)
        Console.WriteLine($"Время={coreTimes[i]}, Место={corePlaces[i]}, Описание={coreDescriptions[i]}");
}

for (int i = 0; i < coreCount - 1; i++)
{
    for (int j = i + 1; j < coreCount; j++)
    {
        if (string.Compare(coreTimes[i], coreTimes[j]) > 0)
        {
            string tmp = coreTimes[i]; coreTimes[i] = coreTimes[j]; coreTimes[j] = tmp;
            tmp = corePlaces[i]; corePlaces[i] = corePlaces[j]; corePlaces[j] = tmp;
            tmp = coreDescriptions[i]; coreDescriptions[i] = coreDescriptions[j]; coreDescriptions[j] = tmp;
        }
    }
}

Console.WriteLine("\n=== Вероятный маршрут подозреваемого ===");
if (coreCount == 0)
{
    Console.WriteLine("Невозможно построить маршрут.");
}
else
{
    for (int i = 0; i < coreCount; i++)
        Console.WriteLine($"{i + 1}. [{coreTimes[i]}] {corePlaces[i]}");
}

Console.WriteLine("\n=== Белые пятна (пропуски во времени) ===");
bool hasGaps = false;
for (int i = 0; i < coreCount - 1; i++)
{
    int h1 = int.Parse(coreTimes[i].Split(':')[0]);
    int m1 = int.Parse(coreTimes[i].Split(':')[1]);
    int h2 = int.Parse(coreTimes[i + 1].Split(':')[0]);
    int m2 = int.Parse(coreTimes[i + 1].Split(':')[1]);
    int diff = (h2 * 60 + m2) - (h1 * 60 + m1);
    if (diff > 60)
    {
        Console.WriteLine($"Пропуск между {coreTimes[i]} и {coreTimes[i + 1]} ({diff} мин.) - требуется расследование.");
        hasGaps = true;
    }
}
if (!hasGaps)
    Console.WriteLine("Белых пятен не обнаружено.");