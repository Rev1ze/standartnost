Console.WriteLine("Сборка робота");

var random = new Random();

bool hasChassis = false;
bool hasMotors = false;
bool hasSensors = false;
bool hasController = false;
bool hasPowerSupply = false;
bool passedQualityCheck = false;

Console.WriteLine("Этап 1: Установка шасси...");
Thread.Sleep(500);
if (random.Next(100) < 90)
{
    hasChassis = true;
    Console.WriteLine("Шасси установлено успешно\n");
}
else
{
    Console.WriteLine("Ошибка: Не удалось установить шасси\n");
    Console.WriteLine("\nСБОРКА РОБОТА НЕ ЗАВЕРШЕНА");
    return;
}

Console.WriteLine("Этап 2: Установка моторов...");
Thread.Sleep(500);
if (random.Next(100) < 90)
{
    hasMotors = true;
    Console.WriteLine("Моторы установлены успешно\n");
}
else
{
    Console.WriteLine("Ошибка: Не удалось установить моторы\n");
    Console.WriteLine("\nСБОРКА РОБОТА НЕ ЗАВЕРШЕНА");
    return;
}

Console.WriteLine("Этап 3: Установка сенсоров...");
Thread.Sleep(500);
if (random.Next(100) < 90)
{
    hasSensors = true;
    Console.WriteLine("Сенсоры установлены успешно\n");
}
else
{
    Console.WriteLine("Ошибка: Не удалось установить сенсоры\n");
    Console.WriteLine("\nСБОРКА РОБОТА НЕ ЗАВЕРШЕНА");
    return;
}

Console.WriteLine("Этап 4: Установка контроллера...");
Thread.Sleep(500);
if (random.Next(100) < 90)
{
    hasController = true;
    Console.WriteLine("Контроллер установлен успешно\n");
}
else
{
    Console.WriteLine("Ошибка: Не удалось установить контроллер\n");
    Console.WriteLine("\nСборка робота не завершена");
    return;
}

Console.WriteLine("Этап 5: Установка блока питания...");
Thread.Sleep(500);
if (random.Next(100) < 90)
{
    hasPowerSupply = true;
    Console.WriteLine("Блок питания установлен успешно\n");
}
else
{
    Console.WriteLine("Ошибка: Не удалось установить блок питания\n");
    Console.WriteLine("\nСБОРКА РОБОТА НЕ ЗАВЕРШЕНА");
    return;
}

Console.WriteLine("\tПроверка качества");
Thread.Sleep(800);
Console.WriteLine("Проверка шасси...");
Console.WriteLine("Проверка моторов...");
Console.WriteLine("Проверка сенсоров...");
Console.WriteLine("Проверка контроллера...");
Console.WriteLine("Проверка блока питания...");
Console.WriteLine("Тестирование системы...");
Thread.Sleep(500);

if (random.Next(100) < 90)
{
    passedQualityCheck = true;
    Console.WriteLine("\nПроверка качества пройдена успешно!");
    Console.WriteLine("\nРобот успешно собран");
}
else
{
    Console.WriteLine("\nОшибка: Робот не прошел проверку качества");
    Console.WriteLine("\nРобот не собран");
}
