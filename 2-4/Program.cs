Console.Write("Введите бюджет: ");
double budget = Convert.ToDouble(Console.ReadLine());

Console.Write("Введите количество проектов: ");
int n = Convert.ToInt32(Console.ReadLine());

string[] names = new string[n];
string[] priorities = new string[n];
double[] costs = new double[n];
double[] rois = new double[n];
bool[] hasSpecialists = new bool[n];
double[] scores = new double[n];
bool[] selected = new bool[n];

for (int i = 0; i < n; i++)
{
    Console.WriteLine($"\n--- Проект {i + 1} ---");
    Console.Write("Название: ");
    names[i] = Console.ReadLine();

    Console.Write("Приоритет (высокий/средний/низкий): ");
    priorities[i] = Console.ReadLine().ToLower();

    Console.Write("Стоимость: ");
    costs[i] = Convert.ToDouble(Console.ReadLine());

    Console.Write("ROI (%): ");
    rois[i] = Convert.ToDouble(Console.ReadLine());

    Console.Write("Есть специалисты? (да/нет): ");
    hasSpecialists[i] = Console.ReadLine().ToLower() == "да";

    double priorityScore = 0;
    if (priorities[i] == "высокий")
        priorityScore = 3;
    else if (priorities[i] == "средний")
        priorityScore = 2;
    else
        priorityScore = 1;

    scores[i] = priorityScore * 10 + rois[i];
    if (hasSpecialists[i])
        scores[i] += 20;
}

double remaining = budget;
bool found = true;

Console.WriteLine("\n--- Распределение бюджета ---\n");

while (found)
{
    found = false;
    int best = -1;
    double bestScore = -1;

    for (int i = 0; i < n; i++)
    {
        if (!selected[i] && hasSpecialists[i] && costs[i] <= remaining)
        {
            if (scores[i] > bestScore)
            {
                bestScore = scores[i];
                best = i;
                found = true;
            }
        }
    }

    if (!found)
    {
        for (int i = 0; i < n; i++)
        {
            if (!selected[i] && !hasSpecialists[i] && costs[i] <= remaining)
            {
                if (scores[i] > bestScore)
                {
                    bestScore = scores[i];
                    best = i;
                    found = true;
                }
            }
        }
    }

    if (found)
    {
        selected[best] = true;
        remaining -= costs[best];
        Console.WriteLine($"Выбран: {names[best]} | Приоритет: {priorities[best]} | Стоимость: {costs[best]} | ROI: {rois[best]}% | Специалисты: {(hasSpecialists[best] ? "да" : "нет")} | Балл: {scores[best]}");
    }
}

Console.WriteLine();

bool anySelected = false;
for (int i = 0; i < n; i++)
{
    if (selected[i])
    {
        anySelected = true;
        break;
    }
}

if (!anySelected)
{
    Console.WriteLine("Бюджет слишком мал для любого проекта.");
}
else
{
    Console.WriteLine($"Остаток бюджета: {remaining}");
    Console.WriteLine($"Израсходовано: {budget - remaining}");
}

bool anyLeft = false;
for (int i = 0; i < n; i++)
{
    if (!selected[i])
    {
        if (!anyLeft)
        {
            Console.WriteLine("\nНе вошли в портфель:");
            anyLeft = true;
        }
        Console.WriteLine($"  {names[i]} (стоимость: {costs[i]})");
    }
}