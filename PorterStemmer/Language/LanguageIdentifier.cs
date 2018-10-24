using System;

namespace PorterStemmer.Language
{
    internal static class LanguageIdentifier
    {
        /// <summary>
        ///     Возвращает True, если слово состоит из английских букв.
        /// </summary>
        /// <param name="word">Слово.</param>
        private static bool IsEnglishLanguage(this string word)
        {
            foreach (char c in word)
            {
                if ((c >= 'a') && (c <= 'z'))
                    continue;
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        ///     Возвращает True, если слово состоит из русских букв.
        /// </summary>
        /// <param name="word">Слово.</param>
        private static bool IsRussianLanguage(this string word)
        {
            foreach (char c in word)
            {
                if ((c >= 'а') && (c <= 'я'))
                    continue;
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        ///     Определяет язык слова.
        /// </summary>
        /// <param name="word">Слово.</param>
        /// <exception cref="Exception"/>
        internal static Languages GetLanguage(this string word)
        {
            word = word.ToLower();

            if (word.IsEnglishLanguage())
                return Languages.English;
            if (word.IsRussianLanguage())
                return Languages.Russian;
            throw new Exception("Язык слова определить не удалось.");
        }
    }
}
