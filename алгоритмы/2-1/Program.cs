string[] coverageNames = { "Ковер", "Плитка", "Паркет" };
string[] roomNames = { "База", "Гостиная", "Спальня", "Кухня", "Ванная", "Коридор" };
int[] roomCoverage = { 2, 0, 0, 1, 1, 2 };
bool[] isTileWet = { false, false, false, true, false, false };
int[,] adjacency =
{
    { 0, 0, 0, 0, 0, 1 },
    { 0, 0, 0, 1, 0, 1 },
    { 0, 0, 0, 0, 0, 1 },
    { 0, 1, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 1 },
    { 1, 1, 1, 0, 1, 0 }
};

int roomCount = 6;
bool[] cleaned = new bool[roomCount];
cleaned[0] = true;

int currentRoom = 0;
int battery = 100;
int batteryPerMove = 5;
int batteryPerClean = 10;

Console.WriteLine("Умный навигатор робота-уборщика");
Console.WriteLine($"Начальный заряд батареи: {battery}%");
Console.WriteLine($"Текущее положение: {roomNames[currentRoom]}\n");

while (true)
{
    if (battery <= 20)
    {
        List<int> pathToBase = new List<int>();
        if (currentRoom == 0)
        {
            pathToBase.Add(0);
        }
        else
        {
            Queue<int> queue = new Queue<int>();
            int[] parent = new int[roomCount];
            bool[] visited = new bool[roomCount];
            for (int i = 0; i < roomCount; i++) parent[i] = -1;

            queue.Enqueue(currentRoom);
            visited[currentRoom] = true;

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();
                if (current == 0) break;

                for (int next = 0; next < roomCount; next++)
                {
                    if (adjacency[current, next] == 1 && !visited[next])
                    {
                        visited[next] = true;
                        parent[next] = current;
                        queue.Enqueue(next);
                    }
                }
            }

            int node = 0;
            while (node != -1)
            {
                pathToBase.Insert(0, node);
                node = parent[node];
            }
        }

        Console.WriteLine("\nНизкий заряд. Возврат на базу.");
        for (int i = 1; i < pathToBase.Count; i++)
        {
            battery -= batteryPerMove;
            currentRoom = pathToBase[i];
            Console.WriteLine($"  -> Переход в: {roomNames[currentRoom]} (батарея: {battery}%)");
        }
        Console.WriteLine("Робот вернулся на базу. Зарядка.");
        battery = 100;
        Console.WriteLine($"Батарея заряжена: {battery}%\n");
    }

    int nextRoom = -1;
    for (int i = 0; i < roomCount; i++)
    {
        if (!cleaned[i] && adjacency[currentRoom, i] == 1)
        {
            int batteryAfterClean = battery - batteryPerMove - batteryPerClean;

            List<int> pathCheck = new List<int>();
            if (i == 0)
            {
                pathCheck.Add(0);
            }
            else
            {
                Queue<int> queue = new Queue<int>();
                int[] parent = new int[roomCount];
                bool[] visited = new bool[roomCount];
                for (int k = 0; k < roomCount; k++) parent[k] = -1;

                queue.Enqueue(i);
                visited[i] = true;

                while (queue.Count > 0)
                {
                    int current = queue.Dequeue();
                    if (current == 0) break;

                    for (int next = 0; next < roomCount; next++)
                    {
                        if (adjacency[current, next] == 1 && !visited[next])
                        {
                            visited[next] = true;
                            parent[next] = current;
                            queue.Enqueue(next);
                        }
                    }
                }

                int node = 0;
                while (node != -1)
                {
                    pathCheck.Insert(0, node);
                    node = parent[node];
                }
            }

            int neededBattery = (pathCheck.Count - 1) * batteryPerMove;
            if (batteryAfterClean - neededBattery >= 20)
            {
                if (roomCoverage[i] == 1 && isTileWet[i])
                {
                    Console.WriteLine($"Плитка в комнате '{roomNames[i]}' мокрая. Ожидание высыхания.");
                    isTileWet[i] = false;
                }
                nextRoom = i;
                break;
            }
        }
    }

    if (nextRoom == -1)
    {
        bool moved = false;
        for (int target = 0; target < roomCount; target++)
        {
            if (!cleaned[target])
            {
                for (int mid = 0; mid < roomCount; mid++)
                {
                    if (adjacency[currentRoom, mid] == 1 && adjacency[mid, target] == 1)
                    {
                        if (battery - batteryPerMove >= 20)
                        {
                            battery -= batteryPerMove;
                            currentRoom = mid;
                            Console.WriteLine($"Переход через: {roomNames[mid]} (батарея: {battery}%)");
                            moved = true;
                            break;
                        }
                    }
                }
                if (moved) break;
            }
        }

        if (!moved)
        {
            bool allCleaned = true;
            for (int i = 1; i < roomCount; i++)
            {
                if (!cleaned[i]) allCleaned = false;
            }

            if (allCleaned)
            {
                Console.WriteLine("\nВсе комнаты убраны.");
                break;
            }

            List<int> pathToBase = new List<int>();
            if (currentRoom == 0)
            {
                pathToBase.Add(0);
            }
            else
            {
                Queue<int> queue = new Queue<int>();
                int[] parent = new int[roomCount];
                bool[] visited = new bool[roomCount];
                for (int i = 0; i < roomCount; i++) parent[i] = -1;

                queue.Enqueue(currentRoom);
                visited[currentRoom] = true;

                while (queue.Count > 0)
                {
                    int current = queue.Dequeue();
                    if (current == 0) break;

                    for (int next = 0; next < roomCount; next++)
                    {
                        if (adjacency[current, next] == 1 && !visited[next])
                        {
                            visited[next] = true;
                            parent[next] = current;
                            queue.Enqueue(next);
                        }
                    }
                }

                int node = 0;
                while (node != -1)
                {
                    pathToBase.Insert(0, node);
                    node = parent[node];
                }
            }

            Console.WriteLine("\nНизкий заряд. Возврат на базу.");
            for (int i = 1; i < pathToBase.Count; i++)
            {
                battery -= batteryPerMove;
                currentRoom = pathToBase[i];
                Console.WriteLine($"  -> Переход в: {roomNames[currentRoom]} (батарея: {battery}%)");
            }
            Console.WriteLine("Робот вернулся на базу. Зарядка.");
            battery = 100;
            Console.WriteLine($"Батарея заряжена: {battery}%\n");
        }

        continue;
    }

    battery -= batteryPerMove;
    currentRoom = nextRoom;
    Console.WriteLine($"\nПереход в: {roomNames[currentRoom]} ({coverageNames[roomCoverage[currentRoom]]})");
    Console.WriteLine($"Батарея после перехода: {battery}%");

    string method;
    if (roomCoverage[currentRoom] == 0) method = "сухая уборка (ковер)";
    else if (roomCoverage[currentRoom] == 1) method = "влажная уборка (плитка)";
    else if (roomCoverage[currentRoom] == 2) method = "деликатная уборка (паркет)";
    else method = "стандартная уборка";

    Console.WriteLine($"Уборка: {method}");
    battery -= batteryPerClean;
    cleaned[currentRoom] = true;
    Console.WriteLine($"Батарея после уборки: {battery}%");
}

if (currentRoom != 0)
{
    List<int> pathToBase = new List<int>();
    if (currentRoom == 0)
    {
        pathToBase.Add(0);
    }
    else
    {
        Queue<int> queue = new Queue<int>();
        int[] parent = new int[roomCount];
        bool[] visited = new bool[roomCount];
        for (int i = 0; i < roomCount; i++) parent[i] = -1;

        queue.Enqueue(currentRoom);
        visited[currentRoom] = true;

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            if (current == 0) break;

            for (int next = 0; next < roomCount; next++)
            {
                if (adjacency[current, next] == 1 && !visited[next])
                {
                    visited[next] = true;
                    parent[next] = current;
                    queue.Enqueue(next);
                }
            }
        }

        int node = 0;
        while (node != -1)
        {
            pathToBase.Insert(0, node);
            node = parent[node];
        }
    }

    Console.WriteLine("\nВозвращаемся на базу.");
    for (int i = 1; i < pathToBase.Count; i++)
    {
        battery -= batteryPerMove;
        currentRoom = pathToBase[i];
        Console.WriteLine($"  -> {roomNames[currentRoom]} (батарея: {battery}%)");
    }
}

Console.WriteLine("\n=== Уборка завершена ===");
Console.WriteLine($"Финальный заряд батареи: {battery}%");
