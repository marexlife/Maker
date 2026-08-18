namespace Maker;

internal static class Converter
{
    internal static string ToScreamingSnakeCase(string name)
    {
        string result = string.Empty;
        bool isFirstCharIteration = true;

        foreach (var nameChar in name)
        {
            if (!isFirstCharIteration)
            {
                var currentCharIsUpper = char.IsUpper(nameChar);

                if (currentCharIsUpper) result.Append('_');
            }

            isFirstCharIteration = false;
            result.Append(char.ToUpper(nameChar));
        }

        return result;
    }
}