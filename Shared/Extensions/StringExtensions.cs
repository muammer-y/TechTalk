namespace Shared.Extensions;

public static class StringExtensions
{
    public static string ToUpperFirst(this string str) //can this be optimized?
    {
        if (string.IsNullOrEmpty(str))
            return str;

        return char.ToUpper(str[0]) + str.Substring(1);
    }

    public static string ToTitleCase(this string str) //is this method necessary to write from scratch.
    {
        if (string.IsNullOrWhiteSpace(str))
            return str;

        var words = str.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length > 0)
                words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
        }

        return string.Join(" ", words);
    }
}
