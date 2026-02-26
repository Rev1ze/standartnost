Console.Write("Введите ID шаблона письма: ");
int templateId = Convert.ToInt32(Console.ReadLine());

Console.Write("Введите сегмент аудитории (например, buyers_last_month): ");
string segment = Console.ReadLine();

string[] emails = { "ivan@mail.ru", "anna@mail.ru", "invalid-email", "petr@mail.ru", "maria@mail.ru" };
string[] names = { "Иван", "Анна", "Сергей", "Пётр", "Мария" };
string[] lastOrders = { "Ноутбук", "Телефон", "Наушники", "Планшет", "Монитор" };
bool[] active = { true, true, true, true, true };

string[] templates = {
    "Здравствуйте, {name}! Спасибо за покупку \"{order}\". Ждём вас снова!",
    "Уважаемый {name}, для вас специальное предложение на основе покупки \"{order}\"!",
    "Привет, {name}! Оставьте отзыв о \"{order}\" и получите скидку!"
};

if (templateId < 0 || templateId >= templates.Length)
{
    Console.WriteLine("Шаблон с таким ID не найден.");
    return;
}

string template = templates[templateId];

int sent = 0;
int errors = 0;
int markedInactive = 0;
Random rnd = new Random();

Console.WriteLine($"\nНачинаем рассылку по сегменту \"{segment}\", шаблон #{templateId}\n");

for (int i = 0; i < emails.Length; i++)
{
    if (!active[i])
    {
        Console.WriteLine($"[ПРОПУСК] {emails[i]} — адрес неактивен");
        continue;
    }

    string message = template.Replace("{name}", names[i]).Replace("{order}", lastOrders[i]);

    Console.WriteLine($"Отправка на {emails[i]}: {message}");

    int result = rnd.Next(0, 10);

    if (emails[i].Contains("invalid") || result == 0)
    {
        Console.WriteLine($"[ОШИБКА] Невалидный email: {emails[i]} — помечен как неактивный");
        active[i] = false;
        markedInactive++;
        errors++;
    }
    else if (result == 1)
    {
        Console.WriteLine("[ОШИБКА] Лимит исчерпан — пауза на 1 час");
        Thread.Sleep(3000);
        Console.WriteLine("Пауза завершена, повторная отправка...");
        Console.WriteLine($"[OK] Письмо отправлено на {emails[i]}");
        sent++;
    }
    else
    {
        Console.WriteLine($"[OK] Письмо отправлено на {emails[i]}");
        sent++;
    }

    Console.WriteLine();
}

Console.WriteLine("----- ОТЧЁТ -----");
Console.WriteLine($"Всего адресов: {emails.Length}");
Console.WriteLine($"Отправлено: {sent}");
Console.WriteLine($"Ошибок: {errors}");
Console.WriteLine($"Помечено неактивными: {markedInactive}");