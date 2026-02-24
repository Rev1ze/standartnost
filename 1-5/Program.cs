using Sharprompt;

int score = 0;

string question1 = Prompt.Select("Вопрос 1", ["Ответ 1", "Ответ 2", "Ответ 3"]);
string question2 = Prompt.Select("Вопрос 2", ["Ответ 1", "Ответ 2", "Ответ 3"]);
string question3 = Prompt.Select("Вопрос 3", ["Ответ 1", "Ответ 2", "Ответ 3"]);

if (question1 == "Ответ 1") score++;
if (question2 == "Ответ 2") score++;
if (question3 == "Ответ 3") score++;

switch (score)
{
    case 0:
        Console.WriteLine("Нужно повторить материал");
        break;

    case 1:
        Console.WriteLine("Удволитворительно");
        break;

    case 2:
        Console.WriteLine("Хорошо");
        break;

    case 3:
        Console.WriteLine("Отлично!");
        break;
}