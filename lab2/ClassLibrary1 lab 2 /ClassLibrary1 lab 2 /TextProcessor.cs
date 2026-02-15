using System;

/// <summary>
/// Бібліотека обробки тексту DLL
/// Функції: підрахунок пробілів, перетворення латиниці у верхній регістр.
/// </summary>
public class TextProcessor
{
    /// <summary>
    /// Визначає кількість пробілів у реченні.
    /// </summary>
    public int CountSpaces(string sentence)
    {
        if (string.IsNullOrEmpty(sentence))
        {
            return 0;
        }

        int count = 0;
        foreach (char c in sentence)
        {
            if (c == ' ')
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// Перетворює всі латинські символи рядка до верхнього регістра (a => A d => D).
    /// </summary>
    public string ConvertToUpper(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        return text.ToUpper();
    
}
}
