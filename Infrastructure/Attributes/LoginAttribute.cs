using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Infrastructure.Attributes;

public class LoginAttribute : StringValidateAttribute
{
    public LoginAttribute(): this(8, 25)
    {
        
    }
    
    public LoginAttribute(int minLength, int maxLength) : base("Логин", minLength, maxLength)
    {
        NeedDigit = false;
        ValidSpecialChars = "_-.";
    }

    public override bool NeedDigit { get; }
    public override string ValidSpecialChars { get; }

    public override bool IsValid(object? value)
    {
        Console.WriteLine(NeedDigit);
        if (!base.IsValid(value)) return false;
        if (value is not string s) return false;

        if (s.First() is '-' or '_' || s.Last() is '-' or '_')
        {
            ErrorMessage = $"{PropertyName} не должен начинаться и заканчиваться символами '_' и '-'.";
            return false;
        }

        return true;
    }
}