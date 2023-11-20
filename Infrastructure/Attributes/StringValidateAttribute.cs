using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Attributes;

public abstract class StringValidateAttribute : ValidationAttribute
{
    protected StringValidateAttribute(string propertyName, int minLength, int maxLength)
    {
        if (minLength <= 0 || maxLength < minLength || maxLength > 40)
            throw new ArgumentException($"Неверные входные параметры MaxLength: {maxLength}, MinLength: {minLength} символов.");
        PropertyName = propertyName;
        MaxLength = maxLength;
        MinLength = minLength;
    }

    public int MaxLength { get; }

    public int MinLength { get; }

    public string PropertyName { get; }

    public abstract bool NeedDigit { get; }

    public abstract string ValidSpecialChars { get; }

    public override bool IsValid(object? value)
    {
        if (value is not string s) return false;

        if (s.Length > MaxLength || s.Length < MinLength)
        {
            ErrorMessage =
                $"Некорректная длина строки. {PropertyName} должен быть не меньше {MinLength} и не больше {MaxLength} символов.";
            return false;
        }

        if (s.Any(c => c == ' '))
        {
            ErrorMessage = $"{PropertyName} не должен содержать пробелы.";
            return false;
        }

        if (NeedDigit && !s.Any(char.IsDigit))
        {
            ErrorMessage = $"{PropertyName} должен содержать хотя бы одну цифру.";
            return false;
        }

        if (s.Where(c => !char.IsLetterOrDigit(c)).Any(c => !ValidSpecialChars.Contains(c)))
        {
            ErrorMessage = $"{PropertyName} содержит недопустимые символы.";
            return false;
        }

        return true;
    }
}