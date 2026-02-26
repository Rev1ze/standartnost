Random rnd = new Random();
int criticalMinutes = 0;

while (true)
{
    double temp = Math.Round(rnd.NextDouble() * 30 - 5, 1);
    string time = DateTime.Now.ToString("HH:mm:ss");

    Console.WriteLine($"[{time}] Показание датчика: {temp} C");

    if (temp >= 2 && temp <= 8)
    {
        Console.WriteLine($"[{time}] Температура в норме. Записано в лог.");
        criticalMinutes = 0;
    }
    else
    {
        Console.WriteLine($"[{time}] Температура вне нормы! Уведомление менеджеру в Telegram.");

        if (temp > 12 || temp < 0)
        {
            criticalMinutes += 5;
            Console.WriteLine($"[{time}] Критическое отклонение! ({criticalMinutes} мин.)");

            if (criticalMinutes > 10)
            {
                Console.WriteLine($"[{time}] Запуск резервной холодильной установки!");
                Console.WriteLine($"[{time}] Экстренный вызов сервисной службе!");
            }
        }
        else
        {
            criticalMinutes = 0;
        }
    }

    Console.WriteLine();
    Thread.Sleep(3000);
}