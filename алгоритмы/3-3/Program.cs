Console.WriteLine("--- Согласование отпуска ---");

Console.Write("Введите ФИО сотрудника: ");
string sotrudnik = Console.ReadLine();

Console.Write("Введите количество запрашиваемых дней отпуска: ");
int zapros = Convert.ToInt32(Console.ReadLine());

Console.Write("Введите остаток дней отпуска: ");
int ostatok = Convert.ToInt32(Console.ReadLine());

if (ostatok < zapros)
{
    Console.WriteLine("Отказ: недостаточно дней отпуска.");
    Console.WriteLine($"Остаток: {ostatok}, запрошено: {zapros}");
}
else
{
    Console.WriteLine("Остаток дней достаточен. Заявка направляется руководителю.");

    Console.Write("Руководитель на месте? (да/нет): ");
    string rukovoditelNaMeste = Console.ReadLine();

    string soglasuyushiy;

    if (rukovoditelNaMeste == "да")
    {
        soglasuyushiy = "руководитель";
    }
    else
    {
        soglasuyushiy = "заместитель";
        Console.WriteLine("Руководитель отсутствует. Заявка перенаправлена заместителю.");
    }

    Console.Write($"Решение ({soglasuyushiy}) - согласовать? (да/нет): ");
    string reshenie = Console.ReadLine();

    if (reshenie == "да")
    {
        Console.WriteLine("Заявка согласована.");
        Console.WriteLine("Уведомление отправлено в отдел кадров.");
        Console.WriteLine($"Уведомление отправлено сотруднику: {sotrudnik}");
        Console.WriteLine($"Отпуск на {zapros} дней оформлен.");
    }
    else
    {
        Console.WriteLine("Заявка отклонена.");
        Console.WriteLine($"Уведомление отправлено сотруднику: {sotrudnik}");
    }
}