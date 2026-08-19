namespace Maker.Utils;

internal static class NameConverter
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

    internal static string ToPascalCase(string name)
    {
        string result = string.Empty;
        char? previousChar = null;

        foreach (var nameChar in name)
        {
            if (IsSeparatorChar(nameChar))
            {
                goto loopIterationEnd;
            }

            if (previousChar == null)
            {
                result += char.ToUpper(nameChar);

                goto loopIterationEnd;
            }

            result += IsSeparatorChar(previousChar.Value) ?
                char.ToUpper(previousChar.Value) :
                char.ToLower(previousChar.Value);

        loopIterationEnd:
            previousChar = nameChar;
        }

        return result;
    }

    static bool IsSeparatorChar(char charInQuestion)
    {
        return charInQuestion == '_' || charInQuestion == '-';
    }
}