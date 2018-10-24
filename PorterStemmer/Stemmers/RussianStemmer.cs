namespace PorterStemmer.Stemmers
{
    public sealed class RussianStemmer : Stemmer
    {
        public RussianStemmer() : base(new char[] { 'а', 'е', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я' }) { }

        /// <summary>
        ///     Возвращает основу русского слова.
        /// </summary>
        /// <param name="word">Слово.</param>
        public override string GetStem(string word)
        {
            word = word.Replace('ё', 'е');

            string Rv = GetRV(word);
            string beforeRv = word.WithoutEnding(Rv);

            Rv = Step1(Rv);
            Rv = Step2(Rv);
            Rv = Step3(Rv);
            Rv = Step4(Rv);

            return beforeRv + Rv;
        }

        private string Step1(string Rv)
        {
            bool isDeleted = false;

            Rv = DeletePerfectiveGerund(Rv, out isDeleted);
            if (isDeleted)
                return Rv;
            else
            {
                Rv = DeleteReflexive(Rv);

                Rv = DeleteAdjective(Rv, out isDeleted);
                if (isDeleted)
                    return DeleteParticiple(Rv, out isDeleted);
                else
                {
                    Rv = DeleteVerb(Rv, out isDeleted);
                    if (isDeleted)
                        return Rv;
                    else
                    {
                        Rv = DeleteNoun(Rv, out isDeleted);
                        if (isDeleted)
                            return Rv;
                    }
                }
            }
            return Rv;
        }
        private string Step2(string Rv)
        {
            return Rv.DeleteLastSymbol('и');
        }
        private string Step3(string Rv)
        {
            string R2 = GetR2(Rv);
            if (R2 == "")
                return Rv;
            else
                return Rv.WithoutEnding(R2) + DeleteDerivational(R2);
        }
        private string Step4(string Rv)
        {
            bool isDeleted = false;

            Rv = Undouble(Rv, out isDeleted);
            if (isDeleted)
                return Rv;
            else
            {
                Rv = DeleteSuperlative(Rv, out isDeleted);
                Rv = Undouble(Rv);
                if (isDeleted)
                    return Rv;
                else
                    Rv = Rv.DeleteLastSymbol('ь');
            }

            return Rv;
        }

        private string DeletePerfectiveGerund(string rv, out bool isFound)
        {
            string[] group1 = new string[] { "в", "вши", "вшись" };
            string[] group2 = new string[] { "ив", "ивши", "ившись", "ыв", "ывши", "ывшись" };
            isFound = true;

            foreach (string ending in group1)
                if (rv.EndsWith(ending) && (rv.WithoutEnding(ending).LastChar() == 'а' ||
                                            rv.WithoutEnding(ending).LastChar() == 'я'))
                    return rv.WithoutEnding(ending);

            foreach (string ending in group2)
                if (rv.EndsWith(ending))
                    return rv.WithoutEnding(ending);

            isFound = false;
            return rv;
        }
        private string DeleteAdjective(string rv, out bool isFound)
        {
            string[] group = new string[] { "ее", "ие", "ые", "ое", "ими", "ыми", "ей", "ий", "ый", "ой",
                                            "ем", "им", "ым", "ом", "его", "ого", "ему", "ому", "их",
                                            "ых", "ую", "юю", "ая", "яя", "ою", "ею" };
            isFound = true;

            foreach (string ending in group)
                if (rv.EndsWith(ending))
                    return rv.WithoutEnding(ending);

            isFound = false;
            return rv;
        }
        private string DeleteParticiple(string rv, out bool isFound)
        {
            string[] group1 = new string[] { "ем", "нн", "вш", "ющ", "щ" };
            string[] group2 = new string[] { "ивш", "ывш", "ующ" };
            isFound = true;

            foreach (string ending in group1)
                if (rv.EndsWith(ending) && ((rv.WithoutEnding(ending).LastChar() == 'а') ||
                                            (rv.WithoutEnding(ending).LastChar() == 'я')))
                    return rv.WithoutEnding(ending);

            foreach (string ending in group2)
                if (rv.EndsWith(ending))
                    return rv.WithoutEnding(ending);

            isFound = false;
            return rv;
        }
        private string DeleteReflexive(string rv)
        {
            foreach (string ending in new string[] { "ся", "сь" })
                if (rv.EndsWith(ending))
                    return rv.WithoutEnding(ending);

            return rv;
        }
        private string DeleteVerb(string rv, out bool isFound)
        {
            string[] group1 = new string[] { "ла", "на", "ете", "йте", "ли", "й", "л", "ем", "н",
                                             "ло", "но", "ет", "ют", "ны", "ть", "ешь", "нно" };
            string[] group2 = new string[] { "ила", "ыла", "ена", "ейте", "уйте", "ите", "или", "ыли", "ей", "уй",
                                             "ил", "ыл", "им", "ым", "ен", "ило", "ыло", "ено", "ят", "ует", "уют",
                                             "ит", "ыт", "ены", "ить", "ыть", "ишь", "ую", "ю" };
            isFound = true;

            foreach (string ending in group1)
                if (rv.EndsWith(ending) && (rv.WithoutEnding(ending).LastChar() == 'а' ||
                                            rv.WithoutEnding(ending).LastChar() == 'я'))
                    return rv.WithoutEnding(ending);

            foreach (string ending in group2)
                if (rv.EndsWith(ending))
                    return rv.WithoutEnding(ending);

            isFound = false;
            return rv;
        }
        private string DeleteNoun(string rv, out bool isFound)
        {
            string[] group = new string[] { "а", "ев", "ов", "ие", "ье", "е", "иями", "ями", "ами",
                                            "еи", "ии", "и", "ией", "ей", "ой", "ий", "й", "иям",
                                            "ям", "ием", "ем", "ам", "ом", "о", "у", "ах", "иях",
                                            "ях", "ы", "ь", "ию", "ью", "ю", "ия", "ья", "я" };
            isFound = true;

            foreach (string ending in group)
                if (rv.EndsWith(ending))
                    return rv.WithoutEnding(ending);

            isFound = false;
            return rv;
        }
        private string DeleteSuperlative(string rv, out bool isFound)
        {
            isFound = true;

            foreach (string ending in new string[] { "ейш", "ейше" })
                if (rv.EndsWith(ending))
                    return rv.WithoutEnding(ending);

            isFound = false;
            return rv;
        }
        private string DeleteDerivational(string rv)
        {
            foreach (string ending in new string[] { "ост", "ость" })
                if (rv.EndsWith(ending))
                    return rv.WithoutEnding(ending);

            return rv;
        }
        private string Undouble(string rv, out bool isFound)
        {
            string oldWord = rv;
            string newWord = Undouble(rv);

            if (oldWord == newWord)
                isFound = false;
            else
                isFound = true;

            return newWord;
        }
        private string Undouble(string rv)
        {
            if (rv.EndsWith("нн"))
                rv = rv.Remove(rv.Length - 1);

            return rv;
        }
    }
}
