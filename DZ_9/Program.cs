using System;
interface ICipher
{
    string Encode(string message);
    string Decode(string message);
}
class ACipher : ICipher
{
    public string Encode(string message)
    {
        return Shift(message, 1);
    }
    public string Decode(string message)
    {
        return Shift(message, -1);
    }

    private string Shift(string message, int offset)
    {
        string result = "";
        foreach (char c in message)
        {
            if (c >= 'А' && c <= 'Я')
            {
                char shifted = (char)(c + offset);
                if (shifted > 'Я') shifted = 'А';
                if (shifted < 'А') shifted = 'Я';
                result += shifted;
            }
            else if (c >= 'а' && c <= 'я')
            {
                char shifted = (char)(c + offset);
                if (shifted > 'я') shifted = 'а';
                if (shifted < 'а') shifted = 'я';
                result += shifted;
            }
            else
            {
                result += c;
            }
        }
        return result;
    }
}

class BCipher : ICipher
{
    private const int LettersCount = 32;

    public string Encode(string message)
    {
        return Replace(message);
    }

    public string Decode(string message)
    {
        return Replace(message);
    }
    
    private string Replace(string message)
    {
        string result = "";
        foreach (char c in message)
        {
            if (c >= 'А' && c <= 'Я')
            {
                int index = c - 'А';
                result += (char)('А' + (LettersCount - 1 - index));
            }
            else if (c >= 'а' && c <= 'я')
            {
                int index = c - 'а';
                result += (char)('а' + (LettersCount - 1 - index));
            }
            else
            {
                result += c;
            }
        }
        return result;
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Введите строку: ");
        string text = Console.ReadLine();

        ICipher a = new ACipher();
        ICipher b = new BCipher();

        string encodedA = a.Encode(text);
        string decodedA = a.Decode(encodedA);
        Console.WriteLine($"ACipher: \"{text}\" -> \"{encodedA}\" -> \"{decodedA}\"");

        string encodedB = b.Encode(text);
        string decodedB = b.Decode(encodedB);
        Console.WriteLine($"BCipher: \"{text}\" -> \"{encodedB}\" -> \"{decodedB}\"");
    }
}
