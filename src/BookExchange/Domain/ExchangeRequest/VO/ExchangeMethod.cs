using System;

namespace Domain.ExchangeRequest.VO
{
    public record ExchangeMethod
    {
        // Добавлена константа макс длины 
        public const int MAX_LENGTH = 150;

        public string Method { get; }

        private ExchangeMethod(string method)
        {
            Method = method;
        }

        public static ExchangeMethod Create(string method)
        {
            if (string.IsNullOrWhiteSpace(method))
                throw new ArgumentException("Метод обмена не может быть пустым.", nameof(method));

            // Добавлена проверка
            if (method.Length > MAX_LENGTH)
            {
                throw new ArgumentException($"Метод обмена не может превышать {MAX_LENGTH} символов.", nameof(method));
            }

            return new ExchangeMethod(method);
        }

        public override string ToString() => Method;
    }
}