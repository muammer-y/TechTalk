using FluentValidation;
using Shared.Constants;

namespace Application.Extensions;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, TProperty> NotEmptyLocal<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string message = Messages.Validation.NotEmpty)
    {
        return ruleBuilder.NotEmpty()
            .WithMessage(message);
    }

    public static IRuleBuilderOptions<T, string> LengthLocal<T>(this IRuleBuilder<T, string> ruleBuilder,
        int minLength,
        int maxLength,
        Func<int, int, string>? message = null)
    {
        return ruleBuilder
            .Length(minLength, maxLength)
            .WithMessage(message?.Invoke(minLength, maxLength) ?? Messages.Validation.RangeLength(minLength, maxLength));
    }

    public static IRuleBuilderOptions<T, string> MaximumLengthLocal<T>(this IRuleBuilder<T, string> ruleBuilder,
        int maxLength,
        Func<int, string>? message = null)
    {
        return ruleBuilder
            .MaximumLength(maxLength)
            .WithMessage(message?.Invoke(maxLength) ?? Messages.Validation.MaxLength(maxLength));
    }
}