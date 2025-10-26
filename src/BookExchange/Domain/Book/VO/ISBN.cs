using System;
using System.Text.RegularExpressions;

namespace Domain.Book.VO
{
    // Объект-значение для Международного стандартного книжного номера
    public sealed record ISBN
    {
        // Добавляем константу максимальной длины (13 символов)
        public const int MAX_LENGTH = 13;

        // Regex для проверки 10 или 13 цифр.
        private static readonly Regex _isbnValidatorRegex = new Regex(
            // Шаблон: должно быть либо 13 либо 10 цифр
            @$"^(\d{{{MAX_LENGTH}}}|\d{{{10}}})$", // Используем константу
            RegexOptions.Compiled
        );

        public string Value { get; }

        private ISBN(string value) => Value = value;

        // ФАБРИЧНЫЙ МЕТОД: с валидацией
        public static ISBN Create(string value)
        {
            // 1. Проверка на пустую строку
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("ISBN книги не может быть пустым.", nameof(value));
            }

            // Очищаем строку от пробелов и дефисов, чтобы проверить только цифры
            var cleanValue = value.Replace("-", "").Replace(" ", "");

            // 2. Проверка формата через регулярное выражение
            if (!_isbnValidatorRegex.IsMatch(cleanValue))
            {
                throw new ArgumentException("ISBN имеет некорректный формат. Ожидается 10 или 13 цифр.", nameof(value));
            }

            // 3. Проверка длины (хотя regex уже это делает, это полезно для ясности)
            if (cleanValue.Length != MAX_LENGTH && cleanValue.Length != 10)
            {
                throw new ArgumentException($"ISBN должен содержать {MAX_LENGTH} или 10 цифр.", nameof(value));
            }

            // сохраняем очищенное (только цифры) значение
            return new ISBN(cleanValue);
        }

        public override string ToString() => Value;
    }
}