using System;
using System.Text.RegularExpressions;

namespace Domain.User.VO
{
    public sealed record UserPhone
    {
        // Regex
        private static readonly Regex _phoneValidatorRegex = new Regex(
            @"\+\d\s\(\d{3}\)\s\d{3}\s\d{2}-\d{2}",
            RegexOptions.Compiled
        );

        public string Value { get; }

        private UserPhone(string value) => Value = value;

        // ФАБРИЧНЫЙ МЕТОД: с валидацией
        public static UserPhone Create(string value)
        {
            // Проверка на пустую строку
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Номер телефона не может быть пустым.", nameof(value));
            }

            // проверка формата через регулярное выражение
            // Если номер не соответствует строгому шаблону +D (DDD) DDD DD-DD
            if (!_phoneValidatorRegex.IsMatch(value))
            {
                throw new ArgumentException("Номер телефона имеет некорректный формат (ожидается: +D (DDD) DDD DD-DD).", nameof(value));
            }

            return new UserPhone(value);
        }

        public override string ToString() => Value;
    }
}