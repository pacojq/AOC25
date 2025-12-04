namespace AOC25.Day1;

public static class Problem1
{
    private const int START_VALUE = 50;
    private const int VALUE_COUNT = 100;

    public static void Solve(string inputPath)
    {
        string[] lines = FileUtil.ReadLines(inputPath);

        int password = 0;
        int value = START_VALUE;

        for (int i = 0; i < lines.Length; i++)
        {
            int delta = ParseRotation(lines[i]);
            value = Modulate(value + delta, VALUE_COUNT);

            if (value == 0)
            {
                password++;
            }
        }

        Console.WriteLine(password);
    }

    private static int Modulate(int n, int max)
    {
        while (n < 0)
        {
            n += max;
        }
        while (n >= max)
        {
            n -= max;
        }
        return n;
    }

    private static int ParseRotation(string line)
    {
        if (string.IsNullOrEmpty(line))
        {
            return 0;
        }
        int value = 0;
        int index = 1;
        while (index < line.Length)
        {
            int digit = (int)(line[index++] - '0'); // convert char to integer
            value = 10 * value + digit;
        }
        return line[0] == 'L' ? -value : value;
    }
}