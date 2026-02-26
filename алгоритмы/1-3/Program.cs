int bankMoney = new Random().Next(1000, 350000);
Console.WriteLine($"Количество на счету: {bankMoney}");

int limit = 2500;

Console.Write("Введите сумму для снятия: ");
int withdrawAmount = Convert.ToInt32(Console.ReadLine());

if (withdrawAmount % 100 == 0 && withdrawAmount < bankMoney && withdrawAmount < limit && withdrawAmount > 0)
{
    Console.WriteLine("Заберите деньги");
    bankMoney = bankMoney - withdrawAmount;
    Console.WriteLine($"Новый баланс: {bankMoney}");
}
else
{
    Console.WriteLine("Проверка не пройдена");
}
