string[] devices = { "light", "thermostat", "blinds", "oven", "door" };
string[] lightActions = { "on", "off" };
string[] thermostatActions = { "set" };
string[] blindsActions = { "open", "close" };
string[] ovenActions = { "on", "off" };
string[] doorActions = { "lock", "unlock" };

List<string> log = new List<string>();
Queue<string> commands = new Queue<string>();

Console.WriteLine("--- Интерпретатор команд умного дома ---");
Console.WriteLine("Формат: устройство.действие или устройство.действие(параметр)");
Console.WriteLine("Введите команды (пустая строка - выполнить все):\n");

while (true)
{
    Console.Write("> ");
    string input = Console.ReadLine()!;
    if (string.IsNullOrWhiteSpace(input))
        break;
    commands.Enqueue(input);
}

int hour = DateTime.Now.Hour;

Console.WriteLine($"\nТекущее время: {DateTime.Now:HH:mm}");
Console.WriteLine("--- Обработка очереди команд ---\n");

while (commands.Count > 0)
{
    string raw = commands.Dequeue();
    log.Add($"[ПОЛУЧЕНО] {raw}");

    string device = "";
    string action = "";
    string param = "";

    int paramStart = raw.IndexOf('(');
    int paramEnd = raw.IndexOf(')');
    string commandPart = raw;

    if (paramStart != -1 && paramEnd != -1 && paramEnd > paramStart)
    {
        param = raw.Substring(paramStart + 1, paramEnd - paramStart - 1);
        commandPart = raw.Substring(0, paramStart);
    }

    int dotIndex = commandPart.IndexOf('.');
    if (dotIndex == -1)
    {
        Console.WriteLine($"[ОШИБКА] Неверный формат команды: {raw}");
        log.Add($"[ОШИБКА] Неверный формат: {raw}");
        continue;
    }

    device = commandPart.Substring(0, dotIndex);
    action = commandPart.Substring(dotIndex + 1);

    bool deviceExists = false;
    for (int i = 0; i < devices.Length; i++)
    {
        if (devices[i] == device)
        {
            deviceExists = true;
            break;
        }
    }

    if (!deviceExists)
    {
        Console.WriteLine($"[ОШИБКА] Устройство '{device}' не найдено.");
        log.Add($"[ОШИБКА] Устройство не найдено: {device}");
        continue;
    }

    string[] allowedActions = device switch
    {
        "light" => lightActions,
        "thermostat" => thermostatActions,
        "blinds" => blindsActions,
        "oven" => ovenActions,
        "door" => doorActions,
        _ => Array.Empty<string>()
    };

    bool actionValid = false;
    for (int i = 0; i < allowedActions.Length; i++)
    {
        if (allowedActions[i] == action)
        {
            actionValid = true;
            break;
        }
    }

    if (!actionValid)
    {
        Console.WriteLine($"[ОШИБКА] Действие '{action}' недопустимо для '{device}'.");
        log.Add($"[ОШИБКА] Недопустимое действие: {device}.{action}");
        continue;
    }

    if (device == "thermostat" && action == "set")
    {
        if (!int.TryParse(param.Replace("%", ""), out int temp) || temp < 5 || temp > 35)
        {
            Console.WriteLine($"[ОШИБКА] Температура должна быть от 5 до 35: {param}");
            log.Add($"[ОШИБКА] Недопустимая температура: {param}");
            continue;
        }
    }

    if (device == "blinds" && action == "open")
    {
        if (!int.TryParse(param.Replace("%", ""), out int pct) || pct < 0 || pct > 100)
        {
            Console.WriteLine($"[ОШИБКА] Процент открытия должен быть от 0 до 100: {param}");
            log.Add($"[ОШИБКА] Недопустимый процент: {param}");
            continue;
        }
    }

    if (device == "oven" && action == "on" && (hour >= 23 || hour < 6))
    {
        Console.WriteLine($"[БЕЗОПАСНОСТЬ] Включение духовки запрещено в ночное время ({hour}:00).");
        log.Add($"[БЕЗОПАСНОСТЬ] Блокировка: духовка ночью");
        continue;
    }

    if (device == "door" && action == "unlock" && (hour >= 0 && hour < 5))
    {
        Console.WriteLine($"[БЕЗОПАСНОСТЬ] Разблокировка двери запрещена ночью ({hour}:00).");
        log.Add($"[БЕЗОПАСНОСТЬ] Блокировка: разблокировка двери ночью");
        continue;
    }

    Random rng = new Random();
    bool success = rng.Next(100) < 90;

    if (success)
    {
        string details = param != "" ? $" с параметром '{param}'" : "";
        Console.WriteLine($"[УСПЕХ] {device}.{action}{details} - выполнено.");
        log.Add($"[УСПЕХ] {device}.{action}({param})");
    }
    else
    {
        Console.WriteLine($"[ОШИБКА] {device}.{action} - устройство не ответило.");
        log.Add($"[ОШИБКА] Нет ответа: {device}.{action}");
    }
}

Console.WriteLine("\n--- Журнал действий ---");
for (int i = 0; i < log.Count; i++)
{
    Console.WriteLine($"  {i + 1}. {log[i]}");
}