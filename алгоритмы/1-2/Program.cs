using Sharprompt;

int[] array = new int[10];

for (int i = 0; i < array.Length; i++)
{
    array[i] = new Random().Next(0, 100);
}

DisplayArray(array);

string sortType = Prompt.Select("Выберите сортировку", ["Bubble sort", "Selection sort"]);

if (sortType == "Bubble sort")
{
    array = BubbleSort(array);
    DisplayArray(array);
} else if (sortType == "Selection sort")
{
    array = SelectionSort(array);
    DisplayArray(array);
}

static void DisplayArray(int[] array)
{
    for (int i = 0; i < array.Length; i++)
    {
        Console.WriteLine(array[i]);
    }
}

static int[] BubbleSort(int[] array)
{
    int t;
    int j = 0;

    for (int p = 0; p <= array.Length - 2; p++)
    {
        for (int i = 0; i <= array.Length - 2; i++)
        {
            if (array[i] > array[i + 1])
            {
                t = array[i + 1];
                array[i + 1] = array[i];
                array[i] = t;
                j++;
            }
        }
    }
    Console.WriteLine(j);
    return array;
}

static int[] SelectionSort(int[] array)
{
    int n = array.Length;

    for (int i = 0; i < n - 1; i++)
    {
        int minIndex = i;

        for (int j = i + 1; j < n; j++)
        {
            if (array[j] < array[minIndex])
            {
                minIndex = j;
            }
        }

        int temp = array[i];
        array[i] = array[minIndex];
        array[minIndex] = temp;
    }

    return array;
}