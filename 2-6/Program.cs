Console.Write("Введите возраст: ");
int age = int.Parse(Console.ReadLine());

Console.Write("Введите ежемесячный доход: ");
double income = double.Parse(Console.ReadLine());

Console.Write("Введите ежемесячные траты: ");
double expenses = double.Parse(Console.ReadLine());

Console.Write("Введите сумму сбережений: ");
double savings = double.Parse(Console.ReadLine());

Console.Write("Введите опыт инвестирования (лет): ");
int experience = int.Parse(Console.ReadLine());

Console.Write("Введите цель (подушка безопасности / покупка жилья / пенсия): ");
string goal = Console.ReadLine().Trim().ToLower();

double freeFunds = income - expenses;
Console.WriteLine($"\nСвободные средства: {freeFunds} руб./мес.");

if (freeFunds <= 0)
{
    Console.WriteLine("Ваши траты превышают доход. Сначала сократите расходы.");
    return;
}

string riskProfile;
if (age < 30 && experience >= 2)
    riskProfile = "высокий";
else if (age < 30 && experience < 2)
    riskProfile = "средний";
else if (age >= 30 && age < 50 && experience >= 3)
    riskProfile = "средний";
else if (age >= 30 && age < 50 && experience < 3)
    riskProfile = "низкий";
else
    riskProfile = "низкий";

Console.WriteLine($"Риск-профиль: {riskProfile}");

double safetyMinimum = expenses * 6;
Console.WriteLine($"Минимальная подушка безопасности: {safetyMinimum} руб.");

if (savings < safetyMinimum)
{
    Console.WriteLine("Подушка безопасности недостаточна!");
    Console.WriteLine("Цель перенаправлена на формирование подушки безопасности.");
    goal = "подушка безопасности";
    double monthsNeeded = Math.Ceiling((safetyMinimum - savings) / freeFunds);
    Console.WriteLine($"Для накопления подушки потребуется {monthsNeeded} мес.");
}

int deposit = 0, funds = 0, stocks = 0, bonds = 0;

if (goal == "подушка безопасности")
{
    if (riskProfile == "высокий")
    { deposit = 50; funds = 20; stocks = 10; bonds = 20; }
    else if (riskProfile == "средний")
    { deposit = 60; funds = 15; stocks = 5; bonds = 20; }
    else
    { deposit = 70; funds = 10; stocks = 0; bonds = 20; }
}
else if (goal == "покупка жилья")
{
    if (riskProfile == "высокий")
    { deposit = 30; funds = 25; stocks = 20; bonds = 25; }
    else if (riskProfile == "средний")
    { deposit = 40; funds = 20; stocks = 15; bonds = 25; }
    else
    { deposit = 50; funds = 15; stocks = 5; bonds = 30; }
}
else if (goal == "пенсия")
{
    double inflationRate = 0.06;
    int yearsToRetirement = 65 - age;
    if (yearsToRetirement < 1) yearsToRetirement = 1;
    double inflationMultiplier = Math.Pow(1 + inflationRate, yearsToRetirement);
    Console.WriteLine($"\nДо пенсии: {yearsToRetirement} лет");
    Console.WriteLine($"Коэффициент инфляции за {yearsToRetirement} лет: {inflationMultiplier:F2}");
    Console.WriteLine($"Текущие траты {expenses} руб. с учётом инфляции составят {expenses * inflationMultiplier:F0} руб./мес.");

    if (riskProfile == "высокий")
    { deposit = 15; funds = 25; stocks = 35; bonds = 25; }
    else if (riskProfile == "средний")
    { deposit = 25; funds = 25; stocks = 20; bonds = 30; }
    else
    { deposit = 35; funds = 20; stocks = 10; bonds = 35; }
}
else
{
    Console.WriteLine("Неизвестная цель.");
    return;
}

Console.WriteLine($"\nРаспределение активов для цели \"{goal}\":");
Console.WriteLine($"Вклад: \t {deposit}% ({freeFunds * deposit / 100:F0} руб.)");
Console.WriteLine($"Фонды: \t {funds}% ({freeFunds * funds / 100:F0} руб.)");
Console.WriteLine($"Акции: \t {stocks}% ({freeFunds * stocks / 100:F0} руб.)");
Console.WriteLine($"Облигации: \t {bonds}% ({freeFunds * bonds / 100:F0} руб.)");