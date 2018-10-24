# PorterStemmer
Описание.
---
Стеммер Мартина Портера на русском и английском языке. Язык определяется автоматически. Англоязычная версия выполнена на основе https://github.com/nemec/porter2-stemmer.

Установка.
---
Install-Package PorterStemmer 

Использование.
---
```C#
"валится".GetStem(); // вал
"conspirators".GetStem() // conspir
```
