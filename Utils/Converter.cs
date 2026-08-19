namespace Maker;

internal static class Converter
{
    internal static string ToScreamingSnakeCase(string name)
    {
        string result = string.Empty;
        bool isFirstCharIteration = true;

        foreach (var nameChar in name)
        {
            if (!isFirstCharIteration && char.IsUpper(nameChar))
                result += '_';

            result += char.ToUpper(nameChar);
            isFirstCharIteration = false;
        }

        return result;
    }
}