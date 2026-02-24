Console.WriteLine("Симуляция лифта");
Console.WriteLine();

Console.Write("Введите текущий этаж: ");
int currentFloor = Convert.ToInt32(Console.ReadLine());
Console.Write("Введите целевой этаж: ");
int targetFloor = Convert.ToInt32(Console.ReadLine());

if (currentFloor == targetFloor)
{
    Console.WriteLine($"Вы уже на данном этаже");
    Console.WriteLine("Двери открываются...");
    Thread.Sleep(1000);
    Console.WriteLine("Двери закрываются...");
}
else
{
    Console.WriteLine($"Лифт на этаже {currentFloor}");
    Console.WriteLine("Двери открываются...");
    Thread.Sleep(1000);
    Console.WriteLine("Двери закрываются...");
    Thread.Sleep(500);
    
    Console.WriteLine();
    
    MoveElevator(currentFloor, targetFloor);
    
    Console.WriteLine("Двери открываются...");
}

static void MoveElevator(int from, int to)
{
    int direction = from < to ? 1 : -1;
    int current = from;
    
    while (current != to)
    {
        current += direction;
        Thread.Sleep(1400);
        
        Console.WriteLine($"Этаж {current}");
    }
}
