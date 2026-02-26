Console.Write("Страна операции: ");
string operationCountry = Console.ReadLine();

Console.Write("Страна телефона клиента: ");
string phoneCountry = Console.ReadLine();

Console.Write("Сумма транзакции: ");
double amount = Convert.ToDouble(Console.ReadLine());

Console.Write("Средний чек клиента: ");
double averageCheck = Convert.ToDouble(Console.ReadLine());

Console.Write("Час операции (0-23): ");
int hour = Convert.ToInt32(Console.ReadLine());

Console.Write("Первая операция с этим получателем? (да/нет): ");
string isNewRecipient = Console.ReadLine();

int redFlags = 0;

if (operationCountry != phoneCountry)
{
    Console.WriteLine("[!] Геолокация не совпадает");
    redFlags++;
}

if (amount > averageCheck * 0.9)
{
    Console.WriteLine("[!] Сумма превышает 90% от среднего чека");
    redFlags++;
}

if (hour >= 2 && hour < 5)
{
    Console.WriteLine("[!] Операция в ночное время");
    redFlags++;
}

if (isNewRecipient == "да")
{
    Console.WriteLine("[!] Первая операция с новым получателем");
    redFlags++;
}

Console.WriteLine($"Количество подозрительных критериев: {redFlags}");

if (redFlags >= 2)
{
    Console.WriteLine("Транзакция приостановлена!");
    Console.WriteLine("Отправлено push-уведомление. Введите код из SMS для подтверждения.");
    Console.Write("Подтверждаете транзакцию? (да/нет): ");
    string confirmation = Console.ReadLine();

    if (confirmation == "да")
    {
        Console.WriteLine("Транзакция подтверждена и проведена.");
    }
    else
    {
        Console.WriteLine("Карта заблокирована. Обратитесь в банк.");
    }
}
else
{
    Console.WriteLine("Транзакция проведена успешно.");
}