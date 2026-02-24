string orderStatus = "Ожидает оплаты";
string orderId = "ORD-" + new Random().Next(1000, 9999);

Console.WriteLine("--- Онлайн-оплата ---");
Console.WriteLine($"Заказ: {orderId}");
Console.WriteLine($"Статус заказа: {orderStatus}");

bool done = false;

while (!done)
{
    Console.Write("\nИнициировать оплату? (да/нет): ");
    string input = Console.ReadLine().Trim().ToLower();

    if (input != "да")
    {
        Console.WriteLine("Оплата отменена.");
        break;
    }

    Console.WriteLine("\nФормирование запроса к Платёжному шлюзу...");
    string gatewayRequestId = "PAY-" + new Random().Next(100000, 999999);
    Console.WriteLine($"Запрос отправлен. ID транзакции: {gatewayRequestId}");

    string paymentLink = $"https://pay.gateway.ru/form/{gatewayRequestId}";
    Console.WriteLine($"Получена ссылка на Платёжную форму: {paymentLink}");

    Console.WriteLine("\nПользователь перенаправлен на Платёжную форму...");
    Console.WriteLine("Ожидание возврата пользователя...");

    Console.WriteLine("\nПользователь вернулся. Проверка статуса Платёжа у шлюза...");

    int result = new Random().Next(1, 4);
    string paymentStatus;

    if (result == 1)
        paymentStatus = "успех";
    else if (result == 2)
        paymentStatus = "неудача";
    else
        paymentStatus = "ожидание";

    Console.WriteLine("Статус Платёжа: " + paymentStatus);

    if (paymentStatus == "успех")
    {
        orderStatus = "Оплачен";
        Console.WriteLine($"\nСтатус заказа изменен на: {orderStatus}");
        string receiptNumber = "CHK-" + new Random().Next(100000, 999999);
        Console.WriteLine($"Чек отправлен покупателю. Номер чека: {receiptNumber}");
        done = true;
    }
    else if (paymentStatus == "неудача")
    {
        orderStatus = "Ожидает оплаты";
        Console.WriteLine("\nПлатёж не прошел.");
        Console.WriteLine($"Статус заказа: {orderStatus}");
        Console.WriteLine("Предлагаем выбрать другой способ оплаты и попробовать снова.");
    }
    else
    {
        Console.WriteLine("\nПлатёж в обработке. Повторная проверка...");

        int retry = new Random().Next(1, 3);

        if (retry == 1)
        {
            orderStatus = "Оплачен";
            Console.WriteLine("Платёж подтвержден.");
            Console.WriteLine($"Статус заказа изменен на: {orderStatus}");
            string receiptNumber = "CHK-" + new Random().Next(100000, 999999);
            Console.WriteLine($"Чек отправлен покупателю. Номер чека: {receiptNumber}");
            done = true;
        }
        else
        {
            orderStatus = "Ожидает оплаты";
            Console.WriteLine("Платёж не подтвержден.");
            Console.WriteLine($"Статус заказа: {orderStatus}");
            Console.WriteLine("Предлагаем выбрать другой способ оплаты и попробовать снова.");
        }
    }
}

Console.WriteLine("\n--- Итог ---");
Console.WriteLine($"Заказ: {orderId}");
Console.WriteLine($"Статус: {orderStatus}");