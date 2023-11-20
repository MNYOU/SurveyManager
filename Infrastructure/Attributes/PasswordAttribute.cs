﻿using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Attributes;

public class PasswordAttribute : StringValidateAttribute
{
    public PasswordAttribute(): this(10, 25)
    {
        
    }
    
    public PasswordAttribute(int minLength, int maxLength) : base("Пароль", minLength, maxLength)
    {
        NeedDigit = false;
        ValidSpecialChars = "@#$%^&*,.?!-_";
    }

    public override bool NeedDigit { get; }
    public override string ValidSpecialChars { get; }

    public override bool IsValid(object? value)
    {
        if (!base.IsValid(value)) return false;
        if (value is not string s) return false;

        if (!s.Any(char.IsUpper))
        {
            ErrorMessage = "Пароль должен содержать хотя бы одну букву в верхнем регистре.";
            return false;
        }

        if (!s.Any(char.IsLower))
        {
            ErrorMessage = "Пароль должен содержать хотя бы одну букву в нижнем регистре.";
            return false;
        }

        return true;
    }
}