Console.Write("Введите дату (дд.мм.гггг): ");
string date = Console.ReadLine();

Console.Write("Введите время (чч:мм): ");
string timeInput = Console.ReadLine();

Console.Write("Введите количество гостей: ");
int guests = Convert.ToInt32(Console.ReadLine());

bool[] tables = { true, false, true, true, true, false, true, true, true, true };
bool[] windowTables = { true, false, true, false, false, false, false, true, false, true };
int[] tableCapacity = { 4, 2, 6, 4, 8, 2, 10, 6, 10, 12 };

Console.Write("У вас в профиле отмечено предпочтение 'у окна'? (да/нет): ");
string windowPref = Console.ReadLine().ToLower();

Console.WriteLine("\nДоступные столы:");

if (guests > 8)
{
    Console.WriteLine("Гостей больше 8 — поиск пар соседних столов для объединения...");
    bool found = false;
    for (int i = 0; i < tables.Length - 1; i++)
    {
        if (tables[i] && tables[i + 1] && tableCapacity[i] + tableCapacity[i + 1] >= guests)
        {
            Console.WriteLine($"Столы {i + 1} и {i + 2} можно объединить (вместимость: {tableCapacity[i] + tableCapacity[i + 1]})");
            found = true;
        }
    }
    if (!found)
    {
        Console.WriteLine("Нет доступных пар столов для объединения.");
        return;
    }
}
else
{
    bool found = false;
    for (int i = 0; i < tables.Length; i++)
    {
        if (tables[i] && tableCapacity[i] >= guests)
        {
            if (windowPref == "да" && windowTables[i])
            {
                Console.WriteLine($"Стол {i + 1} (у окна, вместимость: {tableCapacity[i]}) — рекомендуется");
                found = true;
            }
            else if (windowPref != "да")
            {
                string window = windowTables[i] ? ", у окна" : "";
                Console.WriteLine($"Стол {i + 1} (вместимость: {tableCapacity[i]}{window})");
                found = true;
            }
        }
    }
    if (!found && windowPref == "да")
    {
        Console.WriteLine("Столов у окна нет. Показываем все доступные:");
        for (int i = 0; i < tables.Length; i++)
        {
            if (tables[i] && tableCapacity[i] >= guests)
            {
                Console.WriteLine($"Стол {i + 1} (вместимость: {tableCapacity[i]})");
                found = true;
            }
        }
    }
    if (!found)
    {
        Console.WriteLine("Нет доступных столов.");
        return;
    }
}

Console.Write("\nВведите номер стола (или первого стола из пары): ");
int chosenTable = Convert.ToInt32(Console.ReadLine());
Console.WriteLine($"Вы выбрали стол {chosenTable}.");

DateTime visitTime = DateTime.Parse(date + " " + timeInput);
TimeSpan diff = visitTime - DateTime.Now;

if (diff.TotalHours < 2 && diff.TotalHours > 0)
{
    Console.Write("До визита менее 2 часов. Желаете сделать предзаказ блюд? (да/нет): ");
    string preorder = Console.ReadLine().ToLower();
    if (preorder == "да")
    {
        Console.WriteLine("Предзаказ оформлен.");
    }
}

Console.Write("Желаете внести предоплату? (да/нет): ");
string prepay = Console.ReadLine().ToLower();

if (prepay == "да")
{
    Console.Write("Введите номер карты: ");
    string card = Console.ReadLine();
    Console.Write("Введите сумму депозита: ");
    double deposit = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine($"С карты {card} списан депозит {deposit} руб.");
    Console.WriteLine("Бронирование подтверждено с предоплатой.");
}
else
{
    Console.WriteLine("Бронирование подтверждено без предоплаты.");
    Console.WriteLine("Напоминание будет отправлено за 2 часа до визита.");
}

Console.WriteLine($"\nИтого: дата {date}, время {timeInput}, гостей: {guests}, стол: {chosenTable}.");