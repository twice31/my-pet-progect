using System;
using System.Text.RegularExpressions;

namespace Domain.User.VO
{
    public sealed record UserEmail
    {
        public const int MAX_EMAIL_LENGTH = 100;
        public const int MIN_EMAIL_LENGTH = 10;

        // Regex
        private static readonly Regex _emailValidatorRegex = new Regex(
            @"\b([^\d]\w+)[@]([^\d]\w+)\.(com|ru)\b",
            RegexOptions.Compiled | RegexOptions.IgnoreCase
        );

        public string Value { get; }

        private UserEmail(string value) => Value = value;

        // ФАБРИЧНЫЙ МЕТОД: с валидацией
        public static UserEmail Create(string value)
        {
            // Проверка на пустую строку
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Электронная почта не может быть пустой.", nameof(value));
            }

            // Проверка длины
            if (value.Length < MIN_EMAIL_LENGTH || value.Length > MAX_EMAIL_LENGTH)
            {
                throw new ArgumentException($"Длина почты должна быть от {MIN_EMAIL_LENGTH} до {MAX_EMAIL_LENGTH} символов.", nameof(value));
            }

            // Проверка формата через регулярное выражение
            if (!_emailValidatorRegex.IsMatch(value))
            {
                throw new ArgumentException("Электронная почта имеет некорректный формат (ожидается: user@domain.com/ru).", nameof(value));
            }

            return new UserEmail(value);
        }

        public override string ToString() => Value;
    }
}