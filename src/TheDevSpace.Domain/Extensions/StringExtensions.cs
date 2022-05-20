using System.Net.Mail;

namespace System;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string value)
    {
        return String.IsNullOrEmpty(value);
    }

    public static bool IsValidEmailAddress(this string value)
    {
        try
        {
            MailAddress m = new MailAddress(value);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
