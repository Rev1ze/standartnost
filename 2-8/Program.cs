int gold = 100, wood = 80, food = 50;
int warriors = 5, archers = 3, workers = 4;
int enemyWarriors = 6, enemyArchers = 4, enemyBase = 100;
int baseHP = 100, towers = 1, barracks = 1, farms = 1;
int minGarrison = 2;
int turn = 0;
Random rnd = new Random();

while (enemyBase > 0 && baseHP > 0 && turn < 30)
{
    turn++;
    Console.WriteLine($"--- Ход {turn} ---");
    Console.WriteLine($"Золото: {gold} Дерево: {wood} Еда: {food}");
    Console.WriteLine($"Воины: {warriors} Лучники: {archers} Рабочие: {workers}");
    Console.WriteLine($"Враг — воины: {enemyWarriors} лучники: {enemyArchers} база: {enemyBase} HP");
    Console.WriteLine($"Наша база: {baseHP} HP, башни: {towers}, казармы: {barracks}, фермы: {farms}");

    int ourPower = warriors * 3 + archers * 2 + towers * 4;
    int enemyPower = enemyWarriors * 3 + enemyArchers * 2;

    string strategy;
    if (ourPower > enemyPower * 1.5)
        strategy = "атака";
    else if (ourPower < enemyPower * 0.8)
        strategy = "оборона";
    else
        strategy = "развитие";

    Console.WriteLine($"Стратегия: {strategy}");
    Console.WriteLine($"Наша сила: {ourPower} | Сила врага: {enemyPower}");

    if (strategy == "атака")
    {
        int availWarriors = warriors - minGarrison;
        if (availWarriors < 0) availWarriors = 0;
        int availArchers = archers;

        int strikeForce = availWarriors * 3 + availArchers * 2;

        if (strikeForce > 0)
        {
            bool targetBase = enemyWarriors + enemyArchers <= 2;

            if (!targetBase && (enemyArchers > 0 || enemyWarriors > 0))
            {
                string target = enemyArchers < enemyWarriors ? "лучников" : "воинов";
                Console.WriteLine($"Атакуем вражеских {target} группой: {availWarriors} воинов, {availArchers} лучников");

                int damage = strikeForce;
                int enemyHit = enemyPower / 2;

                if (target == "лучников")
                {
                    int killed = damage / 3;
                    if (killed > enemyArchers) killed = enemyArchers;
                    enemyArchers -= killed;
                    Console.WriteLine($"Уничтожено вражеских лучников: {killed}");
                }
                else
                {
                    int killed = damage / 4;
                    if (killed > enemyWarriors) killed = enemyWarriors;
                    enemyWarriors -= killed;
                    Console.WriteLine($"Уничтожено вражеских воинов: {killed}");
                }

                int ourLoss = enemyHit / 5;
                if (ourLoss > availWarriors) ourLoss = availWarriors;
                warriors -= ourLoss;
                Console.WriteLine($"Потеряно наших воинов: {ourLoss}");
            }
            else
            {
                Console.WriteLine($"Штурм вражеской базы! Группа: {availWarriors} воинов, {availArchers} лучников");
                int dmg = strikeForce * 2;
                enemyBase -= dmg;
                if (enemyBase < 0) enemyBase = 0;
                int ourLoss = rnd.Next(0, availWarriors / 2 + 1);
                warriors -= ourLoss;
                Console.WriteLine($"Нанесено урона базе: {dmg}, потери: {ourLoss}");
            }
        }
        else
        {
            Console.WriteLine("Недостаточно юнитов для атаки, копим силы");
        }
    }
    else if (strategy == "оборона")
    {
        Console.WriteLine("Укрепляем оборону базы");

        int gW = workers / 2;
        int wW = workers - gW;
        int gI = gW * 15;
        int wI = wW * 10;
        gold += gI;
        wood += wI;
        Console.WriteLine($"Рабочие добыли — золото: +{gI} дерево: +{wI}");

        if (wood >= 30 && gold >= 20)
        {
            towers++;
            wood -= 30;
            gold -= 20;
            Console.WriteLine($"Построена башня! Всего башен: {towers}");
        }

        if (gold >= 25 && food >= 10 && barracks >= 1)
        {
            warriors++;
            gold -= 25;
            food -= 10;
            Console.WriteLine($"Обучен воин для обороны! Всего воинов: {warriors}");
        }

        if (gold >= 20 && food >= 10 && barracks >= 1)
        {
            archers++;
            gold -= 20;
            food -= 10;
            Console.WriteLine($"Обучен лучник для обороны! Всего лучников: {archers}");
        }
    }
    else
    {
        Console.WriteLine("Развиваем экономику");

        int goldWorkers = workers / 2;
        int woodWorkers = workers - goldWorkers;
        int goldIncome = goldWorkers * 15;
        int woodIncome = woodWorkers * 10;
        gold += goldIncome;
        wood += woodIncome;
        Console.WriteLine($"Рабочие добыли — золото: +{goldIncome} дерево: +{woodIncome}");

        if (gold >= 40 && wood >= 30 && farms < 3)
        {
            farms++;
            gold -= 40;
            wood -= 30;
            food += 30;
            Console.WriteLine($"Построена ферма! Всего ферм: {farms}, еда +{30}");
        }
        else if (gold >= 50 && wood >= 40 && barracks < 2)
        {
            barracks++;
            gold -= 50;
            wood -= 40;
            Console.WriteLine($"Построена казарма! Всего казарм: {barracks}");
        }
        else if (gold >= 30 && food >= 5)
        {
            workers++;
            gold -= 30;
            food -= 5;
            Console.WriteLine($"Обучен рабочий! Всего рабочих: {workers}");
        }

        if (gold >= 25 && food >= 10 && barracks >= 1)
        {
            warriors++;
            gold -= 25;
            food -= 10;
            Console.WriteLine($"Обучен воин. Всего воинов: {warriors}");
        }
    }

    food += farms * 5;
    Console.WriteLine($"Фермы дали еды: +{farms * 5}");

    if (warriors < minGarrison && gold >= 25 && food >= 10)
    {
        int need = minGarrison - warriors;
        for (int i = 0; i < need && gold >= 25 && food >= 10; i++)
        {
            warriors++;
            gold -= 25;
            food -= 10;
        }
        Console.WriteLine($"Гарнизон пополнен до минимума: {warriors} воинов");
    }

    if (enemyWarriors + enemyArchers > 0 && rnd.Next(100) < 30)
    {
        int raidPower = rnd.Next(1, enemyWarriors + enemyArchers + 1) * 2;
        Console.WriteLine($"Враг атакует нашу базу! Сила набега: {raidPower}");
        int defense = warriors * 3 + archers * 2 + towers * 4;
        if (defense >= raidPower)
        {
            int loss = raidPower / 6;
            if (loss > warriors) loss = warriors;
            warriors -= loss;
            Console.WriteLine($"Атака отбита! Потери: {loss} воинов");
        }
        else
        {
            int dmg = raidPower - defense;
            baseHP -= dmg;
            int loss = rnd.Next(1, warriors / 2 + 2);
            if (loss > warriors) loss = warriors;
            warriors -= loss;
            Console.WriteLine($"База повреждена на {dmg} HP! Потери: {loss} воинов");
        }
    }

    if (rnd.Next(100) < 40 && (enemyWarriors + enemyArchers) < 12)
    {
        int spawn = rnd.Next(1, 3);
        if (rnd.Next(2) == 0)
        {
            enemyWarriors += spawn;
            Console.WriteLine($"Враг обучил {spawn} воинов");
        }
        else
        {
            enemyArchers += spawn;
            Console.WriteLine($"Враг обучил {spawn} лучников");
        }
    }

    Console.WriteLine();
}

Console.WriteLine("--- Итог ---");
if (enemyBase <= 0)
    Console.WriteLine($"Победа! Вражеская база уничтожена на ходу {turn}");
else if (baseHP <= 0)
    Console.WriteLine($"Поражение! Наша база уничтожена на ходу {turn}");
else
    Console.WriteLine($"Ничья — достигнут лимит ходов ({turn})");