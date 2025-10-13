namespace Domain.ExchangeRequest.VO
{
    public record ExchangeMethod
    {
        public string Method { get; }

        private ExchangeMethod(string method)
        {
            Method = method;
        }

        public static ExchangeMethod Create(string method)
        {
            if (string.IsNullOrWhiteSpace(method))
                throw new ArgumentException("Метод обмена не может быть пустым.", nameof(method));

            return new ExchangeMethod(method);
        }

        public override string ToString() => Method;
    }
}
