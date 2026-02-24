int[] ids = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
string[] names = { "Ноутбук ASUS",  "Ноутбук Lenovo", "Мышь Logitech", "Клавиатура Razer",
                     "Монитор Samsung","Наушники Sony",  "Ноутбук HP",    "Мышь Razer",
                     "Монитор LG",     "Наушники JBL",   "Коврик для мыши","Веб-камера Logitech" };
string[] cats =    { "Ноутбуки","Ноутбуки","Мыши","Клавиатуры",
                     "Мониторы","Наушники","Ноутбуки","Мыши",
                     "Мониторы","Наушники","Аксессуары","Аксессуары" };
double[] ratings = { 4.5, 4.2, 4.8, 4.1, 4.7, 4.9, 3.9, 4.3, 4.6, 3.8, 4.0, 4.4 };
bool[] inStock = { true, true, true, false, true, true, true, true, false, true, true, true };
int[][] boughtTogether = {
    new[]{3,5},  new[]{4,6},  new[]{11,1}, new[]{3,2},
    new[]{1,9},  new[]{10,2}, new[]{3,5},  new[]{11,4},
    new[]{5,7},  new[]{6,12}, new[]{3,8},  new[]{6,10}
};

Console.Write("Введите ID товара (1-12): ");
int inputId = int.Parse(Console.ReadLine());

int idx = -1;
for (int i = 0; i < ids.Length; i++)
{
    if (ids[i] == inputId)
    {
        idx = i;
        break;
    }
}

if (idx == -1)
{
    Console.WriteLine("Товар не найден.");
    return;
}

Console.WriteLine($"Просматриваемый товар: {names[idx]}");
Console.WriteLine($"Категория: {cats[idx]}");

string category = cats[idx];

List<int> recIndexes = new List<int>();
double[] scores = new double[ids.Length];

for (int i = 0; i < ids.Length; i++)
{
    if (i == idx) continue;
    if (cats[i] == category && ratings[i] >= 4.0)
    {
        recIndexes.Add(i);
        scores[i] = ratings[i] * 2;
    }
}

if (recIndexes.Count < 5)
{
    int[] together = boughtTogether[idx];
    for (int t = 0; t < together.Length; t++)
    {
        int bIdx = -1;
        for (int i = 0; i < ids.Length; i++)
        {
            if (ids[i] == together[t])
            {
                bIdx = i;
                break;
            }
        }
        if (bIdx != -1 && bIdx != idx && !recIndexes.Contains(bIdx))
        {
            recIndexes.Add(bIdx);
            scores[bIdx] = ratings[bIdx];
        }
    }
}

for (int i = recIndexes.Count - 1; i >= 0; i--)
{
    if (!inStock[recIndexes[i]])
    {
        recIndexes.RemoveAt(i);
    }
}

for (int i = 0; i < recIndexes.Count - 1; i++)
{
    for (int j = 0; j < recIndexes.Count - 1 - i; j++)
    {
        if (scores[recIndexes[j]] < scores[recIndexes[j + 1]])
        {
            int tmp = recIndexes[j];
            recIndexes[j] = recIndexes[j + 1];
            recIndexes[j + 1] = tmp;
        }
    }
}

if (recIndexes.Count > 10)
    recIndexes.RemoveRange(10, recIndexes.Count - 10);

Console.WriteLine($"\nРекомендации ({recIndexes.Count}):");
for (int i = 0; i < recIndexes.Count; i++)
{
    int ri = recIndexes[i];
    Console.WriteLine($"{i + 1}. {names[ri]} | {cats[ri]} | Рейтинг: {ratings[ri]} | Очки: {scores[ri]}");
}