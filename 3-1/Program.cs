string correctLogin = "admin";
string correctPassword = "1234";
int attempts = 0;

Console.Write("Введите логин: ");
string login = Console.ReadLine();

if (login != correctLogin)
{
    Console.WriteLine("Неверный логин");
}
else
{
    while (attempts < 3)
    {
        Console.Write("Введите пароль: ");
        string password = Console.ReadLine();

        if (password == correctPassword)
        {
            Console.WriteLine("Успешно");
            break;
        }
        else
        {
            attempts++;
            if (attempts >= 3)
            {
                Console.WriteLine("Пользователь заблокирован");
            }
            else
            {
                Console.WriteLine("Неверный пароль");
            }
        }
    }
}