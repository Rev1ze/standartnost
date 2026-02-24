DateTime today = new DateTime(2026, 2, 11);
DateTime threeYearsAgo = today.AddYears(-3);

int[] visitIds = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
DateTime[] visitDates = {
    new DateTime(2021, 5, 10),
    new DateTime(2022, 1, 15),
    new DateTime(2020, 3, 20),
    new DateTime(2023, 8, 5),
    new DateTime(2019, 11, 30),
    new DateTime(2024, 6, 12),
    new DateTime(2022, 12, 1),
    new DateTime(2025, 9, 18),
    new DateTime(2020, 7, 22),
    new DateTime(2021, 2, 14)
};
string[] visitPages = {
    "главная", "контакты", "услуги", "о нас", "блог",
    "FAQ", "новости", "поддержка", "каталог", "отзывы"
};

int[] legalCaseVisitIds = { 3, 9 };
bool[] legalCaseOpen = { true, true };

List<int> archivedIds = new List<int>();
List<DateTime> archivedDates = new List<DateTime>();
List<string> archivedPages = new List<string>();
List<string> archivedStatuses = new List<string>();

int archived = 0;
int skippedLegal = 0;
int totalOld = 0;
double freedGB = 0;
double recordSizeGB = 0.005;

Console.WriteLine("--- Запуск ежеквартальной архивации ---");
Console.WriteLine($"Дата запуска: {today:dd.MM.yyyy}");
Console.WriteLine($"Порог: записи старше {threeYearsAgo:dd.MM.yyyy}\n");

for (int i = 0; i < visitIds.Length; i++)
{
    if (visitDates[i] < threeYearsAgo)
    {
        totalOld++;
        Console.WriteLine($"Запись ID: {visitIds[i]}, дата: {visitDates[i]:dd.MM.yyyy} - старше 3 лет");

        bool hasLegal = false;
        for (int j = 0; j < legalCaseVisitIds.Length; j++)
        {
            if (legalCaseVisitIds[j] == visitIds[i] && legalCaseOpen[j])
            {
                hasLegal = true;
                break;
            }
        }

        if (hasLegal)
        {
            Console.WriteLine($"\tПропущена: связана с открытым судебным делом");
            skippedLegal++;
        }
        else
        {
            archivedIds.Add(visitIds[i]);
            archivedDates.Add(visitDates[i]);
            archivedPages.Add(visitPages[i]);
            archivedStatuses.Add("archived");

            visitIds[i] = -1;

            freedGB += recordSizeGB;
            archived++;
            Console.WriteLine($"\t-> Перенесена в Архив_посещений, статус: archived");
        }
    }
}

Console.WriteLine("\n--- Архив посещений ---");
for (int i = 0; i < archivedIds.Count; i++)
{
    Console.WriteLine($"ID: {archivedIds[i]}, дата: {archivedDates[i]:dd.MM.yyyy}, страница: {archivedPages[i]}, статус: {archivedStatuses[i]}");
}

Console.WriteLine("\n--- Отчет администратору ---");
Console.WriteLine($"Всего устаревших записей: {totalOld}");
Console.WriteLine($"Заархивировано: {archived}");
Console.WriteLine($"Исключено (юридические причины): {skippedLegal}");
Console.WriteLine($"Освобождено: {freedGB:F3} ГБ");