int greenTime = 5;
int yellowTime = 2;
int redTime = 5;
int nightStart = 23;
int nightEnd = 6;
int pedCooldown = 10;

bool pedRequest = false;
DateTime lastPedGreen = DateTime.MinValue;
string currentPhase = "green";
int phaseTimer = greenTime;

while (true)
{
    int hour = DateTime.Now.Hour;
    bool isNight = hour >= nightStart || hour < nightEnd;

    if (isNight)
    {
        Console.WriteLine("Ночной режим: мигающий жёлтый");
        Console.WriteLine("  Жёлтый ВКЛ");
        Thread.Sleep(1000);
        Console.WriteLine("  Жёлтый ВЫКЛ");
        Thread.Sleep(1000);
        continue;
    }

    Console.WriteLine("Нажмите [T] - транспорт, [P] - пешеход, [Enter] - следующий такт, [Q] - выход");
    Console.Write($"Фаза: {currentPhase} | Таймер: {phaseTimer} | Запрос пешехода: {(pedRequest ? "да" : "нет")} > ");

    string input = Console.ReadLine();

    if (input == "Q" || input == "q")
        break;

    if (input == "T" || input == "t")
    {
        Console.WriteLine("--- Обнаружен общественный транспорт! Специальная фаза ---");
        Console.WriteLine("  Все направления: КРАСНЫЙ");
        Console.WriteLine("  Полоса общественного транспорта: ЗЕЛЁНЫЙ");
        Thread.Sleep(3000);
        Console.WriteLine("  Специальная фаза завершена");
        currentPhase = "green";
        phaseTimer = greenTime;
        continue;
    }

    if (input == "P" || input == "p")
    {
        double secSinceLast = (DateTime.Now - lastPedGreen).TotalSeconds;
        if (secSinceLast < pedCooldown)
        {
            Console.WriteLine($"Запрос пешехода отложен (прошло {secSinceLast:F0} из {pedCooldown} сек)");
        }
        pedRequest = true;
    }

    phaseTimer--;

    if (phaseTimer <= 0)
    {
        if (currentPhase == "green")
        {
            currentPhase = "yellow";
            phaseTimer = yellowTime;
        }
        else if (currentPhase == "yellow")
        {
            currentPhase = "red";
            phaseTimer = redTime;

            if (pedRequest && (DateTime.Now - lastPedGreen).TotalSeconds >= pedCooldown)
            {
                Console.WriteLine(">>> Пешеходный переход: ЗЕЛЁНЫЙ для пешеходов <<<");
                lastPedGreen = DateTime.Now;
                pedRequest = false;
            }
        }
        else if (currentPhase == "red")
        {
            currentPhase = "green";
            phaseTimer = greenTime;
        }
    }

    if (currentPhase == "green")
        Console.WriteLine("  Светофор: ЗЕЛЁНЫЙ");
    else if (currentPhase == "yellow")
        Console.WriteLine("  Светофор: ЖЁЛТЫЙ");
    else
        Console.WriteLine("  Светофор: КРАСНЫЙ");
}