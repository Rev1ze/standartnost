Console.WriteLine("--- Восстановления системы ---\n");

Console.WriteLine("[Шаг 1] Триггер: Проверка доступности основного сервера БД...");
Console.Write("Сервер доступен? (да/нет): ");
string serverAvailable = Console.ReadLine().ToLower();

if (serverAvailable == "да")
{
    Console.WriteLine("[Лог] Сервер доступен. Восстановление не требуется.");
    Console.WriteLine("[Лог] Тикет закрыт. Статус: Нет сбоя.");
    return;
}

Console.WriteLine("[Лог] Система мониторинга обнаружила недоступность основного сервера БД.");

Console.WriteLine("\n[Шаг 2] Автоматическое оповещение ответственной команды...");
Console.WriteLine("[Лог] Уведомление отправлено команде DBA и DevOps.");

Console.WriteLine("\n[Шаг 3] Принятие решения: Определение масштаба сбоя...");
Console.Write("Тип сбоя (1 - полный отказ, 2 - повреждение данных): ");
string failureType = Console.ReadLine().Trim();

if (failureType == "1")
{
    Console.WriteLine("[Лог] Обнаружен полный отказ сервера.");
    Console.WriteLine("[Лог] Требуется замена оборудования или перезапуск сервера.");
    Console.Write("Сервер перезапущен успешно? (да/нет): ");
    string restarted = Console.ReadLine().Trim().ToLower();
    if (restarted == "да")
    {
        Console.WriteLine("[Лог] Сервер перезапущен. Система работает.");
        Console.WriteLine("[Лог] Тикет закрыт. Статус: Восстановлено перезапуском.");
        return;
    }
    Console.WriteLine("[Лог] Перезапуск не удался. Переход к восстановлению из резервной копии.");
}
else
{
    Console.WriteLine("[Лог] Обнаружено повреждение данных. Запуск восстановления.");
}

Console.WriteLine("\n[Шаг 4] Восстановление из резервной копии...\n");

Console.WriteLine("\t[4.1] Остановка приложения...");
Console.Write("\tПриложение остановлено? (да/нет): ");
string appStopped = Console.ReadLine().Trim().ToLower();
if (appStopped != "да")
{
    Console.WriteLine("\t[Лог] ОШИБКА: Не удалось остановить приложение. Эскалация инцидента.");
    Console.WriteLine("\t[Лог] Тикет обновлен. Статус: Эскалация.");
    return;
}
Console.WriteLine("\t[Лог] Приложение остановлено.");

Console.WriteLine("\n  [4.2] Поиск последней целостной резервной копии...");
Console.Write("\tВведите дату последней резервной копии (дд.мм.гггг): ");
string backupDate = Console.ReadLine().Trim();
Console.Write("\tРезервная копия целостна? (да/нет): ");
string backupOk = Console.ReadLine().Trim().ToLower();
if (backupOk != "да")
{
    Console.WriteLine("\t[Лог] ОШИБКА: Целостная резервная копия не найдена. Эскалация инцидента.");
    Console.WriteLine("\t[Лог] Тикет обновлен. Статус: Эскалация.");
    return;
}
Console.WriteLine("\t[Лог] Найдена целостная резервная копия от " + backupDate + ".");

Console.WriteLine("\n  [4.3] Развертывание копии на standby-сервере...");
Console.Write("\tРазвертывание завершено успешно? (да/нет): ");
string deployed = Console.ReadLine().Trim().ToLower();
if (deployed != "да")
{
    Console.WriteLine("\t[Лог] ОШИБКА: Развертывание не удалось. Эскалация инцидента.");
    Console.WriteLine("\t[Лог] Тикет обновлен. Статус: Эскалация.");
    return;
}
Console.WriteLine("\t[Лог] Резервная копия развернута на standby-сервере.");

Console.WriteLine("\n  [4.4] Проверка целостности данных тестовым запросом...");
Console.Write("\tТестовый запрос выполнен успешно? (да/нет): ");
string testOk = Console.ReadLine().Trim().ToLower();
if (testOk != "да")
{
    Console.WriteLine("\t[Лог] ОШИБКА: Данные повреждены после восстановления. Эскалация инцидента.");
    Console.WriteLine("\t[Лог] Тикет обновлен. Статус: Эскалация.");
    return;
}
Console.WriteLine("\t[Лог] Целостность данных подтверждена.");

Console.WriteLine("\n  [4.5] Переключение (failover) приложения на новый сервер...");
Console.Write("\tFailover выполнен успешно? (да/нет): ");
string failoverOk = Console.ReadLine().Trim().ToLower();
if (failoverOk != "да")
{
    Console.WriteLine("\t[Лог] ОШИБКА: Failover не удался. Эскалация инцидента.");
    Console.WriteLine("\t[Лог] Тикет обновлен. Статус: Эскалация.");
    return;
}
Console.WriteLine("\t[Лог] Приложение переключено на standby-сервер.");

Console.WriteLine("\n  [4.6] Запуск приложения...");
Console.Write("\tПриложение запущено успешно? (да/нет): ");
string appStarted = Console.ReadLine().Trim().ToLower();
if (appStarted != "да")
{
    Console.WriteLine("\t[Лог] ОШИБКА: Приложение не запустилось. Эскалация инцидента.");
    Console.WriteLine("\t[Лог] Тикет обновлен. Статус: Эскалация.");
    return;
}
Console.WriteLine("\t [Лог] Приложение запущено на новом сервере.");

Console.WriteLine("\n[Шаг 5] Логирование в тикет-системе...");
Console.WriteLine("[Лог] Все шаги зафиксированы в тикет-системе.");
Console.WriteLine("[Лог] Тикет закрыт. Статус: Восстановлено из резервной копии от " + backupDate + ".");
Console.WriteLine("\n=== Процедура восстановления завершена ===");