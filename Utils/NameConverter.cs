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
            ToPascalCaseIteration(ref result, nameChar, previousChar);

            previousChar = nameChar;
        }

        return result;
    }

    private static void ToPascalCaseIteration(
        ref string result,
        char nameChar,
        char? previousCharNullable)
    {
        if (IsSeparatorChar(nameChar))
        {
            return;
        }

        if (previousCharNullable == null)
        {
            result += char.ToUpper(nameChar);

            return;
        }

        var previousChar = previousCharNullable.Value;
        var previousWasSeparator = IsSeparatorChar(previousChar);

        result += previousWasSeparator ?
            char.ToUpper(nameChar) :
            char.ToLower(nameChar);
    }

    static bool IsSeparatorChar(char charInQuestion)
    {
        return charInQuestion == '_' || charInQuestion == '-';
    }
}