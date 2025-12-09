namespace AOC25.Day2;

public static class Problem2
{
    public static void Solve(string inputPath)
    {
        string content = FileUtil.ReadContent(inputPath);
        string[] split = content.Split(',');

        long invalidAdd = 0;

        for (int i = 0; i < split.Length; i++)
        {
            Decompose(
                out long valueStart,
                out long valueEnd,
                split[i]);

            invalidAdd += SolveRange(valueStart, valueEnd);
        }

        Console.WriteLine(invalidAdd);
    }

    private static long Parse(string number, int indexStart, int indexEnd)
    {
        long n = 0;
        for (int i = indexStart; i < indexEnd; i++)
        {
            n *= 10;
            n += (long)(number[i] - '0');
        }
        return n;
    }

    private static void Decompose(
        out long outValueStart,
        out long outValueEnd,
        string range)
    {
        int separator = range.IndexOf('-');
        long valueStart = Parse(range, 0, separator);
        long valueEnd = Parse(range, separator + 1, range.Length);
        outValueStart = valueStart;
        outValueEnd = valueEnd;
    }

    private static long SolveRange(long valueStart, long valueEnd)
    {
        long invalidAdd = 0;
        for (long value = valueStart; value <= valueEnd; value++)
        {
            if (!IsValid(value))
            {
                invalidAdd += value;
            }
        }
        return invalidAdd;
    }

    private static bool IsValid(long number)
    {
        int log = 1;
        long pow = 10;
        while (pow < number)
        {
            if (!IsValid(number, pow, log))
            {
                return false;
            }
            pow *= 10;
            log++;
        }
        return true;
    }

    private static bool IsValid(long number, long pow, int log)
    {
        long divisor = number % pow;
        if (Math.Log10(divisor) < log - 1) // substring starts with 0
        {
            return true;
        }

        while (number > 0)
        {
            if (number % pow != divisor)
            {
                return true;
            }
            number /= pow;
        }
        return false;
    }
}