string[] stopWords = { "и", "в", "на", "с", "по", "для", "не", "но", "а", "о", "к", "у", "из", "за", "от", "до", "же", "то", "что", "как", "это", "так", "все", "она", "он", "мы", "вы", "они", "его", "её", "их", "мой", "ваш", "наш", "этот", "тот", "быть", "был", "была", "было", "были", "очень", "уже", "ещё", "еще", "бы", "ли", "да", "нет", "при", "через", "после", "перед", "между", "также", "тоже", "только", "просто", "вот", "там", "тут", "здесь", "где", "когда", "если", "чтобы", "потому", "хотя", "себя", "свой", "свою", "своё", "себе" };

string[] posWords = { "отличн", "хорош", "замечательн", "прекрасн", "супер", "класс", "идеальн", "быстр", "удобн", "качествен", "надежн", "красив", "легк", "приятн", "радует", "нравится", "рекомендую", "доволен", "довольна", "великолепн", "шикарн", "превосходн", "люблю", "лучш", "топ", "огонь", "восторг", "круто", "крут", "норм", "достойн", "яркий", "ярк", "четк", "плавн", "мощн", "стильн" };

string[] negWords = { "плох", "ужасн", "отвратительн", "кошмар", "разочаров", "сломал", "бракован", "медленн", "неудобн", "дорог", "дешев", "некачествен", "тяжел", "жалоб", "проблем", "глючит", "лагает", "тормозит", "треснул", "царапин", "выгорает", "слаб", "шум", "греется", "перегрев", "не работает", "не держит", "разбил", "хрупк", "скрипит", "люфт", "облезл", "выцвет", "зависает", "глюк", "баг", "дефект", "брак" };

string[] entities = { "батарея", "аккумулятор", "зарядка", "экран", "дисплей", "камера", "фото", "звук", "динамик", "доставка", "упаковка", "цена", "стоимость", "дизайн", "корпус", "память", "процессор", "производительность", "клавиатура", "сборка", "качество", "размер", "вес", "материал" };

string[] entityGroups = { "батарея", "батарея", "батарея", "экран", "экран", "камера", "камера", "звук", "звук", "доставка", "доставка", "цена", "цена", "дизайн", "дизайн", "память", "производительность", "производительность", "клавиатура", "сборка", "качество", "размер", "вес", "материал" };

string[] lemmaFrom = { "батареи", "батареей", "батарею", "аккумулятора", "аккумулятору", "аккумулятором", "зарядки", "зарядку", "зарядкой", "экрана", "экрану", "экраном", "дисплея", "дисплею", "дисплеем", "камеры", "камеру", "камерой", "доставки", "доставку", "доставкой", "упаковки", "упаковку", "упаковкой", "цены", "цену", "ценой", "дизайна", "дизайну", "дизайном", "корпуса", "корпусу", "корпусом", "памяти", "памятью", "процессора", "процессору", "процессором", "производительности", "производительностью", "клавиатуры", "клавиатуру", "клавиатурой", "сборки", "сборку", "сборкой", "качества", "качеству", "качеством", "размера", "размеру", "размером", "материала", "материалу", "материалом" };

string[] lemmaTo = { "батарея", "батарея", "батарея", "аккумулятор", "аккумулятор", "аккумулятор", "зарядка", "зарядка", "зарядка", "экран", "экран", "экран", "дисплей", "дисплей", "дисплей", "камера", "камера", "камера", "доставка", "доставка", "доставка", "упаковка", "упаковка", "упаковка", "цена", "цена", "цена", "дизайн", "дизайн", "дизайн", "корпус", "корпус", "корпус", "память", "память", "процессор", "процессор", "процессор", "производительность", "производительность", "клавиатура", "клавиатура", "клавиатура", "сборка", "сборка", "сборка", "качество", "качество", "качество", "размер", "размер", "размер", "материал", "материал", "материал" };

Console.Write("Введите количество отзывов: ");
int n = int.Parse(Console.ReadLine());

string[] reviews = new string[n];
for (int i = 0; i < n; i++)
{
    Console.Write($"Отзыв {i + 1}: ");
    reviews[i] = Console.ReadLine().Trim();
}

string[] uniqueGroups = { "батарея", "экран", "камера", "звук", "доставка", "цена", "дизайн", "память", "производительность", "клавиатура", "сборка", "качество", "размер", "вес", "материал" };
int groupCount = uniqueGroups.Length;
int[] posCount = new int[groupCount];
int[] negCount = new int[groupCount];
int[] neuCount = new int[groupCount];

string[] reviewSentiments = new string[n];

for (int i = 0; i < n; i++)
{
    string text = reviews[i].ToLower();
    string[] words = text.Split(new char[] { ' ', ',', '.', '!', '?', ';', ':', '-', '(', ')', '"', '\'' }, StringSplitOptions.RemoveEmptyEntries);

    int cleanCount = 0;
    for (int w = 0; w < words.Length; w++)
    {
        bool isStop = false;
        for (int s = 0; s < stopWords.Length; s++)
        {
            if (words[w] == stopWords[s])
            {
                isStop = true;
                break;
            }
        }
        if (!isStop)
            cleanCount++;
    }

    string[] cleanWords = new string[cleanCount];
    int ci = 0;
    for (int w = 0; w < words.Length; w++)
    {
        bool isStop = false;
        for (int s = 0; s < stopWords.Length; s++)
        {
            if (words[w] == stopWords[s])
            {
                isStop = true;
                break;
            }
        }
        if (!isStop)
        {
            string word = words[w];
            for (int l = 0; l < lemmaFrom.Length; l++)
            {
                if (word == lemmaFrom[l])
                {
                    word = lemmaTo[l];
                    break;
                }
            }
            cleanWords[ci] = word;
            ci++;
        }
    }

    int posScore = 0;
    int negScore = 0;
    for (int w = 0; w < cleanCount; w++)
    {
        for (int p = 0; p < posWords.Length; p++)
        {
            if (cleanWords[w].Contains(posWords[p]))
            {
                posScore++;
                break;
            }
        }
        for (int ng = 0; ng < negWords.Length; ng++)
        {
            if (cleanWords[w].Contains(negWords[ng]))
            {
                negScore++;
                break;
            }
        }
    }

    string sentiment;
    if (posScore > negScore)
        sentiment = "позитив";
    else if (negScore > posScore)
        sentiment = "негатив";
    else
        sentiment = "нейтраль";

    reviewSentiments[i] = sentiment;

    bool[] foundGroup = new bool[groupCount];
    for (int w = 0; w < cleanCount; w++)
    {
        for (int e = 0; e < entities.Length; e++)
        {
            if (cleanWords[w] == entities[e])
            {
                string grp = entityGroups[e];
                for (int g = 0; g < groupCount; g++)
                {
                    if (uniqueGroups[g] == grp && !foundGroup[g])
                    {
                        foundGroup[g] = true;
                        if (sentiment == "позитив") posCount[g]++;
                        else if (sentiment == "негатив") negCount[g]++;
                        else neuCount[g]++;
                        break;
                    }
                }
                break;
            }
        }
    }
}

Console.WriteLine("\n--- Тональность отзывов ---");
for (int i = 0; i < n; i++)
    Console.WriteLine($"Отзыв {i + 1}: {reviewSentiments[i]}");

Console.WriteLine("\n--- Группировка по сущностям ---");
for (int g = 0; g < groupCount; g++)
{
    int total = posCount[g] + negCount[g] + neuCount[g];
    if (total > 0)
        Console.WriteLine($"{uniqueGroups[g]}: позитив: {posCount[g]}, негатив: {negCount[g]}, нейтраль: {neuCount[g]}");
}

int[] problemOrder = new int[groupCount];
int[] bestOrder = new int[groupCount];
for (int g = 0; g < groupCount; g++)
{
    problemOrder[g] = g;
    bestOrder[g] = g;
}

for (int i = 0; i < groupCount - 1; i++)
{
    for (int j = i + 1; j < groupCount; j++)
    {
        if (negCount[problemOrder[j]] > negCount[problemOrder[i]])
        {
            int tmp = problemOrder[i];
            problemOrder[i] = problemOrder[j];
            problemOrder[j] = tmp;
        }
        if (posCount[bestOrder[j]] > posCount[bestOrder[i]])
        {
            int tmp = bestOrder[i];
            bestOrder[i] = bestOrder[j];
            bestOrder[j] = tmp;
        }
    }
}

Console.WriteLine("\n--- Топ 3 проблемных аспектов ---");
int shown = 0;
for (int i = 0; i < groupCount && shown < 3; i++)
{
    int g = problemOrder[i];
    if (negCount[g] > 0)
    {
        shown++;
        Console.WriteLine($"{shown}. {uniqueGroups[g]} (негативных отзывов: {negCount[g]})");
    }
}
if (shown == 0)
    Console.WriteLine("Проблемных аспектов не найдено.");

Console.WriteLine("\n--- Топ 3 лучших аспектов ---");
shown = 0;
for (int i = 0; i < groupCount && shown < 3; i++)
{
    int g = bestOrder[i];
    if (posCount[g] > 0)
    {
        shown++;
        Console.WriteLine($"{shown}. {uniqueGroups[g]} (позитивных отзывов: {posCount[g]})");
    }
}
if (shown == 0)
    Console.WriteLine("Лучших аспектов не найдено.");