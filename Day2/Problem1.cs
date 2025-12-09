using System.Diagnostics;

namespace AOC25.Day2;

public static class Problem1
{
    public static void Solve(string inputPath)
    {
        string content = FileUtil.ReadContent(inputPath);
        string[] split = content.Split(',');

        long invalidAdd = 0;

        for (int i = 0; i < split.Length; i++)
        {
            Decompose(
                out short[] digits,
                out int lengthStart,
                out long valueStart,
                out long valueEnd,
                split[i]);

            invalidAdd += SolveRange(
                digits,
                lengthStart,
                valueStart,
                valueEnd);
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
        out short[] outDigits,
        out int outLengthStart,
        out long outValueStart,
        out long outValueEnd,
        string range)
    {
        int separator = range.IndexOf('-');
        int lengthStart = separator;
        int lengthEnd = range.Length - (separator + 1);
        long valueStart = Parse(range, 0, separator);
        long valueEnd = Parse(range, separator + 1, range.Length);

        Debug.Assert(lengthEnd >= lengthStart);
        Debug.Assert(valueEnd >= valueStart);

        string composed = $"{valueStart}-{valueEnd}";
        Debug.Assert(string.Equals(composed, range));

        outLengthStart = lengthStart;
        outValueStart = valueStart;
        outValueEnd = valueEnd;

        outDigits = new short[lengthEnd];
        for (int i = 0; i < lengthEnd; i++)
        {
            outDigits[i] = (short)(valueStart % 10);
            valueStart /= 10;
        }
    }

    private static long SolveRange(
        short[] digits,
        int length,
        long valueStart,
        long valueEnd)
    {
        long invalidAdd = 0;

        for (long value = valueStart; value <= valueEnd; value++)
        {
            if (!IsValid(digits, length))
            {
                invalidAdd += value;
            }
            Increase(ref length, digits);
        }

        return invalidAdd;
    }

    private static void Increase(ref int length, short[] digits)
    {
        bool carry = IncreaseDigit(ref digits[0]);
        for (int i = 1; i < digits.Length; i++)
        {
            if (!carry)
            {
                break;
            }
            carry = IncreaseDigit(ref digits[i]);
            length = Math.Max(i + 1, length);
        }
    }

    private static bool IncreaseDigit(ref short digit)
    {
        digit++;
        if (digit > 9)
        {
            digit = 0;
            return true;
        }
        return false;
    }

    private static bool IsValid(short[] number, int length)
    {
        if (length % 2 != 0)
        {
            return true;
        }
        int halfLength = length / 2;
        for (int i = 0; i < halfLength; i++)
        {
            int j = i + halfLength;
            if (number[i] != number[j])
            {
                return true;
            }
        }
        return false;
    }
}