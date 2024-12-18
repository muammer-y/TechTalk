using System.ComponentModel;
using System.Reflection;

namespace Shared.Extensions;

public static class EnumExtensions
{
    private static readonly Dictionary<Enum, string> _cache = [];

    public static string GetDescription(this Enum value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (_cache.TryGetValue(value, out var description))
            return description;

        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();

        description = attribute?.Description ?? value.ToString();

        _cache.TryAdd(value, description);

        return description;
    }
}
