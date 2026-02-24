string[] words = { "КОМПЬЮТЕР", "МЫШЬ", "КЛАВИАТУРА", "ПРОЦЕССОР", "ВИДЕОКАРТА" };

string guessWord = words[new Random().Next(words.Length)];
int attempts = guessWord.Length;
List<char> guessedLetters = [];
bool isPlaying = true;

Console.WriteLine("Угадайте слово, вводя буквы по одной.");
Console.WriteLine();

while (isPlaying)
{
    string displayWord = string.Join("", guessWord.Select(c => guessedLetters.Contains(c) ? c : '_'));

    Console.WriteLine($"Слово: {displayWord}");

    Console.Write("Введите букву: ");
    string input = Console.ReadLine().ToUpper();

    char letter = input[0];

    if (guessedLetters.Contains(letter))
    {
        Console.WriteLine("Вы уже вводили эту букву");
    }
    else if (guessWord.Contains(letter))
    {
        guessedLetters.Add(letter);
    }
    else
    {
        Console.WriteLine("Такой буквы нет в слове");
    }

    Console.WriteLine();

    if (attempts == 0)
    {
        isPlaying = false;
        if (displayWord != guessWord)
        {
            Console.WriteLine($"Вы проиграли. Загаданное слово было: {guessWord}");
        }
        else
        {
            Console.WriteLine("Поздравляем! Вы угадали слово!");
        }
    }
    attempts--;
}


