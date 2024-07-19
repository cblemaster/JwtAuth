using FluentValidation;

namespace JwtAuth.Core.Validation;

public static class ValidationRuleExtensions
{
    public static IRuleBuilderOptions<T, string> StringNotEmpty
        <T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder.Must(s => !string.IsNullOrWhiteSpace(s))
            .WithMessage("{PropertyName} is required.");

    public static IRuleBuilderOptions<T, string> StringLengthDoesNotExceedMax
        <T>(this IRuleBuilder<T, string> ruleBuilder, int max) =>
            ruleBuilder.Must((rootObject, s, context) =>
            {
                context.MessageFormatter.AppendArgument("MaxLength", max);
                return s.Length <= max;
            })
            .WithMessage("{PropertyName} must be {MaxLength} characters or fewer.");
}
