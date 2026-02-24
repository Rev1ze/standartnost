int[] newArray = new int[10];
bool isNotFound = true;

for (int i = 0; i < newArray.Length; i++)
{
    newArray[i] = new Random().Next(0, 100);
}

for (int i = 0; i < newArray.Length; i++)
{
    Console.WriteLine(newArray[i]);
}

while (isNotFound)
{
    Console.Write("Введите число: ");
    int guessNum = Convert.ToInt32(Console.ReadLine());

    if (newArray.Contains(guessNum))
    {
        Console.WriteLine($"Число найдено. Индекс: {newArray.IndexOf(guessNum)}");
        isNotFound = false;
    } else
    {
        Console.WriteLine("Число не найдено");
    }
}