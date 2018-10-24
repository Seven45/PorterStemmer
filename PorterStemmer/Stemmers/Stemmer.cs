using System.Linq;

namespace PorterStemmer.Stemmers
{
    public abstract class Stemmer
    {
        protected readonly char[] Vowels;
        public abstract string GetStem(string word);

        protected Stemmer(char[] vovels)
        {
            Vowels = vovels;
        }

        /// <summary>
        ///     Возвращает "RV-часть" слова (область слова после первой гласной; может быть пустой, если гласные в слове отсутствуют).
        /// </summary>
        /// <param name="word">Слово.</param>
        /// <param name="vovels">Массив гласных букв алфавита.</param>
        protected string GetRV(string word)
        {
            for (int i = 0; i < word.Length; i++)
                if (Vowels.Contains(word[i]))
                    return word.Substring(i + 1);

            return "";
        }

        /// <summary>
        ///     Возвращает "R1-часть" слова (область слова после первого сочетания "гласная-согласная").
        /// </summary>
        /// <param name="word">Слово.</param>
        /// <param name="vovels">Массив гласных букв алфавита.</param>
        protected string GetR1(string word)
        {
            for (int i = 0; i < word.Length - 1; i++)
                if (Vowels.Contains(word[i]) && !Vowels.Contains(word[i + 1]))
                    word = word.Substring(i + 2);

            return word;
        }

        /// <summary>
        ///     Возвращает "R2-часть" слова (область R1 после первого сочетания "гласная-согласная").
        /// </summary>
        /// <param name="word">Слово.</param>
        /// <param name="vovels">Массив гласных букв алфавита.</param>
        protected string GetR2(string word)
        {
            return GetR1(GetR1(word));
        }

        /// <summary>
        ///     Возвращает True, если буква является гласной.
        /// </summary>
        /// <param name="c">Буква.</param>
        protected bool IsVowel(char c)
        {
            if (Vowels.Contains(c))
                return true;
            else
                return false;
        }

        /// <summary>
        ///     Возвращает True, если буква является согласной.
        /// </summary>
        /// <param name="c">Буква.</param>
        protected bool IsNonVowel(char c)
        {
            return !IsVowel(c);
        }
    }
}
