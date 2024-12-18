namespace Shared.Extensions;

public static class DateTimeExtensions
{
    public static string ToLongDate(this DateTime dateTime)
    {
        return dateTime.ToString("dd.MM.yyyy HH:mm:ss");
    }

    public static string ToHumanReadableDate(this DateTime dateTime)
    {
        return dateTime.ToString("dd MMMM yyyy");
    }
}