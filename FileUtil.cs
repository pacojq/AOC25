namespace AOC25;

public static class FileUtil
{
    public static string[] ReadLines(string path)
    {
        string absolutePath = Path.Combine(AppContext.BaseDirectory, path);
        return File.ReadAllLines(absolutePath);
    }
}