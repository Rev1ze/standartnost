while (true)
{
    DateTime now = DateTime.Now;
    DateTime target = new DateTime(now.Year, now.Month, now.Day, 23, 0, 0);

    if (now > target)
        target = target.AddDays(1);

    TimeSpan wait = target - now;
    Console.WriteLine($"Ожидание до 23:00. Осталось: {wait}");
    Thread.Sleep(wait);

    Console.WriteLine($"--- Начало формирования отчёта: {DateTime.Now} ---");

    Random rnd = new Random();
    int salesCount = rnd.Next(10, 50);
    double[] sales = new double[salesCount];
    bool hasGaps = false;

    for (int i = 0; i < salesCount; i++)
    {
        sales[i] = Math.Round(rnd.NextDouble() * 5000 + 100, 2);
    }

    Console.WriteLine($"1. Собраны данные о продажах: {salesCount} записей");

    for (int i = 0; i < salesCount; i++)
    {
        if (sales[i] <= 0)
        {
            hasGaps = true;
            break;
        }
    }

    if (hasGaps)
        Console.WriteLine("2. Проверка целостности: обнаружены пропуски!");
    else
        Console.WriteLine("2. Проверка целостности: данные корректны");

    double revenue = 0;
    for (int i = 0; i < salesCount; i++)
        revenue += sales[i];

    double avgCheck = Math.Round(revenue / salesCount, 2);
    revenue = Math.Round(revenue, 2);

    Console.WriteLine($"3. Выручка: {revenue} руб., Средний чек: {avgCheck} руб.");

    string fileName = "Report_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
    Console.WriteLine($"4. Сформирован файл: {fileName}");

    string email = "manager@company.ru";
    Console.WriteLine($"5. Отчёт отправлен на: {email}");

    string logEntry = $"{DateTime.Now} | Отчёт сформирован | Продаж: {salesCount} | Выручка: {revenue} | Средний чек: {avgCheck}";
    File.AppendAllText("log.txt", logEntry + Environment.NewLine);
    Console.WriteLine("6. Запись в лог-файл выполнена");

    Console.WriteLine("--- Отчёт завершён ---\n");
}