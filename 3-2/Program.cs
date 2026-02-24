Console.Write("Введите название товара: ");
string tovar = Console.ReadLine();

Console.Write("Введите количество товара на складе: ");
int naSklade = Convert.ToInt32(Console.ReadLine());

Console.Write("Введите количество для заказа: ");
int kolvo = Convert.ToInt32(Console.ReadLine());

Console.Write("Введите цену за единицу: ");
double cena = Convert.ToDouble(Console.ReadLine());

Console.Write("Введите скидку в процентах (0 если нет): ");
double skidka = Convert.ToDouble(Console.ReadLine());

double minSumma = 500;

if (kolvo > naSklade)
{
    Console.WriteLine("Отказ: товара нет в достаточном количестве на складе.");
}
else
{
    double summa = kolvo * cena;

    if (skidka > 0)
    {
        summa = summa - summa * skidka / 100;
        Console.WriteLine($"Применена скидка {skidka}%");
    }

    Console.WriteLine($"Итоговая сумма заказа: {summa} руб.");

    if (summa < minSumma)
    {
        Console.WriteLine($"Отказ: сумма заказа меньше минимальной ({minSumma} руб.).");
    }
    else
    {
        Console.WriteLine("Заказ подтверждён!");
        Console.WriteLine($"Товар: {tovar}");
        Console.WriteLine($"Количество: {kolvo}");
        Console.WriteLine($"Сумма к оплате: {summa} руб.");
    }
}