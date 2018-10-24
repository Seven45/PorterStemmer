using Porter2StemmerStandard;

namespace PorterStemmer.Stemmers
{
    public sealed class EnglishStemmer : Stemmer
    {
        public EnglishStemmer() : base(new char[] { 'a', 'e', 'i', 'o', 'u', 'y' }) { }

        /// <summary>
        ///     Возвращает основу английского слова.
        /// </summary>
        /// <param name="word">Слово.</param>
        public override string GetStem(string word)
        {
            return new EnglishPorter2Stemmer().Stem(word).Value;
        }
    }
}
