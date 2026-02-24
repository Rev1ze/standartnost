string[] cities = { "Москва", "Казань", "Новосибирск", "Екатеринбург", "Сочи" };
int[] weatherCodes = { 800, 750, 500, 300, 200 };
double[] windSpeeds = { 10.0, 18.5, 12.3, 16.1, 8.0 };
bool[] apiAvailable = { true, true, false, true, true };

string[] flightFrom = { "Москва", "Казань", "Новосибирск", "Екатеринбург", "Сочи" };
string[] flightTo = { "Казань", "Новосибирск", "Екатеринбург", "Сочи", "Москва" };
string[] statuses = new string[cities.Length];
string[] flags = new string[cities.Length];

Console.WriteLine("Загрузка списка городов на завтра...\n");

for (int i = 0; i < cities.Length; i++)
{
    Console.WriteLine($"Запрос погоды для города: {cities[i]}");

    bool success = false;
    int attempts = 0;
    int maxAttempts = 3;
    int code = 0;
    double wind = 0;

    while (!success && attempts < maxAttempts)
    {
        attempts++;
        Console.WriteLine($"  Попытка {attempts}...");

        if (apiAvailable[i] || attempts > 1)
        {
            code = weatherCodes[i];
            wind = windSpeeds[i];
            success = true;
            Console.WriteLine($"Ответ получен: код погоды = {code}, ветер = {wind} м/с");
        }
        else
        {
            Console.WriteLine("Ошибка: API не ответило.");
            if (attempts < maxAttempts)
            {
                Console.WriteLine("Ожидание перед повторной попыткой...");
                Thread.Sleep(1000);
            }
        }
    }

    if (!success)
    {
        statuses[i] = "Ошибка получения данных";
        flags[i] = "Нет данных";
        Console.WriteLine("  Не удалось получить данные после 3 попыток.\n");
        continue;
    }

    if (code > 700)
        statuses[i] = "Требует ручного перепланирования";
    else
        statuses[i] = "Норма";

    if (wind > 15)
        flags[i] = "Возможная задержка";
    else
        flags[i] = "-";

    Console.WriteLine();
}

Console.WriteLine("--- Таблица Рейсы_Погода ---\n");
Console.WriteLine($"{"Откуда",-16}{"Куда",-16}{"Статус",-36}{"Флаг"}");
Console.WriteLine(new string('-', 85));

for (int i = 0; i < cities.Length; i++)
    Console.WriteLine($"{flightFrom[i],-16}{flightTo[i],-16}{statuses[i],-36}{flags[i]}");