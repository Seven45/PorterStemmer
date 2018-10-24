using System;
using System.Linq;
using PorterStemmer.Language;
using PorterStemmer.Stemmers;

namespace PorterStemmer
{
    public static class Porter
    {
        /// <summary>
        ///     Возвращает основу слова.
        /// </summary>
        /// <param name="word">Слово.</param>
        /// <exception cref="Exception"/>
        public static string GetStem(this string word)
        {
            word = word.ToLower();

            Languages language = word.GetLanguage();
            Stemmer stemmer;

            switch (language)
            {
                case Languages.English:
                    stemmer = new EnglishStemmer();
                    break;
                case Languages.Russian:
                    stemmer = new RussianStemmer();
                    break;
                default:
                    throw new Exception("Язык слова неизвестен.");
            }

            return stemmer.GetStem(word);
        }
    }
}
