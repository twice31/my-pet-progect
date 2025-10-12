namespace Domain.ExchangeRequest.VO
{
    public record ExchangeMethod
    {
        public string Method { get; }

        public ExchangeMethod(string method)
        {
            if (string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentException("Метод обмена не может быть пустым.", nameof(method));
            }

            Method = method;
        }

        public override string ToString() => Method;
    }
}
