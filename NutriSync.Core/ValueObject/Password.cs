using System.Runtime.CompilerServices;

namespace NutriSync.Core.ValueObject;

public class Password
{
    public string Text { get; set; } = string.Empty;

    private const string Valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    private const string Special = "!@#$%ˆ&*(){}[];";

    protected Password()
    {

    }

    public Password(string? text = null)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            text = Generate();
        if (ValidatePassword(text))
            Text = text;
    }

    private static string Generate(short length = 16, bool includeSpecialChars = true, bool upperCase = false)
    {
        var chars = includeSpecialChars ? Valid + Special : Valid;
        var startRandom = upperCase ? 26 : 0;
        var index = 0;
        var res = new char[length];
        var rnd = new Random();

        while (index < length)
            res[index++] = chars[rnd.Next(startRandom, chars.Length)];

        return new string(res);
    }

    private static bool ValidatePassword(string text)
    {
        if (text.Length < 8)
            return false;
        if (!text.Any(char.IsUpper))
            return false;
        if (!text.Any(c => !char.IsLetterOrDigit(c)))
            return false;
        return true;
    }

    public static implicit operator string(Password password)
        => password.Text;
}
