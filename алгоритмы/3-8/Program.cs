Console.WriteLine("--- Обработка инцидентов в службе поддержки ---\n");

Console.Write("Введите описание инцидента: ");
string description = Console.ReadLine().ToLower();

string category = "Общий запрос";
string group = "Общая поддержка";

if (description.Contains("сеть") || description.Contains("интернет") || description.Contains("wifi") || description.Contains("vpn"))
{
    category = "Сетевые проблемы";
    group = "Сетевой отдел";
}
else if (description.Contains("пароль") || description.Contains("доступ") || description.Contains("логин") || description.Contains("учётная"))
{
    category = "Управление доступом";
    group = "Отдел безопасности";
}
else if (description.Contains("принтер") || description.Contains("сканер") || description.Contains("монитор") || description.Contains("клавиатура"))
{
    category = "Оборудование";
    group = "Отдел оборудования";
}
else if (description.Contains("программа") || description.Contains("ошибка") || description.Contains("обновление") || description.Contains("установка"))
{
    category = "Программное обеспечение";
    group = "Отдел ПО";
}
else if (description.Contains("почта") || description.Contains("email") || description.Contains("письмо"))
{
    category = "Электронная почта";
    group = "Отдел коммуникаций";
}

int ticketNumber = new Random().Next(10000, 99999);

Console.WriteLine($"\nТикет #{ticketNumber} создан");
Console.WriteLine($"Категория: {category}");
Console.WriteLine($"Ответственная группа: {group}");

string status = "Открыт";
Console.WriteLine($"Статус: {status}");

Console.Write("\nСпециалист берёт тикет в работу? (да/нет): ");
string answer = Console.ReadLine().ToLower();

if (answer != "да")
{
    Console.WriteLine("\nВремя ожидания истекло. Эскалация тикета руководителю группы!");
    Console.WriteLine($"Тикет передан руководителю группы: {group}");
    Console.Write("Руководитель берёт тикет в работу? (да/нет): ");
    answer = Console.ReadLine().ToLower();

    if (answer != "да")
    {
        Console.WriteLine("Тикет эскалирован на уровень выше. Критический инцидент!");
    }
}

status = "В работе";
Console.WriteLine($"\nСтатус изменён: {status}");

Console.Write("Нужна дополнительная информация от пользователя? (да/нет): ");
string needInfo = Console.ReadLine().ToLower();

if (needInfo == "да")
{
    status = "Ожидание ответа";
    Console.WriteLine($"Статус изменён: {status}");
    Console.Write("Введите запрос к пользователю: ");
    Console.ReadLine();

    Console.Write("\nПользователь предоставил информацию? (да/нет): ");
    string userResponse = Console.ReadLine().ToLower();

    if (userResponse == "да")
    {
        Console.Write("Введите ответ пользователя: ");
        Console.ReadLine();
        status = "В работе";
        Console.WriteLine($"\nСтатус изменён: {status}");
    }
    else
    {
        Console.WriteLine("Время ожидания ответа истекло.");
        Console.Write("Закрыть тикет без решения? (да/нет): ");
        string closeTicket = Console.ReadLine().ToLower();

        if (closeTicket == "да")
        {
            status = "Закрыт";
            Console.WriteLine($"\nСтатус изменён: {status}");
            Console.WriteLine("Тикет закрыт без решения из-за отсутствия ответа пользователя.");
            Console.WriteLine($"\n=== Итог по тикету #{ticketNumber} ===");
            Console.WriteLine($"Категория: {category}");
            Console.WriteLine($"Группа: {group}");
            Console.WriteLine($"Финальный статус: {status}");
            return;
        }
        else
        {
            status = "В работе";
            Console.WriteLine($"Статус изменён: {status}");
        }
    }
}

Console.Write("\nИнцидент решён? (да/нет): ");
string resolved = Console.ReadLine().ToLower();

if (resolved == "да")
{
    Console.Write("Введите описание решения: ");
    Console.ReadLine();
    status = "Решён";
    Console.WriteLine($"\nСтатус изменён: {status}");

    Console.Write("\nПользователь подтверждает решение? (да/нет): ");
    string confirmation = Console.ReadLine().ToLower();

    if (confirmation == "да")
    {
        status = "Закрыт";
        Console.WriteLine($"Статус изменён: {status}");

        Console.Write("Оцените качество обслуживания (1-5): ");
        int rating;
        while (!int.TryParse(Console.ReadLine(), out rating) || rating < 1 || rating > 5)
        {
            Console.Write("Введите оценку от 1 до 5: ");
        }

        Console.WriteLine($"\nОценка удовлетворённости: {rating}/5");
        if (rating >= 4)
            Console.WriteLine("Спасибо за высокую оценку!");
        else if (rating == 3)
            Console.WriteLine("Благодарим за обратную связь. Мы постараемся улучшить сервис.");
        else
            Console.WriteLine("Приносим извинения за неудобства. Ваш отзыв передан руководству.");
    }
    else
    {
        status = "В работе";
        Console.WriteLine($"Статус изменён: {status}");
        Console.WriteLine("Тикет возвращён в работу для доработки решения.");
    }
}
else
{
    Console.WriteLine("Специалист продолжает работу над инцидентом.");
}

Console.WriteLine($"\n--- Итог по тикету #{ticketNumber} ---");
Console.WriteLine($"Категория: {category}");
Console.WriteLine($"Группа: {group}");
Console.WriteLine($"Финальный статус: {status}");