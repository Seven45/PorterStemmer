namespace PorterStemmer
{
    internal static class HelpTools
    {
        internal static string DeleteLastSymbol(this string word, char symbol)
        {
            if (word.EndsWith(symbol.ToString()))
                return word.DeleteLastSymbol();
            else
                return word;
        }
        internal static string DeleteLastSymbol(this string word)
        {
            return word.Remove(word.Length - 1);
        }

        internal static string WithoutEnding(this string word, string ending)
        {
            return word.Substring(0, word.Length - ending.Length);
        }

        internal static char LastChar(this string word)
        {
            if (word.Length == 0)
                return ' ';
            else
                return word[word.Length - 1];
        }
    }
}
