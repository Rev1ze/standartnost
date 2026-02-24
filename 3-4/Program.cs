Console.WriteLine("=== Регистрация пациента в поликлинике ===");

string[] polisy = { "1234-5678-9012", "2345-6789-0123", "3456-7890-1234" };
string[] fio = { "Иванов Иван Иванович", "Петрова Мария Сергеевна", "Сидоров Алексей Дмитриевич" };
string[] telefony = { "89001112233", "89004445566", "89007778899" };
string[] vrachi = { "Терапевт Козлова А.В.", "Терапевт Новикова Е.П.", "Терапевт Морозов Д.С." };

string[] dostupnyeVrachi = { "Терапевт Козлова А.В.", "Терапевт Новикова Е.П.", "Терапевт Морозов Д.С.", "Терапевт Белов И.К." };

Console.Write("Введите номер полиса или ФИО пациента: ");
string vvod = Console.ReadLine();

int naydenIndex = -1;

for (int i = 0; i < polisy.Length; i++)
{
    if (polisy[i] == vvod || fio[i].ToLower() == vvod.ToLower())
    {
        naydenIndex = i;
        break;
    }
}

if (naydenIndex != -1)
{
    Console.WriteLine("\nПациент найден:");
    Console.WriteLine($"ФИО: {fio[naydenIndex]}");
    Console.WriteLine($"Полис: {polisy[naydenIndex]}");
    Console.WriteLine($"Телефон: {telefony[naydenIndex]}");
    Console.WriteLine($"Врач: {vrachi[naydenIndex]}");

    Console.Write("\nОбновить контактный телефон? (да/нет): ");
    string otvet = Console.ReadLine();

    if (otvet.ToLower() == "да")
    {
        Console.Write("Введите новый номер телефона: ");
        telefony[naydenIndex] = Console.ReadLine();
        Console.WriteLine($"Телефон обновлён: {telefony[naydenIndex]}");
    }

    Console.Write("Прикрепить к другому врачу? (да/нет): ");
    otvet = Console.ReadLine();

    if (otvet.ToLower() == "да")
    {
        Console.WriteLine("Доступные врачи:");
        for (int i = 0; i < dostupnyeVrachi.Length; i++)
            Console.WriteLine($"{i + 1}. {dostupnyeVrachi[i]}");

        Console.Write("Выберите номер врача: ");
        int nomerVracha = int.Parse(Console.ReadLine());

        if (nomerVracha >= 1 && nomerVracha <= dostupnyeVrachi.Length)
        {
            vrachi[naydenIndex] = dostupnyeVrachi[nomerVracha - 1];
            Console.WriteLine($"Пациент прикреплён к врачу: {vrachi[naydenIndex]}");
        }
        else
        {
            Console.WriteLine("Неверный номер врача.");
        }
    }

    Console.WriteLine("\nДанные пациента обновлены.");
}
else
{
    Console.WriteLine("\nПациент не найден. Создание новой электронной карты.\n");

    Console.Write("Введите ФИО: ");
    string novoeFio = Console.ReadLine();

    Console.Write("Введите дату рождения: ");
    string dataRozhdeniya = Console.ReadLine();

    Console.Write("Введите номер полиса: ");
    string novyyPolis = Console.ReadLine();

    Console.Write("Введите адрес: ");
    string adres = Console.ReadLine();

    Console.Write("Введите номер телефона: ");
    string telefon = Console.ReadLine();

    Console.WriteLine("\nДоступные участковые врачи:");
    for (int i = 0; i < dostupnyeVrachi.Length; i++)
        Console.WriteLine($"{i + 1}. {dostupnyeVrachi[i]}");

    Console.Write("Выберите номер врача: ");
    int vyborVracha = Convert.ToInt32(Console.ReadLine());

    string naznachennyVrach = "";
    if (vyborVracha >= 1 && vyborVracha <= dostupnyeVrachi.Length)
        naznachennyVrach = dostupnyeVrachi[vyborVracha - 1];
    else
        naznachennyVrach = dostupnyeVrachi[0];

    int nomerKarty = 1000 + polisy.Length + 1;

    Console.WriteLine("\n--- Электронная карта создана ---");
    Console.WriteLine($"Номер карты: {nomerKarty}");
    Console.WriteLine($"ФИО: {novoeFio}");
    Console.WriteLine($"Дата рождения: {dataRozhdeniya}");
    Console.WriteLine($"Полис: {novyyPolis}");
    Console.WriteLine($"Адрес: {adres}");
    Console.WriteLine($"Телефон: {telefon}");
    Console.WriteLine($"Участковый врач: {naznachennyVrach}");

    Console.WriteLine("\n--- Временный талон ---");
    Console.WriteLine($"Талон N{nomerKarty}");
    Console.WriteLine($"Пациент: {novoeFio}");
    Console.WriteLine($"Полис: {novyyPolis}");
    Console.WriteLine($"Врач: {naznachennyVrach}");
    Console.WriteLine($"Дата выдачи: {DateTime.Now:dd.MM.yyyy HH:mm}");
    Console.WriteLine("------------------------");
}